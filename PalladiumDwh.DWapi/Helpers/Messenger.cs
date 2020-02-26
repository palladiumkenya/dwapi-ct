using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Shared.Interfaces.Profiles;
using System.Collections.Generic;

namespace PalladiumDwh.DWapi.Helpers
{
    public class Messenger
    {
        public void Send<T>(string gateway,MessagingSenderService service, List<T> patientProfile) where T: IProfile
        {
            foreach (var profile in patientProfile)
            {
                profile.GeneratePatientRecord();
                service.Send(profile, gateway);
            }
        }
    }
}