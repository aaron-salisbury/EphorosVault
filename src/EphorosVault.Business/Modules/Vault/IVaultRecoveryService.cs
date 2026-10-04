namespace EphorosVault.Business.Modules.Vault;

public interface IVaultRecoveryService
{
    void EnsureInitialized();
    void Export(string filePath);
    void Import(string filePath);
}
