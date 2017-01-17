using System;
using System.Reflection;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Profiles;

namespace PalladiumDwh.Core.Services
{
    public class SyncService:ISyncService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFacilityRepository _facilityRepository;
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IPatientArtExtractRepository _patientArtExtractRepository;

        public SyncService(IFacilityRepository facilityRepository, IPatientExtractRepository patientExtractRepository, IPatientArtExtractRepository patientArtExtractRepository)
        {
            _facilityRepository = facilityRepository;
            _patientExtractRepository = patientExtractRepository;
            _patientArtExtractRepository = patientArtExtractRepository;
        }

        public Guid? SyncCurrentPatient(Facility facility, PatientExtract patient)
        {
            Guid? syncPatientId = null;
            
            var facilityId = SyncFacility(facility);
            if (!(facilityId == Guid.Empty || null == facilityId))
            {
                patient.FacilityId = facilityId.Value;

                syncPatientId = SyncPatientDemographics(patient);
            }
            return syncPatientId;
        }

        private Guid? SyncFacility(Facility facility)
        {
            Guid? facilityId = null;
            
            try
            {
                facilityId = _facilityRepository.GetFacilityIdBCode(facility.Code);
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
            }

            if (facilityId == Guid.Empty || null == facilityId)
            {
                var newFacility = facility;

                try
                {
                    _facilityRepository.Insert(newFacility);
                    _facilityRepository.CommitChanges();
                    facilityId = newFacility.Id;
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                }
            }
            return facilityId;
        }

        private Guid? SyncPatientDemographics(PatientExtract patient)
        {
            Guid? patientId = null;
            try
            {
                patientId = _patientExtractRepository.GetPatientBy(patient.FacilityId, patient.PatientPID);
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
            }

            if (patientId == Guid.Empty || null == patientId)
            {
                var newPatient = patient;
                try
                {
                    _patientExtractRepository.Insert(newPatient);
                    _patientExtractRepository.CommitChanges();
                    patientId = newPatient.Id;
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                }
            }
            else
            {
                _patientExtractRepository.Update(patient);
            }
            
            return patientId;
        }

        public void SyncArt(PatientARTProfile artProfile)
        {
            artProfile.GeneratePatientRecord();
            var patientId = SyncCurrentPatient(artProfile.FacilityInfo, artProfile.PatientInfo);

            if (patientId != Guid.Empty && null != patientId)
            {
                artProfile.PatientInfo.Id = patientId.Value;
            }

            _patientArtExtractRepository.Clear(artProfile.PatientInfo.Id);
            _patientArtExtractRepository.CommitChanges();
         //   _patientArtExtractRepository.Insert(artProfile.ArtExtracts);

        }

        public void SyncBaseline(PatientBaselineProfile baselineProfile)
        {
            throw new System.NotImplementedException();
        }

        public void SyncLab(PatientLabProfile labProfile)
        {
            throw new System.NotImplementedException();
        }

        public void SyncPharmacy(PatientPharmacyProfile patientPharmacyProfile)
        {
            throw new System.NotImplementedException();
        }

        public void SyncStatus(PatientStatusProfile patientStatusProfile)
        {
            throw new System.NotImplementedException();
        }

        public void SyncVisit(PatientVisitProfile patientVisitProfile)
        {
            throw new System.NotImplementedException();
        }
    }
}