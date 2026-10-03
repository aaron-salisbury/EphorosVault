using DotNetFrameworkToolkit.Core;
using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.FileSystem;
using DotNetFrameworkToolkit.Modules.Logging;
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

        InMemorySinkPNP inMemorySink = new();
        services.AddSingleton<ILogger>(new LoggerPNP(LogLevel.Debug, inMemorySink, new FileSinkPNP(Path.Combine(appDirectoryPath, "logs.txt"))));
        services.AddSingleton(inMemorySink);

        services.AddScoped<IFileSystemAccess, FileSystemAccess>();

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
