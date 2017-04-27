using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PagedList;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IClientExtractRepository<T>
    {
        IPagedList<T> GetAll(int? page, int? pageSize, string search="");
    }
}