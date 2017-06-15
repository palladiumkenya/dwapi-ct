using System;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncService
    {
        void Initialize();
        Task<int> InitializeAsync(IProgress<DProgress> dprogress = null);
        RunSummary Sync(ExtractSetting extract);
        Task<RunSummary> SyncAsync(ExtractSetting extract, Progress<ProcessStatus> progressPercent = null, IProgress<DProgress> progress = null);
        void SyncAll();
        void SyncPatients();
        void SynPatientsArt();
        void SynPatientsBaselines();
        void SynPatientsStatus();
        void SynPatientsPharmacy();
        void SynPatientsLab();
        void SynPatientsVisits();

        Task<RunSummary> SyncPatientsAsync();
        Task<RunSummary> SynPatientsArtAsync();
        Task<RunSummary> SynPatientsBaselinesAsync();
        Task<RunSummary> SynPatientsStatusAsync();
        Task<RunSummary> SynPatientsPharmacyAsync();
        Task<RunSummary> SynPatientsLabAsync();
        Task<RunSummary> SynPatientsVisitsAsync();
    }
}