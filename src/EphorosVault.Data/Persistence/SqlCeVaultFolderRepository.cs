using EphorosVault.Business.Modules.Vault;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Net;

namespace EphorosVault.Data.Persistence;

public sealed class SqlCeVaultFolderRepository : IVaultFolderRepository
{
    private readonly VaultDatabase _database;

    public SqlCeVaultFolderRepository(VaultDatabase database)
    {
        Guard.ArgumentNotNull(database, nameof(database));

        _database = database;
    }

    public IList<VaultFolder> GetAll()
    {
        List<VaultFolder> folders = new();
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeCommand command = new("SELECT Id, Name FROM VaultFolders ORDER BY Name", connection);
        using SqlCeDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            folders.Add(new VaultFolder { Id = reader.GetGuid(0), Name = reader.GetString(1) });
        }

        return folders;
    }

    public void Save(VaultFolder folder)
    {
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeCommand exists = new("SELECT COUNT(*) FROM VaultFolders WHERE Id = @id", connection);
        exists.Parameters.AddWithValue("@id", folder.Id);
        bool update = Convert.ToInt32(exists.ExecuteScalar()) > 0;
        using SqlCeCommand command = connection.CreateCommand();
        command.CommandText = update ? "UPDATE VaultFolders SET Name=@name WHERE Id=@id" : "INSERT INTO VaultFolders (Id, Name) VALUES (@id, @name)";
        command.Parameters.AddWithValue("@id", folder.Id);
        command.Parameters.AddWithValue("@name", folder.Name);
        command.ExecuteNonQuery();
    }

    public void Delete(Guid id)
    {
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeTransaction transaction = connection.BeginTransaction();
        using SqlCeCommand clearEntries = connection.CreateCommand();
        clearEntries.Transaction = transaction;
        clearEntries.CommandText = "UPDATE VaultEntries SET FolderId = NULL WHERE FolderId = @id";
        clearEntries.Parameters.AddWithValue("@id", id);
        clearEntries.ExecuteNonQuery();

        using SqlCeCommand delete = connection.CreateCommand();
        delete.Transaction = transaction;
        delete.CommandText = "DELETE FROM VaultFolders WHERE Id = @id";
        delete.Parameters.AddWithValue("@id", id);
        delete.ExecuteNonQuery();
        transaction.Commit();
    }
}
