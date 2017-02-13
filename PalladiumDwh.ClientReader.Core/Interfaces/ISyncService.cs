using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncService
    {
        void Initialize();
        Task InitializeAsync();
        RunSummary Sync(ExtractSetting extract);
        Task<RunSummary> SyncAsync(ExtractSetting extract);
        void SyncAll();
        void SyncPatients();
        void SynPatientsArt();
        void SynPatientsBaselines();
        void SynPatientsStatus();
        void SynPatientsPharmacy();
        void SynPatientsLab();
        void SynPatientsVisits();
    }
}