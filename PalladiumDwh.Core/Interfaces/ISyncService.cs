using System;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Profiles;

namespace PalladiumDwh.Core.Interfaces
{
    public interface ISyncService
    {
        Guid? SyncPatient(PatientProfile profile);
        void SyncArt(PatientARTProfile profile);
        void SyncBaseline(PatientBaselineProfile baselineProfile);
        void SyncLab(PatientLabProfile labProfile);
        void SyncPharmacy(PatientPharmacyProfile patientPharmacyProfile);
        void SyncStatus(PatientStatusProfile patientStatusProfile);
        void SyncVisit(PatientVisitProfile patientVisitProfile);
        Facility GetFacility(int code);
    }
}