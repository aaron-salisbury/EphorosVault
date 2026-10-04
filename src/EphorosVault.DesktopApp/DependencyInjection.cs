using DotNetFrameworkToolkit.Core;
using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.FileSystem;
using DotNetFrameworkToolkit.Modules.Logging;
using EphorosVault.Data.Persistence;
using EphorosVault.Integrations.Cryptography;
using EphorosVault.Presentation;
using System;
using System.IO;
using BusinessDI = EphorosVault.Business.DependencyInjection;
using DataDI = EphorosVault.Data.DependencyInjection;
using IntegrationsDI = EphorosVault.Integrations.DependencyInjection;
using PresentationDI = EphorosVault.Presentation.Desktop.DependencyInjection;

namespace EphorosVault.DesktopApp;

internal static class DependencyInjection
{
    internal static IServiceCollection BuildServiceCollection()
    {
        string appDirectoryPath = GetApplicationDataDirectory();

        IServiceCollection services = new ServiceCollectionPNP();

        services.AddSingleton<ILogger>(new LoggerPNP(LogLevel.Debug, new FileSinkPNP(Path.Combine(appDirectoryPath, "logs.txt"))));
        services.AddScoped<IFileSystemAccess, FileSystemAccess>();

        DpapiVaultKeyStore vaultKeyStore = new(Path.Combine(appDirectoryPath, "EphorosVault.key"));
        vaultKeyStore.EnsureCreated();
        services.AddSingleton<IVaultKeyStore>(vaultKeyStore);

        VaultDatabase vaultDatabase = new(Path.Combine(appDirectoryPath, "EphorosVault.sdf"));
        vaultDatabase.Initialize();
        services.AddSingleton(vaultDatabase);

        services = BusinessDI.RegisterInternalBusinessServices(services);
        services = DataDI.RegisterInternalDataServices(services);
        services = IntegrationsDI.RegisterInternalIntegrationsServices(services);
        services = PresentationDI.RegisterInternalPresentationsServices(services);

        return services;
    }

    private static string GetApplicationDataDirectory()
    {
        FileSystemAccess fileSystemAccess = new(new LoggerPNP());
        ProcessResult<string> appDirectoryPathResult = fileSystemAccess.GetAppDirectoryPath();

        if (appDirectoryPathResult.IsSuccessful)
        {
            return appDirectoryPathResult.Value;
        }

        throw new InvalidOperationException("Failed to get or create application data directory.", appDirectoryPathResult.Error);
    }
}
