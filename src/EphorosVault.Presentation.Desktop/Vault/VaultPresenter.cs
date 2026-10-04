using EphorosVault.Business.Modules.Vault;
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
        foreach (VaultEntry entry in _vaultService.GetEntries()) _entries.Add(entry);
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
            if (folderId.HasValue && entry.FolderId != folderId) continue;
            if (search.Length > 0 &&
                entry.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0 &&
                entry.UserName.IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0 &&
                entry.Url.IndexOf(search, StringComparison.OrdinalIgnoreCase) < 0) continue;
            filtered.Add(entry);
        }

        _view.SetEntries(filtered);
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
        if (entry == null) return;
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
        if (entry == null) return;
        if (System.Windows.Forms.MessageBox.Show("Delete '" + entry.Name + "'?", "Ephoros Vault", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
        {
            _vaultService.Delete(entry.Id);
            Refresh();
        }
    }

    private void CopyUserNameRequested(object sender, EventArgs e)
    {
        VaultEntry entry = _view.SelectedEntry;
        if (entry != null && entry.UserName.Length > 0) System.Windows.Forms.Clipboard.SetText(entry.UserName);
    }

    private void CopyPasswordRequested(object sender, EventArgs e)
    {
        VaultEntry entry = _view.SelectedEntry;
        if (entry != null && entry.Password.Length > 0) System.Windows.Forms.Clipboard.SetText(entry.Password);
    }

    private void FilterChanged(object sender, EventArgs e) => ApplyFilter();
}
