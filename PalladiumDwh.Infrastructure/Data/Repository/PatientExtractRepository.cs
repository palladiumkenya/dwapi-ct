
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using Dapper;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientExtractRepository : GenericRepository<PatientExtract>, IPatientExtractRepository
    {
        
        private readonly DwapiCentralContext _context;

        public PatientExtractRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public Guid? GetPatientBy(Guid facilityId, string patientNumber)
        {
            return Find(
                x => x.FacilityId == facilityId &&
                     x.PatientCccNumber.ToLower() == patientNumber.ToLower()
            )?.Id;
        }

        public Guid? GetPatientBy(Guid facilityId, int patientPID)
        {
            return Find(
                x => x.FacilityId == facilityId &&
                     x.PatientPID == patientPID
            )?.Id;
        }

        public Guid? Sync(PatientExtract patient)
        {
            var patientId = GetPatientBy(patient.FacilityId, patient.PatientPID);

            if (patientId == Guid.Empty || null == patientId)
            {
                Insert(patient);
                CommitChanges();
                patientId = patient.Id;
            }
            else
            {
                Update(patient);
            }

            return patientId;
        }

        public async Task ClearManifest(Manifest manifest)
        {
            var connection = _context.Database.Connection as SqlConnection;

            var sql = $@"
                DELETE FROM PatientExtract
                WHERE        
	                (FacilityId = (SELECT ID FROM Facility WHERE [Code]={manifest.SiteCode})) AND 
	                (PatientPID NOT IN ({manifest.GetPatientPKsJoined()}))
            ";

            if (null != connection)
            {
                using (connection)
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync(sql, null, transaction, 0);
                        transaction.Commit();
                    }
                }
            }
        }
    }
}
