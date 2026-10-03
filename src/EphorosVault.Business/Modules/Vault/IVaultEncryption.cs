namespace EphorosVault.Business.Modules.Vault;

public interface IVaultEncryption
{
    string Encrypt(string plaintext);
    string Decrypt(string ciphertext);
}