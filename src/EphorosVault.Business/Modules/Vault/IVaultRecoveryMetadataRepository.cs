using System;

namespace EphorosVault.Business.Modules.Vault;

public interface IVaultRecoveryMetadataRepository
{
    VaultRecoveryMetadata Get();
    void Save(VaultRecoveryMetadata metadata);
}
