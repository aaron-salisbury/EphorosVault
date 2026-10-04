using EphorosVault.Business.Modules.Vault;
using EphorosVault.Integrations.Export;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml;

namespace EphorosVault.Tests;

[TestClass]
public class ExporterTests
{
    [TestMethod]
    public void BitwardenJsonPreservesFoldersAndEscapesValues()
    {
        Guid folderId = Guid.NewGuid();
        string path = Path.GetTempFileName();
        try
        {
            new BitwardenJsonExporter().Export(path,
                [new VaultEntry { Id = Guid.NewGuid(), FolderId = folderId, Name = "Example \"Login\"", UserName = "user", Password = "secret", Url = "https://example.com", Notes = "line 1\nline 2" }],
                [new VaultFolder { Id = folderId, Name = "Work" }]);

            string json = File.ReadAllText(path);
            StringAssert.Contains(json, "\"encrypted\": false");
            StringAssert.Contains(json, "\"name\": \"Work\"");
            StringAssert.Contains(json, "\"folderId\": \"" + folderId.ToString("D") + "\"");
            StringAssert.Contains(json, "\"name\": \"Example \\\"Login\\\"\"");
            StringAssert.Contains(json, "\"notes\": \"line 1\\nline 2\"");
            StringAssert.Contains(json, "\"uri\": \"https://example.com\"");
        }
        finally { File.Delete(path); }
    }

    [TestMethod]
    public void BitwardenJsonLeavesUnfiledEntryWithoutFolderId()
    {
        string path = Path.GetTempFileName();
        try
        {
            new BitwardenJsonExporter().Export(path, [new VaultEntry { Id = Guid.NewGuid(), Name = "Unfiled" }], []);
            Assert.IsFalse(File.ReadAllText(path).Contains("\"folderId\""));
        }
        finally { File.Delete(path); }
    }

    [TestMethod]
    public void KeePass2XmlPreservesFoldersAndSpecialCharacters()
    {
        Guid folderId = Guid.NewGuid();
        string path = Path.GetTempFileName();
        try
        {
            new KeePass2XmlExporter().Export(path,
                [new VaultEntry { Id = Guid.NewGuid(), FolderId = folderId, Name = "A < B & C", UserName = "user", Password = "p&<>", Url = "https://example.com?a=1&b=2", Notes = "line 1\r\nline 2" }],
                [new VaultFolder { Id = folderId, Name = "Work & Personal" }]);

            XmlDocument document = new();
            document.Load(path);
            XmlNode folder = document.SelectSingleNode("/KeePassFile/Root/Group/Group[Name='Work & Personal']");
            Assert.IsNotNull(folder);
            Assert.AreEqual("A < B & C", folder.SelectSingleNode("Entry/String[Key='Title']/Value").InnerText);
            Assert.AreEqual("p&<>", folder.SelectSingleNode("Entry/String[Key='Password']/Value").InnerText);
            Assert.AreEqual("line 1\r\nline 2", folder.SelectSingleNode("Entry/String[Key='Notes']/Value").InnerText);
        }
        finally { File.Delete(path); }
    }

    [TestMethod]
    public void KeePass2XmlPlacesUnfiledEntriesInRootGroup()
    {
        string path = Path.GetTempFileName();
        try
        {
            new KeePass2XmlExporter().Export(path, new[] { new VaultEntry { Id = Guid.NewGuid(), Name = "Unfiled" } }, Array.Empty<VaultFolder>());
            XmlDocument document = new();
            document.Load(path);
            Assert.AreEqual("Unfiled", document.SelectSingleNode("/KeePassFile/Root/Group/Entry/String[Key='Title']/Value").InnerText);
        }
        finally { File.Delete(path); }
    }
}
