using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using log4net;
using PalladiumDwh.Core.Interfaces.Sync;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model.Extract;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository.Sync
{
    public class StagePatientExtractRepository : IStagePatientExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;

        public StagePatientExtractRepository(DwapiCentralContext context)
        {
            _context = context;
        }

        public async Task ClearSite(Guid facilityId, Guid manifestId)
        {
            var cons = _context.Database.Connection.ConnectionString;

            var sql = @"
                    DELETE FROM StagePatientExtract 
                    WHERE 
                            FacilityId = @facilityId AND
                            LiveSession != @manifestId";

            try
            {

                // assign patientId
                using (var connection = new SqlConnection(cons))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}", new {manifestId = manifestId, facilityId}, transaction, 0);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        public async Task SyncStage(List<StagePatientExtract> extracts, Guid manifestId)
        {
            try
            {
                //stage > Rest
                _context.GetConnection().BulkInsert(extracts);

                //assign > Assigned
                var siteGroup = extracts.GroupBy(x => x.FacilityId);
                foreach (var site in siteGroup)
                {
                    var pks = site.Select(x => x.PatientPID).ToList();
                    await AssignAll(manifestId, site.Key, pks);
                }

                //assign > Assigned
                await AssignId(manifestId);

                //assign > New
                await CreatNew(manifestId);

                //assign > Ups
                await UpdateExisting(manifestId);

                //assign > Merged
                foreach (var site in siteGroup)
                {
                    var pks = site.Select(x => x.PatientPID).ToList();
                    await MergeAll(manifestId, site.Key, pks);
                }


            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task AssignAll(Guid manifestId, Guid facilityId, List<int> patientPIDs)
        {
            var cons = _context.Database.Connection.ConnectionString;

            var sql = @"
                    UPDATE 
                            StagePatientExtract
                    SET 
                            LiveStage= @nextlivestage 
                    FROM 
                            StagePatientExtract 
                    WHERE 
                            LiveSession = @manifestId AND 
                            LiveStage= @livestage AND 
                            FacilityId= @facilityId AND
                            PatientPID IN @patientPIDs";
            try
            {

                // assign patientId
                using (var connection = new SqlConnection(cons))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}",
                            new
                            {
                                manifestId, livestage = LiveStage.Rest, nextlivestage = LiveStage.Assigned, facilityId,
                                patientPIDs
                            }, transaction, 0);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task AssignId(Guid manifestId)
        {
            var cons = _context.Database.Connection.ConnectionString;

            var sql = @"
                    UPDATE 
                            stg
                    SET 
                            stg.CurrentPatientId = p.Id 
                    FROM 
                            StagePatientExtract AS stg INNER JOIN PatientExtract AS p ON 
                                    stg.PatientPID = p.PatientPID AND 
                                    stg.FacilityId = p.FacilityId 
                    WHERE 
                            stg.LiveSession = @manifestId AND 
                            stg.LiveStage= @livestage AND 
                            stg.CurrentPatientId is Null ";

            try
            {

                // assign patientId
                using (var connection = new SqlConnection(cons))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}", new {manifestId, livestage = LiveStage.Assigned},
                            transaction, 0);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        public Task UpdateExisting(Guid manifestId)
        {
            var sqlUpdates = @"
                    SELECT        CurrentPatientId Id, Emr, Project, Voided, Processed, Pkv, Occupation, Gender, DOB, RegistrationDate, RegistrationAtCCC, RegistrationATPMTCT, RegistrationAtTBClinic, Region, PatientSource, District, Village, ContactRelation, LastVisit, 
                         MaritalStatus, EducationLevel, DateConfirmedHIVPositive, PreviousARTExposure, PreviousARTStartDate, StatusAtCCC, StatusAtPMTCT, StatusAtTBClinic, Orphan, Inschool, PatientType, PopulationType, KeyPopulationType, 
                         PatientResidentCounty, PatientResidentSubCounty, PatientResidentLocation, PatientResidentSubLocation, PatientResidentWard, PatientResidentVillage, TransferInDate, PatientPID, PatientCccNumber, FacilityId, 
                         CurrentPatientId, LiveSession, LiveStage,GETDATE() Updated
                    FROM            StagePatientExtract
                    WHERE 
                          LiveSession = @manifestId AND 
                          LiveStage = @livestage AND
                          CurrentPatientId IS NOT NULL";

            try
            {

                //get updates
                var updates = _context.GetConnection()
                    .Query<PatientExtract>(sqlUpdates, new {manifestId, livestage = LiveStage.Assigned});
                _context.GetConnection().BulkUpdate(updates);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }

            return Task.CompletedTask;
        }

        private Task CreatNew(Guid manifestId)
        {
            var sqlNew = @"
                    SELECT 
                           *,GETDATE() Created FROM StagePatientExtract
                    WHERE 
                          LiveSession = @manifestId AND
                          LiveStage = @livestage AND
                          CurrentPatientId IS NULL";
            try
            {
                //  get new
                var inserts = _context.GetConnection()
                    .Query<PatientExtract>(sqlNew, new {manifestId, livestage = LiveStage.Assigned});
                _context.GetConnection().BulkInsert(inserts);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }

            return Task.CompletedTask;
        }

        private async Task MergeAll(Guid manifestId, Guid facilityId, List<int> patientPIDs)
        {
            var cons = _context.Database.Connection.ConnectionString;

            var sql = @"
                    UPDATE 
                            StagePatientExtract
                    SET 
                            LiveStage= @nextlivestage 
                    FROM 
                            StagePatientExtract 
                    WHERE 
                            LiveSession = @manifestId AND 
                            LiveStage= @livestage AND 
                            FacilityId= @facilityId AND
                            PatientPID IN @patientPIDs";
            try
            {

                // assign patientId
                using (var connection = new SqlConnection(cons))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}",
                            new
                            {
                                manifestId, livestage = LiveStage.Assigned, nextlivestage = LiveStage.Merged, facilityId,
                                patientPIDs
                            }, transaction, 0);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
    }
}
