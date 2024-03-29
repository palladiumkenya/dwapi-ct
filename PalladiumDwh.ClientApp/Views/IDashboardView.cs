﻿using System;
using System.Collections.Generic;
using PalladiumDwh.ClientApp.Events;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IDashboardView:IView<IDashboardPresenter>
    {
        #region EMR Information
        string EMRInfoTitle { get; set; }
        string EMR { get; set; }
        string Version { get; set; }
        string Project { get; set; }
        #endregion

        #region Extracts
        List<ExtractSetting> ExtractSettings { get; set; }
        List<ExtractsViewModel> Extracts { get; set; }
        ExtractsViewModel SelectedExtract { get;  }
        string Id { get; set; }

        bool LoadEmrEnabled { get; set; }
        bool CanChangeEmrSettings { get; set; }
        bool CanLoadEmr { get; set; }
        bool CanLoadCsv { get; set; }
        bool CanExport { get; set; }
        bool CanSend { get; set; }
        bool CanSendExports { get; set; }

        event EventHandler<EmrExtractLoadedEvent> EmrExtractLoaded;
        event EventHandler<CsvExtractLoadedEvent> CsvExtractLoaded;
        event EventHandler<ExtractExportedEvent> ExtractExported;
        event EventHandler<ExtractSentEvent> ExtractSent;
        void ClearExtracts();
        void UpdateStatus(ExtractsViewModel viewModel);
        void UpdateStats(ExtractsViewModel viewModel);
        #endregion

        #region ExtractsList
        string SelectedExtractSetting { get; }
        string SelectedExtractSettingDispaly { get; }
        object SelectedValidation { get; }
        List<ExtractSetting> ExtractSettingsList { get; set; }
        string RecordsHeader { get; set; }
        string ValidtionHeader{ get; set; }
        string SendHeader { get; set; }
        object ClientExtracts { get; set; }
        object ClientExtractsValidations { get; set; }
        object ClientExtractsNotSent { get; set; }
        void ClearExtractSettingsList();
        void ClearClientExtracts();
        void ClearClientExtractsValidations();
        void ClearClientExtractsNotSent();
        bool CanGenerateSummary { get; set; }
        List<string> ExportFiles { get; set; }
        List<string> CsvFiles { get; set; }
        int RecordsPage { get; set; }
        int RecordsPageSize { get; set; }
        int ValidationsPageSize { get; set; }
        int NotSentPageSize { get; set; }

        string RecordsViewShowing { get; set; }
        string ValidationsShowing { get; set; }
        string NotSentShowing { get; set; }
        #endregion

        List<string> EventSummaries { get; set; }
        string Status { get; set; }
        
        bool ConfirmAction(string action, string actionTilte);
        void CloseView();
        void UpdateProgress(int i);
        void ShowPleaseWait();
        void ShowReady();
        void ShowMessage(string message);
        void OpenFile(string filename);
    }
}