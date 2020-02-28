using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Interfaces.Profiles;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessengerScheduler
    {
        void Start();

        Task Run<T>(List<T> patientProfile,string gateway) where T : IProfile;
        void Shutdown();
    }
}
