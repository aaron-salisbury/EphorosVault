using EphorosVault.Business.Modules.Access;
using EphorosVault.Presentation.Desktop;
using EphorosVault.Presentation.Desktop.Forms;
using System;
using System.Windows.Forms;

namespace EphorosVault.DesktopApp;

internal sealed class VaultApplicationContext : ApplicationContext
{
    private readonly ShellForm _shell;

    private readonly AccessService _accessService;

    internal VaultApplicationContext(ShellForm shell, AccessService accessService)
    {
        _shell = shell ?? throw new ArgumentNullException(nameof(shell));
        _accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        _shell.LockRequested += Shell_LockRequested;
        _shell.FormClosed += Shell_FormClosed;
    }

    internal bool Start()
    {
        if (!Authenticate())
        {
            return false;
        }

        _shell.PrepareForUnlock();
        MainForm = _shell;
        _shell.Show();
        return true;
    }

    private bool Authenticate()
    {
        using LoginForm login = new(_accessService);
        return login.ShowDialog() == DialogResult.OK && login.IsAuthenticated;
    }

    private void Shell_LockRequested(object sender, EventArgs e)
    {
        _shell.Hide();
        MainForm = null;

        if (!Authenticate())
        {
            ExitThread();
            return;
        }

        _shell.PrepareForUnlock();
        MainForm = _shell;
        _shell.Show();
        _shell.Activate();
    }

    private void Shell_FormClosed(object sender, FormClosedEventArgs e)
    {
        ExitThread();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _shell.LockRequested -= Shell_LockRequested;
            _shell.FormClosed -= Shell_FormClosed;
        }

        base.Dispose(disposing);
    }
}
