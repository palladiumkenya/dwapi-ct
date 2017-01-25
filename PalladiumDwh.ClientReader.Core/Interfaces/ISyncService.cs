using System;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Profiles;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncService
    {
        void Sync(object profile);
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