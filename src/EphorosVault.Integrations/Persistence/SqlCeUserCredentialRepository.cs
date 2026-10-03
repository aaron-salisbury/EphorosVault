using DotNetFrameworkToolkit.Modules.UserAccess;
using EphorosVault.Business.Modules.Access;
using System;
using System.Data.SqlServerCe;

namespace EphorosVault.Integrations.Persistence;

public sealed class SqlCeUserCredentialRepository : IUserCredentialRepository
{
    private readonly VaultDatabase _database;

    public SqlCeUserCredentialRepository(VaultDatabase database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
    }

    public bool HasUser()
    {
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeCommand command = new("SELECT COUNT(*) FROM UserCredential", connection);
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public CryptographyCredential Get()
    {
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeCommand command = new("SELECT LoginSalt, LoginHash, LoginWorkFactor FROM UserCredential", connection);
        using SqlCeDataReader reader = command.ExecuteReader();
        if (!reader.Read()) return null;
        return new CryptographyCredential
        {
            LoginSalt = (byte[])reader.GetValue(0),
            LoginHash = (byte[])reader.GetValue(1),
            LoginWorkFactor = reader.GetInt32(2)
        };
    }

    public void Save(CryptographyCredential credential)
    {
        if (credential == null) throw new ArgumentNullException(nameof(credential));
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeTransaction transaction = connection.BeginTransaction();
        using SqlCeCommand delete = new("DELETE FROM UserCredential", connection, transaction);
        delete.ExecuteNonQuery();
        using SqlCeCommand insert = new("INSERT INTO UserCredential (LoginSalt, LoginHash, LoginWorkFactor) VALUES (@salt, @hash, @work)", connection, transaction);
        insert.Parameters.AddWithValue("@salt", credential.LoginSalt);
        insert.Parameters.AddWithValue("@hash", credential.LoginHash);
        insert.Parameters.AddWithValue("@work", credential.LoginWorkFactor);
        insert.ExecuteNonQuery();
        transaction.Commit();
    }
}