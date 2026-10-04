using System.Collections.Generic;

namespace EphorosVault.Business.Modules.Vault;

public interface IVaultExporter
{
    string FormatName { get; }
    string FileFilter { get; }
    string DefaultExtension { get; }
    void Export(string filePath, IEnumerable<VaultEntry> entries, IEnumerable<VaultFolder> folders);
}
