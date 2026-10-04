using System;

namespace EphorosVault.Business.Modules.Vault;

public sealed class VaultFolder
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }
}
