using EphorosVault.Business.Modules.Vault;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Data.SqlServerCe;

namespace EphorosVault.Data.Persistence;

public sealed class SqlCeVaultRecoveryMetadataRepository : IVaultRecoveryMetadataRepository
{
    private readonly VaultDatabase _database;

    public SqlCeVaultRecoveryMetadataRepository(VaultDatabase database)
    {
        Guard.ArgumentNotNull(database, nameof(database));
        _database = database;
    }

    public VaultRecoveryMetadata Get()
    {
        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeCommand command = connection.CreateCommand();
        command.CommandText = "SELECT TOP 1 VaultId, VerificationValue FROM VaultRecoveryMetadata";
        using SqlCeDataReader reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new VaultRecoveryMetadata
        {
            VaultId = reader.GetGuid(0),
            VerificationValue = reader.GetString(1)
        };
    }

    public void Save(VaultRecoveryMetadata metadata)
    {
        Guard.ArgumentNotNull(metadata, nameof(metadata));

        using SqlCeConnection connection = _database.OpenConnection();
        using SqlCeTransaction transaction = connection.BeginTransaction();
        using SqlCeCommand delete = connection.CreateCommand();
        delete.Transaction = transaction;
        delete.CommandText = "DELETE FROM VaultRecoveryMetadata";
        delete.ExecuteNonQuery();

        using SqlCeCommand insert = connection.CreateCommand();
        insert.Transaction = transaction;
        insert.CommandText = "INSERT INTO VaultRecoveryMetadata (VaultId, VerificationValue) VALUES (@VaultId, @VerificationValue)";
        insert.Parameters.AddWithValue("@VaultId", metadata.VaultId);
        insert.Parameters.AddWithValue("@VerificationValue", metadata.VerificationValue);
        insert.ExecuteNonQuery();
        transaction.Commit();
    }
}
