using EphorosVault.Business.Modules.Vault;
using EphorosVault.Presentation.Desktop.Properties;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;

namespace EphorosVault.Presentation.Desktop.Vault;

public sealed class VaultPresenter
{
    private readonly VaultFolderService _folderService;
    private readonly PasswordGenerator _passwordGenerator;
    private readonly VaultService _vaultService;
    private readonly IVaultView _view;
    private readonly List<VaultEntry> _entries = [];

    public VaultPresenter(IVaultView view, VaultService vaultService, VaultFolderService folderService, PasswordGenerator passwordGenerator)
    {
        Guard.ArgumentNotNull(view, nameof(view));
        Guard.ArgumentNotNull(vaultService, nameof(vaultService));
        Guard.ArgumentNotNull(folderService, nameof(folderService));
        Guard.ArgumentNotNull(passwordGenerator, nameof(passwordGenerator));

        _view = view;
        _vaultService = vaultService;
        _folderService = folderService;
        _passwordGenerator = passwordGenerator;

        _view.NewEntryRequested += NewEntryRequested;
        _view.NewFolderRequested += NewFolderRequested;
        _view.RenameFolderRequested += RenameFolderRequested;
        _view.DeleteFolderRequested += DeleteFolderRequested;
        _view.EditEntryRequested += EditEntryRequested;
        _view.DeleteEntryRequested += DeleteEntryRequested;
        _view.CopyUserNameRequested += CopyUserNameRequested;
        _view.CopyPasswordRequested += CopyPasswordRequested;
        _view.FilterChanged += FilterChanged;
    }

    public void Refresh()
    {
        _view.SetFolders(_folderService.GetFolders());
        _entries.Clear();
        foreach (VaultEntry entry in _vaultService.GetEntries())
        {
            _entries.Add(entry);
        }

        ApplyFilter();
    }

    public void ClearSensitiveState()
    {
        _entries.Clear();
        _view.ClearSensitiveState();
    }

    private void ApplyFilter()
    {
        List<VaultEntry> filtered = new();
        Guid? folderId = _view.SelectedFolderId;
        string search = _view.SearchText.Trim();

        foreach (VaultEntry entry in _entries)
        {
            if (folderId.HasValue && entry.FolderId != folderId)
            {
                continue;
            }

            if (search.Length > 0 &&
                entry.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0 &&
                entry.UserName.IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0 &&
                entry.Url.IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0)
            {
                continue;
            }

            filtered.Add(entry);
        }

        _view.SetEntries(filtered);
    }

    private void NewFolderRequested(object sender, EventArgs e)
    {
        using Forms.FolderNameForm form = new();
        if (form.ShowDialog((System.Windows.Forms.IWin32Window)_view) == System.Windows.Forms.DialogResult.OK)
        {
            try
            {
                _folderService.Save(new VaultFolder { Name = form.FolderName });
                Refresh();
                _view.ShowStatus("Folder created.");
            }
            catch (ArgumentException exception)
            {
                form.ShowValidationError(exception.Message);
                NewFolderRequested(sender, e);
            }
        }
    }

    private void RenameFolderRequested(object sender, EventArgs e)
    {
        Guid? folderId = _view.SelectedFolderId;
        if (!folderId.HasValue)
        {
            return;
        }

        using Forms.FolderNameForm form = new(_view.SelectedFolderName, "Rename Folder");
        if (form.ShowDialog((System.Windows.Forms.IWin32Window)_view) == System.Windows.Forms.DialogResult.OK)
        {
            try
            {
                _folderService.Save(new VaultFolder { Id = folderId.Value, Name = form.FolderName });
                Refresh();
                _view.ShowStatus("Folder renamed.");
            }
            catch (ArgumentException exception)
            {
                form.ShowValidationError(exception.Message);
                RenameFolderRequested(sender, e);
            }
        }
    }

    private void DeleteFolderRequested(object sender, EventArgs e)
    {
        Guid? folderId = _view.SelectedFolderId;
        if (!folderId.HasValue)
        {
            return;
        }

        string message = "Delete folder '" + _view.SelectedFolderName + "'? Credentials in this folder will not be deleted; they will remain in All Entries.";
        if (System.Windows.Forms.MessageBox.Show((System.Windows.Forms.IWin32Window)_view, message, Resources.ProductName, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
        {
            _folderService.Delete(folderId.Value);
            Refresh();
            _view.ShowStatus("Folder deleted.");
        }
    }

    private void NewEntryRequested(object sender, EventArgs e)
    {
        VaultEntry entry = new();
        using Forms.VaultEntryForm form = new(entry, _folderService.GetFolders(), _passwordGenerator);
        if (form.ShowDialog((System.Windows.Forms.IWin32Window)_view) == System.Windows.Forms.DialogResult.OK)
        {
            _vaultService.Save(entry);
            Refresh();
            _view.ShowStatus("Credential created.");
        }
    }

    private void EditEntryRequested(object sender, EventArgs e)
    {
        VaultEntry entry = _view.SelectedEntry;
        if (entry == null)
        {
            return;
        }

        using Forms.VaultEntryForm form = new(entry, _folderService.GetFolders(), _passwordGenerator);
        if (form.ShowDialog((System.Windows.Forms.IWin32Window)_view) == System.Windows.Forms.DialogResult.OK)
        {
            _vaultService.Save(entry);
            Refresh();
            _view.ShowStatus("Changes saved.");
        }
    }

    private void DeleteEntryRequested(object sender, EventArgs e)
    {
        VaultEntry entry = _view.SelectedEntry;
        if (entry == null)
        {
            return;
        }

        if (System.Windows.Forms.MessageBox.Show("Delete '" + entry.Name + "'?", Resources.ProductName, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
        {
            _vaultService.Delete(entry.Id);
            Refresh();
        }
    }

    private void CopyUserNameRequested(object sender, EventArgs e)
    {
        VaultEntry entry = _view.SelectedEntry;
        if (entry != null && entry.UserName.Length > 0)
        {
            System.Windows.Forms.Clipboard.SetText(entry.UserName);
        }
    }

    private void CopyPasswordRequested(object sender, EventArgs e)
    {
        VaultEntry entry = _view.SelectedEntry;
        if (entry != null && entry.Password.Length > 0)
        {
            System.Windows.Forms.Clipboard.SetText(entry.Password);
        }
    }

    private void FilterChanged(object sender, EventArgs e)
    {
        ApplyFilter();
    }
}
