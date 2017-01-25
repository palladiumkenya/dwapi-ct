using System;
using System.Data;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Extracts
{
    public interface IExtractRow
    {
        DateTime DateExtracted { get; set; }
        void Load(IDataReader reader);
    }
}