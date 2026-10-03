using EphorosVault.Business.Modules.Vault;
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

    public VaultEntryForm(VaultEntry entry, IList<VaultFolder> folders)
    {
        _entry = entry ?? throw new ArgumentNullException(nameof(entry));
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

        Button ok = new() { Text = "OK", Left = 294, Top = 292, Width = 75, DialogResult = DialogResult.OK };
        Button cancel = new() { Text = "Cancel", Left = 375, Top = 292, Width = 75, DialogResult = DialogResult.Cancel };
        ok.Click += Ok_Click;
        Controls.Add(ok);
        Controls.Add(cancel);
        AcceptButton = ok;
        CancelButton = cancel;
    }

    private void AddField(string label, Control control, int top)
    {
        Controls.Add(new Label { Text = label, Left = 20, Top = top + 3, Width = 90 });
        control.Left = 115;
        control.Top = top;
        control.Width = 335;
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

    private void Ok_Click(object sender, EventArgs e)
    {
        if (_name.Text.Trim().Length == 0)
        {
            MessageBox.Show(this, "A name is required.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            DialogResult = DialogResult.None;
            return;
        }

        FolderItem folder = (FolderItem)_folder.SelectedItem;
        _entry.FolderId = folder.Id;
        _entry.Name = _name.Text.Trim();
        _entry.UserName = _userName.Text;
        _entry.Password = _password.Text;
        _entry.Url = _url.Text;
        _entry.Notes = _notes.Text;
    }

    private sealed class FolderItem
    {
        public FolderItem(Guid? id, string name) { Id = id; Name = name; }
        public Guid? Id { get; }
        public string Name { get; }
        public override string ToString() => Name;
    }
}
