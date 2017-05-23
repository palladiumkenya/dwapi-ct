using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IDashboardPresenter:IPresenter<IDashboardView>
    {

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
        void ShowSelectedExtract();

        #endregion

        #region Extracts Detail
        void LoadExtractList();
        void LoadExtractDetail();
        void LoadExtractDetailValidationErrors();
        void LoadExtractNotSent();
        void GenerateSummary();
        void SendExtracts();
        Task SendExtractsInParallel();

        Task ExportExtractsAsync();

        Task ImportExtractsAsync();
        #endregion
    }
}