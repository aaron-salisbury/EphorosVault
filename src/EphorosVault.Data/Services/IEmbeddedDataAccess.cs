using EphorosVault.Data.Entities;
using System.Collections.Generic;

namespace EphorosVault.Data
{
    /// <summary>
    /// Read Embedded data.
    /// </summary>
    public interface IEmbeddedDataAccess
    {
        List<FlatColor> ReadFlatColors();
    }
}
