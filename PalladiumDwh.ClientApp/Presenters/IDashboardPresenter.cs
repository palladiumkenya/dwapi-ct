﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IDashboardPresenter:IPresenter<IDashboardView>
    {
        Task RunMigrationsAsync();

        #region EMR Information
        void InitializeEmrInfo();
        void LoadEmrInfo();
        Task LoadEmrInfoAsync();
        #endregion

        #region Extracts
        void InitializeExtracts();
        void LoadExtractSettings();
        void LoadExtracts(List<ExtractSetting> extracts);
        void LoadCsvExtracts(List<ExtractSetting> extracts);
        Task CopyCsvAsync();
        void ShowSelectedExtract();
        void UpdateStatistics();
        void ShowStatistics();

        #endregion

        #region Extracts Detail
        void LoadExtractList();
        Task LoadExtractDetail();       
        void GenerateSummary();
        Task<bool> CheckSpot();
        void SendExtracts();
        Task SendExtractsInParallel();

        Task ExportExtractsAsync();

        #endregion
    }
}