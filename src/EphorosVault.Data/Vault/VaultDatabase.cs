using DotNetFrameworkToolkit.Modules.DataAccess;
using System;
using System.Data.SqlServerCe;
using System.IO;

namespace EphorosVault.Data.Vault;

public sealed class VaultDatabase
{
    private readonly string _connectionString;

    public VaultDatabase(string databasePath)
    {
        if (databasePath == null) throw new ArgumentNullException(nameof(databasePath));
        _connectionString = SqlServerCeDatabase.BuildConnectionString(databasePath);
    }

    public string ConnectionString => _connectionString;

    public void Initialize()
    {
        SqlCeConnectionStringBuilder builder = new(_connectionString);
        if (!File.Exists(builder.DataSource))
        {
            SqlServerCeDatabase.CreateDatabase(_connectionString);
        }

        using SqlCeConnection connection = SqlServerCeDatabase.OpenConnection(_connectionString);
        if (!TableExists(connection, "VaultEntries"))
        {
            using SqlCeCommand command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE VaultEntries (Id uniqueidentifier NOT NULL PRIMARY KEY, Name nvarchar(256) NOT NULL, UserName ntext NOT NULL, Password ntext NOT NULL, Url ntext NOT NULL, Notes ntext NOT NULL)";
            command.ExecuteNonQuery();
        }
    }

    public SqlCeConnection OpenConnection() => SqlServerCeDatabase.OpenConnection(_connectionString);

    private static bool TableExists(SqlCeConnection connection, string tableName)
    {
        using SqlCeCommand command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @name";
        command.Parameters.AddWithValue("@name", tableName);
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }
}