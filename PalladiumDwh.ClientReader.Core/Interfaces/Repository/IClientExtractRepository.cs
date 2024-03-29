﻿using System.Collections.Generic;
using PagedList;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IClientExtractRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllSendErrors();
        IPagedList<T> GetAll(int? page, int? pageSize, string search="");
        IPagedList<T> GetAllSendErrors(int? page, int? pageSize, string search = "");
    }
}