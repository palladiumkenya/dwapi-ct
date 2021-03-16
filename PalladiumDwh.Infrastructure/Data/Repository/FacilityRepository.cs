using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dapper;
using Dapper.Contrib.Extensions;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        private readonly DwapiCentralContext _context;

        public FacilityRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public Guid? GetFacilityIdByCode(int code)
        {
            string sql = "SELECT Id FROM Facility WHERE Code = @Code;";
            //Thread.Sleep(2000);
            var facility = _context.GetConnection().QueryFirstOrDefault<FacilityId>(sql, new {Code = code});
            return facility?.Id;
        }
        private Facility GetFacility(int code)
        {
            string sql = "SELECT * FROM Facility WHERE Code = @Code;";
            //Thread.Sleep(2000);
            var facility = _context.GetConnection().QueryFirstOrDefault<Facility>(sql, new { Code = code });
            return facility;
        }

        public Guid? SyncNew(Facility facility)
        {
            var fac = GetFacility(facility.Code);
            if (null == fac)
            {
                Log.Debug($"NEW FACILITY {facility}");
                _context.GetConnection().Execute(facility.SqlInsert());
                return facility.Id;
            }

            if (fac.ProfileMissing())
            {
                Log.Debug($"UPDATE FACILITY {facility} Profile");
                _context.GetConnection().Execute(facility.SqlUpdateProfile());
            }

            return fac.Id;
        }

        public Guid? GetFacilityIdBCode(int code)
        {
            return Find(x => x.Code == code)?.Id;
        }

        private Facility GetFacilityToEnroll(int code)
        {
            string sql = "SELECT * FROM Facility WHERE Code = @Code;";
            //Thread.Sleep(2000);
            var facility = _context.GetConnection().QueryFirstOrDefault<Facility>(sql, new { Code = code });
            return facility;
        }

        public MasterFacility GetFacilityByCode(int code)
        {
            string sql = "SELECT * FROM MasterFacility WHERE Code = @Code;";
            var facility = _context.GetConnection().QueryFirstOrDefault<MasterFacility>(sql, new {Code = code});
            return facility;
        }

        public Guid? Sync(Facility facility)
        {
            var facilityId = GetFacilityIdBCode(facility.Code);

            if (facilityId == Guid.Empty || null == facilityId)
            {
                Insert(facility);
                CommitChanges();
                facilityId = facility.Id;
            }

            return facilityId;
        }

        public IEnumerable<StatsDto> GetFacStats(IEnumerable<Guid> facilityIds)
        {
            var list = new List<StatsDto>();
            foreach (var facilityId in facilityIds)
            {
                try
                {
                    var stat = GetFacStats(facilityId);
                    if (null != stat)
                        list.Add(stat);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }


            }

            return list;
        }

        public StatsDto GetFacStats(Guid facilityId)
        {
            string sql = $@"
select
(select top 1 Code from Facility where id='{facilityId}') FacilityCode,
(select max(Created) from PatientExtract where facilityid='{facilityId}') Updated,
(select count(Distinct PatientPID) from PatientExtract where facilityid='{facilityId}' and Gender is not null) PatientExtract,
(select count(id) from PatientAdverseEventExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) PatientAdverseEventExtract,
(select count(id) from PatientArtExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) PatientArtExtract,
(select count(id) from PatientBaselinesExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) PatientBaselineExtract,
(select count(id) from PatientLaboratoryExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) PatientLabExtract,
(select count(id) from PatientPharmacyExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) PatientPharmacyExtract,
(select count(id) from PatientStatusExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) PatientStatusExtract,
(select count(id) from PatientVisitExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) PatientVisitExtract,
(select count(id) from AllergiesChronicIllnessExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) AllergiesChronicIllnessExtract,
(select count(id) from IptExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) IptExtract,
(select count(id) from DepressionScreeningExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) DepressionScreeningExtract,
(select count(id) from ContactListingExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) ContactListingExtract,
(select count(id) from GbvScreeningExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) GbvScreeningExtract,
(select count(id) from EnhancedAdherenceCounsellingExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) EnhancedAdherenceCounsellingExtract,
(select count(id) from DrugAlcoholScreeningExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) DrugAlcoholScreeningExtract,
(select count(id) from OvcExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) OvcExtract,
(select count(id) from OtzExtract where PatientId in (select Id from PatientExtract where facilityid='{facilityId}')) OtzExtract


                ";

            var result = _context.GetConnection().Query<dynamic>(sql).FirstOrDefault();

            if (null != result && result.PatientExtract > 0)
            {
                var stats = new StatsDto(result.FacilityCode, result.Updated);
                stats.AddStats("PatientExtract", result.PatientExtract);
                stats.AddStats("PatientAdverseEventExtract", result.PatientAdverseEventExtract);
                stats.AddStats("PatientArtExtract", result.PatientArtExtract);
                stats.AddStats("PatientBaselineExtract", result.PatientBaselineExtract);
                stats.AddStats("PatientLabExtract", result.PatientLabExtract);
                stats.AddStats("PatientPharmacyExtract", result.PatientPharmacyExtract);
                stats.AddStats("PatientStatusExtract", result.PatientStatusExtract);
                stats.AddStats("PatientVisitExtract", result.PatientVisitExtract);

                stats.AddStats("AllergiesChronicIllnessExtract", result.AllergiesChronicIllnessExtract);
                stats.AddStats("IptExtract", result.IptExtract);
                stats.AddStats("DepressionScreeningExtract", result.DepressionScreeningExtract);
                stats.AddStats("ContactListingExtract", result.ContactListingExtract);
                stats.AddStats("GbvScreeningExtract", result.GbvScreeningExtract);
                stats.AddStats("EnhancedAdherenceCounsellingExtract", result.EnhancedAdherenceCounsellingExtract);
                stats.AddStats("DrugAlcoholScreeningExtract", result.DrugAlcoholScreeningExtract);
                stats.AddStats("OvcExtract", result.OvcExtract);
                stats.AddStats("OtzExtract", result.OtzExtract);

                return stats;
            }

            return null;
        }

        public void Enroll(MasterFacility masterFacility,string emr,bool allowSnapshot)
        {
            emr = emr.IsSameAs("CHAK") ? "IQCare" : emr;

            var toEnroll = GetFacilityToEnroll(masterFacility.Code);
            if (null == toEnroll)
            {
                var facility = Facility.create(masterFacility);
                facility.Emr = emr;
                Log.Debug($"NEW FACILITY {facility}");
                _context.GetConnection().Execute(facility.SqlInsert());
                return;

            }

            // Take Facility SnapShot
            if (toEnroll.EmrChanged(emr) && allowSnapshot)
            {

                // SNAP MASTER

                #region SNAP MASTER

                var mfl = _context.MasterFacilities.FirstOrDefault(x => x.Code == masterFacility.Code);
                var mflSnaps = _context.MasterFacilities.Where(x =>  x.SnapshotSiteCode == masterFacility.Code)
                    .ToList();

                if (null == mfl)
                    return;

                var snapMfl=mfl.TakeSnap(mflSnaps);
                _context.Database.Connection.BulkInsert(snapMfl);


                #endregion

                #region SNAP FACILITY

                var fl = _context.Facilities.FirstOrDefault(x => x.Code == masterFacility.Code);

                if (null == fl)
                    return;

                var snapfl=fl.TakeSnapFrom(snapMfl);
                _context.Database.Connection.BulkUpdate(snapfl);

                #endregion

                var facility = Facility.create(masterFacility);
                facility.Emr = emr;
                Log.Debug($"SNAPSHOT FROM FACILITY {facility}");
                _context.GetConnection().Execute(facility.SqlInsert());
            }

        }

        public void EndSession(Guid session)
        {
            var end = DateTime.Now;
            var sql = $"UPDATE {nameof(FacilityManifest)} SET [{nameof(FacilityManifest.End)}]=@end WHERE [{nameof(FacilityManifest.Session)}]=@session";
            _context.GetConnection().Execute(sql, new {session, end});
        }

        public IEnumerable<HandshakeDto> GetSessionHandshakes(Guid session)
        {
            var sql = $"SELECT * FROM {nameof(FacilityManifest)} WHERE [{nameof(FacilityManifest.Session)}]=@session";
            var manifests = _context.GetConnection().Query<FacilityManifest>(sql,new{session}).ToList();
            return manifests.Select(x => new HandshakeDto()
            {
                Id = x.Id, End = x.End, Session = x.Session, Start = x.Start
            });
        }
    }
}
