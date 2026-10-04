namespace EphorosVault.Presentation.Desktop.Vault;

partial class VaultView
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.ToolStripButton _copyPassword;
    private System.Windows.Forms.ToolStripButton _copyUser;
    private System.Windows.Forms.ToolStripButton _delete;
    private System.Windows.Forms.TextBox _detailName;
    private System.Windows.Forms.TextBox _detailNotes;
    private System.Windows.Forms.TextBox _detailPassword;
    private System.Windows.Forms.TextBox _detailUrl;
    private System.Windows.Forms.TextBox _detailUser;
    private System.Windows.Forms.Label _nameLabel;
    private System.Windows.Forms.Label _userLabel;
    private System.Windows.Forms.Label _passwordLabel;
    private System.Windows.Forms.Label _urlLabel;
    private System.Windows.Forms.Label _notesLabel;
    private System.Windows.Forms.ToolStripButton _edit;
    private System.Windows.Forms.ListView _entries;
    private System.Windows.Forms.ComboBox _folders;
    private System.Windows.Forms.Button _folderActions;
    private System.Windows.Forms.ContextMenuStrip _folderMenu;
    private System.Windows.Forms.ToolStripMenuItem _renameFolder;
    private System.Windows.Forms.ToolStripMenuItem _deleteFolder;
    private System.Windows.Forms.TextBox _search;
    private System.Windows.Forms.ToolStripStatusLabel _status;

    protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.create = new System.Windows.Forms.ToolStripButton();
            this._edit = new System.Windows.Forms.ToolStripButton();
            this._delete = new System.Windows.Forms.ToolStripButton();
            this._copyUser = new System.Windows.Forms.ToolStripButton();
            this._copyPassword = new System.Windows.Forms.ToolStripButton();
            this.filters = new System.Windows.Forms.Panel();
            this.folderLabel = new System.Windows.Forms.Label();
            this._folders = new System.Windows.Forms.ComboBox();
            this._folderMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.folderSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._renameFolder = new System.Windows.Forms.ToolStripMenuItem();
            this._deleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this._folderActions = new System.Windows.Forms.Button();
            this.searchLabel = new System.Windows.Forms.Label();
            this._search = new System.Windows.Forms.TextBox();
            this.workspace = new System.Windows.Forms.SplitContainer();
            this._entries = new System.Windows.Forms.ListView();
            this.details = new System.Windows.Forms.Panel();
            this._nameLabel = new System.Windows.Forms.Label();
            this._detailName = new System.Windows.Forms.TextBox();
            this._userLabel = new System.Windows.Forms.Label();
            this._detailUser = new System.Windows.Forms.TextBox();
            this._passwordLabel = new System.Windows.Forms.Label();
            this._detailPassword = new System.Windows.Forms.TextBox();
            this._urlLabel = new System.Windows.Forms.Label();
            this._detailUrl = new System.Windows.Forms.TextBox();
            this._notesLabel = new System.Windows.Forms.Label();
            this._detailNotes = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this._status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tools.SuspendLayout();
            this.filters.SuspendLayout();
            this._folderMenu.SuspendLayout();
            this.workspace.Panel1.SuspendLayout();
            this.workspace.Panel2.SuspendLayout();
            this.workspace.SuspendLayout();
            this.details.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.create,
            this._edit,
            this._delete,
            this._copyUser,
            this._copyPassword});
            this.tools.Location = new System.Drawing.Point(8, 4);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(1743, 34);
            this.tools.TabIndex = 2;
            // 
            // create
            // 
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(51, 29);
            this.create.Text = "New";
            this.create.Click += new System.EventHandler(this.Create_Click);
            // 
            // _edit
            // 
            this._edit.Enabled = false;
            this._edit.Name = "_edit";
            this._edit.Size = new System.Drawing.Size(46, 29);
            this._edit.Text = "Edit";
            this._edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // _delete
            // 
            this._delete.Enabled = false;
            this._delete.Name = "_delete";
            this._delete.Size = new System.Drawing.Size(66, 29);
            this._delete.Text = "Delete";
            this._delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // _copyUser
            // 
            this._copyUser.Enabled = false;
            this._copyUser.Name = "_copyUser";
            this._copyUser.Size = new System.Drawing.Size(98, 29);
            this._copyUser.Text = "Copy User";
            this._copyUser.Click += new System.EventHandler(this.CopyUser_Click);
            // 
            // _copyPassword
            // 
            this._copyPassword.Enabled = false;
            this._copyPassword.Name = "_copyPassword";
            this._copyPassword.Size = new System.Drawing.Size(138, 29);
            this._copyPassword.Text = "Copy Password";
            this._copyPassword.Click += new System.EventHandler(this.CopyPassword_Click);
            // 
            // filters
            // 
            this.filters.Controls.Add(this.folderLabel);
            this.filters.Controls.Add(this._folders);
            this.filters.Controls.Add(this._folderActions);
            this.filters.Controls.Add(this.searchLabel);
            this.filters.Controls.Add(this._search);
            this.filters.Dock = System.Windows.Forms.DockStyle.Top;
            this.filters.Location = new System.Drawing.Point(8, 38);
            this.filters.Name = "filters";
            this.filters.Padding = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.filters.Size = new System.Drawing.Size(1743, 38);
            this.filters.TabIndex = 1;
            // 
            // folderLabel
            // 
            this.folderLabel.Location = new System.Drawing.Point(8, 10);
            this.folderLabel.Name = "folderLabel";
            this.folderLabel.Size = new System.Drawing.Size(45, 20);
            this.folderLabel.TabIndex = 0;
            this.folderLabel.Text = "Folder:";
            // 
            // _folders
            // 
            this._folders.ContextMenuStrip = this._folderMenu;
            this._folders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._folders.Location = new System.Drawing.Point(55, 6);
            this._folders.Name = "_folders";
            this._folders.Size = new System.Drawing.Size(170, 28);
            this._folders.TabIndex = 1;
            this._folders.SelectedIndexChanged += new System.EventHandler(this.Folders_SelectedIndexChanged);
            // 
            // _folderMenu
            // 
            this._folderMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._folderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolder,
            this.folderSeparator,
            this._renameFolder,
            this._deleteFolder});
            this._folderMenu.Name = "_folderMenu";
            this._folderMenu.Size = new System.Drawing.Size(215, 106);
            // 
            // newFolder
            // 
            this.newFolder.Name = "newFolder";
            this.newFolder.Size = new System.Drawing.Size(214, 32);
            this.newFolder.Text = "New Folder...";
            this.newFolder.Click += new System.EventHandler(this.NewFolder_Click);
            // 
            // folderSeparator
            // 
            this.folderSeparator.Name = "folderSeparator";
            this.folderSeparator.Size = new System.Drawing.Size(211, 6);
            // 
            // _renameFolder
            // 
            this._renameFolder.Name = "_renameFolder";
            this._renameFolder.Size = new System.Drawing.Size(214, 32);
            this._renameFolder.Text = "Rename Folder...";
            this._renameFolder.Click += new System.EventHandler(this.RenameFolder_Click);
            // 
            // _deleteFolder
            // 
            this._deleteFolder.Name = "_deleteFolder";
            this._deleteFolder.Size = new System.Drawing.Size(214, 32);
            this._deleteFolder.Text = "Delete Folder...";
            this._deleteFolder.Click += new System.EventHandler(this.DeleteFolder_Click);
            // 
            // _folderActions
            // 
            this._folderActions.Location = new System.Drawing.Point(229, 5);
            this._folderActions.Name = "_folderActions";
            this._folderActions.Size = new System.Drawing.Size(32, 23);
            this._folderActions.TabIndex = 2;
            this._folderActions.Text = "...";
            this._folderActions.Click += new System.EventHandler(this.FolderActions_Click);
            // 
            // searchLabel
            // 
            this.searchLabel.Location = new System.Drawing.Point(276, 10);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(50, 20);
            this.searchLabel.TabIndex = 3;
            this.searchLabel.Text = "Search:";
            // 
            // _search
            // 
            this._search.Location = new System.Drawing.Point(328, 6);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(240, 26);
            this._search.TabIndex = 4;
            this._search.TextChanged += new System.EventHandler(this.Search_TextChanged);
            // 
            // workspace
            // 
            this.workspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspace.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.workspace.Location = new System.Drawing.Point(8, 76);
            this.workspace.Name = "workspace";
            // 
            // workspace.Panel1
            // 
            this.workspace.Panel1.Controls.Add(this._entries);
            // 
            // workspace.Panel2
            // 
            this.workspace.Panel2.Controls.Add(this.details);
            this.workspace.Size = new System.Drawing.Size(1743, 725);
            this.workspace.SplitterDistance = 121;
            this.workspace.TabIndex = 0;
            // 
            // _entries
            // 
            this._entries.Dock = System.Windows.Forms.DockStyle.Fill;
            this._entries.FullRowSelect = true;
            this._entries.HideSelection = false;
            this._entries.Location = new System.Drawing.Point(0, 0);
            this._entries.MultiSelect = false;
            this._entries.Name = "_entries";
            this._entries.Size = new System.Drawing.Size(121, 725);
            this._entries.TabIndex = 0;
            this._entries.UseCompatibleStateImageBehavior = false;
            this._entries.View = System.Windows.Forms.View.List;
            this._entries.DoubleClick += new System.EventHandler(this.Entries_DoubleClick);
            // 
            // details
            // 
            this.details.Controls.Add(this._nameLabel);
            this.details.Controls.Add(this._detailName);
            this.details.Controls.Add(this._userLabel);
            this.details.Controls.Add(this._detailUser);
            this.details.Controls.Add(this._passwordLabel);
            this.details.Controls.Add(this._detailPassword);
            this.details.Controls.Add(this._urlLabel);
            this.details.Controls.Add(this._detailUrl);
            this.details.Controls.Add(this._notesLabel);
            this.details.Controls.Add(this._detailNotes);
            this.details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.details.Location = new System.Drawing.Point(0, 0);
            this.details.Name = "details";
            this.details.Padding = new System.Windows.Forms.Padding(10);
            this.details.Size = new System.Drawing.Size(1618, 725);
            this.details.TabIndex = 0;
            // 
            // _nameLabel
            // 
            this._nameLabel.Location = new System.Drawing.Point(10, 15);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(75, 20);
            this._nameLabel.TabIndex = 0;
            this._nameLabel.Text = "Name:";
            // 
            // _detailName
            // 
            this._detailName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._detailName.Location = new System.Drawing.Point(90, 12);
            this._detailName.Name = "_detailName";
            this._detailName.ReadOnly = true;
            this._detailName.Size = new System.Drawing.Size(1848, 26);
            this._detailName.TabIndex = 1;
            // 
            // _userLabel
            // 
            this._userLabel.Location = new System.Drawing.Point(10, 45);
            this._userLabel.Name = "_userLabel";
            this._userLabel.Size = new System.Drawing.Size(75, 20);
            this._userLabel.TabIndex = 2;
            this._userLabel.Text = "User name:";
            // 
            // _detailUser
            // 
            this._detailUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._detailUser.Location = new System.Drawing.Point(90, 42);
            this._detailUser.Name = "_detailUser";
            this._detailUser.ReadOnly = true;
            this._detailUser.Size = new System.Drawing.Size(1848, 26);
            this._detailUser.TabIndex = 3;
            // 
            // _passwordLabel
            // 
            this._passwordLabel.Location = new System.Drawing.Point(10, 75);
            this._passwordLabel.Name = "_passwordLabel";
            this._passwordLabel.Size = new System.Drawing.Size(75, 20);
            this._passwordLabel.TabIndex = 4;
            this._passwordLabel.Text = "Password:";
            // 
            // _detailPassword
            // 
            this._detailPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._detailPassword.Location = new System.Drawing.Point(90, 72);
            this._detailPassword.Name = "_detailPassword";
            this._detailPassword.PasswordChar = '*';
            this._detailPassword.ReadOnly = true;
            this._detailPassword.Size = new System.Drawing.Size(1848, 26);
            this._detailPassword.TabIndex = 5;
            // 
            // _urlLabel
            // 
            this._urlLabel.Location = new System.Drawing.Point(10, 105);
            this._urlLabel.Name = "_urlLabel";
            this._urlLabel.Size = new System.Drawing.Size(75, 20);
            this._urlLabel.TabIndex = 6;
            this._urlLabel.Text = "URL:";
            // 
            // _detailUrl
            // 
            this._detailUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._detailUrl.Location = new System.Drawing.Point(90, 102);
            this._detailUrl.Name = "_detailUrl";
            this._detailUrl.ReadOnly = true;
            this._detailUrl.Size = new System.Drawing.Size(1848, 26);
            this._detailUrl.TabIndex = 7;
            // 
            // _notesLabel
            // 
            this._notesLabel.Location = new System.Drawing.Point(10, 135);
            this._notesLabel.Name = "_notesLabel";
            this._notesLabel.Size = new System.Drawing.Size(75, 20);
            this._notesLabel.TabIndex = 8;
            this._notesLabel.Text = "Notes:";
            // 
            // _detailNotes
            // 
            this._detailNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._detailNotes.Location = new System.Drawing.Point(90, 132);
            this._detailNotes.Multiline = true;
            this._detailNotes.Name = "_detailNotes";
            this._detailNotes.ReadOnly = true;
            this._detailNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._detailNotes.Size = new System.Drawing.Size(1848, 140);
            this._detailNotes.TabIndex = 9;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._status});
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusStrip.Location = new System.Drawing.Point(8, 801);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1743, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 3;
            // 
            // _status
            // 
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(0, 15);
            // 
            // VaultView
            // 
            this.Controls.Add(this.workspace);
            this.Controls.Add(this.filters);
            this.Controls.Add(this.tools);
            this.Controls.Add(this.statusStrip);
            this.Name = "VaultView";
            this.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.Size = new System.Drawing.Size(1759, 831);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.filters.ResumeLayout(false);
            this.filters.PerformLayout();
            this._folderMenu.ResumeLayout(false);
            this.workspace.Panel1.ResumeLayout(false);
            this.workspace.Panel2.ResumeLayout(false);
            this.workspace.ResumeLayout(false);
            this.details.ResumeLayout(false);
            this.details.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private System.Windows.Forms.ToolStrip tools;
    private System.Windows.Forms.ToolStripButton create;
    private System.Windows.Forms.Panel filters;
    private System.Windows.Forms.Label folderLabel;
    private System.Windows.Forms.ToolStripMenuItem newFolder;
    private System.Windows.Forms.ToolStripSeparator folderSeparator;
    private System.Windows.Forms.Label searchLabel;
    private System.Windows.Forms.SplitContainer workspace;
    private System.Windows.Forms.Panel details;
    private System.Windows.Forms.StatusStrip statusStrip;
}
