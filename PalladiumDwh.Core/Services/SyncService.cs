using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PalladiumDwh.Core.Services
{
    public class SyncService : ISyncService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFacilityRepository _facilityRepository;
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IPatientArtExtractRepository _patientArtExtractRepository;
        private readonly IPatientBaseLinesRepository _patientBaseLinesRepository;
        private readonly IPatientLabRepository _patientLabRepository;
        private readonly IPatientPharmacyRepository _patientPharmacyRepository;
        private readonly IPatientStatusRepository _patientStatusRepository;
        private readonly IPatientVisitRepository _patientVisitRepository;
        private readonly IPatientAdverseEventRepository _patientAdverseEventRepository;

        private List<PatientVisitProfile> _visitProfiles = new List<PatientVisitProfile>();
        private List<PatientARTProfile> _artProfiles = new List<PatientARTProfile>();
        private List<PatientBaselineProfile> _baselineProfiles = new List<PatientBaselineProfile>();
        private List<PatientLabProfile> _labProfiles = new List<PatientLabProfile>();
        private List<PatientPharmacyProfile> _pharmacyProfiles = new List<PatientPharmacyProfile>();
        private List<PatientStatusProfile> _statusProfiles = new List<PatientStatusProfile>();
        private List<PatientAdverseEventProfile> _adverseEventProfiles = new List<PatientAdverseEventProfile>();
        private readonly ILiveSyncService _liveSyncService;
        private readonly IActionRegisterRepository _actionRegisterRepository;


        public SyncService(IFacilityRepository facilityRepository, IPatientExtractRepository patientExtractRepository,
            IPatientArtExtractRepository patientArtExtractRepository,
            IPatientBaseLinesRepository patientBaseLinesRepository, IPatientLabRepository patientLabRepository,
            IPatientPharmacyRepository patientPharmacyRepository, IPatientStatusRepository patientStatusRepository,
            IPatientVisitRepository patientVisitRepository,
            IPatientAdverseEventRepository patientAdverseEventRepository, ILiveSyncService liveSyncService, IActionRegisterRepository actionRegisterRepository)
        {
            _facilityRepository = facilityRepository;
            _patientExtractRepository = patientExtractRepository;
            _patientArtExtractRepository = patientArtExtractRepository;
            _patientBaseLinesRepository = patientBaseLinesRepository;
            _patientLabRepository = patientLabRepository;
            _patientPharmacyRepository = patientPharmacyRepository;
            _patientStatusRepository = patientStatusRepository;
            _patientVisitRepository = patientVisitRepository;
            _patientAdverseEventRepository = patientAdverseEventRepository;
            _liveSyncService = liveSyncService;
            _actionRegisterRepository = actionRegisterRepository;
        }

        /// <summary>
        /// Sync Profile
        /// </summary>
        /// <param name="profile"></param>
        public void Sync(object profile)
        {
            if (profile.GetType() == typeof(Manifest))
            {
                SyncManifest(profile as Manifest);
            }

            if (profile.GetType() == typeof(PatientARTProfile))
            {
                SyncArtNew(profile as PatientARTProfile);
            }
            else if (profile.GetType() == typeof(PatientBaselineProfile))
            {
                SyncBaselineNew(profile as PatientBaselineProfile);
            }
            else if (profile.GetType() == typeof(PatientLabProfile))
            {
                SyncLabNew(profile as PatientLabProfile);
            }
            else if (profile.GetType() == typeof(PatientPharmacyProfile))
            {
                SyncPharmacyNew(profile as PatientPharmacyProfile);
            }
            else if (profile.GetType() == typeof(PatientStatusProfile))
            {
                SyncStatusNew(profile as PatientStatusProfile);
            }
            else if (profile.GetType() == typeof(PatientVisitProfile))
            {
                SyncVisitNew(profile as PatientVisitProfile);
            }
            else if (profile.GetType() == typeof(PatientAdverseEventProfile))
            {
                SyncvAdverseEventNew(profile as PatientAdverseEventProfile);
            }

            else if (profile.GetType() == typeof(List<PatientARTProfile>))
            {
                SyncArtNew(profile as List<PatientARTProfile>);
            }
            else if (profile.GetType() == typeof(List<PatientBaselineProfile>))
            {
                SyncBaselineNew(profile as List<PatientBaselineProfile>);
            }
            else if (profile.GetType() == typeof(List<PatientLabProfile>))
            {
                SyncLabNew(profile as List<PatientLabProfile>);
            }
            else if (profile.GetType() == typeof(List<PatientPharmacyProfile>))
            {
                SyncPharmacyNew(profile as List<PatientPharmacyProfile>);
            }
            else if (profile.GetType() == typeof(List<PatientStatusProfile>))
            {
                SyncStatusNew(profile as List<PatientStatusProfile>);
            }
            else if (profile.GetType() == typeof(List<PatientVisitProfile>))
            {
                SyncVisitNew(profile as List<PatientVisitProfile>);
            }
            else if (profile.GetType() == typeof(List<PatientAdverseEventProfile>))
            {
                SyncvAdverseEventNew(profile as List<PatientAdverseEventProfile>);
            }
        }

        public async void SyncManifest(Manifest manifest)
        {
            var facManifest = FacilityManifest.Create(manifest);
            _patientExtractRepository.SaveManifest(facManifest);
            await _actionRegisterRepository.Clear(manifest.SiteCode);
            // await _patientExtractRepository.ClearManifest(manifest);
            // await _patientExtractRepository.RemoveDuplicates(manifest.SiteCode);
            // await _patientExtractRepository.InitializeManifest(manifest);
        }

        public void InitList(string queueName)
        {
            if (queueName.ToLower().Contains("PatientARTProfile".ToLower()))
            {
                _artProfiles = new List<PatientARTProfile>();
            }

            if (queueName.ToLower().Contains("PatientBaselineProfile".ToLower()))
            {
                _baselineProfiles = new List<PatientBaselineProfile>();
            }

            if (queueName.ToLower().Contains("PatientLabProfile".ToLower()))
            {
                _labProfiles = new List<PatientLabProfile>();
            }

            if (queueName.ToLower().Contains("PatientPharmacyProfile".ToLower()))
            {
                _pharmacyProfiles = new List<PatientPharmacyProfile>();
            }

            if (queueName.ToLower().Contains("PatientStatusProfile".ToLower()))
            {
                _statusProfiles = new List<PatientStatusProfile>();
            }

            if (queueName.ToLower().Contains("PatientVisitProfile".ToLower()))
            {
                _visitProfiles = new List<PatientVisitProfile>();
            }

            if (queueName.ToLower().Contains("PatientAdverseEventProfile".ToLower()))
            {
                _adverseEventProfiles = new List<PatientAdverseEventProfile>();
            }

        }

        public void Commit(string queueName)
        {
            List<Guid> facIds = new List<Guid>();

            if (queueName.ToLower().Contains("PatientARTProfile".ToLower()))
            {
                Log.Debug($"batch processing {queueName}...");
                _patientArtExtractRepository.SyncNewPatients(_artProfiles, _facilityRepository, facIds);
            }

            if (queueName.ToLower().Contains("PatientBaselineProfile".ToLower()))
            {
                Log.Debug($"batch processing {queueName}...");
                _patientBaseLinesRepository.SyncNewPatients(_baselineProfiles, _facilityRepository, facIds);
            }

            if (queueName.ToLower().Contains("PatientLabProfile".ToLower()))
            {
                Log.Debug($"batch processing {queueName}...");
                _patientLabRepository.SyncNewPatients(_labProfiles, _facilityRepository, facIds, _actionRegisterRepository);
            }

            if (queueName.ToLower().Contains("PatientPharmacyProfile".ToLower()))
            {
                Log.Debug($"batch processing {queueName}...");
                _patientPharmacyRepository.SyncNewPatients(_pharmacyProfiles, _facilityRepository, facIds, _actionRegisterRepository);
            }

            if (queueName.ToLower().Contains("PatientStatusProfile".ToLower()))
            {
                Log.Debug($"batch processing {queueName}...");
                _patientStatusRepository.SyncNewPatients(_statusProfiles, _facilityRepository, facIds);
            }

            if (queueName.ToLower().Contains("PatientVisitProfile".ToLower()))
            {
                Log.Debug($"batch processing {queueName}...");
                _patientVisitRepository.SyncNewPatients(_visitProfiles, _facilityRepository, facIds,
                    _actionRegisterRepository);
            }

            if (queueName.ToLower().Contains("PatientAdverseEventProfile".ToLower()))
            {
                Log.Debug($"batch processing {queueName}...");
                _patientAdverseEventRepository.SyncNewPatients(_adverseEventProfiles, _facilityRepository, facIds);
            }

            SyncStats(facIds);
        }

        public Guid? SyncPatient(PatientProfile profile)
        {
            return SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);
        }


        public void SyncArt(PatientARTProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _patientArtExtractRepository.SyncNew(profile.PatientInfo.Id, profile.Extracts);
            }
        }

        public void SyncBaseline(PatientBaselineProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _patientBaseLinesRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncLab(PatientLabProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _patientLabRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncPharmacy(PatientPharmacyProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _patientPharmacyRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncStatus(PatientStatusProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _patientStatusRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncVisit(PatientVisitProfile profile)
        {
            profile.GeneratePatientRecord();

            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _visitProfiles.Add(profile);
                //_patientStatusRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncAdverseEvent(PatientAdverseEventProfile profile)
        {
            profile.GeneratePatientRecord();

            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _adverseEventProfiles.Add(profile);
                //_patientStatusRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncArtNew(PatientARTProfile profile)
        {
            profile.GeneratePatientRecord();
            _artProfiles.Add(profile);
        }

        public void SyncBaselineNew(PatientBaselineProfile baselineProfile)
        {
            baselineProfile.GeneratePatientRecord();
            _baselineProfiles.Add(baselineProfile);
        }

        public void SyncLabNew(PatientLabProfile labProfile)
        {
            labProfile.GeneratePatientRecord();
            _labProfiles.Add(labProfile);
        }

        public void SyncPharmacyNew(PatientPharmacyProfile patientPharmacyProfile)
        {
            patientPharmacyProfile.GeneratePatientRecord();
            _pharmacyProfiles.Add(patientPharmacyProfile);
        }

        public void SyncStatusNew(PatientStatusProfile patientStatusProfile)
        {
            patientStatusProfile.GeneratePatientRecord();
            _statusProfiles.Add(patientStatusProfile);
            ;
        }

        public void SyncVisitNew(PatientVisitProfile profile)
        {
            profile.GeneratePatientRecord();
            _visitProfiles.Add(profile);
        }

        public void SyncvAdverseEventNew(PatientAdverseEventProfile profile)
        {
            profile.GeneratePatientRecord();
            _adverseEventProfiles.Add(profile);
        }



        public void SyncArtNew(List<PatientARTProfile> profile) => profile.ForEach(SyncArtNew);

        public void SyncBaselineNew(List<PatientBaselineProfile> baselineProfile) => baselineProfile.ForEach(SyncBaselineNew);

        public void SyncLabNew(List<PatientLabProfile> labProfile) => labProfile.ForEach(SyncLabNew);

        public void SyncPharmacyNew(List<PatientPharmacyProfile> patientPharmacyProfile) => patientPharmacyProfile.ForEach(SyncPharmacyNew);

        public void SyncStatusNew(List<PatientStatusProfile> patientStatusProfile) => patientStatusProfile.ForEach(SyncStatusNew);

        public void SyncVisitNew(List<PatientVisitProfile> profile) => profile.ForEach(SyncVisitNew);

        public void SyncvAdverseEventNew(List<PatientAdverseEventProfile> profile) => profile.ForEach(SyncvAdverseEventNew);


        public Facility GetFacility(int code)
        {
            return _facilityRepository.Find(x => x.Code == code);
        }

        private Guid? SyncCurrentPatient(Facility facility, PatientExtract patient)
        {

            Guid? syncPatientId = null;

            var facilityId = _facilityRepository.SyncNew(facility);
            if (!(facilityId == Guid.Empty || null == facilityId))
            {
                patient.FacilityId = facilityId.Value;
                syncPatientId = _patientExtractRepository.SyncNew(patient);
            }

            return syncPatientId;
        }

        private Guid? SyncCurrentPatientNew(Facility facility, PatientExtract patient)
        {

            Guid? syncPatientId = null;

            var facilityId = _facilityRepository.SyncNew(facility);
            if (!(facilityId == Guid.Empty || null == facilityId))
            {
                patient.FacilityId = facilityId.Value;
                syncPatientId = _patientExtractRepository.SyncNew(patient);
            }

            return syncPatientId;
        }

        private async void SyncStats(List<Guid> facilities)
        {
            if (null != _liveSyncService)
                await _liveSyncService.SyncStats(_facilityRepository, facilities.Distinct().ToList());
        }
    }
}
