namespace EphorosVault.Integrations.Cryptography;

public interface IVaultKeyStore
{
    bool Exists { get; }
    byte[] Load();
    void Save(byte[] key);
    void ExportRecoveryKey(string filePath);
    void ImportRecoveryKey(string filePath);
}