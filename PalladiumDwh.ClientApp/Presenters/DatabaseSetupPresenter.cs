using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces;

namespace PalladiumDwh.ClientApp.Presenters
{
   public class DatabaseSetupPresenter:IDatabaseSetupPresenter
   {
       private readonly IDatabaseManager _databaseManager;
       private readonly IDatabaseSetupService _databaseSetupService;

        public IDatabaseSetupView View { get; }

       public DatabaseSetupPresenter(IDatabaseSetupView view, IDatabaseManager databaseManager,
           IDatabaseSetupService databaseSetupService)
       {
           View = view;
           View.Presenter = this;
           _databaseManager = databaseManager;
           _databaseSetupService = databaseSetupService;
       }

       public void Initialize()
       {
           View.CanSave = View.CanEdit = View.CanTest = false;
       }

        public Task Load()
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task Test()
        {
            throw new NotImplementedException();
        }
    }
}
