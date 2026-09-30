using EphorosVault.Presentation.Desktop.Base.Helpers;
using System.Windows.Forms;
using System;

namespace EphorosVault.Presentation.Desktop.Views.SampleTools
{
    public partial class UUIDGeneratorView : UserControl
    {
        internal event EventHandler<GenerateCommandEventArgs> GenerateCommand;

        private StandardErrorProvider _errorProvider; // Not doing anything with yet.

        public UUIDGeneratorView()
        {
            InitializeComponent();
        }

        internal void UUIDGenerated(string uUID)
        {
            NewGuidTextBox.Text = uUID;
        }

        private void UUIDGeneratorUC_Load(object sender, EventArgs e)
        {
            _errorProvider = new StandardErrorProvider();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (GenerateCommand == null) { return; }

            GenerateCommand.Invoke(this, new GenerateCommandEventArgs()
            {
                ShouldCapitalize = CapitalizeCheckBox.Checked
            });
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (NewGuidTextBox != null && !string.IsNullOrEmpty(NewGuidTextBox.Text))
            {
                Clipboard.SetText(NewGuidTextBox.Text);
            }
        }
    }

    public class GenerateCommandEventArgs : EventArgs
    {
        public bool ShouldCapitalize { get; set; }
    }
}
