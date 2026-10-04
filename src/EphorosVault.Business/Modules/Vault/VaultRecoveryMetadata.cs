using System;

namespace EphorosVault.Business.Modules.Vault;

public sealed class VaultRecoveryMetadata
{
    public Guid VaultId { get; set; }
    public string VerificationValue { get; set; }
}
