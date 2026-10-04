using DotNetFrameworkToolkit.Modules.DependencyInjection;
using EphorosVault.Business.Modules.Vault;
using EphorosVault.Integrations.Cryptography;
using EphorosVault.Integrations.Export;
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

        services.AddSingleton<IVaultEncryption, VaultKeyEncryption>();
        services.AddSingleton<IVaultRecoveryService, VaultRecoveryService>();
        services.AddScoped<KeePass2XmlExporter, KeePass2XmlExporter>();
        services.AddScoped<BitwardenJsonExporter, BitwardenJsonExporter>();

        return services;
    }
}
