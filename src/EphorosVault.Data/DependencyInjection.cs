using DotNetFrameworkToolkit.Modules.DependencyInjection;
using EphorosVault.Business.Modules.Access;
using EphorosVault.Business.Modules.Vault;
using EphorosVault.Data.Persistence;
using Microsoft.Practices.Unity.Utility;

namespace EphorosVault.Data;

public static class DependencyInjection
{
    /// <summary>
    /// Register internal data-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalDataServices(IServiceCollection services)
    {
        Guard.ArgumentNotNull(services, nameof(services));

        services.AddScoped<IUserCredentialRepository, SqlCeUserCredentialRepository>();
        services.AddScoped<IVaultRepository, SqlCeVaultRepository>();
        services.AddSingleton<IVaultRecoveryMetadataRepository, SqlCeVaultRecoveryMetadataRepository>();
        services.AddScoped<IVaultFolderRepository, SqlCeVaultFolderRepository>();

        return services;
    }
}
