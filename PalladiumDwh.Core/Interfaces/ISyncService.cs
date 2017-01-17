using System;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Profiles;

namespace PalladiumDwh.Core.Interfaces
{
    public interface ISyncService
    {
        Guid? SyncCurrentPatient(Facility facility,PatientExtract patient);

        void SyncArt(PatientARTProfile artProfile);
        void SyncBaseline(PatientBaselineProfile baselineProfile);
        void SyncLab(PatientLabProfile labProfile);
        void SyncPharmacy(PatientPharmacyProfile patientPharmacyProfile);
        void SyncStatus(PatientStatusProfile patientStatusProfile);
        void SyncVisit(PatientVisitProfile patientVisitProfile);
    }
}