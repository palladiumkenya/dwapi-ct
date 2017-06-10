﻿using System;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncCsvService
    {
        void Initialize();
        Task InitializeAsync();
        RunSummary Sync(ExtractSetting extract);
        Task<RunSummary> SyncAsync(ExtractSetting extract, Progress<ProcessStatus> progressPercent = null);
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