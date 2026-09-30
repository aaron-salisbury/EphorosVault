using EphorosVault.Business.Modules.Sample.ApplicationServices;
using EphorosVault.Presentation.Desktop.Base.MVP;
using EphorosVault.Presentation.Desktop.Views.SampleTools;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace EphorosVault.Presentation.Desktop.Presenters.SampleTools
{
    internal class FlatUIColorPickerPresenter : Presenter
    {
        private readonly ISampleToolsService _sampleToolsService;

        private FlatUIColorPickerView _view;

        public FlatUIColorPickerPresenter(Navigator navigator, ISampleToolsService sampleToolsService) : base(navigator)
        {
            _sampleToolsService = sampleToolsService;
        }

        internal override void Display(Control view, ControlCollection window)
        {
            _view = (FlatUIColorPickerView)view;

            _view.Initialize(_sampleToolsService.GetFlatColors().ToList());

            window.Clear();
            window.Add(_view);
        }
    }
}
