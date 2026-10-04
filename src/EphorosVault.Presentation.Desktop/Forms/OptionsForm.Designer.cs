namespace EphorosVault.Presentation.Desktop.Forms;

partial class OptionsForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button CancelButtonControl;
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
        System.Windows.Forms.GroupBox group = new System.Windows.Forms.GroupBox();
        System.Windows.Forms.Label lengthLabel = new System.Windows.Forms.Label();
        this.LengthInput = new System.Windows.Forms.NumericUpDown();
        this.UppercaseCheckBox = new System.Windows.Forms.CheckBox();
        this.LowercaseCheckBox = new System.Windows.Forms.CheckBox();
        this.NumbersCheckBox = new System.Windows.Forms.CheckBox();
        this.SpecialCheckBox = new System.Windows.Forms.CheckBox();
        this.OkButton = new System.Windows.Forms.Button();
        this.CancelButtonControl = new System.Windows.Forms.Button();
        group.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.LengthInput)).BeginInit();
        this.SuspendLayout();
        group.Location = new System.Drawing.Point(12, 12); group.Size = new System.Drawing.Size(336, 180); group.Text = "Password Generation";
        lengthLabel.Location = new System.Drawing.Point(16, 29); lengthLabel.Size = new System.Drawing.Size(80, 20); lengthLabel.Text = "Length:";
        this.LengthInput.Location = new System.Drawing.Point(105, 25); this.LengthInput.Maximum = 128; this.LengthInput.Minimum = 8; this.LengthInput.Size = new System.Drawing.Size(70, 20);
        Configure(this.UppercaseCheckBox, "Require uppercase letters", 58);
        Configure(this.LowercaseCheckBox, "Require lowercase letters", 84);
        Configure(this.NumbersCheckBox, "Require numbers", 110);
        Configure(this.SpecialCheckBox, "Require special characters", 136);
        group.Controls.Add(lengthLabel); group.Controls.Add(this.LengthInput); group.Controls.Add(this.UppercaseCheckBox); group.Controls.Add(this.LowercaseCheckBox); group.Controls.Add(this.NumbersCheckBox); group.Controls.Add(this.SpecialCheckBox);
        this.OkButton.Location = new System.Drawing.Point(192, 207); this.OkButton.Size = new System.Drawing.Size(75, 23); this.OkButton.Text = "OK"; this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
        this.CancelButtonControl.DialogResult = System.Windows.Forms.DialogResult.Cancel; this.CancelButtonControl.Location = new System.Drawing.Point(273, 207); this.CancelButtonControl.Size = new System.Drawing.Size(75, 23); this.CancelButtonControl.Text = "Cancel";
        this.AcceptButton = this.OkButton; this.CancelButton = this.CancelButtonControl; this.ClientSize = new System.Drawing.Size(360, 245); this.Controls.Add(group); this.Controls.Add(this.OkButton); this.Controls.Add(this.CancelButtonControl); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox = false; this.MinimizeBox = false; this.Name = "OptionsForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        group.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)(this.LengthInput)).EndInit(); this.ResumeLayout(false);
    }

    private static void Configure(System.Windows.Forms.CheckBox checkBox, string text, int top)
    {
        checkBox.Location = new System.Drawing.Point(16, top); checkBox.Size = new System.Drawing.Size(250, 20); checkBox.Text = text;
    }
}
