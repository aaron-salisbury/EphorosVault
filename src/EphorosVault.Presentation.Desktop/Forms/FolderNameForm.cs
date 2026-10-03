using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms;

public sealed class FolderNameForm : Form
{
    private readonly TextBox _name = new();

    public FolderNameForm()
    {
        Text = "New Folder - Ephoros Vault";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new System.Drawing.Size(350, 105);
        MaximizeBox = false;
        MinimizeBox = false;
        Controls.Add(new Label { Text = "Folder name:", Left = 15, Top = 20, Width = 80 });
        _name.Left = 100;
        _name.Top = 17;
        _name.Width = 230;
        Controls.Add(_name);
        Button ok = new() { Text = "OK", Left = 174, Top = 62, Width = 75, DialogResult = DialogResult.OK };
        Button cancel = new() { Text = "Cancel", Left = 255, Top = 62, Width = 75, DialogResult = DialogResult.Cancel };
        Controls.Add(ok);
        Controls.Add(cancel);
        AcceptButton = ok;
        CancelButton = cancel;
    }

    public string FolderName => _name.Text.Trim();
}
