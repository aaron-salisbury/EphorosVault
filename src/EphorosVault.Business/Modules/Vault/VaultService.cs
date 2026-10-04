using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;

namespace EphorosVault.Business.Modules.Vault;

public sealed class VaultService
{
    private readonly IVaultRepository _repository;

    public VaultService(IVaultRepository repository)
    {
        Guard.ArgumentNotNull(repository, nameof(repository));

        _repository = repository;
    }

    public IList<VaultEntry> GetEntries()
    {
        return _repository.GetAll();
    }

    public void Save(VaultEntry entry)
    {
        Guard.ArgumentNotNull(entry, nameof(entry));

        if (entry.Name == null || entry.Name.Trim().Length == 0)
        {
            throw new ArgumentException("A name is required.", nameof(entry));
        }

        entry.Name = entry.Name.Trim();

        if (entry.Id == Guid.Empty)
        {
            entry.Id = Guid.NewGuid();
        }

        _repository.Save(entry);
    }

    public void Delete(Guid id)
    {
        _repository.Delete(id);
    }
}
