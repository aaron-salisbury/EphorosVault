namespace EphorosVault.Presentation.Desktop.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.HeadingLabel = new System.Windows.Forms.Label();
        this.InstructionLabel = new System.Windows.Forms.Label();
        this.PasswordLabel = new System.Windows.Forms.Label();
        this.PasswordTextBox = new System.Windows.Forms.TextBox();
        this.ConfirmPasswordLabel = new System.Windows.Forms.Label();
        this.ConfirmPasswordTextBox = new System.Windows.Forms.TextBox();
        this.ErrorLabel = new System.Windows.Forms.Label();
        this.SubmitButton = new System.Windows.Forms.Button();
        this.CancelLoginButton = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // HeadingLabel
        // 
        this.HeadingLabel.AutoSize = true;
        this.HeadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
        this.HeadingLabel.Location = new System.Drawing.Point(20, 18);
        this.HeadingLabel.Name = "HeadingLabel";
        this.HeadingLabel.Size = new System.Drawing.Size(114, 17);
        this.HeadingLabel.TabIndex = 0;
        this.HeadingLabel.Text = "Unlock Vault";
        // 
        // InstructionLabel
        // 
        this.InstructionLabel.Location = new System.Drawing.Point(20, 45);
        this.InstructionLabel.Name = "InstructionLabel";
        this.InstructionLabel.Size = new System.Drawing.Size(360, 34);
        this.InstructionLabel.TabIndex = 1;
        // 
        // PasswordLabel
        // 
        this.PasswordLabel.AutoSize = true;
        this.PasswordLabel.Location = new System.Drawing.Point(20, 88);
        this.PasswordLabel.Name = "PasswordLabel";
        this.PasswordLabel.Size = new System.Drawing.Size(92, 13);
        this.PasswordLabel.TabIndex = 2;
        this.PasswordLabel.Text = "Master Password:";
        // 
        // PasswordTextBox
        // 
        this.PasswordTextBox.Location = new System.Drawing.Point(145, 85);
        this.PasswordTextBox.Name = "PasswordTextBox";
        this.PasswordTextBox.PasswordChar = '*';
        this.PasswordTextBox.Size = new System.Drawing.Size(235, 20);
        this.PasswordTextBox.TabIndex = 3;
        // 
        // ConfirmPasswordLabel
        // 
        this.ConfirmPasswordLabel.AutoSize = true;
        this.ConfirmPasswordLabel.Location = new System.Drawing.Point(20, 118);
        this.ConfirmPasswordLabel.Name = "ConfirmPasswordLabel";
        this.ConfirmPasswordLabel.Size = new System.Drawing.Size(94, 13);
        this.ConfirmPasswordLabel.TabIndex = 4;
        this.ConfirmPasswordLabel.Text = "Confirm Password:";
        // 
        // ConfirmPasswordTextBox
        // 
        this.ConfirmPasswordTextBox.Location = new System.Drawing.Point(145, 115);
        this.ConfirmPasswordTextBox.Name = "ConfirmPasswordTextBox";
        this.ConfirmPasswordTextBox.PasswordChar = '*';
        this.ConfirmPasswordTextBox.Size = new System.Drawing.Size(235, 20);
        this.ConfirmPasswordTextBox.TabIndex = 5;
        // 
        // ErrorLabel
        // 
        this.ErrorLabel.Location = new System.Drawing.Point(20, 145);
        this.ErrorLabel.Name = "ErrorLabel";
        this.ErrorLabel.Size = new System.Drawing.Size(360, 30);
        this.ErrorLabel.TabIndex = 6;
        // 
        // SubmitButton
        // 
        this.SubmitButton.Location = new System.Drawing.Point(224, 181);
        this.SubmitButton.Name = "SubmitButton";
        this.SubmitButton.Size = new System.Drawing.Size(75, 23);
        this.SubmitButton.TabIndex = 7;
        this.SubmitButton.Text = "Unlock";
        this.SubmitButton.UseVisualStyleBackColor = true;
        this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
        // 
        // CancelLoginButton
        // 
        this.CancelLoginButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.CancelLoginButton.Location = new System.Drawing.Point(305, 181);
        this.CancelLoginButton.Name = "CancelLoginButton";
        this.CancelLoginButton.Size = new System.Drawing.Size(75, 23);
        this.CancelLoginButton.TabIndex = 8;
        this.CancelLoginButton.Text = "Cancel";
        this.CancelLoginButton.UseVisualStyleBackColor = true;
        this.CancelLoginButton.Click += new System.EventHandler(this.CancelButton_Click);
        // 
        // LoginForm
        // 
        this.AcceptButton = this.SubmitButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.CancelLoginButton;
        this.ClientSize = new System.Drawing.Size(402, 222);
        this.Controls.Add(this.CancelLoginButton);
        this.Controls.Add(this.SubmitButton);
        this.Controls.Add(this.ErrorLabel);
        this.Controls.Add(this.ConfirmPasswordTextBox);
        this.Controls.Add(this.ConfirmPasswordLabel);
        this.Controls.Add(this.PasswordTextBox);
        this.Controls.Add(this.PasswordLabel);
        this.Controls.Add(this.InstructionLabel);
        this.Controls.Add(this.HeadingLabel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "LoginForm";
        this.ShowInTaskbar = true;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Unlock EphorosVault";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label HeadingLabel;
    private System.Windows.Forms.Label InstructionLabel;
    private System.Windows.Forms.Label PasswordLabel;
    private System.Windows.Forms.TextBox PasswordTextBox;
    private System.Windows.Forms.Label ConfirmPasswordLabel;
    private System.Windows.Forms.TextBox ConfirmPasswordTextBox;
    private System.Windows.Forms.Label ErrorLabel;
    private System.Windows.Forms.Button SubmitButton;
    private System.Windows.Forms.Button CancelLoginButton;
}
