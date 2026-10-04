using EphorosVault.Business.Modules.Vault;
using EphorosVault.Integrations.Cryptography;
using EphorosVault.Integrations.Export;
using EphorosVault.Presentation.Desktop.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop
{
    public partial class ShellForm : Form
    {
        private readonly VaultService _vaultService;
        private readonly VaultFolderService _folderService;
        private readonly PasswordGenerator _passwordGenerator;
        private readonly KeePass2XmlExporter _keePassExporter;
        private readonly BitwardenJsonExporter _bitwardenExporter;
        private readonly IVaultKeyStore _keyStore;
        private readonly ComboBox _folders = new();
        private readonly ListView _entries = new();
        private readonly TextBox _search = new();
        private readonly TextBox _detailName = new();
        private readonly TextBox _detailUser = new();
        private readonly TextBox _detailPassword = new();
        private readonly TextBox _detailUrl = new();
        private readonly TextBox _detailNotes = new();
        private ToolStripButton _editEntryButton;
        private ToolStripButton _deleteEntryButton;
        private ToolStripButton _copyUserButton;
        private ToolStripButton _copyPasswordButton;
        private readonly List<VaultEntry> _loadedEntries = new();
        private readonly ToolStripStatusLabel _statusLabel = new();

        public ShellForm(VaultService vaultService, VaultFolderService folderService, PasswordGenerator passwordGenerator, KeePass2XmlExporter keePassExporter, BitwardenJsonExporter bitwardenExporter, IVaultKeyStore keyStore)
        {
            _vaultService = vaultService;
            _folderService = folderService;
            _passwordGenerator = passwordGenerator;
            _keePassExporter = keePassExporter;
            _bitwardenExporter = bitwardenExporter;
            _keyStore = keyStore;
            InitializeComponent();
            Text = Properties.Settings.Default.ApplicationFriendlyName;
            BuildVaultWorkspace();
            RefreshVault();
        }

        private void BuildVaultWorkspace()
        {
            MainContentPanel.Controls.Clear();
            MainContentPanel.Padding = new Padding(8, 4, 8, 8);

            ToolStrip tools = new() { Dock = DockStyle.Top };
            ToolStripButton newEntry = new("New");
            _editEntryButton = new ToolStripButton("Edit") { Enabled = false };
            _deleteEntryButton = new ToolStripButton("Delete") { Enabled = false };
            _copyUserButton = new ToolStripButton("Copy User") { Enabled = false };
            _copyPasswordButton = new ToolStripButton("Copy Password") { Enabled = false };
            newEntry.Click += NewEntry_Click;
            _editEntryButton.Click += EditEntry_Click;
            _deleteEntryButton.Click += DeleteEntry_Click;
            _copyUserButton.Click += CopyUserName_Click;
            _copyPasswordButton.Click += CopyPassword_Click;
            tools.Items.Add(newEntry);
            tools.Items.Add(_editEntryButton);
            tools.Items.Add(_deleteEntryButton);
            tools.Items.Add(new ToolStripSeparator());
            tools.Items.Add(_copyUserButton);
            tools.Items.Add(_copyPasswordButton);

            Panel filterPanel = new() { Dock = DockStyle.Top, Height = 38, Padding = new Padding(0, 2, 0, 4) };
            filterPanel.Controls.Add(new Label { Text = "Folder:", Left = 8, Top = 10, Width = 45 });
            _folders.Left = 55;
            _folders.Top = 6;
            _folders.Width = 170;
            _folders.DropDownStyle = ComboBoxStyle.DropDownList;
            _folders.SelectedIndexChanged += FilterChanged;
            filterPanel.Controls.Add(_folders);
            filterPanel.Controls.Add(new Label { Text = "Search:", Left = 240, Top = 10, Width = 50 });
            _search.Left = 292;
            _search.Top = 6;
            _search.Width = 240;
            _search.TextChanged += FilterChanged;
            filterPanel.Controls.Add(_search);

            SplitContainer workspace = new() { Dock = DockStyle.Fill, SplitterDistance = 260, FixedPanel = FixedPanel.Panel1 };
            _entries.Dock = DockStyle.Fill;
            _entries.View = View.List;
            _entries.FullRowSelect = true;
            _entries.HideSelection = false;
            _entries.MultiSelect = false;
            _entries.SelectedIndexChanged += EntrySelected;
            _entries.DoubleClick += EditEntry_Click;
            workspace.Panel1.Controls.Add(_entries);

            Panel details = new() { Dock = DockStyle.Fill, Padding = new Padding(10) };
            int labelWidth = 75;
            int fieldLeft = 90;
            int fieldWidth = 430;
            AddDetailField(details, "Name:", _detailName, 12, labelWidth, fieldLeft, fieldWidth);
            AddDetailField(details, "User name:", _detailUser, 42, labelWidth, fieldLeft, fieldWidth);
            AddDetailField(details, "Password:", _detailPassword, 72, labelWidth, fieldLeft, fieldWidth);
            _detailPassword.PasswordChar = '*';
            AddDetailField(details, "URL:", _detailUrl, 102, labelWidth, fieldLeft, fieldWidth);
            AddDetailField(details, "Notes:", _detailNotes, 132, labelWidth, fieldLeft, fieldWidth);
            _detailNotes.Multiline = true;
            _detailNotes.Height = 140;
            _detailNotes.ScrollBars = ScrollBars.Vertical;
            workspace.Panel2.Controls.Add(details);

            StatusStrip status = new();
            status.Items.Add(_statusLabel);
            status.SizingGrip = false;

            MainContentPanel.Controls.Add(workspace);
            MainContentPanel.Controls.Add(filterPanel);
            MainContentPanel.Controls.Add(tools);
            MainContentPanel.Controls.Add(status);
        }

        private static void AddDetailField(Panel panel, string labelText, TextBox field, int top, int labelWidth, int fieldLeft, int fieldWidth)
        {
            panel.Controls.Add(new Label { Text = labelText, Left = 10, Top = top + 3, Width = labelWidth });
            field.Left = fieldLeft;
            field.Top = top;
            field.Width = fieldWidth;
            field.ReadOnly = true;
            field.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel.Controls.Add(field);
        }

        private void RefreshVault()
        {
            object selectedFolder = _folders.SelectedItem;
            _folders.Items.Clear();
            _folders.Items.Add(new FolderFilter(null, "All Entries", true));
            foreach (VaultFolder folder in _folderService.GetFolders())
            {
                _folders.Items.Add(new FolderFilter(folder.Id, folder.Name, false));
            }

            _folders.SelectedIndex = 0;

            _loadedEntries.Clear();
            foreach (VaultEntry entry in _vaultService.GetEntries())
            {
                _loadedEntries.Add(entry);
            }

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            _entries.Items.Clear();
            FolderFilter filter = _folders.SelectedItem as FolderFilter;
            string search = _search.Text.Trim();
            foreach (VaultEntry entry in _loadedEntries)
            {
                if (filter != null && !filter.All && entry.FolderId != filter.Id)
                {
                    continue;
                }

                if (search.Length > 0 && entry.Name.IndexOf(search, System.StringComparison.OrdinalIgnoreCase) < 0 && entry.UserName.IndexOf(search, System.StringComparison.OrdinalIgnoreCase) < 0 && entry.Url.IndexOf(search, System.StringComparison.OrdinalIgnoreCase) < 0)
                {
                    continue;
                }

                ListViewItem item = new(entry.Name);
                item.Tag = entry;
                _entries.Items.Add(item);
            }
            ClearDetails();
        }

        private void FilterChanged(object sender, System.EventArgs e) => ApplyFilter();

        private void EntrySelected(object sender, System.EventArgs e)
        {
            if (_entries.SelectedItems.Count == 0)
            { ClearDetails(); return; }
            VaultEntry entry = (VaultEntry)_entries.SelectedItems[0].Tag;
            _detailName.Text = entry.Name;
            _detailUser.Text = entry.UserName;
            _detailPassword.Text = entry.Password;
            _detailUrl.Text = entry.Url;
            _detailNotes.Text = entry.Notes;
            SetEntryCommandsEnabled(true);
        }

        private void ClearDetails()
        {
            _detailName.Text = string.Empty;
            _detailUser.Text = string.Empty;
            _detailPassword.Text = string.Empty;
            _detailUrl.Text = string.Empty;
            _detailNotes.Text = string.Empty;
            SetEntryCommandsEnabled(false);
        }

        private void SetEntryCommandsEnabled(bool enabled)
        {
            _editEntryButton.Enabled = enabled;
            _deleteEntryButton.Enabled = enabled;
            _copyUserButton.Enabled = enabled;
            _copyPasswordButton.Enabled = enabled;
        }

        private VaultEntry SelectedEntry() => _entries.SelectedItems.Count == 0 ? null : (VaultEntry)_entries.SelectedItems[0].Tag;

        private void NewEntry_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = new();
            using (VaultEntryForm form = new(entry, _folderService.GetFolders(), _passwordGenerator))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                { _vaultService.Save(entry); RefreshVault(); ShowStatus("Credential created."); }
            }
        }

        private void EditEntry_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = SelectedEntry();
            if (entry == null)
            {
                return;
            }

            using (VaultEntryForm form = new(entry, _folderService.GetFolders(), _passwordGenerator))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                { _vaultService.Save(entry); RefreshVault(); ShowStatus("Changes saved."); }
            }
        }

        private void ShowStatus(string message)
        {
            _statusLabel.Text = message;
        }

        private void DeleteEntry_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = SelectedEntry();
            if (entry == null)
            {
                return;
            }

            if (MessageBox.Show(this, "Delete '" + entry.Name + "'?", "Ephoros Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { _vaultService.Delete(entry.Id); RefreshVault(); }
        }

        private void CopyUserName_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = SelectedEntry();
            if (entry != null && entry.UserName.Length > 0)
            {
                Clipboard.SetText(entry.UserName);
            }
        }

        private void CopyPassword_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = SelectedEntry();
            if (entry != null && entry.Password.Length > 0)
            {
                Clipboard.SetText(entry.Password);
            }
        }

        private void PasswordGeneratorMenuItem_Click(object sender, System.EventArgs e)
        {
            string password = GeneratePassword();
            Clipboard.SetText(password);
            MessageBox.Show(this, "A generated password has been copied to the clipboard.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void OptionsMenuItem_Click(object sender, System.EventArgs e)
        {
            using (OptionsForm form = new())
            {
                form.ShowDialog(this);
            }
        }

        private void ExportKeePassMenuItem_Click(object sender, System.EventArgs e) => Export(_keePassExporter);
        private void ExportBitwardenMenuItem_Click(object sender, System.EventArgs e) => Export(_bitwardenExporter);

        private void Export(IVaultExporter exporter)
        {
            if (MessageBox.Show(this, exporter.FormatName + " exports contain passwords in plaintext. Continue?", "Ephoros Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            using (SaveFileDialog dialog = new())
            {
                dialog.Filter = exporter.FileFilter;
                dialog.DefaultExt = exporter.DefaultExtension;
                dialog.AddExtension = true;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                { exporter.Export(dialog.FileName, _vaultService.GetEntries(), _folderService.GetFolders()); MessageBox.Show(this, "Export completed. This file contains your passwords in plaintext. Keep it secure and permanently delete it when you no longer need it.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        private void ExportRecoveryKeyMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(this, "The recovery key grants access to the encrypted vault database. Store it securely and separately from the database. Continue?", "Ephoros Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            using (SaveFileDialog dialog = new())
            {
                dialog.Filter = "Ephoros Vault recovery key (*.evkey)|*.evkey|All files (*.*)|*.*";
                dialog.DefaultExt = "evkey";
                dialog.AddExtension = true;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                { _keyStore.ExportRecoveryKey(dialog.FileName); MessageBox.Show(this, "Recovery key exported.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }

        private void NewFolderMenuItem_Click(object sender, System.EventArgs e)
        {
            using (FolderNameForm form = new())
            {
                if (form.ShowDialog(this) == DialogResult.OK && form.FolderName.Length > 0)
                { _folderService.Save(new VaultFolder { Name = form.FolderName }); RefreshVault(); }
            }
        }

        private void LogMenuItem_Click(object sender, System.EventArgs e)
        {
        }

        private void AboutMenuItem_Click(object sender, System.EventArgs e) { using (AboutForm form = new())
            {
                form.ShowDialog(this);
            }
        }
        private void ExitMenuItem_Click(object sender, System.EventArgs e) => Application.Exit();

        private sealed class FolderFilter
        {
            public FolderFilter(System.Guid? id, string name, bool all) { Id = id; Name = name; All = all; }
            public System.Guid? Id { get; }
            public string Name { get; }
            public bool All { get; }
            public override string ToString() => Name;
        }
    }
}
