using EphorosVault.Business.Modules.Vault;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EphorosVault.Integrations.Export;

public sealed class BitwardenCsvExporter : IVaultExporter
{
    public string FormatName => "Bitwarden CSV";
    public string FileFilter => "CSV files (*.csv)|*.csv";

    public void Export(string filePath, IEnumerable<VaultEntry> entries)
    {
        using StreamWriter writer = new(filePath, false, Encoding.UTF8);
        CsvWriter.WriteRow(writer, "folder", "favorite", "type", "name", "notes", "fields", "reprompt", "login_uri", "login_username", "login_password", "login_totp");
        foreach (VaultEntry entry in entries)
        {
            CsvWriter.WriteRow(writer, string.Empty, "0", "login", entry.Name, entry.Notes, string.Empty, "0", entry.Url, entry.UserName, entry.Password, string.Empty);
        }
    }
}
