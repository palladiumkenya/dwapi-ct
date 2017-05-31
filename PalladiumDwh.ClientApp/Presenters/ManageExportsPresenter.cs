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
        public IManageExportsView View { get; }
        
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

        public Task LoadExisitingExportsAsync()
        {
            return Task.Run(() =>
            {
                
            });
        }

        public async Task<bool> ExtractExportsAsync()
        {
            bool extractComplete = true;
            View.Status = "Reading Export(s)...";
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;

            var progress = new Progress<DProgress>(ShowDProgress);

            try
            {
                var importManifests = await _importService.ExtractExportsAsync(View.ExportFiles, string.Empty, progress);
                View.EventsSummary = new List<string>{$"Read {importManifests.Count} files Successfuly!"};
            }
            catch (Exception e)
            {
                extractComplete = false;
                Log.Debug(e);
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }
            View.Status = "Reading Export(s) Complete!";
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
            }
            catch (Exception e)
            {
                Log.Debug(e);
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }
            View.Status = "Loading Export(s) Complete!";
            View.CanLoadExports = true;
            View.ShowReady();
        }

        public Task SendExportsAsync()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAllExports()
        {
            throw new System.NotImplementedException();
        }

        private void ShowDProgress(DProgress progress)
        {
            //View.Status = progress.ShowProgress();
            View.ShowProgress(progress);
        }
    }
}