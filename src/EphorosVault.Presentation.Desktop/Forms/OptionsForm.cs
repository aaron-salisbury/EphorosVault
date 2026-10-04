using EphorosVault.Presentation.Desktop.Base.Helpers;
using EphorosVault.Presentation.Desktop.Properties;
using System;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms;

public sealed class OptionsForm : Form
{
    private readonly NumericUpDown _length = new();
    private readonly CheckBox _uppercase = new();
    private readonly CheckBox _lowercase = new();
    private readonly CheckBox _numbers = new();
    private readonly CheckBox _special = new();
    private readonly StandardErrorProvider _errors = new();

    public OptionsForm()
    {
        _errors.ContainerControl = this;
        Text = $"Options - {Resources.ProductName}";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new System.Drawing.Size(360, 245);
        MaximizeBox = false;
        MinimizeBox = false;

        GroupBox group = new() { Text = "Password Generation", Left = 12, Top = 12, Width = 336, Height = 180 };
        group.Controls.Add(new Label { Text = "Length:", Left = 16, Top = 29, Width = 80 });
        _length.SetBounds(105, 25, 70, 20);
        _length.Minimum = 8;
        _length.Maximum = 128;
        _length.Value = Properties.Settings.Default.PasswordLength;
        group.Controls.Add(_length);

        ConfigureCheckBox(_uppercase, "Require uppercase letters", 58, Properties.Settings.Default.PasswordRequireUppercase);
        ConfigureCheckBox(_lowercase, "Require lowercase letters", 84, Properties.Settings.Default.PasswordRequireLowercase);
        ConfigureCheckBox(_numbers, "Require numbers", 110, Properties.Settings.Default.PasswordRequireNumbers);
        ConfigureCheckBox(_special, "Require special characters", 136, Properties.Settings.Default.PasswordRequireSpecialCharacters);
        group.Controls.Add(_uppercase);
        group.Controls.Add(_lowercase);
        group.Controls.Add(_numbers);
        group.Controls.Add(_special);
        Controls.Add(group);

        Button ok = new() { Text = "OK", Left = 192, Top = 207, Width = 75 };
        Button cancel = new() { Text = "Cancel", Left = 273, Top = 207, Width = 75, DialogResult = DialogResult.Cancel };
        ok.Click += Ok_Click;
        Controls.Add(ok);
        Controls.Add(cancel);
        AcceptButton = ok;
        CancelButton = cancel;
    }

    private static void ConfigureCheckBox(CheckBox checkBox, string text, int top, bool isChecked)
    {
        checkBox.Text = text;
        checkBox.Left = 16;
        checkBox.Top = top;
        checkBox.Width = 250;
        checkBox.Checked = isChecked;
    }

    private void Ok_Click(object sender, EventArgs e)
    {
        _errors.Clear();
        if (!_uppercase.Checked && !_lowercase.Checked && !_numbers.Checked && !_special.Checked)
        {
            _errors.UpdateError(_special, "Enable at least one character type.");
            return;
        }

        Properties.Settings.Default.PasswordLength = Decimal.ToInt32(_length.Value);
        Properties.Settings.Default.PasswordRequireUppercase = _uppercase.Checked;
        Properties.Settings.Default.PasswordRequireLowercase = _lowercase.Checked;
        Properties.Settings.Default.PasswordRequireNumbers = _numbers.Checked;
        Properties.Settings.Default.PasswordRequireSpecialCharacters = _special.Checked;
        Properties.Settings.Default.Save();
        DialogResult = DialogResult.OK;
        Close();
    }
}
