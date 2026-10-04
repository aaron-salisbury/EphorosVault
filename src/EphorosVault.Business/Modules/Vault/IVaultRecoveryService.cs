namespace EphorosVault.Business.Modules.Vault;

public interface IVaultRecoveryService
{
    void Export(string filePath);
    void Import(string filePath);
}
