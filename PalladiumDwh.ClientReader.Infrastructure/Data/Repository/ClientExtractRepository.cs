﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using log4net;
using PagedList;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public abstract class ClientExtractRepository<T> : IClientExtractRepository<T> where T: ClientExtract
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal DwapiRemoteContext Context;
        internal DbSet<T> DbSet;

        public ClientExtractRepository(DwapiRemoteContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public IEnumerable<T> GetAllSendErrors()
        {
           return DbSet.Where(x => x.Status != "Sent" && !string.IsNullOrEmpty(x.Status));
        }

        public IPagedList<T> GetAll(int? page, int? pageSize, string search = "")
        {
            IEnumerable<T> logs;

            page = page == 0 ? 1 : page;
            pageSize = pageSize == 0 ? 100 : pageSize;
            pageSize = pageSize == -1 ? GetAll().Count() : pageSize;

            logs = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                logs = logs.Where(n =>
                    n.PatientID.ToLower().Contains(search.ToLower())
                );
            }


            var pagedlist = logs
                .OrderBy(x => x.PatientID)
                .ToPagedList(page.Value, pageSize.Value);

            return pagedlist;
        }

        public IPagedList<T> GetAllSendErrors(int? page, int? pageSize, string search = "")
        {
            IEnumerable<T> logs;

            page = page == 0 ? 1 : page;
            pageSize = pageSize == 0 ? 100 : pageSize;
            pageSize = pageSize == -1 ? GetAllSendErrors().Count() : pageSize;

            logs = GetAllSendErrors();

            if (!string.IsNullOrWhiteSpace(search))
            {
                logs = logs.Where(n =>
                    n.PatientID.ToLower().Contains(search.ToLower())
                );
            }


            var pagedlist = logs
                .OrderBy(x => x.PatientID)
                .ToPagedList(page.Value, pageSize.Value);

            return pagedlist;
        }
    }
}
