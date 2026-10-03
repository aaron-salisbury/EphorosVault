using DotNetFrameworkToolkit.Modules.DependencyInjection;
using EphorosVault.Presentation.Desktop.Base.MVP;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Reflection;

namespace EphorosVault.Presentation.Desktop;

public static class DependencyInjection
{
    /// <summary>
    /// Register internal desktop Presentation-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalPresentationsServices(IServiceCollection services)
    {
        Guard.ArgumentNotNull(services, nameof(services));

        services.AddSingleton<Navigator, Navigator>();
        services.AddSingleton<ShellForm, ShellForm>();

        // Presenters.
        foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (assemblyType.Name.EndsWith("Presenter") && !assemblyType.Name.Equals("Presenter"))
            {
                services.AddScoped(assemblyType);
            }
        }

        return services;
    }
}
