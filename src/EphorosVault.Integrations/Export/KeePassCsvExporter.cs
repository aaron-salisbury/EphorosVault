using EphorosVault.Business.Modules.Vault;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EphorosVault.Integrations.Export;

public sealed class KeePassCsvExporter : IVaultExporter
{
    public string FormatName => "KeePass CSV";
    public string FileFilter => "CSV files (*.csv)|*.csv";

    public void Export(string filePath, IEnumerable<VaultEntry> entries)
    {
        using StreamWriter writer = new(filePath, false, Encoding.UTF8);
        CsvWriter.WriteRow(writer, "Account", "Login Name", "Password", "Web Site", "Comments");
        foreach (VaultEntry entry in entries)
        {
            CsvWriter.WriteRow(writer, entry.Name, entry.UserName, entry.Password, entry.Url, entry.Notes);
        }
    }
}
