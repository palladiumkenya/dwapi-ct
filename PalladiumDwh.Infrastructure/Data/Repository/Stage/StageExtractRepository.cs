using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using log4net;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Interfaces.Extracts;
using PalladiumDwh.Shared.Interfaces.Stages;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public abstract class StageExtractRepository<T,D> : IStageExtractRepository<T,D>
        where T:IStage
        where D:IEntity
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;
        private readonly string _stageName;
        private readonly string _extractName;

        public StageExtractRepository(DwapiCentralContext context, IMapper mapper, string stageName, string extractName)
        {
            _context = context;
            _mapper = mapper;
            _stageName = stageName;
            _extractName = extractName;
        }

        public async Task ClearSite(Guid facilityId, Guid manifestId)
        {
            var cons = _context.Database.Connection.ConnectionString;

            var sql = $@"DELETE FROM {_stageName} WHERE FacilityId = @facilityId AND LiveSession != @manifestId";

            try
            {
                using (var connection = new SqlConnection(cons))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync($"{sql}",
                            new {manifestId = manifestId, facilityId}, transaction,0);
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
        public async Task SyncStage(List<T> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.GetConnection().BulkInsert(extracts);

                // assign > Assigned
                await AssignAll(manifestId, extracts.Select(x=>x.Id).ToList());

                // assign > Assigned
                await AssignId(manifestId,extracts.Select(x=>x.Id).ToList());

                // assign > New
                await CreatNewPatientHolders(manifestId,extracts.Select(x=>x.Id).ToList());

                // assign > Merged
                await SmartMarkRegister(manifestId, extracts.Select(x=>x.Id).ToList());

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

            var sql = $@"
                    UPDATE 
                            {_stageName}
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
                    connection.Open();
                    await connection.ExecuteAsync($"{sql}",
                        new
                        {
                            manifestId, livestage = LiveStage.Rest, nextlivestage = LiveStage.Assigned, ids
                        }, null, 0);
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

            var sql = $@"
                    UPDATE 
                            stg
                    SET 
                            stg.CurrentPatientId = p.Id 
                    FROM 
                            {_stageName} AS stg INNER JOIN PatientExtract AS p ON 
                                    stg.PatientPK = p.PatientPID AND 
                                    stg.FacilityId = p.FacilityId 
                    WHERE 
                            stg.LiveSession = @manifestId AND 
                            stg.LiveStage = @livestage AND 
                            stg.Id IN @ids AND
                            stg.CurrentPatientId is Null ";

            try
            {
                using (var connection = new SqlConnection(cons))
                {
                    connection.Open();
                    await connection.ExecuteAsync($"{sql}",
                        new {manifestId, livestage = LiveStage.Assigned, ids}, null, 0);

                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
        private Task CreatNewPatientHolders(Guid manifestId,List<Guid> ids)
        {
            var sqlNew = $@"
                    SELECT 
                           DISTINCT  PatientPK PatientPID,FacilityId,GETDATE() Created FROM {_stageName} WITH (NOLOCK)
                    WHERE 
                          LiveSession = @manifestId AND
                          LiveStage = @livestage AND
                          Id in @ids AND
                          CurrentPatientId IS NULL";
            try
            {
                //  get new
                var holders = _context.GetConnection()
                    .Query<PatientExtractHolderDto>(sqlNew, new {manifestId, livestage = LiveStage.Assigned,ids})
                    .ToList();

                if (holders.Any())
                {
                    var inserts = PatientExtractHolderDto.GeneratePatient(holders);
                    _context.GetConnection().BulkInsert(inserts);

                    AssignId(manifestId,ids);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
            return Task.CompletedTask;
        }
        private async Task MarkRegister(Guid manifestId, List<Guid> ids)
        {
            var cons = _context.Database.Connection.ConnectionString;

            var action = "DELETE";
            var area = $"{_extractName}";

            var sqlNew = $@"
                    SELECT 
                           *,GETDATE() Created FROM {_stageName} WITH (NOLOCK)
                    WHERE 
                          LiveSession = @manifestId AND 
                          LiveStage= @livestage AND 
                          Id in @ids AND
                          CurrentPatientId IS NOT NULL";
            try
            {
                //  get new
                var stages = _context.GetConnection()
                    .Query<T>(sqlNew, new {manifestId, livestage = LiveStage.Assigned,ids})
                    .ToList();

                if (stages.Any())
                {
                    var pidGroup = stages.GroupBy(x => x.CurrentPatientId).ToList();
                    foreach (var site in pidGroup)
                    {
                        // check register
                        if (!_context.ActionRegisters
                            .Any(x => x.PatientId == site.Key.Value))
                        {
                            // clear visit
                            var sqlDelete = $@"DELETE FROM {_extractName} WHERE PatientId = @patientId";

                            try
                            {
                                using (var connection = new SqlConnection(cons))
                                {
                                    connection.Open();
                                    await connection.ExecuteAsync($"{sqlDelete}",
                                        new
                                        {
                                            @patientId = site.Key.Value
                                        }, null, 0);
                                }
                            }
                            catch (Exception e)
                            {
                                Log.Error(e);
                                throw;
                            }

                            var facilityId = site.First().FacilityId.Value;

                            var mark = new ActionRegister()
                            {
                                Action = action, Area = area, FacilityId = facilityId,
                                PatientId = site.Key.Value
                            };
                            _context.GetConnection().BulkInsert(mark);
                        }

                        var visits = _mapper.Map<List<D>>(site.ToList());
                        _context.GetConnection().BulkInsert(visits);

                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task SmartMarkRegister(Guid manifestId, List<Guid> ids)
        {
            var cons = _context.Database.Connection.ConnectionString;

            var action = "DELETE";
            var area = $"{_extractName}";

            var sqlNew = $@"
                    SELECT 
                           *,GETDATE() Created FROM {_stageName} WITH (NOLOCK)
                    WHERE 
                          LiveSession = @manifestId AND 
                          LiveStage= @livestage AND 
                          Id in @ids AND
                          CurrentPatientId IS NOT NULL";
            try
            {

                //  get new
                var stages = _context.GetConnection()
                    .Query<T>(sqlNew, new {manifestId, livestage = LiveStage.Assigned,ids})
                    .ToList();

                if (stages.Any())
                {
                    var pidGroup = stages.Select(x => new { x.FacilityId }).Distinct().ToList();

                    foreach (var site in pidGroup)
                    {
                        // check register
                        if (!_context.SmartActionRegisters.AsNoTracking()
                            .Any(x =>
                                      x.ManifestId == manifestId &&
                                      x.Area==area &&
                                      x.Action==action))
                        {
                            // clear visit
                            var sqlDelete = $@"DELETE FROM {_extractName} WHERE PatientId IN (SELECT {nameof(PatientExtract.Id)} FROM {nameof(PatientExtract)} WHERE {nameof(PatientExtract.FacilityId)}=@facilityId)  ";

                            try
                            {
                                using (var connection = new SqlConnection(cons))
                                {
                                    connection.Open();
                                    await connection.ExecuteAsync($"{sqlDelete}",
                                        new
                                        {
                                            facilityId = site.FacilityId
                                        }, null, 0);
                                }

                                var mark = new SmartActionRegister()
                                {
                                    Action = action, Area = area, FacilityId = site.FacilityId.Value,
                                    ManifestId = manifestId
                                };
                                _context.GetConnection().BulkInsert(mark);
                            }
                            catch (Exception e)
                            {
                                Log.Error(e);
                                throw;
                            }
                        }
                    }
                }

                var visits = _mapper.Map<List<D>>(stages);
                _context.GetConnection().BulkInsert(visits);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
    }
}
