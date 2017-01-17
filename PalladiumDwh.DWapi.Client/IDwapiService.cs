using System;
using System.Threading.Tasks;
using PalladiumDwh.DWapi.Client.Model;
using PalladiumDwh.DWapi.Client.Model.Profiles;

namespace PalladiumDwh.DWapi.Client
{
    public interface IDwapiService
    {
        Facility Get(int id);
        bool Post(PatientARTProfile profile);
        bool Post(PatientBaselineProfile profile);

        bool Post(PatientLabProfile profile);

        bool Post(PatientPharmacyProfile profile);

        bool Post(PatientStatusProfile profile);

        bool Post(PatientVisitProfile profile);
    }
}
