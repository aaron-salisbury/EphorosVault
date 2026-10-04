using EphorosVault.Business.Modules.Vault;
using Microsoft.Practices.Unity.Utility;

namespace EphorosVault.Integrations.Cryptography;

public sealed class VaultRecoveryService : IVaultRecoveryService
{
    private readonly IVaultKeyStore _keyStore;

    public VaultRecoveryService(IVaultKeyStore keyStore)
    {
        Guard.ArgumentNotNull(keyStore, nameof(keyStore));

        _keyStore = keyStore;
    }

    public void Export(string filePath)
    {
        _keyStore.ExportRecoveryKey(filePath);
    }

    public void Import(string filePath)
    {
        _keyStore.ImportRecoveryKey(filePath);
    }
}
