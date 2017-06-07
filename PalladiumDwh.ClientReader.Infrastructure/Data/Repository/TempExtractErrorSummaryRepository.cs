using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using log4net;
using PagedList;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public abstract class TempExtractErrorSummaryRepository<T> : ITempExtractErrorSummaryRepository<T> where T: TempExtractErrorSummary
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal DwapiRemoteContext Context;
        internal DbSet<T> DbSet;

        public TempExtractErrorSummaryRepository(DwapiRemoteContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public IPagedList<T> GetAll(int? page, int? pageSize, string search = "")
        {
            page = page == 0 ? 1 : page;
            pageSize = pageSize == 0 ? 100 : pageSize;
            pageSize = pageSize == -1 ? GetAll().Count() : pageSize;

            var tempExtracts = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                tempExtracts = tempExtracts.Where(n =>
                    n.PatientID.ToLower().Contains(search.ToLower())
                );
            }


            var pagedlist = tempExtracts
                .OrderBy(x => x.PatientID).ThenBy(x=>x.Summary)
                .ToPagedList(page.Value, pageSize.Value);

            return pagedlist;
        }

    }
}
