using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using log4net;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model.Extract;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StagePatientExtractRepository : IStagePatientExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;

        public StagePatientExtractRepository(DwapiCentralContext context)
        {
            _context = context;
        }

        public async Task ClearSite(Guid facilityId)
        {

            var cons = _context.Database.Connection.ConnectionString;

            var sql = @"

delete  from StageAdverseEventExtract WHERE  FacilityId = @facilityId;
delete  from StageAllergiesChronicIllnessExtract WHERE  FacilityId = @facilityId;
delete  from StageArtExtract WHERE  FacilityId = @facilityId;
delete  from StageBaselineExtract WHERE  FacilityId = @facilityId;
delete  from StageContactListingExtract WHERE  FacilityId = @facilityId;
delete  from StageCovidExtract WHERE  FacilityId = @facilityId;
delete  from StageDefaulterTracingExtract WHERE  FacilityId = @facilityId;
delete  from StageDepressionScreeningExtract WHERE  FacilityId = @facilityId;
delete  from StageDrugAlcoholScreeningExtract WHERE  FacilityId = @facilityId;
delete  from StageEnhancedAdherenceCounsellingExtract WHERE  FacilityId = @facilityId;
delete  from StageGbvScreeningExtract WHERE  FacilityId = @facilityId;
delete  from StageIptExtract WHERE  FacilityId = @facilityId;
delete  from StageLaboratoryExtract WHERE  FacilityId = @facilityId;
delete  from StageOtzExtract WHERE  FacilityId = @facilityId;
delete  from StageOvcExtract WHERE  FacilityId = @facilityId;
delete  from StagePatientExtract WHERE  FacilityId = @facilityId;
delete  from StagePharmacyExtract WHERE  FacilityId = @facilityId;
delete  from StageStatusExtract WHERE  FacilityId = @facilityId;
delete  from StageVisitExtract WHERE  FacilityId = @facilityId;
delete  from StageCervicalCancerScreeningExtract WHERE  FacilityId = @facilityId;

";
            try
            {
                using (var connection = new SqlConnection(cons))
                {
                    if(connection.State!=ConnectionState.Open)
                        connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}", new {  facilityId }, transaction, 0);
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
                    if(connection.State!=ConnectionState.Open)
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

                var pks = extracts.Select(x => x.Id).ToList();

                //assign > Assigned
                await AssignAll(manifestId, pks);

                //assign > Assigned
                await AssignId(manifestId,pks);

                //assign > New
                await CreatNew(manifestId,pks);

                //assign > Ups
                await UpdateExisting(manifestId,pks);

                //assign > Merged
                await MergeAll(manifestId,  pks);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task AssignAll(Guid manifestId, List<Guid> ids)
        {
            var cons = _context.Database.Connection.ConnectionString;

            var sql = @"
                    UPDATE 
                            StagePatientExtract
                    SET 
                            LiveStage = @nextlivestage 
                    WHERE 
                            LiveSession = @manifestId AND 
                            LiveStage = @livestage AND 
                            Id IN @ids";
            try
            {
                using (var connection = new SqlConnection(cons))
                {
                    if(connection.State!=ConnectionState.Open)
                        connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}",
                            new
                            {
                                manifestId, livestage = LiveStage.Rest, nextlivestage = LiveStage.Assigned, ids
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

        private async Task AssignId(Guid manifestId,List<Guid> ids)
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
                            stg.Id IN @ids AND
                            stg.CurrentPatientId is Null ";

            try
            {

                // assign patientId
                using (var connection = new SqlConnection(cons))
                {
                    if(connection.State!=ConnectionState.Open)
                        connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}", new {manifestId, livestage = LiveStage.Assigned,ids},
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

        public Task UpdateExisting(Guid manifestId, List<Guid> ids)
        {
            var sqlUpdates = @"
                    SELECT        
                         CurrentPatientId Id, Emr, Project, Voided, Processed, NUPI, Pkv, Occupation, Gender, DOB, RegistrationDate, RegistrationAtCCC, RegistrationATPMTCT, RegistrationAtTBClinic, Region, PatientSource, District, Village, ContactRelation, LastVisit, 
                         MaritalStatus, EducationLevel, DateConfirmedHIVPositive, PreviousARTExposure, PreviousARTStartDate, StatusAtCCC, StatusAtPMTCT, StatusAtTBClinic, Orphan, Inschool, PatientType, PopulationType, KeyPopulationType, 
                         PatientResidentCounty, PatientResidentSubCounty, PatientResidentLocation, PatientResidentSubLocation, PatientResidentWard, PatientResidentVillage, TransferInDate, PatientPID, PatientCccNumber, FacilityId, 
                         CurrentPatientId, LiveSession, LiveStage,GETDATE() Updated
                    FROM            StagePatientExtract WITH (NOLOCK)
                    WHERE 
                          LiveSession = @manifestId AND 
                          LiveStage = @livestage AND
                          Id IN @ids AND
                          CurrentPatientId IS NOT NULL";

            try
            {

                //get updates
                var updates = _context.GetConnection()
                    .Query<PatientExtract>(sqlUpdates, new {manifestId, livestage = LiveStage.Assigned,ids});

                if(updates.Any())
                    _context.GetConnection().BulkUpdate(updates);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }

            return Task.CompletedTask;
        }

        private Task CreatNew(Guid manifestId, List<Guid> ids)
        {
            var sqlNew = @"
                    SELECT 
                           *,GETDATE() Created FROM StagePatientExtract WITH (NOLOCK)
                    WHERE 
                          LiveSession = @manifestId AND
                          LiveStage = @livestage AND
                          Id IN @ids AND
                          CurrentPatientId IS NULL";
            try
            {
                //  get new
                var inserts = _context.GetConnection()
                    .Query<PatientExtract>(sqlNew, new {manifestId, livestage = LiveStage.Assigned,ids});

                if(inserts.Any())
                    _context.GetConnection().BulkInsert(inserts);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }

            return Task.CompletedTask;
        }

        private async Task MergeAll(Guid manifestId, List<Guid> ids)
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
                            Id IN @ids";
            try
            {

                // assign patientId
                using (var connection = new SqlConnection(cons))
                {
                    if(connection.State!=ConnectionState.Open)
                        connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}",
                            new
                            {
                                manifestId, livestage = LiveStage.Assigned, nextlivestage = LiveStage.Merged, ids
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
