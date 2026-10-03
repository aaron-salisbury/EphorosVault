using EphorosVault.Business.Modules.Vault;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EphorosVault.Integrations.Cryptography;

public sealed class VaultKeyEncryption : IVaultEncryption
{
    private readonly IVaultKeyStore _keyStore;

    public VaultKeyEncryption(IVaultKeyStore keyStore)
    {
        _keyStore = keyStore ?? throw new ArgumentNullException(nameof(keyStore));
    }

    public string Encrypt(string plaintext)
    {
        if (plaintext == null) throw new ArgumentNullException(nameof(plaintext));
        byte[] key = _keyStore.Load();
        try
        {
            using RijndaelManaged algorithm = CreateAlgorithm(key);
            algorithm.GenerateIV();
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            using MemoryStream stream = new();
            stream.Write(algorithm.IV, 0, algorithm.IV.Length);
            using (CryptoStream crypto = new(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
            {
                crypto.Write(plaintextBytes, 0, plaintextBytes.Length);
                crypto.FlushFinalBlock();
            }
            Array.Clear(plaintextBytes, 0, plaintextBytes.Length);
            return Convert.ToBase64String(stream.ToArray());
        }
        finally { Array.Clear(key, 0, key.Length); }
    }

    public string Decrypt(string ciphertext)
    {
        if (ciphertext == null) throw new ArgumentNullException(nameof(ciphertext));
        byte[] payload = Convert.FromBase64String(ciphertext);
        byte[] key = _keyStore.Load();
        try
        {
            using RijndaelManaged algorithm = CreateAlgorithm(key);
            int ivLength = algorithm.BlockSize / 8;
            if (payload.Length < ivLength) throw new CryptographicException("The encrypted vault value is invalid.");
            byte[] iv = new byte[ivLength];
            Buffer.BlockCopy(payload, 0, iv, 0, ivLength);
            algorithm.IV = iv;
            using MemoryStream input = new(payload, ivLength, payload.Length - ivLength);
            using CryptoStream crypto = new(input, algorithm.CreateDecryptor(), CryptoStreamMode.Read);
            using MemoryStream output = new();
            byte[] buffer = new byte[256];
            int read;
            while ((read = crypto.Read(buffer, 0, buffer.Length)) > 0) output.Write(buffer, 0, read);
            return Encoding.UTF8.GetString(output.ToArray());
        }
        finally
        {
            Array.Clear(key, 0, key.Length);
            Array.Clear(payload, 0, payload.Length);
        }
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