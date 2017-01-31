using System.Collections.Generic;
using CsvHelper.Configuration;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface ILoadCsvExtractCommand<T> where T: TempExtract
    {
        void Execute();
    }
}