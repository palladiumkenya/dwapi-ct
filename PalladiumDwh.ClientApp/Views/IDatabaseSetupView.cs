using System.Collections.Generic;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IDatabaseSetupView: IView<IDatabaseSetupPresenter>
    {
        List<DatabaseType> DatabaseTypes { get; set; }
        DatabaseType SelectedDatabaseType { get; set; }
    List<string> Servers { get; set; }
        DatabaseConfig DatabaseConfig { get; set; }
        
        bool CanSave { get; set; }
        bool CanTest { get; set; }
        bool CanEdit { get; set; }
        bool CanRefresh { get; set; }

        string Status { get; set; }
        int ProgessStatus { get; set; }

        void Edit(bool status);
        void ShowPleaseWait();
        void ShowReady();
        void ShowMessage(string message);
        void ShowProgress(DProgress progress);
    }
}