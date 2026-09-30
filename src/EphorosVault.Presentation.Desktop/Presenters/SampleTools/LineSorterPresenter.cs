using DotNetFrameworkToolkit.Core.Extensions;
using EphorosVault.Business.Modules.Sample.ApplicationServices;
using EphorosVault.Business.Modules.Sample.DomainServices;
using EphorosVault.Presentation.Desktop.Base.Helpers;
using EphorosVault.Presentation.Desktop.Base.MVP;
using EphorosVault.Presentation.Desktop.Views.SampleTools;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;
using static System.Windows.Forms.Control;

namespace EphorosVault.Presentation.Desktop.Presenters.SampleTools
{
    internal class LineSorterPresenter : Presenter
    {
        private readonly ISampleToolsService _sampleToolsService;
        private readonly List<LineSorter.SortTypes> _sortTypes;
        private readonly List<ComboBoxEnumItem> _sortTypeItems;

        private LineSorterView _view;

        public LineSorterPresenter(Navigator navigator, ISampleToolsService sampleToolsService) : base(navigator)
        {
            _sampleToolsService = sampleToolsService;
            _sortTypes = Enum.GetValues(typeof(LineSorter.SortTypes)).Cast<LineSorter.SortTypes>().ToList();

            _sortTypeItems = [];
            foreach (LineSorter.SortTypes sortType in _sortTypes)
            {
                _sortTypeItems.Add(new ComboBoxEnumItem()
                {
                    Value = (int)sortType,
                    Text = StringExtensions.SplitPascalCase(sortType.ToString())
                });
            }
        }

        internal override void Display(Control view, ControlCollection window)
        {
            _view = (LineSorterView)view;

            _view.Initialize(_sortTypeItems);

            _view.SortCommand += View_SortCommand;

            window.Clear();
            window.Add(_view);
        }

        internal override void Dismiss()
        {
            if (_view != null)
            {
                _view.SortCommand -= View_SortCommand;
            }
        }

        private void View_SortCommand(object sender, SortCommandEventArgs e)
        {
            if (_view != null)
            {
                string sortedText = _sampleToolsService.InitializeLineSorting(_sortTypes[e.SortTypeIndex], e.TextToSort);

                _view.TextSorted(sortedText);
            }
        }
    }
}
