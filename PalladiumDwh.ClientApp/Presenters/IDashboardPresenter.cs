using PalladiumDwh.ClientApp.Views;

namespace PalladiumDwh.ClientApp.Presenters
{
    public interface IDashboardPresenter:IPresenter<IDashboardView>
    {
        #region EMR Information
        void InitializeEmrInfo();
        void LoadEmrInfo();
        #endregion

        #region Extracts
        void InitializeExtracts();
        void LoadExtracts();
        void ShowSelectedExtract();

        #endregion
    }
}