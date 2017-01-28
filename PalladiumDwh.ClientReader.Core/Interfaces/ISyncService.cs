using System.Collections.Generic;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncService
    {
        
        void SyncPatients(IEnumerable<PatientExtract> patients);
        void SyncPatientsArt(IEnumerable<PatientArtExtract> patientArtExtracts);
        void SyncPatientsBaselines(IEnumerable<PatientBaselinesExtract> patientBaselinesExtracts);
        void SyncPatientsLaboratory(IEnumerable<PatientLaboratoryExtract> patientLaboratoryExtracts);
        void SyncPatientsPharmacy(IEnumerable<PatientPharmacyExtract> patientPharmacyExtracts);
        void SyncPatientsVisit(IEnumerable<PatientVisitExtract> patientVisitExtracts);
        void SyncPatientsStatus(IEnumerable<PatientStatusExtract> patientStatusExtracts);
    }
}