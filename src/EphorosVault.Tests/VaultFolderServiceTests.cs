using EphorosVault.Business.Modules.Vault;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EphorosVault.Tests;

[TestClass]
public class VaultFolderServiceTests
{
    [TestMethod]
    public void SaveAssignsIdForNewFolder()
    {
        MemoryFolderRepository repository = new();
        VaultFolderService service = new(repository);
        VaultFolder folder = new() { Name = "Shopping" };

        service.Save(folder);

        Assert.AreNotEqual(Guid.Empty, folder.Id);
        Assert.AreEqual(1, repository.Items.Count);
    }

    [TestMethod]
    public void SaveTrimsName()
    {
        MemoryFolderRepository repository = new();
        VaultFolderService service = new(repository);
        VaultFolder folder = new() { Name = "  Shopping  " };

        service.Save(folder);

        Assert.AreEqual("Shopping", folder.Name);
    }

    [TestMethod]
    public void SaveRejectsNullFolder()
    {
        VaultFolderService service = new(new MemoryFolderRepository());
        Assert.ThrowsException<ArgumentNullException>(() => service.Save(null));
    }

    [TestMethod]
    public void SaveRequiresName()
    {
        VaultFolderService service = new(new MemoryFolderRepository());
        Assert.ThrowsException<ArgumentException>(() => service.Save(new VaultFolder()));
    }

    private sealed class MemoryFolderRepository : IVaultFolderRepository
    {
        internal List<VaultFolder> Items { get; } = [];
        public IList<VaultFolder> GetAll() => Items;
        public void Save(VaultFolder folder) { Items.RemoveAll(x => x.Id == folder.Id); Items.Add(folder); }
        public void Delete(Guid id) => Items.RemoveAll(x => x.Id == id);
    }
}
