using EphorosVault.Business.Modules.Vault;
using EphorosVault.Presentation.Desktop.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms;

public sealed class VaultEntryForm : Form
{
    private readonly ComboBox _folder = new();
    private readonly TextBox _name = new();
    private readonly TextBox _userName = new();
    private readonly TextBox _password = new();
    private readonly TextBox _url = new();
    private readonly TextBox _notes = new();
    private readonly VaultEntry _entry;
    private readonly PasswordGenerator _passwordGenerator;
    private readonly Button _showPassword = new();
    private readonly StandardErrorProvider _errors = new();
    private readonly Button _saveButton = new();
    private readonly Label _saveStatus = new();
    private readonly bool _isNewEntry;
    private bool _initializing;

    public VaultEntryForm(VaultEntry entry, IList<VaultFolder> folders, PasswordGenerator passwordGenerator)
    {
        _entry = entry ?? throw new ArgumentNullException(nameof(entry));
        _isNewEntry = entry.Id == Guid.Empty;
        _initializing = true;
        _passwordGenerator = passwordGenerator ?? throw new ArgumentNullException(nameof(passwordGenerator));
        _errors.ContainerControl = this;
        Text = entry.Id == Guid.Empty ? "New Entry - Ephoros Vault" : "Edit Entry - Ephoros Vault";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new System.Drawing.Size(470, 330);
        MaximizeBox = false;
        MinimizeBox = false;

        AddField("Name:", _name, 20);
        AddField("Folder:", _folder, 50);
        AddField("User name:", _userName, 80);
        AddField("Password:", _password, 110);
        _password.PasswordChar = '*';
        _password.Width = 155;
        _showPassword.Text = "Show";
        _showPassword.Left = 275;
        _showPassword.Top = 108;
        _showPassword.Width = 70;
        _showPassword.Click += ShowPassword_Click;
        Controls.Add(_showPassword);
        Button generate = new() { Text = "Generate...", Left = 350, Top = 108, Width = 100 };
        generate.Click += Generate_Click;
        Controls.Add(generate);
        AddField("URL:", _url, 140);
        AddField("Notes:", _notes, 170);
        _notes.Multiline = true;
        _notes.Height = 80;

        _folder.DropDownStyle = ComboBoxStyle.DropDownList;
        _folder.Items.Add(new FolderItem(null, "(None)"));
        foreach (VaultFolder folder in folders) _folder.Items.Add(new FolderItem(folder.Id, folder.Name));

        _name.Text = entry.Name;
        _userName.Text = entry.UserName;
        _password.Text = entry.Password;
        _url.Text = entry.Url;
        _notes.Text = entry.Notes;
        SelectFolder(entry.FolderId);

        _saveStatus.Left = 20;
        _saveStatus.Top = 297;
        _saveStatus.Width = 180;
        Controls.Add(_saveStatus);

        _saveButton.Text = _isNewEntry ? "Create" : "Save";
        _saveButton.Left = 294;
        _saveButton.Top = 292;
        _saveButton.Width = 75;
        _saveButton.Enabled = _isNewEntry;
        _saveButton.Click += SaveButton_Click;
        Button cancel = new() { Text = "Cancel", Left = 375, Top = 292, Width = 75, DialogResult = DialogResult.Cancel };
        Controls.Add(_saveButton);
        Controls.Add(cancel);
        AcceptButton = _saveButton;
        CancelButton = cancel;

        _name.TextChanged += FieldChanged;
        _folder.SelectedIndexChanged += FieldChanged;
        _userName.TextChanged += FieldChanged;
        _password.TextChanged += FieldChanged;
        _url.TextChanged += FieldChanged;
        _notes.TextChanged += FieldChanged;
        _initializing = false;
    }

    private void AddField(string label, Control control, int top)
    {
        Controls.Add(new Label { Text = label, Left = 20, Top = top + 3, Width = 90 });
        control.Left = 115;
        control.Top = top;
        control.Width = 315;
        Controls.Add(control);
    }

    private void SelectFolder(Guid? folderId)
    {
        for (int i = 0; i < _folder.Items.Count; i++)
        {
            FolderItem item = (FolderItem)_folder.Items[i];
            if (item.Id == folderId) { _folder.SelectedIndex = i; return; }
        }
        _folder.SelectedIndex = 0;
    }

    private void ShowPassword_Click(object sender, EventArgs e)
    {
        bool show = _password.PasswordChar == '*';
        _password.PasswordChar = show ? (char)0 : '*';
        _showPassword.Text = show ? "Hide" : "Show";
        _password.Focus();
    }

    private void Generate_Click(object sender, EventArgs e)
    {
        _password.Text = GeneratePassword();
        _password.SelectAll();
        _password.Focus();
    }

    private string GeneratePassword()
    {
        return _passwordGenerator.Generate(
            Properties.Settings.Default.PasswordLength,
            Properties.Settings.Default.PasswordRequireUppercase,
            Properties.Settings.Default.PasswordRequireLowercase,
            Properties.Settings.Default.PasswordRequireNumbers,
            Properties.Settings.Default.PasswordRequireSpecialCharacters);
    }

    private void FieldChanged(object sender, EventArgs e)
    {
        if (_initializing) return;
        _saveButton.Enabled = true;
        _saveStatus.Text = "Unsaved changes";
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        _errors.Clear();
        if (_name.Text.Trim().Length == 0)
        {
            _errors.UpdateError(_name, "A name is required.");
            _name.Focus();
            return;
        }

        FolderItem folder = (FolderItem)_folder.SelectedItem;
        _entry.FolderId = folder.Id;
        _entry.Name = _name.Text.Trim();
        _entry.UserName = _userName.Text;
        _entry.Password = _password.Text;
        _entry.Url = _url.Text;
        _entry.Notes = _notes.Text;
        _saveStatus.Text = _isNewEntry ? "Created" : "Saved";
        DialogResult = DialogResult.OK;
        Close();
    }

    private sealed class FolderItem
    {
        public FolderItem(Guid? id, string name) { Id = id; Name = name; }
        public Guid? Id { get; }
        public string Name { get; }
        public override string ToString() => Name;
    }
}
