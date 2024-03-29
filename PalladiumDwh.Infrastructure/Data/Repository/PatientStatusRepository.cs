﻿using System;
using System.Collections.Generic;
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

    public class PatientStatusRepository : GenericRepository<PatientStatusExtract>, IPatientStatusRepository
    {
        private readonly DwapiCentralContext _context;
        public PatientStatusRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }
        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }
        public void Sync(Guid patientId, IEnumerable<PatientStatusExtract> extracts)
        {
            Clear(patientId);
            Insert(extracts);
            CommitChanges();
        }

    public void ClearNew(Guid patientId)
      {
        string sql = "DELETE FROM PatientStatusExtract WHERE PatientId = @PatientId";
        _context.GetConnection().Execute(sql, new { PatientId = patientId });
      }

      public void SyncNew(Guid patientId, IEnumerable<PatientStatusExtract> extracts)
      {
        ClearNew(patientId);
        _context.GetConnection().BulkInsert(extracts);
      }

      public void SyncNewPatients(IEnumerable<PatientStatusProfile> profiles, IFacilityRepository facilityRepository,
          List<Guid> facIds)
      {
          var updates = new List<PatientExtract>();
          var inserts = new List<PatientExtract>();
          var updatedProfiles = new List<PatientStatusProfile>();

          //  get facilities from profiles
          var facilities = profiles.Select(x => x.FacilityInfo).ToList().Distinct();

          foreach (var facility in facilities)
          {
              var facilityUpdatedProfiles = new List<PatientStatusProfile>();
              //sync facility
              var facilityId = facilityRepository.SyncNew(facility);

              //update profiles with facilityId.
              if (null != facilityId)
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
          {
              var patientGroup = inserts.GroupBy(x => x.Id);
              foreach (var group in patientGroup)
                  _context.GetConnection().BulkMerge(group.First());
          }

          if (updates.Count > 0)
          {
              var patientGroup = updates.GroupBy(x => x.Id);
              foreach (var group in patientGroup)
                  _context.GetConnection().BulkUpdate(group.First());
          }

          foreach (var patientStatusProfile in updatedProfiles)
          {
              patientStatusProfile.GenerateRecords(patientStatusProfile.PatientInfo.Id);
          }

          SyncNew(updatedProfiles);
      }

      public void SyncNew(IEnumerable<PatientStatusProfile> profiles)
        {
            var ids = new List<string>();
            var extracts = new List<PatientStatusExtract>();

            foreach (var p in profiles)
            {
                ids.Add($"'{p.PatientInfo.Id}'");
                extracts.AddRange(p.Extracts);
            }

            //clear patient data

            if (ids.Count > 0)
            {
                var connection = _context.GetConnection();
                var allIds = string.Join(",", ids);

                var sql = $@" DELETE FROM PatientStatusExtract WHERE (PatientId In ({allIds}))";
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
        }
    }
}
