using EphorosVault.Business.Modules.Vault;
using Microsoft.Practices.Unity.Utility;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EphorosVault.Integrations.Cryptography;

public sealed class VaultKeyEncryption : IVaultEncryption
{
    private const byte CurrentVersion = 1;
    private const string CurrentPrefix = "EV1:";
    private readonly IVaultKeyStore _keyStore;

    public VaultKeyEncryption(IVaultKeyStore keyStore)
    {
        _keyStore = keyStore ?? throw new ArgumentNullException(nameof(keyStore));
    }

    public string Encrypt(string plaintext)
    {
        Guard.ArgumentNotNull(plaintext, nameof(plaintext));

        byte[] masterKey = _keyStore.Load();
        byte[] encryptionKey = DeriveKey(masterKey, "Ephoros Vault encryption");
        byte[] authenticationKey = DeriveKey(masterKey, "Ephoros Vault authentication");
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        try
        {
            using RijndaelManaged algorithm = CreateAlgorithm(encryptionKey);
            algorithm.GenerateIV();
            using MemoryStream encrypted = new();
            using (CryptoStream crypto = new(encrypted, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
            {
                crypto.Write(plaintextBytes, 0, plaintextBytes.Length);
                crypto.FlushFinalBlock();
            }

            byte[] ciphertext = encrypted.ToArray();
            byte[] authenticated = new byte[1 + algorithm.IV.Length + ciphertext.Length];
            authenticated[0] = CurrentVersion;
            Buffer.BlockCopy(algorithm.IV, 0, authenticated, 1, algorithm.IV.Length);
            Buffer.BlockCopy(ciphertext, 0, authenticated, 1 + algorithm.IV.Length, ciphertext.Length);
            byte[] mac;
            using (HMACSHA256 hmac = new(authenticationKey))
            {
                mac = hmac.ComputeHash(authenticated);
            }

            byte[] payload = new byte[authenticated.Length + mac.Length];
            Buffer.BlockCopy(authenticated, 0, payload, 0, authenticated.Length);
            Buffer.BlockCopy(mac, 0, payload, authenticated.Length, mac.Length);
            try
            {
                return CurrentPrefix + Convert.ToBase64String(payload);
            }
            finally
            {
                Array.Clear(ciphertext, 0, ciphertext.Length);
                Array.Clear(authenticated, 0, authenticated.Length);
                Array.Clear(mac, 0, mac.Length);
                Array.Clear(payload, 0, payload.Length);
            }
        }
        finally
        {
            Array.Clear(masterKey, 0, masterKey.Length);
            Array.Clear(encryptionKey, 0, encryptionKey.Length);
            Array.Clear(authenticationKey, 0, authenticationKey.Length);
            Array.Clear(plaintextBytes, 0, plaintextBytes.Length);
        }
    }

    public string Decrypt(string ciphertext)
    {
        if (ciphertext == null)
        {
            throw new ArgumentNullException(nameof(ciphertext));
        }

        return ciphertext.StartsWith(CurrentPrefix, StringComparison.Ordinal)
            ? DecryptCurrent(ciphertext.Substring(CurrentPrefix.Length))
            : DecryptLegacy(ciphertext);
    }

    private string DecryptCurrent(string ciphertext)
    {
        byte[] payload = Convert.FromBase64String(ciphertext);
        byte[] masterKey = _keyStore.Load();
        byte[] encryptionKey = DeriveKey(masterKey, "Ephoros Vault encryption");
        byte[] authenticationKey = DeriveKey(masterKey, "Ephoros Vault authentication");
        try
        {
            const int ivLength = 16;
            const int macLength = 32;
            if (payload.Length <= 1 + ivLength + macLength || payload[0] != CurrentVersion)
            {
                throw new CryptographicException("The encrypted vault value is invalid.");
            }

            int authenticatedLength = payload.Length - macLength;
            byte[] expectedMac;
            using (HMACSHA256 hmac = new(authenticationKey))
            {
                expectedMac = hmac.ComputeHash(payload, 0, authenticatedLength);
            }

            try
            {
                if (!FixedTimeEquals(payload, authenticatedLength, expectedMac))
                {
                    throw new CryptographicException("The encrypted vault value failed authentication.");
                }
            }
            finally
            {
                Array.Clear(expectedMac, 0, expectedMac.Length);
            }

            byte[] iv = new byte[ivLength];
            Buffer.BlockCopy(payload, 1, iv, 0, ivLength);
            int encryptedLength = authenticatedLength - 1 - ivLength;
            using RijndaelManaged algorithm = CreateAlgorithm(encryptionKey);
            algorithm.IV = iv;
            using MemoryStream input = new(payload, 1 + ivLength, encryptedLength);
            using CryptoStream crypto = new(input, algorithm.CreateDecryptor(), CryptoStreamMode.Read);
            return ReadPlaintext(crypto);
        }
        finally
        {
            Array.Clear(payload, 0, payload.Length);
            Array.Clear(masterKey, 0, masterKey.Length);
            Array.Clear(encryptionKey, 0, encryptionKey.Length);
            Array.Clear(authenticationKey, 0, authenticationKey.Length);
        }
    }

    private string DecryptLegacy(string ciphertext)
    {
        byte[] payload = Convert.FromBase64String(ciphertext);
        byte[] key = _keyStore.Load();
        try
        {
            using RijndaelManaged algorithm = CreateAlgorithm(key);
            int ivLength = algorithm.BlockSize / 8;
            if (payload.Length <= ivLength)
            {
                throw new CryptographicException("The encrypted vault value is invalid.");
            }

            byte[] iv = new byte[ivLength];
            Buffer.BlockCopy(payload, 0, iv, 0, ivLength);
            algorithm.IV = iv;
            using MemoryStream input = new(payload, ivLength, payload.Length - ivLength);
            using CryptoStream crypto = new(input, algorithm.CreateDecryptor(), CryptoStreamMode.Read);
            return ReadPlaintext(crypto);
        }
        finally
        {
            Array.Clear(key, 0, key.Length);
            Array.Clear(payload, 0, payload.Length);
        }
    }

    private static string ReadPlaintext(CryptoStream crypto)
    {
        using MemoryStream output = new();
        byte[] buffer = new byte[256];
        try
        {
            int read;
            while ((read = crypto.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }

            byte[] plaintext = output.ToArray();
            try
            { return Encoding.UTF8.GetString(plaintext); }
            finally { Array.Clear(plaintext, 0, plaintext.Length); }
        }
        finally { Array.Clear(buffer, 0, buffer.Length); }
    }

    private static byte[] DeriveKey(byte[] masterKey, string purpose)
    {
        using HMACSHA256 hmac = new(masterKey);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(purpose));
    }

    private static bool FixedTimeEquals(byte[] payload, int offset, byte[] expected)
    {
        if (payload.Length - offset != expected.Length)
        {
            return false;
        }

        int difference = 0;
        for (int i = 0; i < expected.Length; i++)
        {
            difference |= payload[offset + i] ^ expected[i];
        }

        return difference == 0;
    }

    private static RijndaelManaged CreateAlgorithm(byte[] key)
    {
        return new RijndaelManaged
        {
            BlockSize = 128,
            KeySize = 256,
            Key = key,
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7
        };
    }
}
