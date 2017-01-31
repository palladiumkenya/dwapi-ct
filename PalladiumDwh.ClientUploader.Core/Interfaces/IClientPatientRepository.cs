using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IClientPatientRepository:IRepository<ClientPatientExtract>
    {
        void UpdateProcessd(ClientPatientExtract patient);
    }
}