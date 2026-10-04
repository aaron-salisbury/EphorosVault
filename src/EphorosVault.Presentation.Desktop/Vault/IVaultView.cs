using EphorosVault.Business.Modules.Vault;
using System;
using System.Collections.Generic;

namespace EphorosVault.Presentation.Desktop.Vault;

public interface IVaultView
{
    event EventHandler CopyPasswordRequested;
    event EventHandler CopyUserNameRequested;
    event EventHandler DeleteEntryRequested;
    event EventHandler EditEntryRequested;
    event EventHandler FilterChanged;
    event EventHandler NewEntryRequested;

    Guid? SelectedFolderId { get; }
    VaultEntry SelectedEntry { get; }
    string SearchText { get; }

    void ClearSensitiveState();
    void SetEntries(IEnumerable<VaultEntry> entries);
    void SetFolders(IEnumerable<VaultFolder> folders);
    void ShowEntry(VaultEntry entry);
    void ShowStatus(string message);
}
