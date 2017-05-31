using System.Collections.Generic;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IManageExportsView : IView<IManageExportsPresenter>
    {
        bool CanLoadExports { get; set; }
        bool CanSend { get; set; }
        bool CanDeleteAll { get; set; }
        List<string> ExportFiles { get; set; }
        List<ExportsViewModel> Exports { get; set; }
        int ExportsCount { get; set; }
        List<string> EventsSummary { get; set; }
        string Status { get; set; }
        int ProgessStatus { get; set; }

        void ShowPleaseWait();
        void ShowReady();
        void ShowMessage(string message);
        void ShowProgress(DProgress progress);
        void ClearExports();
        void ClearEvents();
    }
}