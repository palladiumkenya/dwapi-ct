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

        private readonly IAllergiesChronicIllnessRepository _allergiesChronicIllnessRepository;
        private readonly IIptRepository _iptRepository;
        private readonly IDepressionScreeningRepository _depressionScreeningRepository;
        private readonly IContactListingRepository _contactListingRepository;
        private readonly IGbvScreeningRepository _gbvScreeningRepository;
        private readonly IEnhancedAdherenceCounsellingRepository _enhancedAdherenceCounsellingRepository;
        private readonly IDrugAlcoholScreeningRepository _drugAlcoholScreeningRepository;
        private readonly IOvcRepository _ovcRepository;
        private readonly IOtzRepository _otzRepository;


        private List<PatientVisitProfile> _visitProfiles = new List<PatientVisitProfile>();
        private List<PatientARTProfile> _artProfiles = new List<PatientARTProfile>();
        private List<PatientBaselineProfile> _baselineProfiles = new List<PatientBaselineProfile>();
        private List<PatientLabProfile> _labProfiles = new List<PatientLabProfile>();
        private List<PatientPharmacyProfile> _pharmacyProfiles = new List<PatientPharmacyProfile>();
        private List<PatientStatusProfile> _statusProfiles = new List<PatientStatusProfile>();
        private List<PatientAdverseEventProfile> _adverseEventProfiles = new List<PatientAdverseEventProfile>();

        private List<AllergiesChronicIllnessProfile> _allergiesChronicIllnessProfiles =
            new List<AllergiesChronicIllnessProfile>();

        private List<IptProfile> _iptProfiles = new List<IptProfile>();
        private List<DepressionScreeningProfile> _depressionScreeningProfiles = new List<DepressionScreeningProfile>();
        private List<ContactListingProfile> _contactListingProfiles = new List<ContactListingProfile>();
        private List<GbvScreeningProfile> _gbvScreeningProfiles = new List<GbvScreeningProfile>();

        private List<EnhancedAdherenceCounsellingProfile> _enhancedAdherenceCounsellingProfiles =
            new List<EnhancedAdherenceCounsellingProfile>();

        private List<DrugAlcoholScreeningProfile> _drugAlcoholScreeningProfiles =
            new List<DrugAlcoholScreeningProfile>();

        private List<OvcProfile> _ovcProfiles = new List<OvcProfile>();
        private List<OtzProfile> _otzProfiles = new List<OtzProfile>();

        private readonly ILiveSyncService _liveSyncService;
        private readonly IActionRegisterRepository _actionRegisterRepository;

        public SyncService(IFacilityRepository facilityRepository, IPatientExtractRepository patientExtractRepository,
            IPatientArtExtractRepository patientArtExtractRepository,
            IPatientBaseLinesRepository patientBaseLinesRepository, IPatientLabRepository patientLabRepository,
            IPatientPharmacyRepository patientPharmacyRepository, IPatientStatusRepository patientStatusRepository,
            IPatientVisitRepository patientVisitRepository,
            IPatientAdverseEventRepository patientAdverseEventRepository,
            IAllergiesChronicIllnessRepository allergiesChronicIllnessRepository, IIptRepository iptRepository,
            IDepressionScreeningRepository depressionScreeningRepository,
            IContactListingRepository contactListingRepository, IGbvScreeningRepository gbvScreeningRepository,
            IEnhancedAdherenceCounsellingRepository enhancedAdherenceCounsellingRepository,
            IDrugAlcoholScreeningRepository drugAlcoholScreeningRepository, IOvcRepository ovcRepository,
            IOtzRepository otzRepository, ILiveSyncService liveSyncService,
            IActionRegisterRepository actionRegisterRepository)
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
            _allergiesChronicIllnessRepository = allergiesChronicIllnessRepository;
            _iptRepository = iptRepository;
            _depressionScreeningRepository = depressionScreeningRepository;
            _contactListingRepository = contactListingRepository;
            _gbvScreeningRepository = gbvScreeningRepository;
            _enhancedAdherenceCounsellingRepository = enhancedAdherenceCounsellingRepository;
            _drugAlcoholScreeningRepository = drugAlcoholScreeningRepository;
            _ovcRepository = ovcRepository;
            _otzRepository = otzRepository;
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
            else if (profile.GetType() == typeof(AllergiesChronicIllnessProfile))
            {
                SyncAllergiesChronicIllness(profile as AllergiesChronicIllnessProfile);
            }
            else if (profile.GetType() == typeof(IptProfile))
            {
                SyncIpt(profile as IptProfile);
            }
            else if (profile.GetType() == typeof(DepressionScreeningProfile))
            {
                SyncDepressionScreening(profile as DepressionScreeningProfile);
            }
            else if (profile.GetType() == typeof(ContactListingProfile))
            {
                SyncContactListing(profile as ContactListingProfile);
            }
            else if (profile.GetType() == typeof(GbvScreeningProfile))
            {
                SyncGbvScreening(profile as GbvScreeningProfile);
            }
            else if (profile.GetType() == typeof(EnhancedAdherenceCounsellingProfile))
            {
                SyncEnhancedAdherenceCounselling(profile as EnhancedAdherenceCounsellingProfile);
            }
            else if (profile.GetType() == typeof(DrugAlcoholScreeningProfile))
            {
                SyncDrugAlcoholScreening(profile as DrugAlcoholScreeningProfile);
            }
            else if (profile.GetType() == typeof(OvcProfile))
            {
                SyncOvc(profile as OvcProfile);
            }
            else if (profile.GetType() == typeof(OtzProfile))
            {
                SyncOtz(profile as OtzProfile);
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
            else if (profile.GetType() == typeof(List<AllergiesChronicIllnessProfile>))
            {
                SyncAllergiesChronicIllnessNew(profile as List<AllergiesChronicIllnessProfile>);
            }
            else if (profile.GetType() == typeof(List<IptProfile>))
            {
                SyncIptNew(profile as List<IptProfile>);
            }
            else if (profile.GetType() == typeof(List<DepressionScreeningProfile>))
            {
                SyncDepressionScreeningNew(profile as List<DepressionScreeningProfile>);
            }
            else if (profile.GetType() == typeof(List<ContactListingProfile>))
            {
                SyncContactListingNew(profile as List<ContactListingProfile>);
            }
            else if (profile.GetType() == typeof(List<GbvScreeningProfile>))
            {
                SyncGbvScreeningNew(profile as List<GbvScreeningProfile>);
            }
            else if (profile.GetType() == typeof(List<EnhancedAdherenceCounsellingProfile>))
            {
                SyncEnhancedAdherenceCounsellingNew(profile as List<EnhancedAdherenceCounsellingProfile>);
            }
            else if (profile.GetType() == typeof(List<DrugAlcoholScreeningProfile>))
            {
                SyncDrugAlcoholScreeningNew(profile as List<DrugAlcoholScreeningProfile>);
            }
            else if (profile.GetType() == typeof(List<OvcProfile>))
            {
                SyncOvcNew(profile as List<OvcProfile>);
            }
            else if (profile.GetType() == typeof(List<OtzProfile>))
            {
                SyncOtzNew(profile as List<OtzProfile>);
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
                _patientLabRepository.SyncNewPatients(_labProfiles, _facilityRepository, facIds,
                    _actionRegisterRepository);
            }

            if (queueName.ToLower().Contains("PatientPharmacyProfile".ToLower()))
            {
                Log.Debug($"batch processing {queueName}...");
                _patientPharmacyRepository.SyncNewPatients(_pharmacyProfiles, _facilityRepository, facIds,
                    _actionRegisterRepository);
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

        public void SyncAllergiesChronicIllness(AllergiesChronicIllnessProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _allergiesChronicIllnessRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncIpt(IptProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _iptRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncDepressionScreening(DepressionScreeningProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _depressionScreeningRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncContactListing(ContactListingProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _contactListingRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncGbvScreening(GbvScreeningProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _gbvScreeningRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncEnhancedAdherenceCounselling(EnhancedAdherenceCounsellingProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _enhancedAdherenceCounsellingRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncDrugAlcoholScreening(DrugAlcoholScreeningProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _drugAlcoholScreeningRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncOvc(OvcProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _ovcRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

        public void SyncOtz(OtzProfile profile)
        {
            profile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(profile.FacilityInfo, profile.PatientInfo);

            if (!(patientId == Guid.Empty || null == patientId))
            {
                profile.GenerateRecords(patientId.Value);
                _otzRepository.SyncNew(patientId.Value, profile.Extracts);
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

        public void SyncBaselineNew(List<PatientBaselineProfile> baselineProfile) =>
            baselineProfile.ForEach(SyncBaselineNew);

        public void SyncLabNew(List<PatientLabProfile> labProfile) => labProfile.ForEach(SyncLabNew);

        public void SyncPharmacyNew(List<PatientPharmacyProfile> patientPharmacyProfile) =>
            patientPharmacyProfile.ForEach(SyncPharmacyNew);

        public void SyncStatusNew(List<PatientStatusProfile> patientStatusProfile) =>
            patientStatusProfile.ForEach(SyncStatusNew);

        public void SyncVisitNew(List<PatientVisitProfile> profile) => profile.ForEach(SyncVisitNew);

        public void SyncAllergiesChronicIllnessNew(AllergiesChronicIllnessProfile profile)
        {
            profile.GeneratePatientRecord();
            _allergiesChronicIllnessProfiles.Add(profile);
        }

        public void SyncIptNew(IptProfile profile)
        {
            profile.GeneratePatientRecord();
            _iptProfiles.Add(profile);
        }

        public void SyncDepressionScreeningNew(DepressionScreeningProfile profile)
        {
            profile.GeneratePatientRecord();
            _depressionScreeningProfiles.Add(profile);
        }

        public void SyncContactListingNew(ContactListingProfile profile)
        {
            profile.GeneratePatientRecord();
            _contactListingProfiles.Add(profile);
        }

        public void SyncGbvScreeningNew(GbvScreeningProfile profile)
        {
            profile.GeneratePatientRecord();
            _gbvScreeningProfiles.Add(profile);
        }

        public void SyncEnhancedAdherenceCounsellingNew(EnhancedAdherenceCounsellingProfile profile)
        {
            profile.GeneratePatientRecord();
            _enhancedAdherenceCounsellingProfiles.Add(profile);
        }

        public void SyncDrugAlcoholScreeningNew(DrugAlcoholScreeningProfile profile)
        {
            profile.GeneratePatientRecord();
            _drugAlcoholScreeningProfiles.Add(profile);
        }

        public void SyncOvcNew(OvcProfile profile)
        {
            profile.GeneratePatientRecord();
            _ovcProfiles.Add(profile);
        }

        public void SyncOtzNew(OtzProfile profile)
        {
            profile.GeneratePatientRecord();
            _otzProfiles.Add(profile);
        }


        public void SyncvAdverseEventNew(List<PatientAdverseEventProfile> profile) =>
            profile.ForEach(SyncvAdverseEventNew);

        public void SyncAllergiesChronicIllnessNew(List<AllergiesChronicIllnessProfile> profile) =>
            profile.ForEach(SyncAllergiesChronicIllnessNew);

        public void SyncIptNew(List<IptProfile> profile) => profile.ForEach(SyncIptNew);

        public void SyncDepressionScreeningNew(List<DepressionScreeningProfile> profile) =>
            profile.ForEach(SyncDepressionScreeningNew);

        public void SyncContactListingNew(List<ContactListingProfile> profile) =>
            profile.ForEach(SyncContactListingNew);

        public void SyncGbvScreeningNew(List<GbvScreeningProfile> profile) => profile.ForEach(SyncGbvScreeningNew);

        public void SyncEnhancedAdherenceCounsellingNew(List<EnhancedAdherenceCounsellingProfile> profile) =>
            profile.ForEach(SyncEnhancedAdherenceCounsellingNew);

        public void SyncDrugAlcoholScreeningNew(List<DrugAlcoholScreeningProfile> profile) =>
            profile.ForEach(SyncDrugAlcoholScreeningNew);

        public void SyncOvcNew(List<OvcProfile> profile) => profile.ForEach(SyncOvcNew);
        public void SyncOtzNew(List<OtzProfile> profile) => profile.ForEach(SyncOtzNew);



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
            {
                try
                {
                    await _liveSyncService.SyncStats(_facilityRepository, facilities.Distinct().ToList());
                }
                catch (Exception e)
                {
                    Log.Error("Stats", e);
                }
            }

        }
    }
}
