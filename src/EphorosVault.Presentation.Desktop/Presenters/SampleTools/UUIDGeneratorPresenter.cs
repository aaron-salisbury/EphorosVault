using EphorosVault.Business.Modules.Sample.ApplicationServices;
using EphorosVault.Presentation.Desktop.Base.MVP;
using EphorosVault.Presentation.Desktop.Views.SampleTools;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace EphorosVault.Presentation.Desktop.Presenters.SampleTools
{
    internal class UUIDGeneratorPresenter : Presenter
    {
        private readonly ISampleToolsService _sampleToolsService;

        private UUIDGeneratorView _view;

        public UUIDGeneratorPresenter(Navigator navigator, ISampleToolsService sampleToolsService) : base(navigator)
        {
            _sampleToolsService = sampleToolsService;
        }

        internal override void Display(Control view, ControlCollection window)
        {
            _view = (UUIDGeneratorView)view;

            _view.GenerateCommand += View_GenerateCommand;

            window.Clear();
            window.Add(_view);
        }

        internal override void Dismiss()
        {
            if (_view != null)
            {
                _view.GenerateCommand -= View_GenerateCommand;
            }
        }

        private void View_GenerateCommand(object sender, GenerateCommandEventArgs e)
        {
            if (_view != null)
            {
                string generatedUUID = _sampleToolsService.InitializeGUIDGeneration(e.ShouldCapitalize);

                _view.UUIDGenerated(generatedUUID);
            }
        }
    }
}
