using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncService
    {
        IEnumerable<Facility> SyncFacilities(IEnumerable<PatientExtractRow> patientExtractRows);
        void SyncPatients();
        /*
        void SyncPatientsArt(IEnumerable<PatientArtExtract> patientArtExtracts);
        void SyncPatientsBaselines(IEnumerable<PatientBaselinesExtract> patientBaselinesExtracts);
        void SyncPatientsLaboratory(IEnumerable<PatientLaboratoryExtract> patientLaboratoryExtracts);
        void SyncPatientsPharmacy(IEnumerable<PatientPharmacyExtract> patientPharmacyExtracts);
        void SyncPatientsVisit(IEnumerable<PatientVisitExtract> patientVisitExtracts);
        void SyncPatientsExtracts(IEnumerable<PatientStatusExtract> patientStatusExtracts);
        */
    }
}