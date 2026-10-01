using DotNetFrameworkToolkit.Modules.DependencyInjection;
using Microsoft.Practices.Unity.Utility;

namespace EphorosVault.Integrations
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Register internal integrations-tier services.
        /// </summary>
        public static IServiceCollection RegisterInternalIntegrationsServices(IServiceCollection services)
        {
            Guard.ArgumentNotNull(services, nameof(services));

            return services;
        }
    }
}
