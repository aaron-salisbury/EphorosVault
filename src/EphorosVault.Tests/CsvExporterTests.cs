using EphorosVault.Business.Modules.Vault;
using EphorosVault.Integrations.Export;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace EphorosVault.Tests;

[TestClass]
public class CsvExporterTests
{
    [TestMethod]
    public void BitwardenExportEscapesCsvValues()
    {
        string path = Path.GetTempFileName();
        try
        {
            new BitwardenCsvExporter().Export(path, new[] { new VaultEntry { Name = "Example, Inc.", UserName = "user", Password = "secret", Url = "https://example.com", Notes = "a \"quoted\" note" } });
            string text = File.ReadAllText(path);
            StringAssert.Contains(text, "\"Example, Inc.\"");
            StringAssert.Contains(text, "\"a \"\"quoted\"\" note\"");
        }
        finally { File.Delete(path); }
    }

    [TestMethod]
    public void KeePassExportIncludesExpectedHeader()
    {
        string path = Path.GetTempFileName();
        try
        {
            new KeePassCsvExporter().Export(path, Array.Empty<VaultEntry>());
            StringAssert.Contains(File.ReadAllText(path), "Account,Login Name,Password,Web Site,Comments");
        }
        finally { File.Delete(path); }
    }
}