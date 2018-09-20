
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using Dapper;
using Dapper.Contrib.Extensions;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model.DTO;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientExtractRepository : GenericRepository<PatientExtract>, IPatientExtractRepository
    {
        
        private readonly DwapiCentralContext _context;

        public PatientExtractRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public PatientExtract Find(Guid facilityId, int patientPID)
        {
            return Find(
                x => x.FacilityId == facilityId &&
                     x.PatientPID == patientPID
            );
        }
        
        public PatientExtract FindBy(Guid id)
        {
            string sql = "SELECT * FROM PatientExtract WHERE Id=@Id";
            var patientExtract = _context.GetConnection().QueryFirstOrDefault<PatientExtract>(sql, new { Id = id });
            return patientExtract;
        }

        public PatientExtract FindBy(Guid facilityId, int patientPID)
        {
            string sql = "SELECT * FROM PatientExtract WHERE FacilityId=@FacilityId and PatientPID=@PatientPID;";
            var patientExtract = _context.GetConnection().QueryFirstOrDefault<PatientExtract>(sql, new { FacilityId = facilityId, PatientPID = patientPID });
            return patientExtract;
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
            return _context.PatientExtracts.AsNoTracking().FirstOrDefault(

                x => x.FacilityId == facilityId &&
                     x.PatientPID == patientPID
            )?.Id;
        }

        public Guid? GetPatientByIds(Guid facilityId, int patientPID)
        {
          string sql = "SELECT Id FROM PatientExtract WHERE FacilityId=@FacilityId and PatientPID=@PatientPID;";
          var patientExtract = _context.GetConnection().QueryFirstOrDefault<PatientExtractId>(sql, new { FacilityId=facilityId,PatientPID = patientPID});
          return patientExtract?.Id;
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
//                patient.Id = patientId.Value;
//                Update(patient);
            }

            return patientId;
        }

        public Guid? SyncNew(PatientExtract patient)
        {
          var patientId = GetPatientByIds(patient.FacilityId, patient.PatientPID);

            if (patientId == Guid.Empty || null == patientId)
            {
                _context.GetConnection().BulkInsert(patient);
                patientId = patient.Id;
            }
            else
            {
                patient.Id = patientId.Value;
                _context.GetConnection().BulkUpdate(patient);
            }

            return patientId;
        }

        public void SaveManifest(FacilityManifest facilityManifest)
        {
            _context.GetConnection()
                .BulkInsert(facilityManifest)
                .AlsoBulkInsert(f => f.Cargoes);
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

      public Task<MasterFacility> VerifyFacility(int siteCode)
        {
            int originalSiteCode = siteCode;

            string fcode = siteCode.ToString();
            if (fcode.Length == 8)
            {
                Log.Debug(new string('^', 40));
                Log.Debug($"Invalid SiteCode:{siteCode}");
                if (fcode.StartsWith("648"))
                {
                    siteCode =  siteCode- 64800000;
                }

                if (fcode.StartsWith("254"))
                {
                    siteCode =  siteCode- 25400000;
                }

                Log.Debug($"Invalid SiteCode {originalSiteCode} Fixed to:{siteCode}");

                Log.Debug(new string('^',40));
            }

            return _context.MasterFacilities
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Code == siteCode);
        }
    }
}
