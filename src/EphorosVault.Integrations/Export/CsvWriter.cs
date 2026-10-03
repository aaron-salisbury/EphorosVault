using System.IO;
using System.Text;

namespace EphorosVault.Integrations.Export;

internal static class CsvWriter
{
    internal static void WriteRow(TextWriter writer, params string[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (i > 0)
            {
                writer.Write(",");
            }

            writer.Write(Escape(values[i]));
        }
        writer.WriteLine();
    }

    private static string Escape(string value)
    {
        value ??= string.Empty;
        bool quote = value.IndexOf(',') >= 0 || value.IndexOf('"') >= 0 || value.IndexOf('\r') >= 0 || value.IndexOf('\n') >= 0;
        if (!quote)
        {
            return value;
        }

        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
