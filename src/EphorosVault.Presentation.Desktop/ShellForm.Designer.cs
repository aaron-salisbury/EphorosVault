
namespace EphorosVault.Presentation.Desktop;

partial class ShellForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellForm));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LockVaultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportKeePassMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportBitwardenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportRecoveryKeyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportRecoveryKeyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasswordGeneratorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainContentPanel = new System.Windows.Forms.Panel();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MenuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ToolsMenuItem,
            this.HelpMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(1138, 33);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewFolderMenuItem,
            this.LockVaultMenuItem,
            this.ExportMenuItem,
            this.ExportRecoveryKeyMenuItem,
            this.ImportRecoveryKeyMenuItem,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(54, 29);
            this.FileMenuItem.Text = "&File";
            // 
            // NewFolderMenuItem
            // 
            this.NewFolderMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NewFolderMenuItem.Image")));
            this.NewFolderMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.NewFolderMenuItem.Name = "NewFolderMenuItem";
            this.NewFolderMenuItem.Size = new System.Drawing.Size(290, 34);
            this.NewFolderMenuItem.Text = "New &Folder...";
            this.NewFolderMenuItem.Click += new System.EventHandler(this.NewFolderMenuItem_Click);
            // 
            // LockVaultMenuItem
            // 
            this.LockVaultMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LockVaultMenuItem.Image")));
            this.LockVaultMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LockVaultMenuItem.Name = "LockVaultMenuItem";
            this.LockVaultMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.LockVaultMenuItem.Size = new System.Drawing.Size(290, 34);
            this.LockVaultMenuItem.Text = "&Lock Vault";
            this.LockVaultMenuItem.Click += new System.EventHandler(this.LockVaultMenuItem_Click);
            // 
            // ExportMenuItem
            // 
            this.ExportMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportKeePassMenuItem,
            this.ExportBitwardenMenuItem});
            this.ExportMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExportMenuItem.Image")));
            this.ExportMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ExportMenuItem.Name = "ExportMenuItem";
            this.ExportMenuItem.Size = new System.Drawing.Size(290, 34);
            this.ExportMenuItem.Text = "&Export";
            // 
            // ExportKeePassMenuItem
            // 
            this.ExportKeePassMenuItem.Name = "ExportKeePassMenuItem";
            this.ExportKeePassMenuItem.Size = new System.Drawing.Size(252, 34);
            this.ExportKeePassMenuItem.Text = "&KeePass 2 XML...";
            this.ExportKeePassMenuItem.Click += new System.EventHandler(this.ExportKeePassMenuItem_Click);
            // 
            // ExportBitwardenMenuItem
            // 
            this.ExportBitwardenMenuItem.Name = "ExportBitwardenMenuItem";
            this.ExportBitwardenMenuItem.Size = new System.Drawing.Size(252, 34);
            this.ExportBitwardenMenuItem.Text = "&Bitwarden JSON...";
            this.ExportBitwardenMenuItem.Click += new System.EventHandler(this.ExportBitwardenMenuItem_Click);
            // 
            // ExportRecoveryKeyMenuItem
            // 
            this.ExportRecoveryKeyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExportRecoveryKeyMenuItem.Image")));
            this.ExportRecoveryKeyMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ExportRecoveryKeyMenuItem.Name = "ExportRecoveryKeyMenuItem";
            this.ExportRecoveryKeyMenuItem.Size = new System.Drawing.Size(290, 34);
            this.ExportRecoveryKeyMenuItem.Text = "Export &Recovery Key...";
            this.ExportRecoveryKeyMenuItem.Click += new System.EventHandler(this.ExportRecoveryKeyMenuItem_Click);
            // 
            // ImportRecoveryKeyMenuItem
            // 
            this.ImportRecoveryKeyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ImportRecoveryKeyMenuItem.Image")));
            this.ImportRecoveryKeyMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ImportRecoveryKeyMenuItem.Name = "ImportRecoveryKeyMenuItem";
            this.ImportRecoveryKeyMenuItem.Size = new System.Drawing.Size(290, 34);
            this.ImportRecoveryKeyMenuItem.Text = "Import R&ecovery Key...";
            this.ImportRecoveryKeyMenuItem.Click += new System.EventHandler(this.ImportRecoveryKeyMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExitMenuItem.Image")));
            this.ExitMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(290, 34);
            this.ExitMenuItem.Text = "E&xit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // ToolsMenuItem
            // 
            this.ToolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PasswordGeneratorMenuItem,
            this.OptionsMenuItem});
            this.ToolsMenuItem.Name = "ToolsMenuItem";
            this.ToolsMenuItem.Size = new System.Drawing.Size(69, 29);
            this.ToolsMenuItem.Text = "&Tools";
            // 
            // PasswordGeneratorMenuItem
            // 
            this.PasswordGeneratorMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PasswordGeneratorMenuItem.Image")));
            this.PasswordGeneratorMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PasswordGeneratorMenuItem.Name = "PasswordGeneratorMenuItem";
            this.PasswordGeneratorMenuItem.Size = new System.Drawing.Size(272, 34);
            this.PasswordGeneratorMenuItem.Text = "&Password Generator";
            this.PasswordGeneratorMenuItem.Click += new System.EventHandler(this.PasswordGeneratorMenuItem_Click);
            // 
            // OptionsMenuItem
            // 
            this.OptionsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OptionsMenuItem.Image")));
            this.OptionsMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OptionsMenuItem.Name = "OptionsMenuItem";
            this.OptionsMenuItem.Size = new System.Drawing.Size(272, 34);
            this.OptionsMenuItem.Text = "&Options...";
            this.OptionsMenuItem.Click += new System.EventHandler(this.OptionsMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(65, 29);
            this.HelpMenuItem.Text = "&Help";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AboutMenuItem.Image")));
            this.AboutMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(164, 34);
            this.AboutMenuItem.Text = "&About";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // MainContentPanel
            // 
            this.MainContentPanel.AutoSize = true;
            this.MainContentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContentPanel.Location = new System.Drawing.Point(0, 33);
            this.MainContentPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainContentPanel.Name = "MainContentPanel";
            this.MainContentPanel.Size = new System.Drawing.Size(1138, 599);
            this.MainContentPanel.TabIndex = 1;
            // 
            // ShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 632);
            this.Controls.Add(this.MainContentPanel);
            this.Controls.Add(this.MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(536, 531);
            this.Name = "ShellForm";
            this.Text = "Form1";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip MenuStrip;
    private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
    private System.Windows.Forms.ToolStripMenuItem NewFolderMenuItem;
    private System.Windows.Forms.ToolStripMenuItem LockVaultMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExportMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExportKeePassMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExportBitwardenMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExportRecoveryKeyMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ImportRecoveryKeyMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ToolsMenuItem;
    private System.Windows.Forms.ToolStripMenuItem PasswordGeneratorMenuItem;
    private System.Windows.Forms.ToolStripMenuItem OptionsMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
    private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
    private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
    private System.Windows.Forms.Panel MainContentPanel;
}

