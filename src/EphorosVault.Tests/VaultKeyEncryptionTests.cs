using EphorosVault.Integrations.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EphorosVault.Tests;

[TestClass]
public class VaultKeyEncryptionTests
{
    [TestMethod]
    public void EncryptDecryptRoundTrips()
    {
        VaultKeyEncryption encryption = CreateEncryption();
        string encrypted = encryption.Encrypt("pässword value");

        Assert.AreEqual("pässword value", encryption.Decrypt(encrypted));
        StringAssert.StartsWith(encrypted, "EV1:");
    }

    [TestMethod]
    public void EncryptUsesRandomInitializationVector()
    {
        VaultKeyEncryption encryption = CreateEncryption();

        Assert.AreNotEqual(encryption.Encrypt("same"), encryption.Encrypt("same"));
    }

    [TestMethod]
    public void DecryptReadsLegacyCiphertext()
    {
        byte[] key = CreateKey();
        VaultKeyEncryption encryption = new(new MemoryKeyStore(key));

        Assert.AreEqual("legacy secret", encryption.Decrypt(EncryptLegacy("legacy secret", key)));
    }

    [TestMethod]
    public void DecryptRejectsTamperedCiphertext()
    {
        VaultKeyEncryption encryption = CreateEncryption();
        string encrypted = encryption.Encrypt("secret");
        char replacement = encrypted[8] == 'A' ? 'B' : 'A';
        string tampered = encrypted.Substring(0, 8) + replacement + encrypted.Substring(9);

        Assert.ThrowsException<CryptographicException>(() => encryption.Decrypt(tampered));
    }

    private static VaultKeyEncryption CreateEncryption()
    {
        return new VaultKeyEncryption(new MemoryKeyStore(CreateKey()));
    }

    private static byte[] CreateKey()
    {
        byte[] key = new byte[32];
        for (int i = 0; i < key.Length; i++)
        {
            key[i] = (byte)(i + 1);
        }

        return key;
    }

    private static string EncryptLegacy(string plaintext, byte[] key)
    {
        using RijndaelManaged algorithm = new()
        {
            BlockSize = 128,
            KeySize = 256,
            Key = key,
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7
        };
        algorithm.GenerateIV();
        byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
        using MemoryStream stream = new();
        stream.Write(algorithm.IV, 0, algorithm.IV.Length);
        using (CryptoStream crypto = new(stream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
        {
            crypto.Write(bytes, 0, bytes.Length);
            crypto.FlushFinalBlock();
        }

        return Convert.ToBase64String(stream.ToArray());
    }

    private sealed class MemoryKeyStore : IVaultKeyStore
    {
        private byte[] _key;
        internal MemoryKeyStore(byte[] key) { _key = (byte[])key.Clone(); }
        public bool Exists => true;
        public void EnsureCreated() { }
        public byte[] Load() => (byte[])_key.Clone();
        public void Save(byte[] key) => _key = (byte[])key.Clone();
        public void ExportRecoveryKey(string filePath) => throw new NotSupportedException();
        public void ImportRecoveryKey(string filePath) => throw new NotSupportedException();
    }
}
