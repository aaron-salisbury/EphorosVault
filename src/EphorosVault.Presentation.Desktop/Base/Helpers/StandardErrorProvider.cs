using System.Windows.Forms;

namespace EphorosVault.Presentation.Desktop.Base.Helpers
{
    internal class StandardErrorProvider : ErrorProvider
    {
        private const int DEFAULT_ICON_PADDING = 10;

        internal StandardErrorProvider()
        {
            BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        internal void UpdateError(Control control, string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
                SetIconPadding(control, DEFAULT_ICON_PADDING);
                SetError(control, errorMessage);
            }
            else
            {
                SetError(control, string.Empty);
            }
        }
    }
}
