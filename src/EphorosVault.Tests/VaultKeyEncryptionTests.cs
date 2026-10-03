using EphorosVault.Integrations.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;

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
        byte[] key = new byte[32];
        for (int i = 0; i < key.Length; i++) key[i] = (byte)(i + 1);
        return new VaultKeyEncryption(new MemoryKeyStore(key));
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
