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
    private System.Windows.Forms.ToolStripButton _edit;
    private System.Windows.Forms.ListView _entries;
    private System.Windows.Forms.ComboBox _folders;
    private System.Windows.Forms.ContextMenuStrip _folderMenu;
    private System.Windows.Forms.ToolStripMenuItem _renameFolder;
    private System.Windows.Forms.ToolStripMenuItem _deleteFolder;
    private System.Windows.Forms.TextBox _search;
    private System.Windows.Forms.ToolStripStatusLabel _status;

    protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.Windows.Forms.ToolStrip tools = new System.Windows.Forms.ToolStrip();
        System.Windows.Forms.ToolStripButton create = new System.Windows.Forms.ToolStripButton();
        System.Windows.Forms.Panel filters = new System.Windows.Forms.Panel();
        System.Windows.Forms.Label folderLabel = new System.Windows.Forms.Label();
        System.Windows.Forms.Button folderActions = new System.Windows.Forms.Button();
        System.Windows.Forms.Label searchLabel = new System.Windows.Forms.Label();
        System.Windows.Forms.SplitContainer workspace = new System.Windows.Forms.SplitContainer();
        System.Windows.Forms.Panel details = new System.Windows.Forms.Panel();
        System.Windows.Forms.StatusStrip statusStrip = new System.Windows.Forms.StatusStrip();
        this._copyPassword = new System.Windows.Forms.ToolStripButton(); this._copyUser = new System.Windows.Forms.ToolStripButton(); this._delete = new System.Windows.Forms.ToolStripButton(); this._edit = new System.Windows.Forms.ToolStripButton();
        this._detailName = new System.Windows.Forms.TextBox(); this._detailNotes = new System.Windows.Forms.TextBox(); this._detailPassword = new System.Windows.Forms.TextBox(); this._detailUrl = new System.Windows.Forms.TextBox(); this._detailUser = new System.Windows.Forms.TextBox();
        this._entries = new System.Windows.Forms.ListView(); this._folders = new System.Windows.Forms.ComboBox(); this._search = new System.Windows.Forms.TextBox(); this._status = new System.Windows.Forms.ToolStripStatusLabel();
        this._folderMenu = new System.Windows.Forms.ContextMenuStrip(this.components); this._renameFolder = new System.Windows.Forms.ToolStripMenuItem(); this._deleteFolder = new System.Windows.Forms.ToolStripMenuItem();
        System.Windows.Forms.ToolStripMenuItem newFolder = new System.Windows.Forms.ToolStripMenuItem(); System.Windows.Forms.ToolStripSeparator folderSeparator = new System.Windows.Forms.ToolStripSeparator();
        this.SuspendLayout();
        tools.Dock = System.Windows.Forms.DockStyle.Top; create.Text = "New"; create.Click += (s,e) => NewEntryRequested?.Invoke(this,System.EventArgs.Empty); this._edit.Text="Edit"; this._delete.Text="Delete"; this._copyUser.Text="Copy User"; this._copyPassword.Text="Copy Password";
        this._edit.Enabled=false; this._delete.Enabled=false; this._copyUser.Enabled=false; this._copyPassword.Enabled=false;
        this._edit.Click += (s,e)=>EditEntryRequested?.Invoke(this,System.EventArgs.Empty); this._delete.Click += (s,e)=>DeleteEntryRequested?.Invoke(this,System.EventArgs.Empty); this._copyUser.Click += (s,e)=>CopyUserNameRequested?.Invoke(this,System.EventArgs.Empty); this._copyPassword.Click += (s,e)=>CopyPasswordRequested?.Invoke(this,System.EventArgs.Empty);
        tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[]{create,this._edit,this._delete,new System.Windows.Forms.ToolStripSeparator(),this._copyUser,this._copyPassword});
        filters.Dock=System.Windows.Forms.DockStyle.Top; filters.Height=38; filters.Padding=new System.Windows.Forms.Padding(0,2,0,4);
        folderLabel.Text="Folder:"; folderLabel.SetBounds(8,10,45,20); this._folders.SetBounds(55,6,170,26); this._folders.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList; this._folders.ContextMenuStrip=this._folderMenu;
        this._folders.SelectedIndexChanged += (s,e)=>{UpdateFolderCommands();FilterChanged?.Invoke(this,System.EventArgs.Empty);};
        folderActions.Text="..."; folderActions.SetBounds(229,5,32,23); folderActions.Click += (s,e)=>this._folderMenu.Show(folderActions,0,folderActions.Height);
        searchLabel.Text="Search:"; searchLabel.SetBounds(276,10,50,20); this._search.SetBounds(328,6,240,26); this._search.TextChanged += (s,e)=>FilterChanged?.Invoke(this,System.EventArgs.Empty);
        filters.Controls.AddRange(new System.Windows.Forms.Control[]{folderLabel,this._folders,folderActions,searchLabel,this._search});
        newFolder.Text="New Folder..."; this._renameFolder.Text="Rename Folder..."; this._deleteFolder.Text="Delete Folder...";
        newFolder.Click += (s,e)=>NewFolderRequested?.Invoke(this,System.EventArgs.Empty); this._renameFolder.Click += (s,e)=>RenameFolderRequested?.Invoke(this,System.EventArgs.Empty); this._deleteFolder.Click += (s,e)=>DeleteFolderRequested?.Invoke(this,System.EventArgs.Empty);
        this._folderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[]{newFolder,folderSeparator,this._renameFolder,this._deleteFolder});
        workspace.Dock=System.Windows.Forms.DockStyle.Fill; workspace.SplitterDistance=260; workspace.FixedPanel=System.Windows.Forms.FixedPanel.Panel1;
        this._entries.Dock=System.Windows.Forms.DockStyle.Fill; this._entries.View=System.Windows.Forms.View.List; this._entries.FullRowSelect=true; this._entries.HideSelection=false; this._entries.MultiSelect=false; this._entries.SelectedIndexChanged += this.EntrySelected; this._entries.DoubleClick += (s,e)=>EditEntryRequested?.Invoke(this,System.EventArgs.Empty); workspace.Panel1.Controls.Add(this._entries);
        details.Dock=System.Windows.Forms.DockStyle.Fill; details.Padding=new System.Windows.Forms.Padding(10);
        AddDetail(details,"Name:",this._detailName,12); AddDetail(details,"User name:",this._detailUser,42); AddDetail(details,"Password:",this._detailPassword,72); AddDetail(details,"URL:",this._detailUrl,102); AddDetail(details,"Notes:",this._detailNotes,132);
        this._detailPassword.PasswordChar='*'; this._detailNotes.Multiline=true; this._detailNotes.Height=140; this._detailNotes.ScrollBars=System.Windows.Forms.ScrollBars.Vertical; workspace.Panel2.Controls.Add(details);
        statusStrip.SizingGrip=false; statusStrip.Items.Add(this._status);
        this.Controls.Add(workspace); this.Controls.Add(filters); this.Controls.Add(tools); this.Controls.Add(statusStrip); this.Dock=System.Windows.Forms.DockStyle.Fill; this.Name="VaultView"; this.Padding=new System.Windows.Forms.Padding(8,4,8,8); this.ResumeLayout(false);
    }

    private static void AddDetail(System.Windows.Forms.Panel panel,string text,System.Windows.Forms.TextBox field,int top)
    {
        System.Windows.Forms.Label label=new System.Windows.Forms.Label(); label.Text=text; label.SetBounds(10,top+3,75,20); field.ReadOnly=true; field.SetBounds(90,top,430,field.Height); field.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right; panel.Controls.Add(label); panel.Controls.Add(field);
    }
}
