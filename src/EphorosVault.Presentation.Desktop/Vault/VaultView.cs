using EphorosVault.Business.Modules.Vault;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Vault;

public sealed class VaultView : UserControl, IVaultView
{
    private readonly ToolStripButton _copyPassword = new("Copy Password") { Enabled = false };
    private readonly ToolStripButton _copyUser = new("Copy User") { Enabled = false };
    private readonly ToolStripButton _delete = new("Delete") { Enabled = false };
    private readonly TextBox _detailName = CreateDetail();
    private readonly TextBox _detailNotes = CreateDetail();
    private readonly TextBox _detailPassword = CreateDetail();
    private readonly TextBox _detailUrl = CreateDetail();
    private readonly TextBox _detailUser = CreateDetail();
    private readonly ToolStripButton _edit = new("Edit") { Enabled = false };
    private readonly ListView _entries = new();
    private readonly ComboBox _folders = new();
    private readonly TextBox _search = new();
    private readonly ToolStripStatusLabel _status = new();

    public VaultView()
    {
        Dock = DockStyle.Fill;
        Padding = new Padding(8, 4, 8, 8);
        Build();
    }

    public event EventHandler CopyPasswordRequested;
    public event EventHandler CopyUserNameRequested;
    public event EventHandler DeleteEntryRequested;
    public event EventHandler EditEntryRequested;
    public event EventHandler FilterChanged;
    public event EventHandler NewEntryRequested;

    public Guid? SelectedFolderId => (_folders.SelectedItem as FolderItem)?.Id;
    public VaultEntry SelectedEntry => _entries.SelectedItems.Count == 0 ? null : (VaultEntry)_entries.SelectedItems[0].Tag;
    public string SearchText => _search.Text;

    public void SetFolders(IEnumerable<VaultFolder> folders)
    {
        _folders.Items.Clear();
        _folders.Items.Add(new FolderItem(null, "All Entries"));
        foreach (VaultFolder folder in folders) _folders.Items.Add(new FolderItem(folder.Id, folder.Name));
        _folders.SelectedIndex = 0;
    }

    public void SetEntries(IEnumerable<VaultEntry> entries)
    {
        _entries.Items.Clear();
        foreach (VaultEntry entry in entries)
        {
            ListViewItem item = new(entry.Name) { Tag = entry };
            _entries.Items.Add(item);
        }
        ClearDetails();
    }

    public void ShowEntry(VaultEntry entry)
    {
        _detailName.Text = entry.Name;
        _detailUser.Text = entry.UserName;
        _detailPassword.Text = entry.Password;
        _detailUrl.Text = entry.Url;
        _detailNotes.Text = entry.Notes;
        SetCommands(true);
    }

    public void ShowStatus(string message) => _status.Text = message;

    public void ClearSensitiveState()
    {
        _entries.Items.Clear();
        _search.Clear();
        ClearDetails();
        Clipboard.Clear();
    }

    private void Build()
    {
        ToolStrip tools = new() { Dock = DockStyle.Top };
        ToolStripButton create = new("New");
        create.Click += (s, e) => NewEntryRequested?.Invoke(this, EventArgs.Empty);
        _edit.Click += (s, e) => EditEntryRequested?.Invoke(this, EventArgs.Empty);
        _delete.Click += (s, e) => DeleteEntryRequested?.Invoke(this, EventArgs.Empty);
        _copyUser.Click += (s, e) => CopyUserNameRequested?.Invoke(this, EventArgs.Empty);
        _copyPassword.Click += (s, e) => CopyPasswordRequested?.Invoke(this, EventArgs.Empty);
        tools.Items.Add(create);
        tools.Items.Add(_edit);
        tools.Items.Add(_delete);
        tools.Items.Add(new ToolStripSeparator());
        tools.Items.Add(_copyUser);
        tools.Items.Add(_copyPassword);

        Panel filters = new() { Dock = DockStyle.Top, Height = 38, Padding = new Padding(0, 2, 0, 4) };
        filters.Controls.Add(new Label { Text = "Folder:", Left = 8, Top = 10, Width = 45 });
        _folders.SetBounds(55, 6, 170, 26);
        _folders.DropDownStyle = ComboBoxStyle.DropDownList;
        _folders.SelectedIndexChanged += (s, e) => FilterChanged?.Invoke(this, EventArgs.Empty);
        filters.Controls.Add(_folders);
        filters.Controls.Add(new Label { Text = "Search:", Left = 240, Top = 10, Width = 50 });
        _search.SetBounds(292, 6, 240, 26);
        _search.TextChanged += (s, e) => FilterChanged?.Invoke(this, EventArgs.Empty);
        filters.Controls.Add(_search);

        SplitContainer workspace = new() { Dock = DockStyle.Fill, SplitterDistance = 260, FixedPanel = FixedPanel.Panel1 };
        _entries.Dock = DockStyle.Fill;
        _entries.View = View.List;
        _entries.FullRowSelect = true;
        _entries.HideSelection = false;
        _entries.MultiSelect = false;
        _entries.SelectedIndexChanged += EntrySelected;
        _entries.DoubleClick += (s, e) => EditEntryRequested?.Invoke(this, EventArgs.Empty);
        workspace.Panel1.Controls.Add(_entries);

        Panel details = new() { Dock = DockStyle.Fill, Padding = new Padding(10) };
        AddDetail(details, "Name:", _detailName, 12);
        AddDetail(details, "User name:", _detailUser, 42);
        AddDetail(details, "Password:", _detailPassword, 72);
        _detailPassword.PasswordChar = '*';
        AddDetail(details, "URL:", _detailUrl, 102);
        AddDetail(details, "Notes:", _detailNotes, 132);
        _detailNotes.Multiline = true;
        _detailNotes.Height = 140;
        _detailNotes.ScrollBars = ScrollBars.Vertical;
        workspace.Panel2.Controls.Add(details);

        StatusStrip status = new() { SizingGrip = false };
        status.Items.Add(_status);

        Controls.Add(workspace);
        Controls.Add(filters);
        Controls.Add(tools);
        Controls.Add(status);
    }

    private static TextBox CreateDetail() => new() { ReadOnly = true };

    private static void AddDetail(Panel panel, string text, TextBox field, int top)
    {
        panel.Controls.Add(new Label { Text = text, Left = 10, Top = top + 3, Width = 75 });
        field.SetBounds(90, top, 430, field.Height);
        field.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panel.Controls.Add(field);
    }

    private void EntrySelected(object sender, EventArgs e)
    {
        VaultEntry entry = SelectedEntry;
        if (entry == null) ClearDetails();
        else ShowEntry(entry);
    }

    private void ClearDetails()
    {
        _detailName.Clear();
        _detailUser.Clear();
        _detailPassword.Clear();
        _detailUrl.Clear();
        _detailNotes.Clear();
        SetCommands(false);
    }

    private void SetCommands(bool enabled)
    {
        _edit.Enabled = enabled;
        _delete.Enabled = enabled;
        _copyUser.Enabled = enabled;
        _copyPassword.Enabled = enabled;
    }

    private sealed class FolderItem
    {
        internal FolderItem(Guid? id, string name) { Id = id; Name = name; }
        internal Guid? Id { get; }
        internal string Name { get; }
        public override string ToString() => Name;
    }
}
