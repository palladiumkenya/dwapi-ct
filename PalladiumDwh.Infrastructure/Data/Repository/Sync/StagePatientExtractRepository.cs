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

        public async Task ClearSite(Guid facilityId, Guid sessionId)
        {
            var cons = _context.Database.Connection.ConnectionString;

            var sql = @"
                    DELETE FROM StagePatientExtract 
                    WHERE 
                            FacilityId = @facilityId AND
                            LiveSession != @sessionId";

            try
            {

                // assign patientId
                using (var connection = new SqlConnection(cons))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}", new {sessionId, facilityId}, transaction, 0);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public async Task Stage(List<StagePatientExtract> extracts, Guid session)
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
                    await AssignAll(session, site.Key, pks);
                }

                //assign > Assigned
                await AssignId(session);

                //assign > New
                await CreatNew(session);

                //assign > Ups
                await UpdateExisting(session);

                //assign > Merged
                foreach (var site in siteGroup)
                {
                    var pks = site.Select(x => x.PatientPID).ToList();
                    await MergeAll(session, site.Key, pks);
                }


            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private async Task AssignAll(Guid session, Guid facilityId, List<int> patientPIDs)
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
                            LiveSession = @session AND 
                            LiveStage= @livestage AND 
                            FacilityId= @facilityId AND
                            PatientPID IN @patientPIDs AND";
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
                                session, livestage = LiveStage.Rest, nextlivestage = LiveStage.Assigned, facilityId,
                                patientPIDs
                            }, transaction, 0);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private async Task AssignId(Guid session)
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
                            stg.LiveSession = @session AND 
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
                        await connection.ExecuteAsync($"{sql}", new {session, livestage = LiveStage.Assigned},
                            transaction, 0);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public Task UpdateExisting(Guid session)
        {
            var sqlUpdates = @"
                    SELECT        CurrentPatientId Id, Emr, Project, Voided, Processed, Pkv, Occupation, Gender, DOB, RegistrationDate, RegistrationAtCCC, RegistrationATPMTCT, RegistrationAtTBClinic, Region, PatientSource, District, Village, ContactRelation, LastVisit, 
                         MaritalStatus, EducationLevel, DateConfirmedHIVPositive, PreviousARTExposure, PreviousARTStartDate, StatusAtCCC, StatusAtPMTCT, StatusAtTBClinic, Orphan, Inschool, PatientType, PopulationType, KeyPopulationType, 
                         PatientResidentCounty, PatientResidentSubCounty, PatientResidentLocation, PatientResidentSubLocation, PatientResidentWard, PatientResidentVillage, TransferInDate, PatientPID, PatientCccNumber, FacilityId, 
                         CurrentPatientId, LiveSession, LiveStage,GETDATE() Updated
                    FROM            StagePatientExtract
                    WHERE 
                          LiveSession = @session AND 
                          LiveStage = @livestage AND
                          CurrentPatientId IS NOT NULL";

            try
            {

                //get updates
                var updates = _context.GetConnection()
                    .Query<PatientExtract>(sqlUpdates, new {session, livestage = LiveStage.Assigned});
                _context.GetConnection().BulkUpdate(updates);

            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return Task.CompletedTask;
        }

        private Task CreatNew(Guid session)
        {
            var sqlNew = @"
                    SELECT 
                           *,GETDATE() Created FROM StagePatientExtract
                    WHERE 
                          LiveSession = @session AND
                          LiveStage = @livestage AND
                          CurrentPatientId IS NULL";
            try
            {
                //  get new
                var inserts = _context.GetConnection()
                    .Query<PatientExtract>(sqlNew, new {session, livestage = LiveStage.Assigned});
                _context.GetConnection().BulkInsert(inserts);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return Task.CompletedTask;
        }

        private async Task MergeAll(Guid session, Guid facilityId, List<int> patientPIDs)
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
                            LiveSession = @session AND 
                            LiveStage= @livestage AND 
                            FacilityId= @facilityId AND
                            PatientPID IN @patientPIDs AND";
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
                                session, livestage = LiveStage.Assigned, nextlivestage = LiveStage.Merged, facilityId,
                                patientPIDs
                            }, transaction, 0);
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
