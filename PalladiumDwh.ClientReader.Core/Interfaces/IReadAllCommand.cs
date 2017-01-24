using System.Collections.Generic;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IReadAllCommand<T>
    {
        IEnumerable<T> Execute();
    }
}