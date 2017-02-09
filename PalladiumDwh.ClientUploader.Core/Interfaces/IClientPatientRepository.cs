using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IClientPatientRepository:IRepository<ClientPatientExtract>
    {
        IEnumerable<ClientPatientExtract> GetAll(bool processed);
        void UpdateProcessd(ClientPatientExtract patient,string profileExtract);
    }
}