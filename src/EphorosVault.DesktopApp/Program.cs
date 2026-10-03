using DotNetFrameworkToolkit.Modules.DependencyInjection;
using EphorosVault.Presentation.Desktop;
using System;
using System.Windows.Forms;

namespace EphorosVault.DesktopApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IServiceCollection services = DependencyInjection.BuildServiceCollection();
            IServiceProvider provider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(provider);

            Application.ApplicationExit += Application_ApplicationExit;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Ioc.Default.GetRequiredService<ShellForm>());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (Ioc.Default != null)
            {
                Ioc.Default.Dispose();
            }
        }
    }
}
