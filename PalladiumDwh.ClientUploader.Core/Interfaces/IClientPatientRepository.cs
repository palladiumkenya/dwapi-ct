using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IClientPatientRepository:IRepository<ClientPatientExtract>
    {
        Manifest GetManifest();
        IEnumerable<ClientPatientExtract> GetAll(bool processed);
        void UpdatePush(ClientPatientExtract patient,string profileExtract,PushResponse response);
    }
}