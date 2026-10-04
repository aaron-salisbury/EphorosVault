namespace EphorosVault.Integrations.Cryptography;

public interface IVaultKeyStore
{
    bool Exists { get; }

    void EnsureCreated();
    byte[] Load();
    void Save(byte[] key);
}
