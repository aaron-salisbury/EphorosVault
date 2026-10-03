using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.FileSystem;
using DotNetFrameworkToolkit.Modules.Logging;
using BusinessDI = EphorosVault.Business.DependencyInjection;
using DataDI = EphorosVault.Data.DependencyInjection;
using IntegrationsDI = EphorosVault.Integrations.DependencyInjection;
using PresentationDI = EphorosVault.Presentation.Desktop.DependencyInjection;

namespace EphorosVault.DesktopApp
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection BuildServiceCollection()
        {
            IServiceCollection services = new ServiceCollectionPNP();

            InMemorySinkPNP inMemorySink = new();
            services.AddSingleton<ILogger>(new LoggerPNP(LogLevel.Debug, inMemorySink));
            services.AddSingleton(inMemorySink);

            services.AddScoped<IFileSystemAccess, FileSystemAccess>();

            services = BusinessDI.RegisterInternalBusinessServices(services);
            services = DataDI.RegisterInternalDataServices(services);
            services = IntegrationsDI.RegisterInternalIntegrationsServices(services);
            services = PresentationDI.RegisterInternalPresentationsServices(services);

            return services;
        }
    }
}
