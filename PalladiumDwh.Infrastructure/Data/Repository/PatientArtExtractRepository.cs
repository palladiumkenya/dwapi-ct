
using System;
using System.Collections.Generic;
using Dapper;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model.Extract;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
  public class PatientArtExtractRepository : GenericRepository<PatientArtExtract>, IPatientArtExtractRepository
  {
    private readonly DwapiCentralContext _context;

    public PatientArtExtractRepository(DwapiCentralContext context) : base(context)
    {
      _context = context;
    }

    public void Clear(Guid patientId)
    {
      DeleteBy(x => x.PatientId == patientId);
    }

    public void Sync(Guid patientId, IEnumerable<PatientArtExtract> extracts)
    {
      Clear(patientId);
      Insert(extracts);
      CommitChanges();
    }

    public void ClearNew(Guid patientId)
    {
      string sql = "DELETE FROM PatientArtExtract WHERE PatientId = @PatientId";
      _context.GetConnection().Execute(sql, new {PatientId = patientId});
    }

    public void SyncNew(Guid patientId, IEnumerable<PatientArtExtract> extracts)
    {
      ClearNew(patientId);
      _context.GetConnection().BulkInsert(extracts);
    }
  }

}
