using EphorosVault.Business.Modules.Access;
using EphorosVault.Business.Modules.Vault;
using EphorosVault.Presentation.Desktop.Base.Helpers;
using EphorosVault.Presentation.Desktop.Properties;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms;

public sealed partial class VaultEntryForm : Form
{
    private readonly VaultEntry _entry;
    private readonly PasswordGenerator _passwordGenerator;
    private readonly StandardErrorProvider _errors = new();
    private readonly bool _isNewEntry;
    private bool _initializing;

    public VaultEntryForm(VaultEntry entry, IList<VaultFolder> folders, PasswordGenerator passwordGenerator)
    {
        Guard.ArgumentNotNull(entry, nameof(entry));
        Guard.ArgumentNotNull(passwordGenerator, nameof(passwordGenerator));

        _entry = entry;
        _isNewEntry = entry.Id == Guid.Empty;
        _initializing = true;
        _passwordGenerator = passwordGenerator;
        InitializeComponent();
        _errors.ContainerControl = this;
        Text = entry.Id == Guid.Empty ? $"New Entry - {Resources.ProductName}" : $"Edit Entry - {Resources.ProductName}";
        SaveButton.Text = _isNewEntry ? "Create" : "Save";
        SaveButton.Enabled = _isNewEntry;

        FolderComboBox.Items.Add(new FolderItem(null, "(None)"));
        foreach (VaultFolder folder in folders)
        {
            FolderComboBox.Items.Add(new FolderItem(folder.Id, folder.Name));
        }

        NameTextBox.Text = entry.Name;
        UserNameTextBox.Text = entry.UserName;
        PasswordTextBox.Text = entry.Password;
        UrlTextBox.Text = entry.Url;
        NotesTextBox.Text = entry.Notes;
        SelectFolder(entry.FolderId);


        NameTextBox.TextChanged += FieldChanged;
        FolderComboBox.SelectedIndexChanged += FieldChanged;
        UserNameTextBox.TextChanged += FieldChanged;
        PasswordTextBox.TextChanged += FieldChanged;
        UrlTextBox.TextChanged += FieldChanged;
        NotesTextBox.TextChanged += FieldChanged;
        _initializing = false;
    }

    private void SelectFolder(Guid? folderId)
    {
        for (int i = 0; i < FolderComboBox.Items.Count; i++)
        {
            FolderItem item = (FolderItem)FolderComboBox.Items[i];
            if (item.Id == folderId)
            { FolderComboBox.SelectedIndex = i; return; }
        }
        FolderComboBox.SelectedIndex = 0;
    }

    private void ShowPassword_Click(object sender, EventArgs e)
    {
        bool show = PasswordTextBox.PasswordChar == '*';
        PasswordTextBox.PasswordChar = show ? (char)0 : '*';
        ShowPasswordButton.Text = show ? "Hide" : "Show";
        PasswordTextBox.Focus();
    }

    private void Generate_Click(object sender, EventArgs e)
    {
        PasswordTextBox.Text = GeneratePassword();
        PasswordTextBox.SelectAll();
        PasswordTextBox.Focus();
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
        if (_initializing)
        {
            return;
        }

        SaveButton.Enabled = true;
        SaveStatusLabel.Text = "Unsaved changes";
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        _errors.Clear();
        if (NameTextBox.Text.Trim().Length == 0)
        {
            _errors.UpdateError(NameTextBox, "A name is required.");
            NameTextBox.Focus();
            return;
        }

        FolderItem folder = (FolderItem)FolderComboBox.SelectedItem;
        _entry.FolderId = folder.Id;
        _entry.Name = NameTextBox.Text.Trim();
        _entry.UserName = UserNameTextBox.Text;
        _entry.Password = PasswordTextBox.Text;
        _entry.Url = UrlTextBox.Text;
        _entry.Notes = NotesTextBox.Text;
        SaveStatusLabel.Text = _isNewEntry ? "Created" : "Saved";
        DialogResult = DialogResult.OK;
        Close();
    }

    private sealed class FolderItem
    {
        public FolderItem(Guid? id, string name) { Id = id; Name = name; }
        public Guid? Id { get; }
        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
