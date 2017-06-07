using System.Collections.Generic;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IExtractListView:IControlView<IExtractListPresenter>
    {
        string Header { get; set; }
        List<ExtractSetting> ExtractSettings { get; set; }
        void ClearExtractSettings();
    }
}