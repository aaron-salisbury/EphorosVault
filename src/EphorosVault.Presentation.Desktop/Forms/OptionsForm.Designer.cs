namespace EphorosVault.Presentation.Desktop.Forms;

partial class OptionsForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button CancelButtonControl;
    private System.Windows.Forms.GroupBox GenerationGroupBox;
    private System.Windows.Forms.Label LengthLabel;
    private System.Windows.Forms.NumericUpDown LengthInput;
    private System.Windows.Forms.CheckBox LowercaseCheckBox;
    private System.Windows.Forms.CheckBox NumbersCheckBox;
    private System.Windows.Forms.Button OkButton;
    private System.Windows.Forms.CheckBox SpecialCheckBox;
    private System.Windows.Forms.CheckBox UppercaseCheckBox;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.GenerationGroupBox = new System.Windows.Forms.GroupBox();
            this.LengthLabel = new System.Windows.Forms.Label();
            this.LengthInput = new System.Windows.Forms.NumericUpDown();
            this.UppercaseCheckBox = new System.Windows.Forms.CheckBox();
            this.LowercaseCheckBox = new System.Windows.Forms.CheckBox();
            this.NumbersCheckBox = new System.Windows.Forms.CheckBox();
            this.SpecialCheckBox = new System.Windows.Forms.CheckBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButtonControl = new System.Windows.Forms.Button();
            this.GenerationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LengthInput)).BeginInit();
            this.SuspendLayout();
            // 
            // GenerationGroupBox
            // 
            this.GenerationGroupBox.Controls.Add(this.LengthLabel);
            this.GenerationGroupBox.Controls.Add(this.LengthInput);
            this.GenerationGroupBox.Controls.Add(this.UppercaseCheckBox);
            this.GenerationGroupBox.Controls.Add(this.LowercaseCheckBox);
            this.GenerationGroupBox.Controls.Add(this.NumbersCheckBox);
            this.GenerationGroupBox.Controls.Add(this.SpecialCheckBox);
            this.GenerationGroupBox.Location = new System.Drawing.Point(12, 12);
            this.GenerationGroupBox.Name = "GenerationGroupBox";
            this.GenerationGroupBox.Size = new System.Drawing.Size(336, 180);
            this.GenerationGroupBox.TabIndex = 0;
            this.GenerationGroupBox.TabStop = false;
            this.GenerationGroupBox.Text = "Password Generation";
            // 
            // LengthLabel
            // 
            this.LengthLabel.Location = new System.Drawing.Point(16, 29);
            this.LengthLabel.Name = "LengthLabel";
            this.LengthLabel.Size = new System.Drawing.Size(80, 20);
            this.LengthLabel.TabIndex = 0;
            this.LengthLabel.Text = "Length:";
            // 
            // LengthInput
            // 
            this.LengthInput.Location = new System.Drawing.Point(105, 25);
            this.LengthInput.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.LengthInput.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.LengthInput.Name = "LengthInput";
            this.LengthInput.Size = new System.Drawing.Size(70, 26);
            this.LengthInput.TabIndex = 1;
            this.LengthInput.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // UppercaseCheckBox
            // 
            this.UppercaseCheckBox.Location = new System.Drawing.Point(16, 58);
            this.UppercaseCheckBox.Name = "UppercaseCheckBox";
            this.UppercaseCheckBox.Size = new System.Drawing.Size(250, 20);
            this.UppercaseCheckBox.TabIndex = 2;
            this.UppercaseCheckBox.Text = "Require uppercase letters";
            // 
            // LowercaseCheckBox
            // 
            this.LowercaseCheckBox.Location = new System.Drawing.Point(16, 84);
            this.LowercaseCheckBox.Name = "LowercaseCheckBox";
            this.LowercaseCheckBox.Size = new System.Drawing.Size(250, 20);
            this.LowercaseCheckBox.TabIndex = 3;
            this.LowercaseCheckBox.Text = "Require lowercase letters";
            // 
            // NumbersCheckBox
            // 
            this.NumbersCheckBox.Location = new System.Drawing.Point(16, 110);
            this.NumbersCheckBox.Name = "NumbersCheckBox";
            this.NumbersCheckBox.Size = new System.Drawing.Size(250, 20);
            this.NumbersCheckBox.TabIndex = 4;
            this.NumbersCheckBox.Text = "Require numbers";
            // 
            // SpecialCheckBox
            // 
            this.SpecialCheckBox.Location = new System.Drawing.Point(16, 136);
            this.SpecialCheckBox.Name = "SpecialCheckBox";
            this.SpecialCheckBox.Size = new System.Drawing.Size(250, 20);
            this.SpecialCheckBox.TabIndex = 5;
            this.SpecialCheckBox.Text = "Require special characters";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(192, 207);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButtonControl
            // 
            this.CancelButtonControl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButtonControl.Location = new System.Drawing.Point(273, 207);
            this.CancelButtonControl.Name = "CancelButtonControl";
            this.CancelButtonControl.Size = new System.Drawing.Size(75, 23);
            this.CancelButtonControl.TabIndex = 2;
            this.CancelButtonControl.Text = "Cancel";
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.OkButton;
            this.CancelButton = this.CancelButtonControl;
            this.ClientSize = new System.Drawing.Size(360, 245);
            this.Controls.Add(this.GenerationGroupBox);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.CancelButtonControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.GenerationGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LengthInput)).EndInit();
            this.ResumeLayout(false);

    }
}
