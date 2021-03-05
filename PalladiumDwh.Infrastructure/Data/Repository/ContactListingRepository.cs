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

    public class ContactListingRepository : GenericRepository<ContactListingExtract>, IContactListingRepository
    {
        private readonly DwapiCentralContext _context;
        public ContactListingRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }
        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }
        public void Sync(Guid patientId, IEnumerable<ContactListingExtract> extracts)
        {
            Clear(patientId);
            Insert(extracts);
            CommitChanges();
        }

    public void ClearNew(Guid patientId)
      {
        string sql = "DELETE FROM ContactListingExtract WHERE PatientId = @PatientId";
        _context.GetConnection().Execute(sql, new { PatientId = patientId });
      }

      public void SyncNew(Guid patientId, IEnumerable<ContactListingExtract> extracts)
      {
        ClearNew(patientId);
        _context.GetConnection().BulkInsert(extracts);
      }


        public void SyncNewPatients(IEnumerable<ContactListingProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository actionRegisterRepository)
        {
            var updates = new List<PatientExtract>();
            var inserts = new List<PatientExtract>();
            var updatedProfiles = new List<ContactListingProfile>();

            //  get facilities from profiles
            var facilities = profiles.Select(x => x.FacilityInfo).ToList().Distinct();

            foreach (var facility in facilities)
            {
                var facilityUpdatedProfiles = new List<ContactListingProfile>();
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

            foreach (var ContactListingProfile in updatedProfiles)
            {
                ContactListingProfile.GenerateRecords(ContactListingProfile.PatientInfo.Id);
            }

            SyncNew(updatedProfiles,actionRegisterRepository);
        }

        public void SyncNew(List<ContactListingProfile> profiles, IActionRegisterRepository actionRegisterRepository)
        {

            var extracts = new List<ContactListingExtract>();
            var action = "DELETE";
            var area = $"{nameof(ContactListingExtract)}";

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

                var sql = $@"  DELETE FROM {nameof(ContactListingExtract)} 
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
