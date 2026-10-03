using EphorosVault.Business.Modules.Vault;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EphorosVault.Tests;

[TestClass]
public class VaultServiceTests
{
    [TestMethod]
    public void SaveAssignsIdForNewEntry()
    {
        MemoryRepository repository = new();
        VaultService service = new(repository);
        VaultEntry entry = new()
        {
            Name = "Example"
        };

        service.Save(entry);

        Assert.AreNotEqual(Guid.Empty, entry.Id);
        Assert.AreEqual(1, repository.Items.Count);
    }

    [TestMethod]
    public void SaveRequiresName()
    {
        VaultService service = new(new MemoryRepository());
        Assert.ThrowsException<ArgumentException>(() => service.Save(new VaultEntry()));
    }

    [TestMethod]
    public void PasswordGeneratorIncludesEveryRequiredCharacterType()
    {
        PasswordGenerator generator = new();
        string password = generator.Generate(20, true, true, true, true);

        Assert.AreEqual(20, password.Length);
        Assert.IsTrue(ContainsAny(password, "ABCDEFGHJKLMNPQRSTUVWXYZ"));
        Assert.IsTrue(ContainsAny(password, "abcdefghijkmnopqrstuvwxyz"));
        Assert.IsTrue(ContainsAny(password, "23456789"));
        Assert.IsTrue(ContainsAny(password, "!@#$%^&*()-_=+"));
    }

    private static bool ContainsAny(string value, string characters)
    {
        foreach (char character in value)
        {
            if (characters.IndexOf(character) >= 0) return true;
        }

        return false;
    }

    private sealed class MemoryRepository : IVaultRepository
    {
        internal List<VaultEntry> Items { get; } = new();
        public IList<VaultEntry> GetAll() => Items;
        public VaultEntry Get(Guid id) => Items.Find(x => x.Id == id);
        public void Save(VaultEntry entry)
        {
            Items.RemoveAll(x => x.Id == entry.Id);
            Items.Add(entry);
        }
        public void Delete(Guid id) => Items.RemoveAll(x => x.Id == id);
    }
}
