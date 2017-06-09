using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using Microsoft.Build.Framework;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

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
           View.CanSave = View.CanEdit = View.CanRefresh = View.CanTest = false;
            View.Edit(View.CanSave);
       }

        public async Task Load()
        {
            View.ShowPleaseWait();
            DatabaseConfig configs = null;
            var dbtypes = DatabaseType.GetAll();
            
            //Load DWAPIRemote Config
            await Task.Run(() =>
            {
                configs = _databaseSetupService.Read();
            });

            View.DatabaseTypes = dbtypes;
            View.DatabaseConfig = configs;

            View.DatabaseConfig = null;

            if (null == View.DatabaseConfig)
            {
                View.Edit(true);
                View.CanRefresh = View.CanTest =  View.CanSave = true;
                View.CanEdit = false;
            }
            else
            {
                View.Edit(false);
                View.CanRefresh = View.CanTest = View.CanEdit = false;
                View.CanSave = true;
            }

            View.ShowReady();
        }

       public async Task Refresh()
       {
           var progress = new Progress<DProgress>(ShowDProgress);
            View.ShowPleaseWait();
           try
           {
               View.Servers = await _databaseManager.GetSqlServersList(progress);
           }
           catch (Exception e)
           {
               View.ShowErrorMessage(Utility.GetErrorMessage(e));
           }
           View.ShowReady();
        }

       public async Task Save()
       {
           View.ShowPleaseWait();
           View.Status = "Saving...";
           await Task.Run(() =>
           {
               _databaseSetupService.Save(View.DatabaseConfig);
           });

           await Load();
           View.ShowReady();
        }

       public async Task Test()
        {
            View.ShowPleaseWait();
            View.Status = "Testing Connection...";
            try
            {
                var ok = await _databaseSetupService.CanConnect(View.DatabaseConfig);
                View.Status = $"Connection {(ok?"Successful": "FAILED")}";
            }
            catch (Exception e)
            {
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }
            View.ShowReady();
        }

       private void ShowDProgress(DProgress progress)
       {
           //View.Status = progress.ShowProgress();
           View.ShowProgress(progress);
       }
    }
}
