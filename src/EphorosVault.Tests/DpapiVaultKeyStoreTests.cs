using EphorosVault.Integrations.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace EphorosVault.Tests;

[TestClass]
public class DpapiVaultKeyStoreTests
{
    [TestMethod]
    public void EnsureCreatedCreatesStableThirtyTwoByteKey()
    {
        string directory = CreateTempDirectory();
        try
        {
            DpapiVaultKeyStore store = new(Path.Combine(directory, "vault.key"));
            store.EnsureCreated();
            byte[] first = store.Load();
            store.EnsureCreated();
            byte[] second = store.Load();

            Assert.AreEqual(32, first.Length);
            CollectionAssert.AreEqual(first, second);
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [TestMethod]
    public void SaveRejectsInvalidKeyLength()
    {
        string directory = CreateTempDirectory();
        try
        {
            DpapiVaultKeyStore store = new(Path.Combine(directory, "vault.key"));
            Assert.ThrowsException<ArgumentException>(() => store.Save(new byte[16]));
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    private static string CreateTempDirectory()
    {
        string path = Path.Combine(Path.GetTempPath(), "EphorosVaultTests-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(path);
        return path;
    }
}
