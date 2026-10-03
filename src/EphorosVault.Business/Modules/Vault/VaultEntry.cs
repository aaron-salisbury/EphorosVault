using System;

namespace EphorosVault.Business.Modules.Vault;

public sealed class VaultEntry
{
    public Guid Id
    {
        get; set;
    }
    public Guid? FolderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
