using EphorosVault.Business.Modules.Vault;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EphorosVault.Integrations.Export;

public sealed class BitwardenJsonExporter : IVaultExporter
{
    public string FormatName => "Bitwarden JSON";
    public string FileFilter => "JSON files (*.json)|*.json";
    public string DefaultExtension => "json";

    public void Export(string filePath, IEnumerable<VaultEntry> entries, IEnumerable<VaultFolder> folders)
    {
        Dictionary<Guid, string> folderIds = new();
        StringBuilder json = new();
        json.Append("{\r\n  \"encrypted\": false,\r\n  \"folders\": [");

        bool first = true;
        foreach (VaultFolder folder in folders)
        {
            if (!first) json.Append(",");
            string id = folder.Id.ToString("D");
            folderIds[folder.Id] = id;
            json.Append("\r\n    {\"id\": ").Append(Json(folder.Id.ToString("D"))).Append(", \"name\": ").Append(Json(folder.Name)).Append("}");
            first = false;
        }

        if (!first) json.Append("\r\n  ");
        json.Append("],\r\n  \"items\": [");
        first = true;
        foreach (VaultEntry entry in entries)
        {
            if (!first) json.Append(",");
            json.Append("\r\n    {");
            json.Append("\"id\": ").Append(Json(entry.Id.ToString("D"))).Append(", ");
            if (entry.FolderId.HasValue && folderIds.ContainsKey(entry.FolderId.Value))
            {
                json.Append("\"folderId\": ").Append(Json(folderIds[entry.FolderId.Value])).Append(", ");
            }
            json.Append("\"type\": 1, \"name\": ").Append(Json(entry.Name)).Append(", \"notes\": ").Append(Json(entry.Notes)).Append(", ");
            json.Append("\"login\": {\"username\": ").Append(Json(entry.UserName)).Append(", \"password\": ").Append(Json(entry.Password)).Append(", \"totp\": null, \"uris\": [");
            if (!string.IsNullOrEmpty(entry.Url))
            {
                json.Append("{\"match\": null, \"uri\": ").Append(Json(entry.Url)).Append("}");
            }
            json.Append("]}}");
            first = false;
        }

        if (!first) json.Append("\r\n  ");
        json.Append("]\r\n}\r\n");
        File.WriteAllText(filePath, json.ToString(), new UTF8Encoding(false));
    }

    private static string Json(string value)
    {
        if (value == null) return "null";
        StringBuilder result = new("\"");
        foreach (char character in value)
        {
            switch (character)
            {
                case '\"': result.Append("\\\""); break;
                case '\\': result.Append("\\\\"); break;
                case '\b': result.Append("\\b"); break;
                case '\f': result.Append("\\f"); break;
                case '\n': result.Append("\\n"); break;
                case '\r': result.Append("\\r"); break;
                case '\t': result.Append("\\t"); break;
                default:
                    if (character < 0x20)
                    {
                        result.Append("\\u").Append(((int)character).ToString("x4", CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        result.Append(character);
                    }
                    break;
            }
        }
        return result.Append("\"").ToString();
    }
}
