using System.Collections.Generic;
using PagedList;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IRepository<T> where T:ClientExtract
    {
        IEnumerable<T> GetAll();


        IPagedList<T> GetAll(int? page,int pageSize=200);
    }
}