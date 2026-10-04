namespace EphorosVault.Presentation.Desktop.Forms;

partial class FolderNameForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button CancelButtonControl;
    private System.Windows.Forms.Label FolderNameLabel;
    private System.Windows.Forms.TextBox NameTextBox;
    private System.Windows.Forms.Button OkButton;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.FolderNameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButtonControl = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FolderNameLabel
            // 
            this.FolderNameLabel.Location = new System.Drawing.Point(15, 20);
            this.FolderNameLabel.Name = "FolderNameLabel";
            this.FolderNameLabel.Size = new System.Drawing.Size(80, 20);
            this.FolderNameLabel.TabIndex = 0;
            this.FolderNameLabel.Text = "Folder name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(100, 17);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(230, 26);
            this.NameTextBox.TabIndex = 1;
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(174, 62);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            // 
            // CancelButtonControl
            // 
            this.CancelButtonControl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButtonControl.Location = new System.Drawing.Point(255, 62);
            this.CancelButtonControl.Name = "CancelButtonControl";
            this.CancelButtonControl.Size = new System.Drawing.Size(75, 23);
            this.CancelButtonControl.TabIndex = 3;
            this.CancelButtonControl.Text = "Cancel";
            // 
            // FolderNameForm
            // 
            this.AcceptButton = this.OkButton;
            this.CancelButton = this.CancelButtonControl;
            this.ClientSize = new System.Drawing.Size(350, 105);
            this.Controls.Add(this.FolderNameLabel);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.CancelButtonControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FolderNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
