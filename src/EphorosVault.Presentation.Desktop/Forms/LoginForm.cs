using EphorosVault.Business.Modules.Access;
using System;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms;

public partial class LoginForm : Form
{
    private readonly AccessService _accessService;

    public LoginForm(AccessService accessService)
    {
        _accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));

        InitializeComponent();
        ConfigureMode();
    }

    public bool IsAuthenticated
    {
        get; private set;
    }

    private void ConfigureMode()
    {
        bool requiresSetup = _accessService.RequiresSetup;

        Text = requiresSetup ? "Create Ephoros Vault Master Password" : "Unlock Ephoros Vault";
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
        ErrorLabel.Text = string.Empty;

        try
        {
            if (_accessService.RequiresSetup)
            {
                if (!string.Equals(PasswordTextBox.Text, ConfirmPasswordTextBox.Text, StringComparison.Ordinal))
                {
                    ErrorLabel.Text = "The passwords do not match.";
                    ConfirmPasswordTextBox.Focus();
                    ConfirmPasswordTextBox.SelectAll();
                    return;
                }

                _accessService.CreateMasterPassword(PasswordTextBox.Text);
            }
            else if (!_accessService.Authenticate(PasswordTextBox.Text))
            {
                ErrorLabel.Text = "The master password is incorrect.";
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
            ErrorLabel.Text = ex.Message;
        }
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
