using DotNetFrameworkToolkit.Modules.DataAccess;
using System;
using System.Data.SqlServerCe;
using System.IO;

namespace EphorosVault.Integrations.Persistence;

public sealed class VaultDatabase
{
    private readonly string _connectionString;

    public VaultDatabase(string databasePath)
    {
        if (databasePath == null) throw new ArgumentNullException(nameof(databasePath));
        _connectionString = SqlServerCeDatabase.BuildConnectionString(databasePath);
    }

    public void Initialize()
    {
        SqlCeConnectionStringBuilder builder = new(_connectionString);
        if (!File.Exists(builder.DataSource)) SqlServerCeDatabase.CreateDatabase(_connectionString);

        using SqlCeConnection connection = SqlServerCeDatabase.OpenConnection(_connectionString);
        using SqlCeCommand check = connection.CreateCommand();
        check.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'VaultEntries'";
        if (Convert.ToInt32(check.ExecuteScalar()) == 0)
        {
            using SqlCeCommand create = connection.CreateCommand();
            create.CommandText = "CREATE TABLE VaultEntries (Id uniqueidentifier NOT NULL PRIMARY KEY, Name nvarchar(256) NOT NULL, UserName ntext NOT NULL, Password ntext NOT NULL, Url ntext NOT NULL, Notes ntext NOT NULL)";
            create.ExecuteNonQuery();
        }
    }

    public SqlCeConnection OpenConnection() => SqlServerCeDatabase.OpenConnection(_connectionString);
}