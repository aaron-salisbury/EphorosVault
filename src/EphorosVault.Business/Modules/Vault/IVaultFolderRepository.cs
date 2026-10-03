using System;
using System.Collections.Generic;

namespace EphorosVault.Business.Modules.Vault;

public interface IVaultFolderRepository
{
    IList<VaultFolder> GetAll();
    void Save(VaultFolder folder);
    void Delete(Guid id);
}
