using System.Collections.Generic;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IReader<T> where T : class 
    {
        IEnumerable<T> Read();
    }
}