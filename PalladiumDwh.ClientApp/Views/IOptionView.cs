﻿using System.Collections.Generic;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;

namespace PalladiumDwh.ClientApp.Views
{
    public interface IOptionView:IView<IOptionPresenter>
    {
        string SubOptionTitle { get; set; }
        List<EmrViewModel> Emrs { get; set; }
        EmrViewModel SelectedEmr { get; }
        bool CanMarkDefault { get; set; }
        string Id { get; set; }
        
         string Info { get; set; }
        void SetupDefaults();
        bool ConfirmAction(string action,string actionTilte);
        void CloseView();
    }
}