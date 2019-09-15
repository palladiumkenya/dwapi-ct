
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
            Log.Debug($"clearing {manifest.SiteCode}...");
            var cons = _context.Database.Connection.ConnectionString;

            var sql = $@"
                DELETE FROM PatientExtract
                WHERE        
	                (FacilityId IN (SELECT ID FROM Facility WHERE [Code]={manifest.SiteCode})) AND 
	                (PatientPID NOT IN ({manifest.GetPatientPKsJoined()}));";


            using (var connection=new SqlConnection(cons))
            {
                try
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}", null, transaction, 0);
                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
                
            }

        }

        public async Task RemoveDuplicates(int siteCode)
        {
            Log.Debug($"deduplicating {siteCode}...");

            var cons = _context.Database.Connection.ConnectionString;

            var cleanUpSql = $@"
                DELETE FROM PatientExtract 
                WHERE 
	                Id IN (
	                SELECT 
		                Id
	                FROM 
		                PatientExtract p inner join
		                (
		                SELECT        
			                FacilityId, PatientPID, COUNT(Id) AS PCount
		                FROM            
			                PatientExtract
		                WHERE
			                FacilityId IN (SELECT ID FROM Facility WHERE [Code]={siteCode})
		                GROUP BY 
			                FacilityId, PatientPID
		                HAVING        
			                (COUNT(*) > 1)
		                ) d on p.PatientPID=d.PatientPID and p.FacilityId=d.FacilityId
                );";

            using (var connection=new SqlConnection(cons))
            {
                try
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{cleanUpSql}", null, transaction, 0);
                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                  Log.Error(e.Message);
                }
                
            }

        }

        public Guid? GetFacilityId(int siteCode)
        {
            var cons = _context.Database.Connection.ConnectionString;
            string sql = " SELECT TOP 1 ID FROM Facility WHERE [Code]=@Code AND Voided=0;";
            var patientFacility = _context.GetConnection()
                .QueryFirstOrDefault<PatientFacilityId>(sql, new {Code = siteCode});
            return patientFacility?.Id;
        }

        public async Task InitializeManifest(Manifest manifest)
        {
            Log.Debug($"initializing {manifest.SiteCode}...");

            var cons = _context.Database.Connection.ConnectionString;
            var facId = GetFacilityId(manifest.SiteCode);

            if (null != facId)
            {
                var sql = manifest.GetInitExtracts(facId.Value);

                try
                {
                    using (var connection=new  SqlConnection(cons))
                    {
                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            await connection.ExecuteAsync($"{sql}", null, transaction, 0);
                            transaction.Commit();
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }

            }

        }

        public Task<MasterFacility> VerifyFacility(int siteCode)
        {
            int originalSiteCode = siteCode;

            string fcode = siteCode.ToString();
            if (fcode.Length != 5)
            {
                Log.Debug(new string('^', 40));
                Log.Debug($"Invalid SiteCode:{siteCode}");
                /*
                if (fcode.StartsWith("648"))
                {
                    siteCode =  siteCode- 64800000;
                }

                if (fcode.StartsWith("254"))
                {
                    siteCode =  siteCode- 25400000;
                }
                */
                //Log.Debug($"Invalid SiteCode {originalSiteCode} Fixed to:{siteCode}");

                Log.Debug(new string('^', 40));
            }

            return _context.MasterFacilities
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Code == siteCode);
        }
    }
}
