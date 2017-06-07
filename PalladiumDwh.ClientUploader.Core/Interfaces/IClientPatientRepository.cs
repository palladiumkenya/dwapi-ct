using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IClientPatientRepository : IRepository<ClientPatientExtract>
    {
        IEnumerable<Manifest> GetManifests();
        IEnumerable<ClientPatientExtract> GetAll(bool processed);
        void UpdatePush(ClientPatientExtract patient, string profileExtract, PushResponse response);
    }
}