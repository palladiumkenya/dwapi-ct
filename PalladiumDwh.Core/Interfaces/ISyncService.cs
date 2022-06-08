using System;
using System.Collections.Generic;
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
        void SyncAdverseEvent(PatientAdverseEventProfile patientVisitProfile);

        void SyncAllergiesChronicIllness(AllergiesChronicIllnessProfile allergiesChronicIllnessprofile);
        void SyncIpt(IptProfile iptprofile);
        void SyncDepressionScreening(DepressionScreeningProfile depressionScreeningprofile);
        void SyncContactListing(ContactListingProfile contactListingprofile);
        void SyncGbvScreening(GbvScreeningProfile gbvScreeningprofile);
        void SyncEnhancedAdherenceCounselling(EnhancedAdherenceCounsellingProfile enhancedAdherenceCounsellingprofile);
        void SyncDrugAlcoholScreening(DrugAlcoholScreeningProfile drugAlcoholScreeningprofile);
        void SyncOvc(OvcProfile ovcprofile);
        void SyncOtz(OtzProfile otzprofile);

        void SyncCovid(CovidProfile covidprofile);
        void SyncDefaulterTracing(DefaulterTracingProfile defaulterTracingProfile);



        void SyncArtNew(PatientARTProfile profile);
        void SyncBaselineNew(PatientBaselineProfile baselineProfile);
        void SyncLabNew(PatientLabProfile labProfile);
        void SyncPharmacyNew(PatientPharmacyProfile patientPharmacyProfile);
        void SyncStatusNew(PatientStatusProfile patientStatusProfile);
        void SyncVisitNew(PatientVisitProfile profile);
        void SyncvAdverseEventNew(PatientAdverseEventProfile profile);
        void SyncAllergiesChronicIllnessNew(AllergiesChronicIllnessProfile profile);
        void SyncIptNew(IptProfile profile);
        void SyncDepressionScreeningNew(DepressionScreeningProfile profile);
        void SyncContactListingNew(ContactListingProfile profile);
        void SyncGbvScreeningNew(GbvScreeningProfile profile);
        void SyncEnhancedAdherenceCounsellingNew(EnhancedAdherenceCounsellingProfile profile);
        void SyncDrugAlcoholScreeningNew(DrugAlcoholScreeningProfile profile);
        void SyncOvcNew(OvcProfile profile);
        void SyncOtzNew(OtzProfile profile);

        void SyncCovidNew(CovidProfile profile);
        void SyncDefaulterTracingNew(DefaulterTracingProfile profile);


        void SyncArtNew(List<PatientARTProfile> profile);
        void SyncBaselineNew(List<PatientBaselineProfile> baselineProfile);
        void SyncLabNew(List<PatientLabProfile> labProfile);
        void SyncPharmacyNew(List<PatientPharmacyProfile> patientPharmacyProfile);
        void SyncStatusNew(List<PatientStatusProfile> patientStatusProfile);
        void SyncVisitNew(List<PatientVisitProfile> profile);
        void SyncvAdverseEventNew(List<PatientAdverseEventProfile> profile);
        void SyncAllergiesChronicIllnessNew(List<AllergiesChronicIllnessProfile> profile);
        void SyncIptNew(List<IptProfile> profile);
        void SyncDepressionScreeningNew(List<DepressionScreeningProfile> profile);
        void SyncContactListingNew(List<ContactListingProfile> profile);
        void SyncGbvScreeningNew(List<GbvScreeningProfile> profile);
        void SyncEnhancedAdherenceCounsellingNew(List<EnhancedAdherenceCounsellingProfile> profile);
        void SyncDrugAlcoholScreeningNew(List<DrugAlcoholScreeningProfile> profile);
        void SyncOvcNew(List<OvcProfile> profile);
        void SyncOtzNew(List<OtzProfile> profile);

        void SyncCovidNew(List<CovidProfile> profile);
        void SyncDefaulterTracingNew(List<DefaulterTracingProfile> profile);


        Facility GetFacility(int code);
        void SyncManifest(Manifest manifest);
        void InitList(string queueName);
        void Commit(string queueName);
    }
}
