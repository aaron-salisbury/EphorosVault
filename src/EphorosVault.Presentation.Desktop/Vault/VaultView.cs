using EphorosVault.Business.Modules.Vault;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Vault;

public sealed partial class VaultView : UserControl, IVaultView
{

    public VaultView()
    {
        InitializeComponent();
    }

    public event EventHandler CopyPasswordRequested;
    public event EventHandler CopyUserNameRequested;
    public event EventHandler DeleteEntryRequested;
    public event EventHandler EditEntryRequested;
    public event EventHandler DeleteFolderRequested;
    public event EventHandler FilterChanged;
    public event EventHandler NewEntryRequested;
    public event EventHandler NewFolderRequested;
    public event EventHandler RenameFolderRequested;
    public event EventHandler<VaultStatusEventArgs> StatusChanged;

    public Guid? SelectedFolderId => (_folders.SelectedItem as FolderItem)?.Id;
    public string SelectedFolderName => (_folders.SelectedItem as FolderItem)?.Name ?? string.Empty;
    public VaultEntry SelectedEntry => _entries.SelectedItems.Count == 0 ? null : (VaultEntry)_entries.SelectedItems[0].Tag;
    public string SearchText => _search.Text;

    public void SetFolders(IEnumerable<VaultFolder> folders)
    {
        Guid? selectedFolderId = SelectedFolderId;
        _folders.Items.Clear();
        _folders.Items.Add(new FolderItem(null, "All Entries"));
        foreach (VaultFolder folder in folders)
        {
            _folders.Items.Add(new FolderItem(folder.Id, folder.Name));
        }

        _folders.SelectedIndex = 0;
        if (selectedFolderId.HasValue)
        {
            for (int i = 1; i < _folders.Items.Count; i++)
            {
                if (((FolderItem)_folders.Items[i]).Id == selectedFolderId)
                {
                    _folders.SelectedIndex = i;
                    break;
                }
            }
        }

        UpdateFolderCommands();
    }

    public void SetEntries(IEnumerable<VaultEntry> entries)
    {
        _entries.Items.Clear();
        foreach (VaultEntry entry in entries)
        {
            ListViewItem item = new(entry.Name) { Tag = entry };
            _entries.Items.Add(item);
        }
        ClearDetails();
    }

    public void ShowEntry(VaultEntry entry)
    {
        _detailName.Text = entry.Name;
        _detailUser.Text = entry.UserName;
        _detailPassword.Text = entry.Password;
        _detailUrl.Text = entry.Url;
        _detailNotes.Text = entry.Notes;
        SetCommands(true);
    }

    public void ShowStatus(string message)
    {
        StatusChanged?.Invoke(this, new VaultStatusEventArgs(message));
    }

    public void ClearSensitiveState()
    {
        _entries.Items.Clear();
        _search.Clear();
        ClearDetails();
        Clipboard.Clear();
    }


    private void Create_Click(object sender, EventArgs e) => NewEntryRequested?.Invoke(this, EventArgs.Empty);
    private void Edit_Click(object sender, EventArgs e) => EditEntryRequested?.Invoke(this, EventArgs.Empty);
    private void Delete_Click(object sender, EventArgs e) => DeleteEntryRequested?.Invoke(this, EventArgs.Empty);
    private void CopyUser_Click(object sender, EventArgs e) => CopyUserNameRequested?.Invoke(this, EventArgs.Empty);
    private void CopyPassword_Click(object sender, EventArgs e) => CopyPasswordRequested?.Invoke(this, EventArgs.Empty);
    private void Search_TextChanged(object sender, EventArgs e) => FilterChanged?.Invoke(this, EventArgs.Empty);
    private void NewFolder_Click(object sender, EventArgs e) => NewFolderRequested?.Invoke(this, EventArgs.Empty);
    private void RenameFolder_Click(object sender, EventArgs e) => RenameFolderRequested?.Invoke(this, EventArgs.Empty);
    private void DeleteFolder_Click(object sender, EventArgs e) => DeleteFolderRequested?.Invoke(this, EventArgs.Empty);
    private void Entries_DoubleClick(object sender, EventArgs e) => EditEntryRequested?.Invoke(this, EventArgs.Empty);

    private void Folders_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateFolderCommands();
        FilterChanged?.Invoke(this, EventArgs.Empty);
    }

    private void FolderActions_Click(object sender, EventArgs e)
    {
        _folderMenu.Show(_folderActions, 0, _folderActions.Height);
    }

    private void EntrySelected(object sender, EventArgs e)
    {
        VaultEntry entry = SelectedEntry;
        if (entry == null) ClearDetails();
        else ShowEntry(entry);
    }

    private void ClearDetails()
    {
        _detailName.Clear();
        _detailUser.Clear();
        _detailPassword.Clear();
        _detailUrl.Clear();
        _detailNotes.Clear();
        SetCommands(false);
    }

    private void UpdateFolderCommands()
    {
        bool hasFolder = SelectedFolderId.HasValue;
        _renameFolder.Enabled = hasFolder;
        _deleteFolder.Enabled = hasFolder;
    }

    private void SetCommands(bool enabled)
    {
        _edit.Enabled = enabled;
        _delete.Enabled = enabled;
        _copyUser.Enabled = enabled;
        _copyPassword.Enabled = enabled;
    }

    private sealed class FolderItem
    {
        internal FolderItem(Guid? id, string name) { Id = id; Name = name; }
        internal Guid? Id { get; }
        internal string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
