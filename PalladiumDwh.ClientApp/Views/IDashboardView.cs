using System.Collections.Generic;
using System.Windows.Forms;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;

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
        List<ExtractsViewModel> Extracts { get; set; }
        ExtractsViewModel SelectedExtract { get;  }
        string Id { get; set; }
        void ClearExtracts();
        #endregion

        bool ConfirmAction(string action, string actionTilte);
        void CloseView(); 
    }
}