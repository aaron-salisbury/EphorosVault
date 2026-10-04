using EphorosVault.Business.Modules.Access;
using EphorosVault.Presentation.Desktop.Base.Helpers;
using EphorosVault.Presentation.Desktop.Properties;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms;

public partial class LoginForm : Form
{
    private readonly AccessService _accessService;
    private readonly StandardErrorProvider _errors = new();

    public LoginForm(AccessService accessService)
    {
        Guard.ArgumentNotNull(accessService, nameof(accessService));

        _accessService = accessService;
        InitializeComponent();
        _errors.ContainerControl = this;
        ConfigureMode();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        ResetForAuthentication();
    }

    private void ResetForAuthentication()
    {
        IsAuthenticated = false;
        PasswordTextBox.Clear();
        ConfirmPasswordTextBox.Clear();
        _errors.Clear();
        ConfigureMode();
        PasswordTextBox.Focus();
    }

    public bool IsAuthenticated
    {
        get; private set;
    }

    private void ConfigureMode()
    {
        bool requiresSetup = _accessService.RequiresSetup;

        Text = requiresSetup ? $"Create {Resources.ProductName} Master Password" : $"Unlock {Resources.ProductName}";
        HeadingLabel.Text = requiresSetup ? "Create Master Password" : "Unlock Vault";
        InstructionLabel.Text = requiresSetup
            ? "Choose the master password that will protect access to this vault."
            : "Enter your master password to unlock the vault.";
        ConfirmPasswordLabel.Visible = requiresSetup;
        ConfirmPasswordTextBox.Visible = requiresSetup;
        SubmitButton.Text = requiresSetup ? "Create Vault" : "Unlock";
    }

    private void SubmitButton_Click(object sender, EventArgs e)
    {
        _errors.Clear();

        try
        {
            if (_accessService.RequiresSetup)
            {
                if (!string.Equals(PasswordTextBox.Text, ConfirmPasswordTextBox.Text, StringComparison.Ordinal))
                {
                    _errors.UpdateError(ConfirmPasswordTextBox, "The passwords do not match.");
                    ConfirmPasswordTextBox.Focus();
                    ConfirmPasswordTextBox.SelectAll();
                    return;
                }

                _accessService.CreateMasterPassword(PasswordTextBox.Text);
            }
            else if (!_accessService.Authenticate(PasswordTextBox.Text))
            {
                _errors.UpdateError(PasswordTextBox, "The master password is incorrect.");
                PasswordTextBox.Focus();
                PasswordTextBox.SelectAll();
                return;
            }

            IsAuthenticated = true;
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (ArgumentException ex)
        {
            _errors.UpdateError(PasswordTextBox, ex.Message);
        }
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
