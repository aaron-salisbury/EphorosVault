using System;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace EphorosVault.Presentation.Desktop.Base.MVP;

internal abstract class Presenter
{
    internal Navigator Navigator
    {
        get; set;
    }

    internal Presenter(Navigator navigator)
    {
        Navigator = navigator;
    }

    internal virtual void Display(Control view, ControlCollection window)
    {
        throw new NotImplementedException();
    }

    internal virtual void Dismiss()
    {
    }
}
