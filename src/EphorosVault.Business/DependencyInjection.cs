using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.UserAccess;
using EphorosVault.Business.Modules.Access;
using EphorosVault.Business.Modules.Sample.ApplicationServices;
using EphorosVault.Business.Modules.Sample.DomainServices;
using EphorosVault.Business.Modules.Vault;
using Microsoft.Practices.Unity.Utility;

namespace EphorosVault.Business;

public static class DependencyInjection
{
    /// <summary>
    /// Register internal business-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalBusinessServices(IServiceCollection services)
    {
        Guard.ArgumentNotNull(services, nameof(services));

        // Internal business domain logic.
        services.AddScoped<FlatUIColorProvider, FlatUIColorProvider>();
        services.AddScoped<LineSorter, LineSorter>();
        services.AddScoped<UUIDGenerator, UUIDGenerator>();

        services.AddSingleton<IUserAuthenticator, UserAuthenticator>();
        services.AddScoped<AccessService, AccessService>();
        services.AddScoped<PasswordGenerator, PasswordGenerator>();
        services.AddScoped<VaultService, VaultService>();

        // Orchestrated public-facing (application) services.
        services.AddScoped<ISampleToolsService, SampleToolsService>();

        return services;
    }
}
