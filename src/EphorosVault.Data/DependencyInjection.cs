using DotNetFrameworkToolkit.Modules.DependencyInjection;
using EphorosVault.Business.Modules.Vault;
using EphorosVault.Data.Vault;
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

        services.AddScoped<IEmbeddedDataAccess, EmbeddedDataAccess>();
        services.AddScoped<IVaultRepository, SqlCeVaultRepository>();

        return services;
    }
}
