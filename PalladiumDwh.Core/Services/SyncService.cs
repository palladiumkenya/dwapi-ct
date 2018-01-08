﻿using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;
using System;
using System.Reflection;

namespace PalladiumDwh.Core.Services
{
  public class SyncService:ISyncService
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

        public SyncService(IFacilityRepository facilityRepository, IPatientExtractRepository patientExtractRepository, IPatientArtExtractRepository patientArtExtractRepository, IPatientBaseLinesRepository patientBaseLinesRepository, IPatientLabRepository patientLabRepository, IPatientPharmacyRepository patientPharmacyRepository, IPatientStatusRepository patientStatusRepository, IPatientVisitRepository patientVisitRepository)
        {
            _facilityRepository = facilityRepository;
            _patientExtractRepository = patientExtractRepository;
            _patientArtExtractRepository = patientArtExtractRepository;
            _patientBaseLinesRepository = patientBaseLinesRepository;
            _patientLabRepository = patientLabRepository;
            _patientPharmacyRepository = patientPharmacyRepository;
            _patientStatusRepository = patientStatusRepository;
            _patientVisitRepository = patientVisitRepository;
        }


        public void Sync(object profile)
        {
            if (profile.GetType() == typeof(Manifest))
            {
                SyncManifest(profile as Manifest);
            }

            if (profile.GetType() == typeof(PatientARTProfile))
            {
                SyncArt(profile as PatientARTProfile);
            }
            else if (profile.GetType() == typeof(PatientBaselineProfile))
            {
                SyncBaseline(profile as PatientBaselineProfile);
            }
            else if (profile.GetType() == typeof(PatientLabProfile))
            {
                SyncLab(profile as PatientLabProfile);
            }
            else if (profile.GetType() == typeof(PatientPharmacyProfile))
            {
                SyncPharmacy(profile as PatientPharmacyProfile);
            }
            else if (profile.GetType() == typeof(PatientStatusProfile))
            {
                SyncStatus(profile as PatientStatusProfile);
            }
            else if (profile.GetType() == typeof(PatientVisitProfile))
            {
                SyncVisit(profile as PatientVisitProfile);
            }
        }

        public void SyncManifest(Manifest manifest)
        {
            _patientExtractRepository.ClearManifest(manifest);
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
                _patientArtExtractRepository.SyncNew(profile.PatientInfo.Id,profile.Extracts);
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
                _patientVisitRepository.SyncNew(patientId.Value, profile.Extracts);
            }
        }

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
    }
}