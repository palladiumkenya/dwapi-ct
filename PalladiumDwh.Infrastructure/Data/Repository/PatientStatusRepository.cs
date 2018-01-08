using System;
using System.Collections.Generic;
using Dapper;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model.Extract;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
  
    public class PatientStatusRepository : GenericRepository<PatientStatusExtract>, IPatientStatusRepository
    {
        private readonly DwapiCentralContext _context;
        public PatientStatusRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }
        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }
        public void Sync(Guid patientId, IEnumerable<PatientStatusExtract> extracts)
        {
            Clear(patientId);
            Insert(extracts);
            CommitChanges();
        }

    public void ClearNew(Guid patientId)
      {
        string sql = "DELETE FROM PatientStatusExtract WHERE PatientId = @PatientId";
        _context.GetConnection().Execute(sql, new { PatientId = patientId });
      }

      public void SyncNew(Guid patientId, IEnumerable<PatientStatusExtract> extracts)
      {
        ClearNew(patientId);
        _context.GetConnection().BulkInsert(extracts);
      }
  }
}