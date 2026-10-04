namespace EphorosVault.Presentation.Desktop.Forms;

partial class VaultEntryForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button CancelButtonControl;
    private System.Windows.Forms.ComboBox FolderComboBox;
    private System.Windows.Forms.Label FolderLabel;
    private System.Windows.Forms.Button GenerateButton;
    private System.Windows.Forms.Label NameLabel;
    private System.Windows.Forms.TextBox NameTextBox;
    private System.Windows.Forms.Label NotesLabel;
    private System.Windows.Forms.TextBox NotesTextBox;
    private System.Windows.Forms.Label PasswordLabel;
    private System.Windows.Forms.TextBox PasswordTextBox;
    private System.Windows.Forms.Button SaveButton;
    private System.Windows.Forms.Label SaveStatusLabel;
    private System.Windows.Forms.Button ShowPasswordButton;
    private System.Windows.Forms.Label UrlLabel;
    private System.Windows.Forms.TextBox UrlTextBox;
    private System.Windows.Forms.Label UserNameLabel;
    private System.Windows.Forms.TextBox UserNameTextBox;

    protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

    private void InitializeComponent()
    {
        this.NameLabel = new System.Windows.Forms.Label(); this.NameTextBox = new System.Windows.Forms.TextBox();
        this.FolderLabel = new System.Windows.Forms.Label(); this.FolderComboBox = new System.Windows.Forms.ComboBox();
        this.UserNameLabel = new System.Windows.Forms.Label(); this.UserNameTextBox = new System.Windows.Forms.TextBox();
        this.PasswordLabel = new System.Windows.Forms.Label(); this.PasswordTextBox = new System.Windows.Forms.TextBox();
        this.ShowPasswordButton = new System.Windows.Forms.Button(); this.GenerateButton = new System.Windows.Forms.Button();
        this.UrlLabel = new System.Windows.Forms.Label(); this.UrlTextBox = new System.Windows.Forms.TextBox();
        this.NotesLabel = new System.Windows.Forms.Label(); this.NotesTextBox = new System.Windows.Forms.TextBox();
        this.SaveStatusLabel = new System.Windows.Forms.Label(); this.SaveButton = new System.Windows.Forms.Button(); this.CancelButtonControl = new System.Windows.Forms.Button();
        this.SuspendLayout();
        this.NameLabel.Location=new System.Drawing.Point(20,23); this.NameLabel.Size=new System.Drawing.Size(90,20); this.NameLabel.Text="Name:";
        this.NameTextBox.Location=new System.Drawing.Point(115,20); this.NameTextBox.Size=new System.Drawing.Size(315,20);
        this.FolderLabel.Location=new System.Drawing.Point(20,53); this.FolderLabel.Size=new System.Drawing.Size(90,20); this.FolderLabel.Text="Folder:";
        this.FolderComboBox.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList; this.FolderComboBox.Location=new System.Drawing.Point(115,50); this.FolderComboBox.Size=new System.Drawing.Size(315,21);
        this.UserNameLabel.Location=new System.Drawing.Point(20,83); this.UserNameLabel.Size=new System.Drawing.Size(90,20); this.UserNameLabel.Text="User name:";
        this.UserNameTextBox.Location=new System.Drawing.Point(115,80); this.UserNameTextBox.Size=new System.Drawing.Size(315,20);
        this.PasswordLabel.Location=new System.Drawing.Point(20,113); this.PasswordLabel.Size=new System.Drawing.Size(90,20); this.PasswordLabel.Text="Password:";
        this.PasswordTextBox.Location=new System.Drawing.Point(115,110); this.PasswordTextBox.PasswordChar='*'; this.PasswordTextBox.Size=new System.Drawing.Size(155,20);
        this.ShowPasswordButton.Location=new System.Drawing.Point(275,108); this.ShowPasswordButton.Size=new System.Drawing.Size(70,23); this.ShowPasswordButton.Text="Show"; this.ShowPasswordButton.Click+=new System.EventHandler(this.ShowPassword_Click);
        this.GenerateButton.Location=new System.Drawing.Point(350,108); this.GenerateButton.Size=new System.Drawing.Size(100,23); this.GenerateButton.Text="Generate..."; this.GenerateButton.Click+=new System.EventHandler(this.Generate_Click);
        this.UrlLabel.Location=new System.Drawing.Point(20,143); this.UrlLabel.Size=new System.Drawing.Size(90,20); this.UrlLabel.Text="URL:";
        this.UrlTextBox.Location=new System.Drawing.Point(115,140); this.UrlTextBox.Size=new System.Drawing.Size(315,20);
        this.NotesLabel.Location=new System.Drawing.Point(20,173); this.NotesLabel.Size=new System.Drawing.Size(90,20); this.NotesLabel.Text="Notes:";
        this.NotesTextBox.Location=new System.Drawing.Point(115,170); this.NotesTextBox.Multiline=true; this.NotesTextBox.Size=new System.Drawing.Size(315,80);
        this.SaveStatusLabel.Location=new System.Drawing.Point(20,297); this.SaveStatusLabel.Size=new System.Drawing.Size(180,20);
        this.SaveButton.Location=new System.Drawing.Point(294,292); this.SaveButton.Size=new System.Drawing.Size(75,23); this.SaveButton.Text="Save"; this.SaveButton.Click+=new System.EventHandler(this.SaveButton_Click);
        this.CancelButtonControl.DialogResult=System.Windows.Forms.DialogResult.Cancel; this.CancelButtonControl.Location=new System.Drawing.Point(375,292); this.CancelButtonControl.Size=new System.Drawing.Size(75,23); this.CancelButtonControl.Text="Cancel";
        this.AcceptButton=this.SaveButton; this.CancelButton=this.CancelButtonControl; this.ClientSize=new System.Drawing.Size(470,330);
        this.Controls.Add(this.NameLabel); this.Controls.Add(this.NameTextBox); this.Controls.Add(this.FolderLabel); this.Controls.Add(this.FolderComboBox); this.Controls.Add(this.UserNameLabel); this.Controls.Add(this.UserNameTextBox); this.Controls.Add(this.PasswordLabel); this.Controls.Add(this.PasswordTextBox); this.Controls.Add(this.ShowPasswordButton); this.Controls.Add(this.GenerateButton); this.Controls.Add(this.UrlLabel); this.Controls.Add(this.UrlTextBox); this.Controls.Add(this.NotesLabel); this.Controls.Add(this.NotesTextBox); this.Controls.Add(this.SaveStatusLabel); this.Controls.Add(this.SaveButton); this.Controls.Add(this.CancelButtonControl);
        this.FormBorderStyle=System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox=false; this.MinimizeBox=false; this.Name="VaultEntryForm"; this.StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;
        this.ResumeLayout(false); this.PerformLayout();
    }
}
