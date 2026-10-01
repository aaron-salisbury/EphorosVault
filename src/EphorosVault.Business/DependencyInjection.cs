using DotNetFrameworkToolkit.Modules.DependencyInjection;
using Microsoft.Practices.Unity.Utility;

namespace EphorosVault.Business
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Register internal business-tier services.
        /// </summary>
        public static IServiceCollection RegisterInternalBusinessServices(IServiceCollection services)
        {
            Guard.ArgumentNotNull(services, nameof(services));

            return services;
        }
    }
}
