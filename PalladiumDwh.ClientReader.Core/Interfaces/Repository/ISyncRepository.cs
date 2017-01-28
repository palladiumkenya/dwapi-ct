using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface ISyncRepository
    {
        void SyncPatientData<TSrc, TDest>() where TSrc : TempPatientExtract where TDest : ClientPatientExtract;
    }
}