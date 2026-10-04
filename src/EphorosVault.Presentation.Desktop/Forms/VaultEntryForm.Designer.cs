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
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.FolderLabel = new System.Windows.Forms.Label();
            this.FolderComboBox = new System.Windows.Forms.ComboBox();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.ShowPasswordButton = new System.Windows.Forms.Button();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.UrlLabel = new System.Windows.Forms.Label();
            this.UrlTextBox = new System.Windows.Forms.TextBox();
            this.NotesLabel = new System.Windows.Forms.Label();
            this.NotesTextBox = new System.Windows.Forms.TextBox();
            this.SaveStatusLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButtonControl = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.Location = new System.Drawing.Point(20, 23);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(90, 20);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(115, 20);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(315, 26);
            this.NameTextBox.TabIndex = 1;
            // 
            // FolderLabel
            // 
            this.FolderLabel.Location = new System.Drawing.Point(20, 53);
            this.FolderLabel.Name = "FolderLabel";
            this.FolderLabel.Size = new System.Drawing.Size(90, 20);
            this.FolderLabel.TabIndex = 2;
            this.FolderLabel.Text = "Folder:";
            // 
            // FolderComboBox
            // 
            this.FolderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FolderComboBox.Location = new System.Drawing.Point(115, 50);
            this.FolderComboBox.Name = "FolderComboBox";
            this.FolderComboBox.Size = new System.Drawing.Size(315, 28);
            this.FolderComboBox.TabIndex = 3;
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.Location = new System.Drawing.Point(20, 83);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(90, 20);
            this.UserNameLabel.TabIndex = 4;
            this.UserNameLabel.Text = "User name:";
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(115, 80);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(315, 26);
            this.UserNameTextBox.TabIndex = 5;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.Location = new System.Drawing.Point(20, 113);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(90, 20);
            this.PasswordLabel.TabIndex = 6;
            this.PasswordLabel.Text = "Password:";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(115, 110);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(155, 26);
            this.PasswordTextBox.TabIndex = 7;
            // 
            // ShowPasswordButton
            // 
            this.ShowPasswordButton.Location = new System.Drawing.Point(275, 108);
            this.ShowPasswordButton.Name = "ShowPasswordButton";
            this.ShowPasswordButton.Size = new System.Drawing.Size(70, 23);
            this.ShowPasswordButton.TabIndex = 8;
            this.ShowPasswordButton.Text = "Show";
            this.ShowPasswordButton.Click += new System.EventHandler(this.ShowPassword_Click);
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(350, 108);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(100, 23);
            this.GenerateButton.TabIndex = 9;
            this.GenerateButton.Text = "Generate...";
            this.GenerateButton.Click += new System.EventHandler(this.Generate_Click);
            // 
            // UrlLabel
            // 
            this.UrlLabel.Location = new System.Drawing.Point(20, 143);
            this.UrlLabel.Name = "UrlLabel";
            this.UrlLabel.Size = new System.Drawing.Size(90, 20);
            this.UrlLabel.TabIndex = 10;
            this.UrlLabel.Text = "URL:";
            // 
            // UrlTextBox
            // 
            this.UrlTextBox.Location = new System.Drawing.Point(115, 140);
            this.UrlTextBox.Name = "UrlTextBox";
            this.UrlTextBox.Size = new System.Drawing.Size(315, 26);
            this.UrlTextBox.TabIndex = 11;
            // 
            // NotesLabel
            // 
            this.NotesLabel.Location = new System.Drawing.Point(20, 173);
            this.NotesLabel.Name = "NotesLabel";
            this.NotesLabel.Size = new System.Drawing.Size(90, 20);
            this.NotesLabel.TabIndex = 12;
            this.NotesLabel.Text = "Notes:";
            // 
            // NotesTextBox
            // 
            this.NotesTextBox.Location = new System.Drawing.Point(115, 170);
            this.NotesTextBox.Multiline = true;
            this.NotesTextBox.Name = "NotesTextBox";
            this.NotesTextBox.Size = new System.Drawing.Size(315, 80);
            this.NotesTextBox.TabIndex = 13;
            // 
            // SaveStatusLabel
            // 
            this.SaveStatusLabel.Location = new System.Drawing.Point(20, 297);
            this.SaveStatusLabel.Name = "SaveStatusLabel";
            this.SaveStatusLabel.Size = new System.Drawing.Size(180, 20);
            this.SaveStatusLabel.TabIndex = 14;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(294, 292);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 15;
            this.SaveButton.Text = "Save";
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButtonControl
            // 
            this.CancelButtonControl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButtonControl.Location = new System.Drawing.Point(375, 292);
            this.CancelButtonControl.Name = "CancelButtonControl";
            this.CancelButtonControl.Size = new System.Drawing.Size(75, 23);
            this.CancelButtonControl.TabIndex = 16;
            this.CancelButtonControl.Text = "Cancel";
            // 
            // VaultEntryForm
            // 
            this.AcceptButton = this.SaveButton;
            this.CancelButton = this.CancelButtonControl;
            this.ClientSize = new System.Drawing.Size(470, 330);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.FolderLabel);
            this.Controls.Add(this.FolderComboBox);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.ShowPasswordButton);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.UrlLabel);
            this.Controls.Add(this.UrlTextBox);
            this.Controls.Add(this.NotesLabel);
            this.Controls.Add(this.NotesTextBox);
            this.Controls.Add(this.SaveStatusLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CancelButtonControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VaultEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
