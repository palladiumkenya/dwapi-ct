using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class SyncService:ISyncService
    {
        private readonly IReadExtractService _readExtractsService;
        private readonly ILoadPatientExtractCommand _extractCommand;
        private readonly ILoadPatientArtExtractCommand _artExtractCommand;

        public void SyncFacilities(IEnumerable<Facility> facilities)
        {
            throw new System.NotImplementedException();
        }

        public void SyncPatients(IEnumerable<PatientExtract> patients)
        {
            throw new System.NotImplementedException();
        }

        public void SyncPatientsArt(IEnumerable<PatientArtExtract> patientArtExtracts)
        {
            throw new System.NotImplementedException();
        }

        public void SyncPatientsBaselines(IEnumerable<PatientBaselinesExtract> patientBaselinesExtracts)
        {
            throw new System.NotImplementedException();
        }

        public void SyncPatientsLaboratory(IEnumerable<PatientLaboratoryExtract> patientLaboratoryExtracts)
        {
            throw new System.NotImplementedException();
        }

        public void SyncPatientsPharmacy(IEnumerable<PatientPharmacyExtract> patientPharmacyExtracts)
        {
            throw new System.NotImplementedException();
        }

        public void SyncPatientsVisit(IEnumerable<PatientVisitExtract> patientVisitExtracts)
        {
            throw new System.NotImplementedException();
        }

        public void SyncPatientsStatus(IEnumerable<PatientStatusExtract> patientStatusExtracts)
        {
            throw new System.NotImplementedException();
        }
    }
}