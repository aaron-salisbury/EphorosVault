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
        this.GenerationGroupBox.Controls.Add(this.LengthLabel);
        this.GenerationGroupBox.Controls.Add(this.LengthInput);
        this.GenerationGroupBox.Controls.Add(this.UppercaseCheckBox);
        this.GenerationGroupBox.Controls.Add(this.LowercaseCheckBox);
        this.GenerationGroupBox.Controls.Add(this.NumbersCheckBox);
        this.GenerationGroupBox.Controls.Add(this.SpecialCheckBox);
        this.GenerationGroupBox.Location = new System.Drawing.Point(12, 12);
        this.GenerationGroupBox.Size = new System.Drawing.Size(336, 180);
        this.GenerationGroupBox.Text = "Password Generation";
        this.LengthLabel.Location = new System.Drawing.Point(16, 29);
        this.LengthLabel.Size = new System.Drawing.Size(80, 20);
        this.LengthLabel.Text = "Length:";
        this.LengthInput.Location = new System.Drawing.Point(105, 25);
        this.LengthInput.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
        this.LengthInput.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
        this.LengthInput.Size = new System.Drawing.Size(70, 20);
        this.UppercaseCheckBox.Location = new System.Drawing.Point(16, 58);
        this.UppercaseCheckBox.Size = new System.Drawing.Size(250, 20);
        this.UppercaseCheckBox.Text = "Require uppercase letters";
        this.LowercaseCheckBox.Location = new System.Drawing.Point(16, 84);
        this.LowercaseCheckBox.Size = new System.Drawing.Size(250, 20);
        this.LowercaseCheckBox.Text = "Require lowercase letters";
        this.NumbersCheckBox.Location = new System.Drawing.Point(16, 110);
        this.NumbersCheckBox.Size = new System.Drawing.Size(250, 20);
        this.NumbersCheckBox.Text = "Require numbers";
        this.SpecialCheckBox.Location = new System.Drawing.Point(16, 136);
        this.SpecialCheckBox.Size = new System.Drawing.Size(250, 20);
        this.SpecialCheckBox.Text = "Require special characters";
        this.OkButton.Location = new System.Drawing.Point(192, 207);
        this.OkButton.Size = new System.Drawing.Size(75, 23);
        this.OkButton.Text = "OK";
        this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
        this.CancelButtonControl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.CancelButtonControl.Location = new System.Drawing.Point(273, 207);
        this.CancelButtonControl.Size = new System.Drawing.Size(75, 23);
        this.CancelButtonControl.Text = "Cancel";
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
