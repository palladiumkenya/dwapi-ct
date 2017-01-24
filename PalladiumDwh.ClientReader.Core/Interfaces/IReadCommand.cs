using System.Collections.Generic;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IReadCommand<T>
    {
        IEnumerable<T> Execute();
    }
}