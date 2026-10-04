using EphorosVault.Business.Modules.Vault;
using EphorosVault.Integrations.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace EphorosVault.Tests;

[TestClass]
public class VaultRecoveryServiceTests
{
    [TestMethod]
    public void ExportAndImportRoundTrip()
    {
        string directory = CreateTempDirectory();
        try
        {
            string recoveryPath = Path.Combine(directory, "vault.evkey");
            MemoryKeyStore sourceStore = new(CreateKey(1));
            MemoryMetadataRepository metadata = new();
            VaultRecoveryService source = new(sourceStore, metadata);
            source.EnsureInitialized();
            source.Export(recoveryPath);

            MemoryKeyStore restoredStore = new(CreateKey(2));
            VaultRecoveryService restored = new(restoredStore, metadata);
            restored.Import(recoveryPath);

            CollectionAssert.AreEqual(sourceStore.Key, restoredStore.Key);
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [TestMethod]
    public void ImportRejectsRecoveryKeyFromDifferentVaultWithoutReplacingKey()
    {
        string directory = CreateTempDirectory();
        try
        {
            string recoveryPath = Path.Combine(directory, "other.evkey");
            VaultRecoveryService other = new(new MemoryKeyStore(CreateKey(1)), new MemoryMetadataRepository());
            other.EnsureInitialized();
            other.Export(recoveryPath);

            MemoryKeyStore targetStore = new(CreateKey(2));
            byte[] original = (byte[])targetStore.Key.Clone();
            VaultRecoveryService target = new(targetStore, new MemoryMetadataRepository());
            target.EnsureInitialized();

            Assert.ThrowsException<InvalidDataException>(() => target.Import(recoveryPath));
            CollectionAssert.AreEqual(original, targetStore.Key);
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [TestMethod]
    public void ImportRejectsCorruptFileWithoutReplacingKey()
    {
        string directory = CreateTempDirectory();
        try
        {
            string recoveryPath = Path.Combine(directory, "vault.evkey");
            MemoryMetadataRepository metadata = new();
            VaultRecoveryService service = new(new MemoryKeyStore(CreateKey(1)), metadata);
            service.EnsureInitialized();
            service.Export(recoveryPath);

            byte[] file = File.ReadAllBytes(recoveryPath);
            file[file.Length - 1] ^= 0x01;
            File.WriteAllBytes(recoveryPath, file);

            MemoryKeyStore targetStore = new(CreateKey(1));
            byte[] original = (byte[])targetStore.Key.Clone();
            VaultRecoveryService target = new(targetStore, metadata);

            Assert.ThrowsException<InvalidDataException>(() => target.Import(recoveryPath));
            CollectionAssert.AreEqual(original, targetStore.Key);
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [TestMethod]
    public void LegacyRawKeyImportsOnlyWhenItMatchesVaultVerification()
    {
        string directory = CreateTempDirectory();
        try
        {
            byte[] correctKey = CreateKey(1);
            MemoryMetadataRepository metadata = new();
            VaultRecoveryService service = new(new MemoryKeyStore(correctKey), metadata);
            service.EnsureInitialized();

            string validPath = Path.Combine(directory, "valid.evkey");
            File.WriteAllBytes(validPath, correctKey);
            MemoryKeyStore targetStore = new(CreateKey(2));
            new VaultRecoveryService(targetStore, metadata).Import(validPath);
            CollectionAssert.AreEqual(correctKey, targetStore.Key);

            string invalidPath = Path.Combine(directory, "invalid.evkey");
            File.WriteAllBytes(invalidPath, CreateKey(3));
            byte[] before = (byte[])targetStore.Key.Clone();
            Assert.ThrowsException<InvalidDataException>(() => new VaultRecoveryService(targetStore, metadata).Import(invalidPath));
            CollectionAssert.AreEqual(before, targetStore.Key);
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    private static byte[] CreateKey(byte value)
    {
        byte[] key = new byte[32];
        for (int i = 0; i < key.Length; i++) key[i] = value;
        return key;
    }

    private static string CreateTempDirectory()
    {
        string path = Path.Combine(Path.GetTempPath(), "EphorosVaultTests-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(path);
        return path;
    }

    private sealed class MemoryKeyStore : IVaultKeyStore
    {
        internal MemoryKeyStore(byte[] key) { Key = (byte[])key.Clone(); }
        internal byte[] Key { get; private set; }
        public bool Exists => true;
        public void EnsureCreated() { }
        public byte[] Load() => (byte[])Key.Clone();
        public void Save(byte[] key) => Key = (byte[])key.Clone();
    }

    private sealed class MemoryMetadataRepository : IVaultRecoveryMetadataRepository
    {
        private VaultRecoveryMetadata _metadata;
        public VaultRecoveryMetadata Get() => _metadata;
        public void Save(VaultRecoveryMetadata metadata) => _metadata = metadata;
    }
}
