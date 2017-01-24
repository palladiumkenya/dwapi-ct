using System.Data;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IExtractRow
    {
        void Load(IDataReader reader);
    }
}