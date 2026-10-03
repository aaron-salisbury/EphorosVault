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
        private readonly KeePassCsvExporter _keePassExporter;
        private readonly BitwardenCsvExporter _bitwardenExporter;
        private readonly IVaultKeyStore _keyStore;
        private readonly ListBox _folders = new();
        private readonly ListView _entries = new();
        private readonly TextBox _search = new();
        private readonly Label _detailName = new();
        private readonly Label _detailUser = new();
        private readonly Label _detailUrl = new();
        private readonly TextBox _detailNotes = new();
        private readonly List<VaultEntry> _loadedEntries = new();

        public ShellForm(VaultService vaultService, VaultFolderService folderService, PasswordGenerator passwordGenerator, KeePassCsvExporter keePassExporter, BitwardenCsvExporter bitwardenExporter, IVaultKeyStore keyStore)
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

            ToolStrip tools = new();
            ToolStripButton newEntry = new("New");
            ToolStripButton editEntry = new("Edit");
            ToolStripButton deleteEntry = new("Delete");
            ToolStripButton copyUser = new("Copy User");
            ToolStripButton copyPassword = new("Copy Password");
            newEntry.Click += NewEntry_Click; editEntry.Click += EditEntry_Click; deleteEntry.Click += DeleteEntry_Click;
            copyUser.Click += CopyUserName_Click; copyPassword.Click += CopyPassword_Click;
            tools.Items.Add(newEntry); tools.Items.Add(editEntry); tools.Items.Add(deleteEntry); tools.Items.Add(new ToolStripSeparator()); tools.Items.Add(copyUser); tools.Items.Add(copyPassword);
            tools.Dock = DockStyle.Top;

            SplitContainer outer = new() { Dock = DockStyle.Fill, SplitterDistance = 155 };
            _folders.Dock = DockStyle.Fill;
            _folders.SelectedIndexChanged += FilterChanged;
            outer.Panel1.Controls.Add(_folders);

            SplitContainer right = new() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 235 };
            Panel searchPanel = new() { Dock = DockStyle.Top, Height = 30 };
            searchPanel.Controls.Add(new Label { Text = "Search:", Left = 6, Top = 8, Width = 50 });
            _search.Left = 60; _search.Top = 5; _search.Width = 260; _search.TextChanged += FilterChanged; searchPanel.Controls.Add(_search);

            _entries.Dock = DockStyle.Fill; _entries.View = View.Details; _entries.FullRowSelect = true; _entries.HideSelection = false;
            _entries.Columns.Add("Name", 190); _entries.Columns.Add("User Name", 170); _entries.Columns.Add("URL", 230);
            _entries.SelectedIndexChanged += EntrySelected; _entries.DoubleClick += EditEntry_Click;
            Panel listPanel = new() { Dock = DockStyle.Fill }; listPanel.Controls.Add(_entries); listPanel.Controls.Add(searchPanel);
            right.Panel1.Controls.Add(listPanel);

            Panel details = new() { Dock = DockStyle.Fill, Padding = new Padding(8) };
            _detailName.SetBounds(8, 8, 600, 20); _detailName.Font = new System.Drawing.Font(_detailName.Font, System.Drawing.FontStyle.Bold);
            _detailUser.SetBounds(8, 34, 600, 20); _detailUrl.SetBounds(8, 58, 600, 20);
            _detailNotes.SetBounds(8, 84, 600, 90); _detailNotes.Multiline = true; _detailNotes.ReadOnly = true; _detailNotes.ScrollBars = ScrollBars.Vertical;
            details.Controls.Add(_detailName); details.Controls.Add(_detailUser); details.Controls.Add(_detailUrl); details.Controls.Add(_detailNotes);
            right.Panel2.Controls.Add(details);
            outer.Panel2.Controls.Add(right);

            MainContentPanel.Controls.Add(outer);
            MainContentPanel.Controls.Add(tools);
        }

        private void RefreshVault()
        {
            object selectedFolder = _folders.SelectedItem;
            _folders.Items.Clear();
            _folders.Items.Add(new FolderFilter(null, "All Entries", true));
            foreach (VaultFolder folder in _folderService.GetFolders()) _folders.Items.Add(new FolderFilter(folder.Id, folder.Name, false));
            _folders.SelectedIndex = 0;

            _loadedEntries.Clear();
            foreach (VaultEntry entry in _vaultService.GetEntries()) _loadedEntries.Add(entry);
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            _entries.Items.Clear();
            FolderFilter filter = _folders.SelectedItem as FolderFilter;
            string search = _search.Text.Trim();
            foreach (VaultEntry entry in _loadedEntries)
            {
                if (filter != null && !filter.All && entry.FolderId != filter.Id) continue;
                if (search.Length > 0 && entry.Name.IndexOf(search, System.StringComparison.OrdinalIgnoreCase) < 0 && entry.UserName.IndexOf(search, System.StringComparison.OrdinalIgnoreCase) < 0 && entry.Url.IndexOf(search, System.StringComparison.OrdinalIgnoreCase) < 0) continue;
                ListViewItem item = new(entry.Name); item.SubItems.Add(entry.UserName); item.SubItems.Add(entry.Url); item.Tag = entry; _entries.Items.Add(item);
            }
            ClearDetails();
        }

        private void FilterChanged(object sender, System.EventArgs e) => ApplyFilter();

        private void EntrySelected(object sender, System.EventArgs e)
        {
            if (_entries.SelectedItems.Count == 0) { ClearDetails(); return; }
            VaultEntry entry = (VaultEntry)_entries.SelectedItems[0].Tag;
            _detailName.Text = entry.Name; _detailUser.Text = "User name: " + entry.UserName; _detailUrl.Text = "URL: " + entry.Url; _detailNotes.Text = entry.Notes;
        }

        private void ClearDetails() { _detailName.Text = string.Empty; _detailUser.Text = string.Empty; _detailUrl.Text = string.Empty; _detailNotes.Text = string.Empty; }

        private VaultEntry SelectedEntry() => _entries.SelectedItems.Count == 0 ? null : (VaultEntry)_entries.SelectedItems[0].Tag;

        private void NewEntry_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = new();
            using (VaultEntryForm form = new(entry, _folderService.GetFolders()))
                if (form.ShowDialog(this) == DialogResult.OK) { _vaultService.Save(entry); RefreshVault(); }
        }

        private void EditEntry_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = SelectedEntry(); if (entry == null) return;
            using (VaultEntryForm form = new(entry, _folderService.GetFolders()))
                if (form.ShowDialog(this) == DialogResult.OK) { _vaultService.Save(entry); RefreshVault(); }
        }

        private void DeleteEntry_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = SelectedEntry(); if (entry == null) return;
            if (MessageBox.Show(this, "Delete '" + entry.Name + "'?", "Ephoros Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { _vaultService.Delete(entry.Id); RefreshVault(); }
        }

        private void CopyUserName_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = SelectedEntry();
            if (entry != null && entry.UserName.Length > 0) Clipboard.SetText(entry.UserName);
        }

        private void CopyPassword_Click(object sender, System.EventArgs e)
        {
            VaultEntry entry = SelectedEntry();
            if (entry != null && entry.Password.Length > 0) Clipboard.SetText(entry.Password);
        }

        private void PasswordGeneratorMenuItem_Click(object sender, System.EventArgs e)
        {
            string password = _passwordGenerator.Generate(16);
            Clipboard.SetText(password);
            MessageBox.Show(this, "A generated 16-character password has been copied to the clipboard.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportKeePassMenuItem_Click(object sender, System.EventArgs e) => Export(_keePassExporter);
        private void ExportBitwardenMenuItem_Click(object sender, System.EventArgs e) => Export(_bitwardenExporter);

        private void Export(IVaultExporter exporter)
        {
            if (MessageBox.Show(this, "CSV exports contain passwords in plaintext. Continue?", "Ephoros Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            using (SaveFileDialog dialog = new()) {
                dialog.Filter = exporter.FileFilter; dialog.DefaultExt = "csv"; dialog.AddExtension = true;
                if (dialog.ShowDialog(this) == DialogResult.OK) { exporter.Export(dialog.FileName, _vaultService.GetEntries()); MessageBox.Show(this, "Export completed.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }

        private void ExportRecoveryKeyMenuItem_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(this, "The recovery key grants access to the encrypted vault database. Store it securely and separately from the database. Continue?", "Ephoros Vault", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            using (SaveFileDialog dialog = new()) {
                dialog.Filter = "Ephoros Vault recovery key (*.evkey)|*.evkey|All files (*.*)|*.*"; dialog.DefaultExt = "evkey"; dialog.AddExtension = true;
                if (dialog.ShowDialog(this) == DialogResult.OK) { _keyStore.ExportRecoveryKey(dialog.FileName); MessageBox.Show(this, "Recovery key exported.", "Ephoros Vault", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }

        private void NewFolderMenuItem_Click(object sender, System.EventArgs e)
        {
            using (FolderNameForm form = new())
                if (form.ShowDialog(this) == DialogResult.OK && form.FolderName.Length > 0) { _folderService.Save(new VaultFolder { Name = form.FolderName }); RefreshVault(); }
        }

        private void LogMenuItem_Click(object sender, System.EventArgs e)
        {
        }

        private void AboutMenuItem_Click(object sender, System.EventArgs e) { using (AboutForm form = new()) form.ShowDialog(this); }
        private void ExitMenuItem_Click(object sender, System.EventArgs e) => Application.Exit();

        private sealed class FolderFilter
        {
            public FolderFilter(System.Guid? id, string name, bool all) { Id = id; Name = name; All = all; }
            public System.Guid? Id { get; } public string Name { get; } public bool All { get; }
            public override string ToString() => Name;
        }
    }
}
