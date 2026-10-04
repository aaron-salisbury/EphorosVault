using EphorosVault.Presentation.Desktop.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Forms
{
    public partial class AboutForm : Form
    {
        private const int MODAL_WIDTH = 525;
        private const int MODAL_HIGHT = 325;

        public AboutForm()
        {
            InitializeComponent();

            SetupAboutPage();
        }

        private void SetupAboutPage()
        {
            Size modalSize = new(MODAL_WIDTH, MODAL_HIGHT);
            Size = modalSize;
            MinimumSize = modalSize;
            MaximumSize = modalSize;
            SizeGripStyle = SizeGripStyle.Hide;

            Text = string.Format("About {0}", Resources.ProductName);
            AppNameLabel.Text = Resources.ProductName;
            VersionLabel.Text = string.Format("Version {0}", Application.ProductVersion);
            AppDescriptionLabel.Text = Resources.AppDescription;
        }

        private void AppLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Resources.Homepage);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
