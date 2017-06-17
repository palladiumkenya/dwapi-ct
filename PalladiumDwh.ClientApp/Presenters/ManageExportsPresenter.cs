using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class ManageExportsPresenter:IManageExportsPresenter
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IImportService _importService;
        private IPushProfileService _pushProfileService;
        private IProfileManager _profileManager;
        public IManageExportsView View { get; private set; }
        
        public ManageExportsPresenter(IManageExportsView view, IImportService importService, IPushProfileService pushProfileService)
        {
            View = view;
            View.Presenter = this;
            _importService = importService;
            _pushProfileService = pushProfileService;
            _profileManager=new ProfileManager();
        }

        public void Initialize()
        {
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;
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
                var manifestsCount = importManifests.ToList().Count;
                extractComplete = manifestsCount > 0;
                View.EventsSummary = new List<string>{$"Copied {manifestsCount} file(s) Successfuly!"};
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

        public async Task LoadExportsAsync(bool startup)
        {
            var exports = new List<ExportsViewModel>();

            View.Status = "Loading Export(s)...";
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;

            var progress = new Progress<DProgress>(ShowDProgress);

            try
            {
                var siteManifests = await _importService.ReadExportsAsync(string.Empty, progress);
                foreach (var s in siteManifests)
                {
                    exports.AddRange(ExportsViewModel.Create(s));
                }
                View.Exports = exports;
                if(!startup)
                    View.EventsSummary = new List<string> { $"Loaded {exports.Count} Sites" };
            }
            catch (Exception e)
            {
                Log.Debug(e);
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }

            View.ExportsCount = exports.Count;
            View.CanSend = View.CanDeleteAll = exports.Count > 0;
            View.CanLoadExports = true;
            await Task.Delay(1);
            View.ShowReady();
        }

        public async Task SendExportsAsync()
        {
            View.Status = "Sending Export(s)...";
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;

            var progress = new Progress<DProgress>(ShowDProgress);

            int exportCount = View.ExportsCount;
            int count = 0;

            foreach (var export in View.Exports)
            {
                count++;
                string status = $"Sending Export {count} of {exportCount}";

                //Read Sites data
                var siteManifest =await _importService.GetSiteManifest(export.Location);

                //Disaggregate into profiles
                var siteProfiles = _profileManager.GenerateSiteProfiles(siteManifest);

                progress.ReportStatus(status);
                
                foreach (var siteProfile in siteProfiles)
                {
                    bool siteOk = false;
                    try
                    {
                        View.Status = $"{status} | Verifying MFLCode:{siteProfile.Manifest.SiteCode}";
                        
                        var response = await _pushProfileService.SpotAsync(siteProfile.Manifest);
                        siteOk = true;
                        View.Status = $"{status} | {response}";
                        
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e);
                        View.Status = $"{status} | MFLCode:{siteProfile.Manifest.SiteCode} Error";
                    }

                    if (siteOk)
                    {
                        var tasks = new List<Task<PushResponse>>();

                        int patientTotal=siteProfile.ClientPatientExtracts.Count;
                        int patientCount = 0;

                        foreach (var p in siteProfile.ClientPatientExtracts)
                        {
                            
                            //save file and response then delete files

                            patientCount++;
                            var extractsToSend = _profileManager.Generate(p);

                            PushResponse[] responses = null;

                            foreach (var e in extractsToSend)
                            {
                                tasks.Add(_pushProfileService.PushAsync(e, false));
                            }

                            progress.ReportStatus($"{status} | Site:{siteProfile.Manifest.SiteCode}", patientCount,
                                patientTotal);

                            
                            try
                            {
                              responses=  await Task.WhenAll(tasks.ToArray());
                            }
                            catch (Exception ex)
                            {
                                Log.Debug(ex);
                            }


                            // delete if success for all 
                            if (null != responses)
                            {
                                var badResponse = responses.FirstOrDefault(x => x.IsSuccess == false);
                                if (null == badResponse)
                                {
                                    //delete file
                                    var file = $"{export.Location.HasToEndsWith(@"\")}{p.Id}.dwh";
                                    await _importService.DeleteProfile(file);
                                }   
                            }


                        }
                    }
                    
                }

                await _importService.CleanUpExportsAsync(export.Location);

                await LoadExportsAsync(false);
            }

            await Task.Delay(1);
            View.CanLoadExports = View.CanSend = View.CanDeleteAll = false;
            View.ShowReady();
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

            
            await LoadExportsAsync(false);
            
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