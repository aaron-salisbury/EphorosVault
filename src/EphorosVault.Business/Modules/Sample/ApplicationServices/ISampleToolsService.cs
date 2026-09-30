using EphorosVault.Business.Modules.Sample.DTOs;
using System.Collections.Generic;
using static EphorosVault.Business.Modules.Sample.DomainServices.LineSorter;

namespace EphorosVault.Business.Modules.Sample.ApplicationServices
{
    public interface ISampleToolsService
    {
        IEnumerable<FlatColorDto> GetFlatColors();

        string InitializeLineSorting(SortTypes _selectedSortType, string textToSort);

        string InitializeGUIDGeneration(bool shouldCapitalize = true);
    }
}
