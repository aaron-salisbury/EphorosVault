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
    public void SaveTrimsName()
    {
        MemoryRepository repository = new();
        VaultService service = new(repository);
        VaultEntry entry = new() { Name = "  Example  " };

        service.Save(entry);

        Assert.AreEqual("Example", entry.Name);
    }

    [TestMethod]
    public void SaveRejectsNullEntry()
    {
        VaultService service = new(new MemoryRepository());
        Assert.ThrowsException<ArgumentNullException>(() => service.Save(null));
    }

    [TestMethod]
    public void SaveRequiresName()
    {
        VaultService service = new(new MemoryRepository());
        Assert.ThrowsException<ArgumentException>(() => service.Save(new VaultEntry()));
    }

    private sealed class MemoryRepository : IVaultRepository
    {
        internal List<VaultEntry> Items { get; } = [];
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
