using System.Collections.Generic;
using PagedList;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface ITempExtractRepository<T>
    {
        IEnumerable<T> GetAll();
        IPagedList<T> GetAll(int? page, int? pageSize, string search = "");
    }
}