using System.Collections.Generic;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IOptionView:IView<IOptionPresenter>
    {
        string SubOptionTitle { get; set; }
        List<EmrViewModel> Emrs { get; set; }
        EmrViewModel SelectedEmr { get; }
        string Id { get; set; }
        
         string Info { get; set; }
    }
}