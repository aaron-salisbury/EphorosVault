using DotNetFrameworkToolkit.Modules.DependencyInjection;
using EphorosVault.Presentation.Desktop;
using EphorosVault.Presentation.Desktop.Forms;
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

            using (LoginForm loginForm = Ioc.Default.GetRequiredService<LoginForm>())
            {
                if (loginForm.ShowDialog() != DialogResult.OK || !loginForm.IsAuthenticated)
                {
                    return;
                }
            }

            Application.Run(Ioc.Default.GetRequiredService<ShellForm>());
        }
        finally
        {
            Ioc.Default?.Dispose();
        }
    }
}
