using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.DWapi.Client
{
    public interface IDwapiService
    {
        bool Post(PatientARTProfile profile);
        bool Post(PatientBaselineProfile profile);

        bool Post(PatientLabProfile profile);

        bool Post(PatientPharmacyProfile profile);

        bool Post(PatientStatusProfile profile);

        bool Post(PatientVisitProfile profile);
    }
}
