using DotNetFrameworkToolkit.Modules.DependencyInjection;
using EphorosVault.Business.Modules.Access;
using EphorosVault.Business.Modules.Vault;
using EphorosVault.Integrations.Cryptography;
using EphorosVault.Integrations.Export;
using EphorosVault.Presentation.Desktop;
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

            Ioc.Default.GetRequiredService<IVaultRecoveryService>().EnsureInitialized();

            using ShellForm shell = new(
                Ioc.Default.GetRequiredService<VaultService>(),
                Ioc.Default.GetRequiredService<VaultFolderService>(),
                Ioc.Default.GetRequiredService<PasswordGenerator>(),
                Ioc.Default.GetRequiredService<KeePass2XmlExporter>(),
                Ioc.Default.GetRequiredService<BitwardenJsonExporter>(),
                Ioc.Default.GetRequiredService<IVaultRecoveryService>());

            using VaultApplicationContext applicationContext = new(
                shell,
                Ioc.Default.GetRequiredService<AccessService>());

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
