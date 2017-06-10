using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            View.Edit(false);
       }

        public async Task Load()
        {
            View.ShowPleaseWait();
            DatabaseConfig databaseConfig = null;
            var dbtypes = DatabaseType.GetAll();
            
            //Load DWAPIRemote Config
            await Task.Run(() =>
            {
                databaseConfig = _databaseSetupService.Read();
            });

           //databaseConfig = null;

            View.DatabaseTypes = dbtypes;
            View.DatabaseConfig = databaseConfig;

            if (null == databaseConfig)
            {
                //No connections 

                View.Edit(true);
                View.CanRefresh = View.CanTest = View.CanSave = true;
                View.CanEdit = false;
            }
            else
            {
                //Has connections 

                View.Edit(false);
                View.CanRefresh = View.CanTest = View.CanSave = false;
                View.CanEdit= true;
            }

            View.ShowReady();
        }

       public async Task Refresh()
       {
           View.CanRefresh = false;

            var progress = new Progress<DProgress>(ShowDProgress);
            View.ShowPleaseWait();
           try
           {
               View.Servers = await _databaseManager.GetServersList(View.DatabaseConfig.DatabaseType,progress);
           }
           catch (Exception e)
           {
               //View.ShowErrorMessage(Utility.GetErrorMessage(e));
           }
           View.ShowReady();
           View.CanRefresh = true;
        }

       public async Task Save()
       {
           View.CanTest = View.CanRefresh= View.CanEdit = View.CanSave = false;

            View.ShowPleaseWait();
           View.Status = "Saving...";
           try
           {
                await _databaseSetupService.Save(View.DatabaseConfig);
               View.ShowMessage("Settings have been saved, Application will now restart");
                Application.Restart();
            }
           catch (Exception e)
           {
               View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }
           await Load();
           View.ShowReady();
        }

       public async Task Test()
        {
            View.CanTest = View.CanRefresh= View.CanEdit= View.CanSave=false;

            View.ShowPleaseWait();
            View.Status = "Testing Connection...";
            try
            {
                var ok = await _databaseSetupService.CanConnect(View.DatabaseConfig);
                View.Status = $"Connection {(ok?"Successful": "FAILED")}";
                if (ok)
                {
                    View.ShowMessage(View.Status);
                }
                else
                {
                    View.ShowErrorMessage(View.Status);
                }
                    
            }
            catch (Exception e)
            {
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }
            View.ShowReady();
            View.CanTest = View.CanRefresh=View.CanSave = true;

        }

       public void Change()
       {
           View.Edit(true);
           View.CanEdit = false;
           View.CanSave = View.CanRefresh = View.CanTest = true;
       }

       private void ShowDProgress(DProgress progress)
       {
           //View.Status = progress.ShowProgress();
           View.ShowProgress(progress);
       }
    }
}
