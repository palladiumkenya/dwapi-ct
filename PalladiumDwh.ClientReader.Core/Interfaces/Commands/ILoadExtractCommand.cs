using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface ILoadExtractCommand<T> where T: TempExtract
    {
        void Execute();
    }
}