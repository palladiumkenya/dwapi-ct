using System;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
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

        void SyncArtNew(PatientARTProfile profile);
        void SyncBaselineNew(PatientBaselineProfile baselineProfile);
        void SyncLabNew(PatientLabProfile labProfile);
        void SyncPharmacyNew(PatientPharmacyProfile patientPharmacyProfile);
        void SyncStatusNew(PatientStatusProfile patientStatusProfile);
        void SyncVisitNew(PatientVisitProfile profile);

        Facility GetFacility(int code);
        void SyncManifest(Manifest manifest);
        void InitList(string queueName);
        void Commit(string queueName);
    }
}