using EphorosVault.Business.Modules.Vault;
using EphorosVault.Presentation.Desktop.Forms;
using EphorosVault.Presentation.Desktop.Vault;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop;

public partial class ShellForm : Form
{
    private readonly IVaultExporter _bitwardenExporter;
    private readonly VaultFolderService _folderService;
    private readonly IVaultRecoveryService _recoveryService;
    private readonly IVaultExporter _keePassExporter;
    private readonly PasswordGenerator _passwordGenerator;
    private readonly VaultPresenter _vaultPresenter;
    private readonly VaultService _vaultService;
    private readonly VaultView _vaultView;

    public ShellForm(VaultService vaultService, VaultFolderService folderService, PasswordGenerator passwordGenerator, IVaultExporter keePassExporter, IVaultExporter bitwardenExporter, IVaultRecoveryService recoveryService)
    {
        Guard.ArgumentNotNull(vaultService, nameof(vaultService));
        Guard.ArgumentNotNull(folderService, nameof(folderService));
        Guard.ArgumentNotNull(passwordGenerator, nameof(passwordGenerator));
        Guard.ArgumentNotNull(keePassExporter, nameof(keePassExporter));
        Guard.ArgumentNotNull(bitwardenExporter, nameof(bitwardenExporter));
        Guard.ArgumentNotNull(recoveryService, nameof(recoveryService));

        _vaultService = vaultService;
        _folderService = folderService;
        _passwordGenerator = passwordGenerator;
        _keePassExporter = keePassExporter;
        _bitwardenExporter = bitwardenExporter;
        _recoveryService = recoveryService;

        InitializeComponent();
        Text = Properties.Settings.Default.ApplicationFriendlyName;

        _vaultView = new VaultView();
        MainContentPanel.Controls.Add(_vaultView);
        _vaultPresenter = new VaultPresenter(_vaultView, _vaultService, _folderService, _passwordGenerator);
    }

    public event EventHandler LockRequested;

    public void PrepareForUnlock()
    {
        _vaultPresenter.Refresh();
    }

    private void LockVaultMenuItem_Click(object sender, EventArgs e)
    {
        _vaultPresenter.ClearSensitiveState();
        LockRequested?.Invoke(this, EventArgs.Empty);
    }

    private void NewFolderMenuItem_Click(object sender, EventArgs e)
    {
        using FolderNameForm form = new();
        if (form.ShowDialog(this) == DialogResult.OK && form.FolderName.Length > 0)
        {
            _folderService.Save(new VaultFolder { Name = form.FolderName });
            _vaultPresenter.Refresh();
        }
    }

    private void PasswordGeneratorMenuItem_Click(object sender, EventArgs e)
    {
        string password = _passwordGenerator.Generate(
            Properties.Settings.Default.PasswordLength,
            Properties.Settings.Default.PasswordRequireUppercase,
            Properties.Settings.Default.PasswordRequireLowercase,
            Properties.Settings.Default.PasswordRequireNumbers,
            Properties.Settings.Default.PasswordRequireSpecialCharacters);
        Clipboard.SetText(password);
        MessageBox.Show(this, "A generated password has been copied to the clipboard.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void OptionsMenuItem_Click(object sender, EventArgs e)
    {
        using OptionsForm form = new();
        form.ShowDialog(this);
    }

    private void ExportKeePassMenuItem_Click(object sender, EventArgs e)
    {
        Export(_keePassExporter);
    }

    private void ExportBitwardenMenuItem_Click(object sender, EventArgs e)
    {
        Export(_bitwardenExporter);
    }

    private void Export(IVaultExporter exporter)
    {
        if (MessageBox.Show(this, exporter.FormatName + " exports contain passwords in plaintext. Continue?", "Ephoros Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
        {
            return;
        }

        using SaveFileDialog dialog = new()
        {
            Filter = exporter.FileFilter,
            DefaultExt = exporter.DefaultExtension,
            AddExtension = true
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            exporter.Export(dialog.FileName, _vaultService.GetEntries(), _folderService.GetFolders());
            MessageBox.Show(this, "Export completed. This file contains your passwords in plaintext. Keep it secure and permanently delete it when you no longer need it.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void ExportRecoveryKeyMenuItem_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(this, "The recovery key grants access to the encrypted vault database. Store it securely and separately from the database. Continue?", "Ephoros Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
        {
            return;
        }

        using SaveFileDialog dialog = new()
        {
            Filter = "Ephoros Vault recovery key (*.evkey)|*.evkey|All files (*.*)|*.*",
            DefaultExt = "evkey",
            AddExtension = true
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _recoveryService.Export(dialog.FileName);
            MessageBox.Show(this, "Recovery key exported.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void AboutMenuItem_Click(object sender, EventArgs e)
    {
        using AboutForm form = new();
        form.ShowDialog(this);
    }

    private void ExitMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
