using EphorosVault.Presentation.Desktop.Base.Helpers;
using EphorosVault.Presentation.Desktop.Properties;
using System;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms;

public sealed partial class FolderNameForm : Form
{
    private readonly StandardErrorProvider _errors = new();
    public FolderNameForm() : this(string.Empty, "New Folder")
    {
    }

    public FolderNameForm(string folderName, string action)
    {
        InitializeComponent();
        Text = action + " - " + Resources.ProductName;
        NameTextBox.Text = folderName;
        _errors.ContainerControl = this;
    }

    public void ShowValidationError(string message)
    {
        _errors.UpdateError(NameTextBox, message);
        NameTextBox.Focus();
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        _errors.Clear();
        if (FolderName.Length == 0)
        {
            ShowValidationError("A folder name is required.");
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    public string FolderName => NameTextBox.Text.Trim();
}
