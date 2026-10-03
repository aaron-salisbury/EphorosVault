
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
        this.ExportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.ExportKeePassMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.ExportBitwardenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.ExportRecoveryKeyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.PasswordGeneratorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.LogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.MainContentPanel = new System.Windows.Forms.Panel();
        this.MenuStrip.SuspendLayout();
        this.SuspendLayout();
        // 
        // MenuStrip
        // 
        this.MenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.FileMenuItem,
        this.ToolsMenuItem,
        this.HelpMenuItem});
        this.MenuStrip.Location = new System.Drawing.Point(0, 0);
        this.MenuStrip.Name = "MenuStrip";
        this.MenuStrip.Size = new System.Drawing.Size(759, 24);
        this.MenuStrip.TabIndex = 0;
        this.MenuStrip.Text = "menuStrip1";
        // 
        // FileMenuItem
        // 
        this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.NewFolderMenuItem,
        this.ExportMenuItem,
        this.ExportRecoveryKeyMenuItem,
        this.ExitMenuItem});
        this.FileMenuItem.Name = "FileMenuItem";
        this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
        this.FileMenuItem.Text = "&File";
        // 
        // NewFolderMenuItem
        // 
        this.NewFolderMenuItem.Name = "NewFolderMenuItem";
        this.NewFolderMenuItem.Size = new System.Drawing.Size(180, 22);
        this.NewFolderMenuItem.Text = "New &Folder...";
        this.NewFolderMenuItem.Click += new System.EventHandler(this.NewFolderMenuItem_Click);
        // 
        // ExportMenuItem
        //
        this.ExportMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.ExportKeePassMenuItem, this.ExportBitwardenMenuItem });
        this.ExportMenuItem.Text = "&Export";
        this.ExportKeePassMenuItem.Text = "&KeePass CSV...";
        this.ExportKeePassMenuItem.Click += new System.EventHandler(this.ExportKeePassMenuItem_Click);
        this.ExportBitwardenMenuItem.Text = "&Bitwarden CSV...";
        this.ExportBitwardenMenuItem.Click += new System.EventHandler(this.ExportBitwardenMenuItem_Click);
        // ExportRecoveryKeyMenuItem
        //
        this.ExportRecoveryKeyMenuItem.Text = "Export &Recovery Key...";
        this.ExportRecoveryKeyMenuItem.Click += new System.EventHandler(this.ExportRecoveryKeyMenuItem_Click);
        // ToolsMenuItem
        //
        this.ToolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.PasswordGeneratorMenuItem });
        this.ToolsMenuItem.Text = "&Tools";
        this.PasswordGeneratorMenuItem.Text = "&Password Generator";
        this.PasswordGeneratorMenuItem.Click += new System.EventHandler(this.PasswordGeneratorMenuItem_Click);
        // ExitMenuItem
        // 
        this.ExitMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExitMenuItem.Image")));
        this.ExitMenuItem.Name = "ExitMenuItem";
        this.ExitMenuItem.Size = new System.Drawing.Size(180, 22);
        this.ExitMenuItem.Text = "E&xit";
        this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
        // 
        // HelpMenuItem
        // 
        this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.LogMenuItem,
        this.AboutMenuItem});
        this.HelpMenuItem.Name = "HelpMenuItem";
        this.HelpMenuItem.Size = new System.Drawing.Size(44, 20);
        this.HelpMenuItem.Text = "&Help";
        // 
        // LogMenuItem
        // 
        this.LogMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LogMenuItem.Image")));
        this.LogMenuItem.Name = "LogMenuItem";
        this.LogMenuItem.Size = new System.Drawing.Size(180, 22);
        this.LogMenuItem.Text = "&Log";
        this.LogMenuItem.Click += new System.EventHandler(this.LogMenuItem_Click);
        // 
        // AboutMenuItem
        // 
        this.AboutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AboutMenuItem.Image")));
        this.AboutMenuItem.Name = "AboutMenuItem";
        this.AboutMenuItem.Size = new System.Drawing.Size(180, 22);
        this.AboutMenuItem.Text = "&About";
        this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
        // 
        // MainContentPanel
        // 
        this.MainContentPanel.AutoSize = true;
        this.MainContentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.MainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.MainContentPanel.Location = new System.Drawing.Point(0, 24);
        this.MainContentPanel.Name = "MainContentPanel";
        this.MainContentPanel.Size = new System.Drawing.Size(759, 387);
        this.MainContentPanel.TabIndex = 1;
        // 
        // ShellForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(759, 411);
        this.Controls.Add(this.MainContentPanel);
        this.Controls.Add(this.MenuStrip);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MinimumSize = new System.Drawing.Size(365, 365);
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
    private System.Windows.Forms.ToolStripMenuItem ExportMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExportKeePassMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExportBitwardenMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExportRecoveryKeyMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ToolsMenuItem;
    private System.Windows.Forms.ToolStripMenuItem PasswordGeneratorMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
    private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
    private System.Windows.Forms.ToolStripMenuItem LogMenuItem;
    private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
    private System.Windows.Forms.Panel MainContentPanel;
}

