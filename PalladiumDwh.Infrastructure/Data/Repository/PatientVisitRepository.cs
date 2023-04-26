using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientVisitRepository : GenericRepository<PatientVisitExtract>, IPatientVisitRepository
    {
        private readonly DwapiCentralContext _context;

        public PatientVisitRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }

        public void Sync(Guid patientId, IEnumerable<PatientVisitExtract> extracts)
        {
            Clear(patientId);
            Insert(extracts);
            CommitChanges();
        }

        public void ClearNew(Guid patientId)
        {
            string sql = "DELETE FROM PatientVisitExtract WHERE PatientId = @PatientId";
            _context.GetConnection().Execute(sql, new {PatientId = patientId});
        }

        public void SyncNew(Guid patientId, IEnumerable<PatientVisitExtract> extracts)
        {
            ClearNew(patientId);
            _context.GetConnection().BulkInsert(extracts);
        }

        public void SyncNewPatients(IEnumerable<PatientVisitProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository actionRegisterRepository)
        {
            var updates = new List<PatientExtract>();
            var inserts = new List<PatientExtract>();
            var updatedProfiles = new List<PatientVisitProfile>();

            //  get facilities from profiles
            var facilities = profiles.Select(x => x.FacilityInfo).ToList().Distinct();

            foreach (var facility in facilities)
            {
                var facilityUpdatedProfiles = new List<PatientVisitProfile>();
                //sync facility
                var facilityId = facilityRepository.SyncNew(facility);

                //update profiles with facilityId.
                if (!(facilityId == Guid.Empty || null == facilityId))
                {
                    facIds.Add(facilityId.Value);
                    var facilityProfiles = profiles.Where(x => x.FacilityInfo.Code == facility.Code).ToList();

                    foreach (var profile in facilityProfiles)
                    {
                        profile.PatientInfo.FacilityId = facilityId.Value;
                        facilityUpdatedProfiles.Add(profile);
                    }

                    if (facilityUpdatedProfiles.Count > 0)
                    {
                        var patientPIds = facilityUpdatedProfiles.Select(x => x.PatientInfo.PatientPID).ToList();
                        var allpatientPIds = string.Join(",", patientPIds);

                        //sync patients

                        //Get Exisitng
                        string exisitingSql =
                            $"SELECT Id,PatientPID,FacilityId FROM PatientExtract WHERE FacilityId='{facilityId}' and PatientPID in ({allpatientPIds});";
                        var exisitingPatients = _context.GetConnection().Query<PatientExtractId>(exisitingSql).ToList();

                        foreach (var profile in facilityUpdatedProfiles)
                        {
                            var p = exisitingPatients.FirstOrDefault(
                                x => x.PatientPID == profile.PatientInfo.PatientPID);

                            if (null != p)
                            {
                                profile.PatientInfo.Id = p.Id;
                                updates.Add(profile.PatientInfo);
                                updates.ForEach(x => x.Updated = DateTime.Now);
                            }
                            else
                            {
                                inserts.Add(profile.PatientInfo);
                            }
                        }

                        updatedProfiles.AddRange(facilityUpdatedProfiles);
                    }
                }
            }

            if (inserts.Count > 0)
                _context.GetConnection().BulkMerge(inserts);


            if (updates.Count > 0)
                _context.GetConnection().BulkUpdate(updates);

            foreach (var patientVisitProfile in updatedProfiles)
            {
                patientVisitProfile.GenerateRecords(patientVisitProfile.PatientInfo.Id);
            }

            SyncNew(updatedProfiles,actionRegisterRepository);
        }

        public void SyncNew(List<PatientVisitProfile> profiles, IActionRegisterRepository actionRegisterRepository)
        {

            var extracts = new List<PatientVisitExtract>();
            var action = "DELETE";
            var area = $"{nameof(PatientVisitExtract)}";

            if (profiles.Any())
            {
                extracts.AddRange(profiles.SelectMany(x => x.Extracts));

                var patientFacProfiles = profiles
                    .Select(x => new PatientFacilityProfile(x.PatientInfo.Id, x.PatientInfo.FacilityId))
                    .Distinct()
                    .ToList();

                var patientIds = patientFacProfiles.Select(x => x.Id).ToArray();

                var connectionString = _context.GetConnection().ConnectionString;


                // clear patient data not in register

                var sql = $@"  DELETE FROM {nameof(PatientVisitExtract)} 
                                    WHERE  PatientId IN @patientId AND
                                    PatientId NOT In (        
                                        SELECT DISTINCT PatientId FROM {nameof(ActionRegister)}
                                        WHERE  Action=@action AND Area=@area AND PatientId IN @patientId
                                    ) 
                                ";

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {

                        connection.Execute(sql,
                            new {action, area, patientId = patientIds});

                    }

                    // markregister

                    actionRegisterRepository.CreateAction(ActionRegister.Generate(patientFacProfiles, action, area));
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            //process extracts

            if (extracts.Count > 0)
            {
                try
                {
                    _context.GetConnection().BulkInsert(extracts);
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                }
            }
        }

    }
}
