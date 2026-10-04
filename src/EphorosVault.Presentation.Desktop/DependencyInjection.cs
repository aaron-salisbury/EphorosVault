using DotNetFrameworkToolkit.Modules.DependencyInjection;
using Microsoft.Practices.Unity.Utility;

namespace EphorosVault.Presentation.Desktop;

public static class DependencyInjection
{
    /// <summary>
    /// Register internal desktop Presentation-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalPresentationsServices(IServiceCollection services)
    {
        Guard.ArgumentNotNull(services, nameof(services));

        return services;
    }
}
