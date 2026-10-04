namespace EphorosVault.Presentation.Desktop.Forms;

partial class VaultEntryForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.ComboBox FolderComboBox;
    private System.Windows.Forms.TextBox NameTextBox;
    private System.Windows.Forms.TextBox UserNameTextBox;
    private System.Windows.Forms.TextBox PasswordTextBox;
    private System.Windows.Forms.TextBox UrlTextBox;
    private System.Windows.Forms.TextBox NotesTextBox;
    private System.Windows.Forms.Button ShowPasswordButton;
    private System.Windows.Forms.Button SaveButton;
    private System.Windows.Forms.Label SaveStatusLabel;

    protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

    private void InitializeComponent()
    {
        this.NameTextBox = new System.Windows.Forms.TextBox(); this.FolderComboBox = new System.Windows.Forms.ComboBox(); this.UserNameTextBox = new System.Windows.Forms.TextBox(); this.PasswordTextBox = new System.Windows.Forms.TextBox(); this.UrlTextBox = new System.Windows.Forms.TextBox(); this.NotesTextBox = new System.Windows.Forms.TextBox(); this.ShowPasswordButton = new System.Windows.Forms.Button(); this.SaveButton = new System.Windows.Forms.Button(); this.SaveStatusLabel = new System.Windows.Forms.Label();
        System.Windows.Forms.Button generateButton = new System.Windows.Forms.Button(); System.Windows.Forms.Button cancelButton = new System.Windows.Forms.Button();
        AddField("Name:", this.NameTextBox, 20); AddField("Folder:", this.FolderComboBox, 50); AddField("User name:", this.UserNameTextBox, 80); AddField("Password:", this.PasswordTextBox, 110); AddField("URL:", this.UrlTextBox, 140); AddField("Notes:", this.NotesTextBox, 170);
        this.FolderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.PasswordTextBox.PasswordChar = '*'; this.PasswordTextBox.Width = 155;
        this.ShowPasswordButton.Location = new System.Drawing.Point(275, 108); this.ShowPasswordButton.Size = new System.Drawing.Size(70, 23); this.ShowPasswordButton.Text = "Show"; this.ShowPasswordButton.Click += new System.EventHandler(this.ShowPassword_Click);
        generateButton.Location = new System.Drawing.Point(350, 108); generateButton.Size = new System.Drawing.Size(100, 23); generateButton.Text = "Generate..."; generateButton.Click += new System.EventHandler(this.Generate_Click);
        this.NotesTextBox.Multiline = true; this.NotesTextBox.Height = 80;
        this.SaveStatusLabel.Location = new System.Drawing.Point(20, 297); this.SaveStatusLabel.Size = new System.Drawing.Size(180, 20);
        this.SaveButton.Location = new System.Drawing.Point(294, 292); this.SaveButton.Size = new System.Drawing.Size(75, 23); this.SaveButton.Text = "Save"; this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
        cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel; cancelButton.Location = new System.Drawing.Point(375, 292); cancelButton.Size = new System.Drawing.Size(75, 23); cancelButton.Text = "Cancel";
        this.Controls.Add(this.ShowPasswordButton); this.Controls.Add(generateButton); this.Controls.Add(this.SaveStatusLabel); this.Controls.Add(this.SaveButton); this.Controls.Add(cancelButton);
        this.AcceptButton = this.SaveButton; this.CancelButton = cancelButton; this.ClientSize = new System.Drawing.Size(470, 330); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox = false; this.MinimizeBox = false; this.Name = "VaultEntryForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.ResumeLayout(false); this.PerformLayout();
    }

    private void AddField(string labelText, System.Windows.Forms.Control control, int top)
    {
        System.Windows.Forms.Label label = new System.Windows.Forms.Label(); label.Text = labelText; label.Location = new System.Drawing.Point(20, top + 3); label.Size = new System.Drawing.Size(90, 20);
        control.Location = new System.Drawing.Point(115, top); control.Width = 315; this.Controls.Add(label); this.Controls.Add(control);
    }
}
