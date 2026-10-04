using EphorosVault.Presentation.Desktop.Properties;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms;

public sealed partial class FolderNameForm : Form
{
    public FolderNameForm() : this(string.Empty, "New Folder")
    {
    }

    public FolderNameForm(string folderName, string action)
    {
        InitializeComponent();
        Text = action + " - " + Resources.ProductName;
        NameTextBox.Text = folderName;
    }

    public string FolderName => NameTextBox.Text.Trim();
}
