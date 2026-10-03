using EphorosVault.Business.Modules.Vault;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;

namespace EphorosVault.Data.Vault;

public sealed class SqlCeVaultRepository : IVaultRepository
{
    private readonly VaultDatabase _database;
    private readonly IVaultEncryption _encryption;

    public SqlCeVaultRepository(VaultDatabase database, IVaultEncryption encryption)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _encryption = encryption ?? throw new ArgumentNullException(nameof(encryption));
    }

    public IList<VaultEntry> GetAll()
    {
        List<VaultEntry> entries = new();
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeCommand command = new("SELECT Id, Name, UserName, Password, Url, Notes FROM VaultEntries ORDER BY Name", connection);
        using SqlCeDataReader reader = command.ExecuteReader();
        while (reader.Read()) entries.Add(Read(reader));
        return entries;
    }

    public VaultEntry Get(Guid id)
    {
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeCommand command = new("SELECT Id, Name, UserName, Password, Url, Notes FROM VaultEntries WHERE Id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        using SqlCeDataReader reader = command.ExecuteReader();
        return reader.Read() ? Read(reader) : null;
    }

    public void Save(VaultEntry entry)
    {
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeCommand exists = new("SELECT COUNT(*) FROM VaultEntries WHERE Id = @id", connection);
        exists.Parameters.AddWithValue("@id", entry.Id);
        bool update = Convert.ToInt32(exists.ExecuteScalar()) > 0;

        using SqlCeCommand command = connection.CreateCommand();
        command.CommandText = update
            ? "UPDATE VaultEntries SET Name=@name, UserName=@user, Password=@password, Url=@url, Notes=@notes WHERE Id=@id"
            : "INSERT INTO VaultEntries (Id, Name, UserName, Password, Url, Notes) VALUES (@id, @name, @user, @password, @url, @notes)";
        command.Parameters.AddWithValue("@id", entry.Id);
        command.Parameters.AddWithValue("@name", entry.Name);
        command.Parameters.AddWithValue("@user", _encryption.Encrypt(entry.UserName ?? string.Empty));
        command.Parameters.AddWithValue("@password", _encryption.Encrypt(entry.Password ?? string.Empty));
        command.Parameters.AddWithValue("@url", _encryption.Encrypt(entry.Url ?? string.Empty));
        command.Parameters.AddWithValue("@notes", _encryption.Encrypt(entry.Notes ?? string.Empty));
        command.ExecuteNonQuery();
    }

    public void Delete(Guid id)
    {
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeCommand command = new("DELETE FROM VaultEntries WHERE Id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

    private VaultEntry Read(SqlCeDataReader reader)
    {
        return new VaultEntry
        {
            Id = reader.GetGuid(0),
            Name = reader.GetString(1),
            UserName = _encryption.Decrypt(reader.GetString(2)),
            Password = _encryption.Decrypt(reader.GetString(3)),
            Url = _encryption.Decrypt(reader.GetString(4)),
            Notes = _encryption.Decrypt(reader.GetString(5))
        };
    }
}