using System.Collections.Generic;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IReader<out T>
    {
        IEnumerable<T> Read();
    }
}