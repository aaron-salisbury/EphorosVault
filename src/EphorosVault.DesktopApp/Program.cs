using DotNetFrameworkToolkit.Modules.DependencyInjection;
using System;
using System.Windows.Forms;

namespace EphorosVault.DesktopApp;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        try
        {
            IServiceCollection services = DependencyInjection.BuildServiceCollection();
            IServiceProvider provider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(provider);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using VaultApplicationContext applicationContext = new();
            if (!applicationContext.Start())
            {
                return;
            }

            Application.Run(applicationContext);
        }
        finally
        {
            Ioc.Default?.Dispose();
        }
    }
}
