using EphorosVault.Business.Modules.Vault;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace EphorosVault.Integrations.Export;

public sealed class KeePass2XmlExporter : IVaultExporter
{
    public string FormatName => "KeePass 2 XML";
    public string FileFilter => "KeePass 2 XML files (*.xml)|*.xml";
    public string DefaultExtension => "xml";

    public void Export(string filePath, IEnumerable<VaultEntry> entries, IEnumerable<VaultFolder> folders)
    {
        Dictionary<Guid, List<VaultEntry>> grouped = new();
        List<VaultEntry> unfiled = new();
        foreach (VaultEntry entry in entries)
        {
            if (!entry.FolderId.HasValue)
            {
                unfiled.Add(entry);
                continue;
            }

            if (!grouped.ContainsKey(entry.FolderId.Value))
            {
                grouped.Add(entry.FolderId.Value, new List<VaultEntry>());
            }

            grouped[entry.FolderId.Value].Add(entry);
        }

        XmlWriterSettings settings = new() { Encoding = new UTF8Encoding(false), Indent = true };
        using XmlWriter writer = XmlWriter.Create(filePath, settings);
        writer.WriteStartDocument();
        writer.WriteStartElement("KeePassFile");
        writer.WriteStartElement("Meta");
        writer.WriteElementString("Generator", "Ephoros Vault");
        writer.WriteEndElement();
        writer.WriteStartElement("Root");
        WriteGroupStart(writer, Guid.NewGuid(), "Ephoros Vault");

        foreach (VaultEntry entry in unfiled)
        {
            WriteEntry(writer, entry);
        }

        foreach (VaultFolder folder in folders)
        {
            WriteGroupStart(writer, folder.Id, folder.Name);
            if (grouped.ContainsKey(folder.Id))
            {
                foreach (VaultEntry entry in grouped[folder.Id])
                {
                    WriteEntry(writer, entry);
                }
            }
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
        writer.WriteEndElement();
        writer.WriteEndElement();
        writer.WriteEndDocument();
    }

    private static void WriteGroupStart(XmlWriter writer, Guid id, string name)
    {
        writer.WriteStartElement("Group");
        writer.WriteElementString("UUID", Convert.ToBase64String(id.ToByteArray()));
        writer.WriteElementString("Name", name ?? string.Empty);
    }

    private static void WriteEntry(XmlWriter writer, VaultEntry entry)
    {
        writer.WriteStartElement("Entry");
        writer.WriteElementString("UUID", Convert.ToBase64String(entry.Id.ToByteArray()));
        WriteString(writer, "Title", entry.Name);
        WriteString(writer, "UserName", entry.UserName);
        WriteString(writer, "Password", entry.Password);
        WriteString(writer, "URL", entry.Url);
        WriteString(writer, "Notes", entry.Notes);
        writer.WriteEndElement();
    }

    private static void WriteString(XmlWriter writer, string key, string value)
    {
        writer.WriteStartElement("String");
        writer.WriteElementString("Key", key);
        writer.WriteElementString("Value", value ?? string.Empty);
        writer.WriteEndElement();
    }
}
