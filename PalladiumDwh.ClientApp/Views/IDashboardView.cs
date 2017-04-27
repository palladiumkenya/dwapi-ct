using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PalladiumDwh.ClientApp.Events;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core.Interfaces;
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
        bool CanLoadEmr { get; set; }
        bool CanLoadCsv { get; set; }
        bool CanExport { get; set; }
        bool CanSend { get; set; }

        event EventHandler<EmrExtractLoadedEvent> EmrExtractLoaded;
        event EventHandler<CsvExtractLoadedEvent> CsvExtractLoaded;
        event EventHandler<ExtractExportedEvent> ExtractExported;
        event EventHandler<ExtractSentEvent> ExtractSent;

        void ClearExtracts();
        void UpdateStatus(ExtractsViewModel viewModel);
        #endregion

        #region ExtractsList
        string SelectedExtractSetting { get; }
        string SelectedExtractSettingDispaly { get; }
        object SelectedValidation { get; }
        List<ExtractSetting> ExtractSettingsList { get; set; }
        string RecordsHeader { get; set; }
        string ValidtionHeader{ get; set; }
        object ClientExtracts { get; set; }
        object ClientExtractsValidations { get; set; }
        object ClientExtractsValidationErrors { get; set; }
        void ClearExtractSettingsList();
        void ClearClientExtracts();
        void ClearClientExtractsValidations();
        void ClearClientExtractsValidationErrors();
        #endregion

        List<string> EventSummaries { get; set; }
        string Status { get; set; }
        bool ConfirmAction(string action, string actionTilte);
        void CloseView();
        void UpdateProgress(int i);
        void ShowPleaseWait();
        void ShowReady();
    }
}