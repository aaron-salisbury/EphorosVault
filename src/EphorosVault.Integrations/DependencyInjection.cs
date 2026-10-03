using DotNetFrameworkToolkit.Modules.DependencyInjection;
using EphorosVault.Business.Modules.Vault;
using EphorosVault.Integrations.Cryptography;
using EphorosVault.Integrations.Export;
using EphorosVault.Integrations.Persistence;
using Microsoft.Practices.Unity.Utility;

namespace EphorosVault.Integrations;

public static class DependencyInjection
{
    /// <summary>
    /// Register internal integrations-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalIntegrationsServices(IServiceCollection services)
    {
        Guard.ArgumentNotNull(services, nameof(services));

        services.AddSingleton<IVaultEncryption, EnterpriseLibraryVaultEncryption>();
        services.AddScoped<IVaultRepository, SqlCeVaultRepository>();
        services.AddScoped<KeePassCsvExporter, KeePassCsvExporter>();
        services.AddScoped<BitwardenCsvExporter, BitwardenCsvExporter>();

        return services;
    }
}
