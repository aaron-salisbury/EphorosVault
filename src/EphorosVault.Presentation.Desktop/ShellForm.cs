using EphorosVault.Presentation.Desktop.Forms;
using EphorosVault.Presentation.Desktop.Presenters;
using System;
using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop
{
    public partial class ShellForm : Form
    {
        public ShellForm()
        {
            InitializeComponent();
            Text = Properties.Settings.Default.ApplicationFriendlyName;
        }

        private void LogMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new())
            {
                aboutForm.ShowDialog(this);
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
