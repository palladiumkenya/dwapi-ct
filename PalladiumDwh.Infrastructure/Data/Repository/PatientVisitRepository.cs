using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PalladiumDwh.Core.Interfaces;
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

        public void SyncNewPatients(IEnumerable<PatientVisitProfile> profiles, IFacilityRepository facilityRepository)
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
                        string exisitingSql =$"SELECT Id,PatientPID,FacilityId FROM PatientExtract WHERE FacilityId='{facilityId}' and PatientPID in ({allpatientPIds});";
                        var exisitingPatients = _context.GetConnection().Query<PatientExtractId>(exisitingSql).ToList();

                        foreach (var profile in facilityUpdatedProfiles)
                        {
                            var p = exisitingPatients.FirstOrDefault(x => x.PatientPID == profile.PatientInfo.PatientPID);

                            if (null != p)
                            {
                                profile.PatientInfo.Id = p.Id;
                                updates.Add(profile.PatientInfo);
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
                _context.GetConnection().BulkInsert(inserts);

            if (updates.Count > 0)
                _context.GetConnection().BulkUpdate(updates);

            foreach (var patientVisitProfile in updatedProfiles)
            {
                patientVisitProfile.GenerateRecords(patientVisitProfile.PatientInfo.Id);
            }

            Log.Debug($"Visit extracts processing...");
            SyncNew(updatedProfiles);
        }

        public void SyncNew(IEnumerable<PatientVisitProfile> profiles)
        {
            var ids = new List<string>();
            var extracts = new List<PatientVisitExtract>();

            foreach (var p in profiles)
            {
                ids.Add($"'{p.PatientInfo.Id}'");
                extracts.AddRange(p.Extracts);
            }
            //clear patient data

            if (ids.Count > 0)
            {

                Log.Debug($"Visit extracts processing clearing for {ids.Count} patients");

                var connection = _context.GetConnection();
                var allIds = string.Join(",", ids);

                var sql = $@" DELETE FROM PatientVisitExtract WHERE (PatientId In ({allIds}))";
                try
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        connection.Execute(sql, null, transaction, 0);
                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    Log.Debug(e);
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
            Log.Debug($"Visit extracts processing processed {extracts.Count} extracts");
        }
    }
}