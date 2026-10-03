using EphorosVault.Business.Modules.Vault;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System;

namespace EphorosVault.Integrations.Cryptography;

public sealed class EnterpriseLibraryVaultEncryption : IVaultEncryption
{
    public const string ProviderName = "VaultEncryption";

    public string Encrypt(string plaintext)
    {
        if (plaintext == null)
        {
            throw new ArgumentNullException(nameof(plaintext));
        }

        return Cryptographer.EncryptSymmetric(ProviderName, plaintext);
    }

    public string Decrypt(string ciphertext)
    {
        if (ciphertext == null)
        {
            throw new ArgumentNullException(nameof(ciphertext));
        }

        return Cryptographer.DecryptSymmetric(ProviderName, ciphertext);
    }
}
