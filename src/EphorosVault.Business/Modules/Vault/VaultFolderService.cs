using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;

namespace EphorosVault.Business.Modules.Vault;

public sealed class VaultFolderService
{
    private readonly IVaultFolderRepository _repository;

    public VaultFolderService(IVaultFolderRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IList<VaultFolder> GetFolders()
    {
        return _repository.GetAll();
    }

    public void Save(VaultFolder folder)
    {
        Guard.ArgumentNotNull(folder, nameof(folder));

        if (folder.Name == null || folder.Name.Trim().Length == 0)
        {
            throw new ArgumentException("A folder name is required.", nameof(folder));
        }

        folder.Name = folder.Name.Trim();

        if (folder.Id == Guid.Empty)
        {
            folder.Id = Guid.NewGuid();
        }

        _repository.Save(folder);
    }

    public void Delete(Guid id) => _repository.Delete(id);
}
