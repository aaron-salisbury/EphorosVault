using System;
using System.Collections.Generic;

namespace EphorosVault.Business.Modules.Vault;

public interface IVaultRepository
{
    IList<VaultEntry> GetAll();
    VaultEntry Get(Guid id);
    void Save(VaultEntry entry);
    void Delete(Guid id);
}