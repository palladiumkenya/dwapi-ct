using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class ManageExportsPresenter:IManageExportsPresenter
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IImportService _importService;
        public IManageExportsView View { get; private set; }
        
        public ManageExportsPresenter(IManageExportsView view, IImportService importService)
        {
            View = view;
            View.Presenter = this;
            _importService = importService;
        }

        public void Initialize()
        {
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;
        }

        public async Task LoadExisitingExportsAsync()
        {
            View.Status = "Loading Export(s)...";
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;

            var progress = new Progress<DProgress>(ShowDProgress);

            try
            {
                var exports = new List<ExportsViewModel>();
                var siteManifests = await _importService.ReadExportsAsync(string.Empty, progress);
                foreach (var s in siteManifests)
                {
                    exports.AddRange(ExportsViewModel.Create(s));
                }
                View.Exports = exports;
                View.ExportsCount = exports.Count;
                View.CanDeleteAll = exports.Count > 0;
                View.ShowReady();
                
                
            }
            catch (Exception e)
            {
                Log.Debug(e);
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }

            View.Status = "Loading Export(s) Complete!";

            View.ShowReady();
            View.CanLoadExports = true;
            View.CanSend = View.CanDeleteAll = false;
            View.Status = "Deleting Export(s) Complete!";
        }

     

        public async Task<bool> ExtractExportsAsync()
        {
            bool extractComplete = false;
            View.Status = "Copying Export(s)...";
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;

            var progress = new Progress<DProgress>(ShowDProgress);

            try
            {
                var importManifests = await _importService.ExtractExportsAsync(View.ExportFiles, string.Empty, progress);
                extractComplete = importManifests.Count > 0;
                View.EventsSummary = new List<string>{$"Copied {importManifests.Count} file(s) Successfuly!"};
            }
            catch (Exception e)
            {
                Log.Debug(e);
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }
            View.Status = "Copying Export(s) Complete!";
            View.CanLoadExports = true;
            View.ShowReady();
            return extractComplete;
        }

        public async Task LoadExportsAsync()
        {
            View.Status = "Loading Export(s)...";
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;

            var progress = new Progress<DProgress>(ShowDProgress);

            try
            {
                var exports = new List<ExportsViewModel>();
                var siteManifests = await _importService.ReadExportsAsync(string.Empty,progress);
                foreach (var s in siteManifests)
                {
                    exports.AddRange( ExportsViewModel.Create(s));
                }
                View.Exports = exports;
                View.EventsSummary = new List<string> { $"Loaded {exports.Count} Sites" };
                View.ExportsCount = exports.Count;
                View.CanDeleteAll = exports.Count > 0;
                
            }
            catch (Exception e)
            {
                Log.Debug(e);
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }

            View.Status = "Loading Export(s) Complete!";

            View.CanLoadExports = true;
            View.ShowReady();
            View.Status = "Deleting Export(s) Complete!";
        }

        public Task SendExportsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAllExportsAsync()
        {
            View.Status = "Deleting Export(s)...";
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;

            var progress = new Progress<DProgress>(ShowDProgress);

            if (View.Exports.Count > 0)
            {
                var folders = View.Exports.Select(x => x.Location).ToList();

                await _importService.ClearExportsAsync(folders,progress);

            }
            View.Status = "Deleting Export(s) Complete!";

            
            await LoadExportsAsync();
            
            View.CanLoadExports = true;
            View.ShowReady();
            View.ClearEvents();
        }



        private void ShowDProgress(DProgress progress)
        {
            //View.Status = progress.ShowProgress();
            View.ShowProgress(progress);
        }
    }
}