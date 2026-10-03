using EphorosVault.Presentation.Desktop.Base.Helpers;
using System.Collections.Generic;

namespace EphorosVault.Presentation.Desktop.Models;

internal class LineSorterModel
{
    private List<ComboBoxEnumItem> _sortTypes;
    public List<ComboBoxEnumItem> SortTypes
    {
        get { return _sortTypes; }
        set { _sortTypes = value; }
    }

    private int _selectedSortTypeIndex;
    public int SelectedSortTypeIndex
    {
        get { return _selectedSortTypeIndex; }
        set { _selectedSortTypeIndex = value; }
    }

    private string _text;
    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }
}
