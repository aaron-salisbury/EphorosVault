using EphorosVault.Business.Modules.Vault;
using EphorosVault.Presentation.Desktop.Vault;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EphorosVault.Tests;

[TestClass]
public class VaultPresenterTests
{
    [TestMethod]
    public void RefreshLoadsFoldersAndEntries()
    {
        MemoryView view = new();
        MemoryVaultRepository entries = new();
        MemoryFolderRepository folders = new();
        entries.Items.Add(new VaultEntry { Id = Guid.NewGuid(), Name = "Example" });
        folders.Items.Add(new VaultFolder { Id = Guid.NewGuid(), Name = "Work" });
        VaultPresenter presenter = new(view, new VaultService(entries), new VaultFolderService(folders), new PasswordGenerator());

        presenter.Refresh();

        Assert.AreEqual(1, view.Entries.Count);
        Assert.AreEqual(1, view.Folders.Count);
    }

    [TestMethod]
    public void FilterChangedFiltersEntriesWithoutWinFormsControls()
    {
        MemoryView view = new() { SearchText = "work" };
        MemoryVaultRepository entries = new();
        entries.Items.Add(new VaultEntry { Id = Guid.NewGuid(), Name = "Work Portal", UserName = string.Empty, Url = string.Empty });
        entries.Items.Add(new VaultEntry { Id = Guid.NewGuid(), Name = "Personal", UserName = string.Empty, Url = string.Empty });
        VaultPresenter presenter = new(view, new VaultService(entries), new VaultFolderService(new MemoryFolderRepository()), new PasswordGenerator());

        presenter.Refresh();

        Assert.AreEqual(1, view.Entries.Count);
        Assert.AreEqual("Work Portal", view.Entries[0].Name);
    }

    private sealed class MemoryView : IVaultView
    {
        public event EventHandler CopyPasswordRequested;
        public event EventHandler CopyUserNameRequested;
        public event EventHandler DeleteEntryRequested;
        public event EventHandler EditEntryRequested;
        public event EventHandler FilterChanged;
        public event EventHandler NewEntryRequested;
        public Guid? SelectedFolderId { get; set; }
        public VaultEntry SelectedEntry { get; set; }
        public string SearchText { get; set; } = string.Empty;
        internal List<VaultEntry> Entries { get; } = new();
        internal List<VaultFolder> Folders { get; } = new();
        public void ClearSensitiveState() => Entries.Clear();
        public void SetEntries(IEnumerable<VaultEntry> entries) { Entries.Clear(); Entries.AddRange(entries); }
        public void SetFolders(IEnumerable<VaultFolder> folders) { Folders.Clear(); Folders.AddRange(folders); }
        public void ShowEntry(VaultEntry entry) { }
        public void ShowStatus(string message) { }
    }

    private sealed class MemoryVaultRepository : IVaultRepository
    {
        internal List<VaultEntry> Items { get; } = new();
        public IList<VaultEntry> GetAll() => Items;
        public VaultEntry Get(Guid id) => Items.Find(x => x.Id == id);
        public void Save(VaultEntry entry) { }
        public void Delete(Guid id) { }
    }

    private sealed class MemoryFolderRepository : IVaultFolderRepository
    {
        internal List<VaultFolder> Items { get; } = new();
        public IList<VaultFolder> GetAll() => Items;
        public void Save(VaultFolder folder) { }
        public void Delete(Guid id) { }
    }
}
