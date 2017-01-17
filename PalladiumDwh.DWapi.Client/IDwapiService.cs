using System;
using System.Threading.Tasks;
using PalladiumDwh.DWapi.Client.Model;
using PalladiumDwh.DWapi.Client.Model.Profiles;

namespace PalladiumDwh.DWapi.Client
{
    public interface IDwapiService
    {
        Facility Get(int id);
        Guid? Post(PatientARTProfile profile);
    }
}
