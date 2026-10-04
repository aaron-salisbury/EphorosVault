using EphorosVault.Presentation.Desktop.Base.Helpers;
using EphorosVault.Presentation.Desktop.Properties;
using System;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms;

public sealed partial class OptionsForm : Form
{
    public bool SettingsChanged { get; private set; }
    private readonly StandardErrorProvider _errors = new();

    public OptionsForm()
    {
        InitializeComponent();
        _errors.ContainerControl = this;
        Text = "Options - " + Resources.ProductName;
        LengthInput.Value = Properties.Settings.Default.PasswordLength;
        UppercaseCheckBox.Checked = Properties.Settings.Default.PasswordRequireUppercase;
        LowercaseCheckBox.Checked = Properties.Settings.Default.PasswordRequireLowercase;
        NumbersCheckBox.Checked = Properties.Settings.Default.PasswordRequireNumbers;
        SpecialCheckBox.Checked = Properties.Settings.Default.PasswordRequireSpecialCharacters;
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        _errors.Clear();
        if (!UppercaseCheckBox.Checked && !LowercaseCheckBox.Checked && !NumbersCheckBox.Checked && !SpecialCheckBox.Checked)
        {
            _errors.UpdateError(SpecialCheckBox, "Enable at least one character type.");
            return;
        }

        int length = decimal.ToInt32(LengthInput.Value);
        SettingsChanged =
            length != Properties.Settings.Default.PasswordLength ||
            UppercaseCheckBox.Checked != Properties.Settings.Default.PasswordRequireUppercase ||
            LowercaseCheckBox.Checked != Properties.Settings.Default.PasswordRequireLowercase ||
            NumbersCheckBox.Checked != Properties.Settings.Default.PasswordRequireNumbers ||
            SpecialCheckBox.Checked != Properties.Settings.Default.PasswordRequireSpecialCharacters;

        if (SettingsChanged)
        {
            Properties.Settings.Default.PasswordLength = length;
            Properties.Settings.Default.PasswordRequireUppercase = UppercaseCheckBox.Checked;
            Properties.Settings.Default.PasswordRequireLowercase = LowercaseCheckBox.Checked;
            Properties.Settings.Default.PasswordRequireNumbers = NumbersCheckBox.Checked;
            Properties.Settings.Default.PasswordRequireSpecialCharacters = SpecialCheckBox.Checked;
            Properties.Settings.Default.Save();
        }
        DialogResult = DialogResult.OK;
        Close();
    }
}
