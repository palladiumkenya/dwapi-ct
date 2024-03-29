﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using log4net;
using log4net.Util;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Reports;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Core.Reports;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Wordprocessing;
using PagedList;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class DashboardPresenter : IDashboardPresenter
    {
        
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IEMRRepository _emrRepository;
        private IDatabaseManager _databaseManager;

        private IProjectRepository _projectRepository;
        private readonly ISyncService _syncService;
        private readonly ISyncCsvService _syncCsvService;
        private readonly IClientPatientRepository _clientPatientRepository;
        private readonly IProfileManager _profileManager;
        private readonly IPushProfileService _pushService;

        private readonly IClientPatientArtExtractRepository _clientPatientArtExtractRepository;
        private readonly IClientPatientBaselinesExtractRepository _clientPatientBaselinesExtractRepository;
        private readonly IClientPatientExtractRepository _clientPatientExtractRepository;
        private readonly IClientPatientLaboratoryExtractRepository _clientPatientLaboratoryExtractRepository;
        private readonly IClientPatientPharmacyExtractRepository _clientPatientPharmacyExtractRepository;
        private readonly IClientPatientStatusExtractRepository _clientPatientStatusExtractRepository;
        private readonly IClientPatientVisitExtractRepository _clientPatientVisitExtractRepository;

        private readonly ITempPatientArtExtractRepository _tempPatientArtExtractRepository;
        private readonly ITempPatientBaselinesExtractRepository _tempPatientBaselinesExtractRepository;
        private readonly ITempPatientExtractRepository _tempPatientExtractRepository;
        private readonly ITempPatientLaboratoryExtractRepository _tempPatientLaboratoryExtractRepository;
        private readonly ITempPatientPharmacyExtractRepository _tempPatientPharmacyExtractRepository;
        private readonly ITempPatientStatusExtractRepository _tempPatientStatusExtractRepository;
        private readonly ITempPatientVisitExtractRepository _tempPatientVisitExtractRepository;

        private readonly ITempPatientArtExtractErrorSummaryRepository _tempPatientArtExtractErrorSummaryRepository;
        private readonly ITempPatientBaselinesExtractErrorSummaryRepository _tempPatientBaselinesExtractErrorSummaryRepository;
        private readonly ITempPatientExtractErrorSummaryRepository _tempPatientExtractErrorSummaryRepository;
        private readonly ITempPatientLaboratoryExtractErrorSummaryRepository _tempPatientLaboratoryExtractErrorSummaryRepository;
        private readonly ITempPatientPharmacyExtractErrorSummaryRepository _tempPatientPharmacyExtractErrorSummaryRepository;
        private readonly ITempPatientStatusExtractErrorSummaryRepository _tempPatientStatusExtractErrorSummaryRepository;
        private readonly ITempPatientVisitExtractErrorSummaryRepository _tempPatientVisitExtractErrorSummaryRepository;


        private ISummaryReport _summaryReport;
        private IExportService _exportService;
        private IImportService _importService;
        private IImportCsvService _importCsvService;

        private long timeTaken = 0;

        private EmrViewModel _emrmodel;
        private IEnumerable<ClientPatientExtract> _clientPatientExtracts;
        public IDashboardView View { get; }


        public DashboardPresenter()
        {
          
        }
        
        public DashboardPresenter(IDashboardView view, IDatabaseManager databaseManager, IProjectRepository projectRepository, ISyncService syncService,
            IClientPatientRepository clientPatientRepository, IProfileManager profileManager,
            IPushProfileService pushService,
            IClientPatientArtExtractRepository clientPatientArtExtractRepository, IClientPatientBaselinesExtractRepository clientPatientBaselinesExtractRepository, IClientPatientExtractRepository clientPatientExtractRepository, IClientPatientLaboratoryExtractRepository clientPatientLaboratoryExtractRepository, IClientPatientPharmacyExtractRepository clientPatientPharmacyExtractRepository, IClientPatientStatusExtractRepository clientPatientStatusExtractRepository, IClientPatientVisitExtractRepository clientPatientVisitExtractRepository,
        ITempPatientExtractRepository tempPatientExtractRepository, ITempPatientArtExtractRepository tempPatientArtExtractRepository, ITempPatientBaselinesExtractRepository tempPatientBaselinesExtractRepository, ITempPatientLaboratoryExtractRepository tempPatientLaboratoryExtractRepository, ITempPatientPharmacyExtractRepository tempPatientPharmacyExtractRepository, ITempPatientStatusExtractRepository tempPatientStatusExtractRepository, ITempPatientVisitExtractRepository tempPatientVisitExtractRepository,
            ITempPatientArtExtractErrorSummaryRepository tempPatientArtExtractErrorSummaryRepository, ITempPatientBaselinesExtractErrorSummaryRepository tempPatientBaselinesExtractErrorSummaryRepository, ITempPatientExtractErrorSummaryRepository tempPatientExtractErrorSummaryRepository, ITempPatientLaboratoryExtractErrorSummaryRepository tempPatientLaboratoryExtractErrorSummaryRepository, ITempPatientPharmacyExtractErrorSummaryRepository tempPatientPharmacyExtractErrorSummaryRepository, ITempPatientStatusExtractErrorSummaryRepository tempPatientStatusExtractErrorSummaryRepository, ITempPatientVisitExtractErrorSummaryRepository tempPatientVisitExtractErrorSummaryRepository,
            IExportService exportService,IImportService importService, IImportCsvService importCsvService,
        ISyncCsvService syncCsvService,
            IEMRRepository emrRepository
            )
        {
            view.Presenter = this;
            View = view;

            _emrRepository=emrRepository;
            _databaseManager = databaseManager;

            _projectRepository = projectRepository;
            _syncService = syncService;
            _clientPatientRepository = clientPatientRepository;

            _clientPatientArtExtractRepository = clientPatientArtExtractRepository;
            _clientPatientBaselinesExtractRepository = clientPatientBaselinesExtractRepository;
            _clientPatientExtractRepository = clientPatientExtractRepository;
            _clientPatientLaboratoryExtractRepository = clientPatientLaboratoryExtractRepository;
            _clientPatientPharmacyExtractRepository = clientPatientPharmacyExtractRepository;
            _clientPatientStatusExtractRepository = clientPatientStatusExtractRepository;
            _clientPatientVisitExtractRepository = clientPatientVisitExtractRepository;
           
            _tempPatientArtExtractRepository = tempPatientArtExtractRepository;
            _tempPatientBaselinesExtractRepository = tempPatientBaselinesExtractRepository;
            _tempPatientExtractRepository = tempPatientExtractRepository;
            _tempPatientLaboratoryExtractRepository = tempPatientLaboratoryExtractRepository;
            _tempPatientPharmacyExtractRepository = tempPatientPharmacyExtractRepository;
            _tempPatientStatusExtractRepository = tempPatientStatusExtractRepository;
            _tempPatientVisitExtractRepository = tempPatientVisitExtractRepository;

            _tempPatientArtExtractErrorSummaryRepository = tempPatientArtExtractErrorSummaryRepository;
            _tempPatientBaselinesExtractErrorSummaryRepository = tempPatientBaselinesExtractErrorSummaryRepository;
            _tempPatientExtractErrorSummaryRepository = tempPatientExtractErrorSummaryRepository;
            _tempPatientLaboratoryExtractErrorSummaryRepository = tempPatientLaboratoryExtractErrorSummaryRepository;
            _tempPatientPharmacyExtractErrorSummaryRepository = tempPatientPharmacyExtractErrorSummaryRepository;
            _tempPatientStatusExtractErrorSummaryRepository = tempPatientStatusExtractErrorSummaryRepository;
            _tempPatientVisitExtractErrorSummaryRepository = tempPatientVisitExtractErrorSummaryRepository;

            _profileManager = profileManager;
            _pushService = pushService;
            _exportService = exportService;
            _importService = importService;
            _importCsvService = importCsvService;
            _syncCsvService = syncCsvService;

            View.EmrExtractLoaded += View_EmrExtractLoaded;
            View.CsvExtractLoaded += View_CsvExtractLoaded;
            View.ExtractExported += View_ExtractExported;
            View.ExtractSent += View_ExtractSent;

            View.RecordsPageSize = 50;
        }

      

        public void Initialize()
        {
            View.Title = $"National Data Warehouse Client | v{Application.ProductVersion}";
            View.CanExport = View.CanLoadCsv = View.CanSend = View.CanLoadEmr =View.CanGenerateSummary= false;
            View.Status = "Starting ,please wait ...";
        }

        #region EMR Information

        public async Task RunMigrationsAsync()
        {
            var progress = new Progress<DProgress>(ShowDProgress);
            await _databaseManager.RunUpdateAsync(progress);
            View.Status = "Starting ,please wait ...";
        }

        public void InitializeEmrInfo()
        {
            View.Project = View.EMR = View.Version = string.Empty;
        }

        public void LoadEmrInfo()
        {
            _projectRepository = Program.IOC.GetInstance<IProjectRepository>();
            var project = _projectRepository.GetActiveProject();
            _emrmodel = EmrViewModel.Create(project);

            View.EMR = _emrmodel.EMR;
            View.Version = _emrmodel.Version;
            View.Project = _emrmodel.Project;
        }

        public async Task LoadEmrInfoAsync()
        {
            Log.Debug("Loading default EMR...");
            _projectRepository = Program.IOC.GetInstance<IProjectRepository>();
            Project project = null;
            try
            {
                project = await _projectRepository.GetActiveProjectAsync();
            }
            catch (Exception e)
            {
                View.ShowErrorMessage(e.Message);
                View.Status = e.Message;
            }
            
            if(null==project)
                return;
            

            _emrmodel = EmrViewModel.Create(project);

            View.EMR = _emrmodel.EMR;
            View.Version = _emrmodel.Version;
            View.Project = _emrmodel.Project;
        }

        #endregion

        #region Extracts

        public void InitializeExtracts()
        {
            View.ClearExtracts();
        }

        public void LoadExtractSettings()
        {
            if (null == _emrmodel)
                return;

            var extracts = _emrmodel
                .ExtractSettings
                .OrderBy(x => x.Rank)
                .ToList();
            View.ExtractSettings = extracts;
            View.Extracts = ExtractsViewModel.CreateList(extracts);
            View.CanLoadEmr = View.CanLoadCsv = View.CanExport = View.CanSend = View.ExtractSettings.Count > 0;
            View.CanExport = View.CanLoadCsv = View.CanSend = View.CanLoadEmr = true;
            View.Status = "Ready";
            try
            {
                var extractWithSQL = extracts.First(x => x.ExtractSql.Trim().Length > 0);

                View.LoadEmrEnabled = null != extractWithSQL;
            }
            catch
            {
                View.LoadEmrEnabled = false;
            }
            
        }

        //TODO: consider Parallel processing
        public async void LoadExtracts(List<ExtractSetting> extracts)
        {
            var dprogress = new Progress<DProgress>(ShowDProgress);

            string logMessages = "Loading EMR extracts";
            Log.Debug($"{logMessages}...");

            var watch = System.Diagnostics.Stopwatch.StartNew();
            View.CanLoadEmr = false;
            View.Status = "initializing loading...";
            int total = extracts.Count;
            int count = 0;

            //clear db

            Log.Debug($"{logMessages} [Clearing database]...");
            try
            {
                await _syncService.InitializeAsync(dprogress);
            }
            catch (Exception e)
            {
                Log.Debug(e);
                string errorMessage = "Error clearing data!";
                View.ShowErrorMessage($"{errorMessage},{e.Message}");
                View.Status = "Error clearing data!";
                View.CanLoadEmr = true;
                return;                
            }

            await Task.Delay(1);

            //Patient Extract
            var priorityExtracts = extracts.Where(x => x.IsPriority).OrderBy(x => x.Rank);

            foreach (var extract in priorityExtracts)
            {
                Log.Debug($"{logMessages} [{extract}]...");
                var progressIndicator = new Progress<ProcessStatus>(ReportProgress);
                var dpprogress = new Progress<DProgress>(ShowDProgressViewModel);
                count++;
                var vm = new ExtractsViewModel(extract) {Status = "loading..."};
                View.UpdateStatus(vm);

                //View.Status = $"loading {count}/{total} ({extract.Display}) ...";
                View.Status = $"loading...";

                RunSummary summary = null;

                try
                {
                    summary = await _syncService.SyncAsync(extract, progressIndicator, dpprogress);
                }
                catch (Exception e)
                {
                    View.ShowErrorMessage(e.Message);
                    View.Status = "Error loading data! Check logs for details";
                }

                vm = new ExtractsViewModel(extract)
                {
                    Total = (null == summary ? 0 : summary.SyncSummary.Total),
                    Status = (null == summary ? "Error loading !" : summary.ToString())
                };
                View.UpdateStatus(vm);
                Log.Debug($"{logMessages} [{extract}] Complete");
            }

            await Task.Delay(1);
            Log.Debug($"Loading other extracts...");

            //Other Extracts
            var otherExtracts = extracts.Where(x => x.IsPriority == false).OrderBy(x => x.Rank);

            var tasks =new List<Task>();

            foreach (var extract in otherExtracts)
            {
                Log.Debug($"{logMessages} [{extract}]...");
                var progressIndicator = new Progress<ProcessStatus>(ReportProgress);
                count++;
                var vm = new ExtractsViewModel(extract) {Status = "loading..."};
                View.UpdateStatus(vm);               
                tasks.Add(GetTask(_syncService,extract));
            }
                        
            await Task.WhenAll(tasks.ToArray());

            await Task.Delay(1);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            timeTaken += elapsedMs;
            var msg = $"Load Completed ({count} of {total}) Extracts Time: {elapsedMs*0.001} s !";
            UpdateUi(msg);

            Log.Debug($"{msg}");

            this.View.EventSummaries=new List<string>() {msg};


            await LoadExtractDetail();
            UpdateStatistics();
            View.CanLoadEmr = true;
            View.ShowReady();
        }

        public async void LoadCsvExtracts(List<ExtractSetting> extracts)
        {
            var dprogress = new Progress<DProgress>(ShowDProgress);

            string logMessages = "Loading Csv extracts";
            Log.Debug($"{logMessages}...");

            var watch = System.Diagnostics.Stopwatch.StartNew();
            View.CanLoadEmr = false;
            View.Status = "initializing loading csv...";
            int total = extracts.Count;
            int count = 0;

            //clear db
            Log.Debug($"{logMessages} [Clearing database]...");
            try
            {
                await _syncCsvService.InitializeAsync(View.CsvFiles, dprogress);
            }
            catch (Exception e)
            {
                Log.Debug(e);
                string errorMessage = "Error clearing data!";
                View.ShowErrorMessage($"{errorMessage},{e.Message}");
                View.Status = "Error clearing data!";
                View.CanLoadEmr = true;
                return;
            }

            await Task.Delay(1);

            //Patient Extract
            var priorityExtracts = extracts.Where(x => x.IsPriority).OrderBy(x => x.Rank);

          
            foreach (var extract in priorityExtracts)
            {
                var ecsv = $@"\{extract.ExtractCsv.HasToEndsWith(".csv")}";

                var csv = View.CsvFiles.FirstOrDefault(x => x.ToLower().Contains(ecsv.ToLower()));

                Log.Debug($"{logMessages} [{extract}]...");
                var progressIndicator = new Progress<ProcessStatus>(ReportProgress);
                var dpprogress = new Progress<DProgress>(ShowDProgressViewModel);
                count++;
                var vm = new ExtractsViewModel(extract) { Status = "loading..." };
                View.UpdateStatus(vm);

                //View.Status = $"loading {count}/{total} ({extract.Display}) ...";
                View.Status = $"loading...";

                RunSummary summary = null;

                try
                {
                    //csv priorities
                    summary = await _syncCsvService.SyncAsync(extract, csv, progressIndicator, dpprogress);
                }
                catch (Exception e)
                {                    
                    View.ShowErrorMessage(e.Message);
                    View.Status = "Error loading data! Check logs for details";
                }

                vm = new ExtractsViewModel(extract)
                {
                    Total = (null == summary ? 0 : summary.SyncSummary.Total),
                    Status = (null == summary ? "Error loading !" : summary.ToString())
                };
                View.UpdateStatus(vm);
                Log.Debug($"{logMessages} [{extract}] Complete");
            }

            await Task.Delay(1);
            Log.Debug($"Loading other extracts...");

            //Other Extracts
            var otherExtracts = extracts.Where(x => x.IsPriority == false).OrderBy(x => x.Rank);

            var tasks = new List<Task>();

            foreach (var extract in otherExtracts)
            {
                var ecsv = $@"\{extract.ExtractCsv.HasToEndsWith(".csv")}";
                var csv = View.CsvFiles.FirstOrDefault(x => x.ToLower().Contains(ecsv.ToLower()));

                Log.Debug($"{logMessages} [{extract}]...");
                var progressIndicator = new Progress<ProcessStatus>(ReportProgress);
                count++;
                var vm = new ExtractsViewModel(extract) { Status = "loading..." };
                View.UpdateStatus(vm);
                tasks.Add(GetTask(_syncCsvService, extract,csv));
            }

            await Task.WhenAll(tasks.ToArray());
            await Task.Delay(1);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            timeTaken += elapsedMs;
            var msg = $"Load Completed ({count} of {total}) Extracts Time: {elapsedMs * 0.001} s !";
            UpdateUi(msg);

            Log.Debug($"{msg}");

            this.View.EventSummaries = new List<string>() { msg };


            await LoadExtractDetail();
            UpdateStatistics();
            View.CanLoadEmr = true;
            View.ShowReady();
        }

        public async Task CopyCsvAsync()
        {

            View.Status = "Copying Csv(s)...";


            View.CanLoadCsv = View.CanSend = View.CanExport = false;

            var progress = new Progress<DProgress>(ShowDProgress);

            try
            {
                var csvFiles = await _importCsvService.CopyCsvFilesAsync(View.CsvFiles, string.Empty, progress);
                var csvFilesCount = csvFiles.ToList().Count;
                View.CsvFiles = csvFiles;
                View.EventSummaries = new List<string> { $"Copied {csvFilesCount} csv file(s) Successfuly!" };                
            }
            catch (Exception e)
            {
                Log.Debug(e);
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }

            View.Status = "Copying Csv(s) Complete!";
            View.CanLoadCsv = View.CanSend = View.CanExport = true;

            View.ShowReady();


        }

        private void ReportProgress(ProcessStatus processStatus)
        {
            return;
            var vm = new ExtractsViewModel(processStatus.ExtractSetting)
            {
                Total =processStatus.Progress,
                Status = $"loaded {processStatus.Progress}"
            };
            View.UpdateStatus(vm);
        }

        private async Task<RunSummary> GetTask(ISyncCsvService service, ExtractSetting extractSetting,string csvFile)
        {
            RunSummary summary = null;

            var progressIndicator = new Progress<ProcessStatus>(ReportProgress);
            var dprogress = new Progress<DProgress>(ShowDProgressViewModel);

            try
            {
                summary = await service.SyncAsync(extractSetting,csvFile, progressIndicator,dprogress);
            }
            catch (Exception e)
            {
                Log.Debug(e);
                View.ShowErrorMessage(e.Message);
                View.Status = "Error loading data! Check logs for details";
            }

            var vm = new ExtractsViewModel(extractSetting)
            {
                Total = (null == summary ? 0 : summary.SyncSummary.Total),
                Status = (null == summary ? "Error loading !" : summary.ToString())
            };
            View.UpdateStatus(vm);

            return summary;
        }

        private async Task<RunSummary> GetTask(ISyncService service, ExtractSetting extractSetting)
        {
            RunSummary summary = null;

            var progressIndicator = new Progress<ProcessStatus>(ReportProgress);
            var dpprogress = new Progress<DProgress>(ShowDProgressViewModel);

            try
            {
                summary = await service.SyncAsync(extractSetting, progressIndicator,dpprogress);
            }
            catch (Exception e)
            {
                Log.Debug(e);
                View.ShowErrorMessage(e.Message);
                View.Status = "Error loading data! Check logs for details";
            }


            var vm = new ExtractsViewModel(extractSetting)
            {
                Total = (null == summary ? 0 : summary.SyncSummary.Total),
                Status = (null == summary ? "Error loading !": summary.ToString())
            };
            View.UpdateStatus(vm);

            return summary;
        }

        public void ShowSelectedExtract()
        {
            //TODO: Enable/Disable some stuff

            var emr = View.SelectedExtract;
            if (null != emr)
            {
                View.Id = emr.Id.ToString();
            }
        }

        public void UpdateStatistics()
        {
            View.Status = "Updating dashboard...";
            var progress = new Progress<DProgress>(ShowDProgress);          

            try
            {
                _emrRepository.ResetSendStats();

                if (null != View.ExtractSettings)
                {
                    int total = View.ExtractSettings.Count;
                    int count = 0;
                    foreach (var extractSetting in View.ExtractSettings)
                    {
                        var eventHistory = _emrRepository.GetStats(extractSetting.Id);
                        if (null != eventHistory)
                        {
                            var vm = ExtractsViewModel.CreateHistory(eventHistory, extractSetting.Id);
                            View.UpdateStats(vm);
                        }
                        View.Status = $"Updating dashboard {Utility.GetPercentage(count, total)}... ";
                    }
                }
            }
            catch (Exception e)
            {
                Log.Debug(e);
                View.ShowErrorMessage(Utility.GetErrorMessage(e));
            }

            View.Status = "Ready";
            View.ShowReady();
        }

        public void ShowStatistics()
        {
            try
            {
                if (null != View.ExtractSettings)
                {
                    int total = View.ExtractSettings.Count;
                    int count = 0;
                    foreach (var extractSetting in View.ExtractSettings)
                    {
                        var eventHistory = _emrRepository.GetStats(extractSetting.Id);
                        if (null != eventHistory)
                        {
                            var vm = ExtractsViewModel.CreateHistory(eventHistory, extractSetting.Id);
                            View.UpdateStats(vm);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Debug(e);
            }
        }


        private  void View_EmrExtractLoaded(object sender, Events.EmrExtractLoadedEvent e)
        {
            View.ShowPleaseWait();
            LoadExtracts(e.Extracts);
        }
        private void View_CsvExtractLoaded(object sender, Events.CsvExtractLoadedEvent e)
        {
            throw new System.NotImplementedException();
        }
        private async void View_ExtractSent(object sender, Events.ExtractSentEvent e)
        {
            await SendExtractsInParallel();
            //SendExtracts();
        }

        private async void View_ExtractExported(object sender, Events.ExtractExportedEvent e)
        {
            await ExportExtractsAsync();
        }


        #endregion

        #region Extracts Detail

        public void LoadExtractList()
        {
            var list = View.ExtractSettings;
            if(null!=list)
                View.ExtractSettingsList = View.ExtractSettings;
        }

        public async Task LoadExtractDetail()
        {
            

            View.RecordsHeader = $"{View.SelectedExtractSettingDispaly} Records";
            View.ValidtionHeader = $"{View.SelectedExtractSettingDispaly} Validation Summary";
            View.SendHeader = $"{View.SelectedExtractSettingDispaly} Send Summary";

            View.ClearClientExtracts();
            View.ClearClientExtractsValidations();
            View.ClearClientExtractsNotSent();


            switch (View.SelectedExtractSetting)
            {
                case "TempPatientStatusExtract":
                {
                    var  recordsList = await Task.Run(() => _clientPatientStatusExtractRepository.GetAll(View.RecordsPage, View.RecordsPageSize));
                    View.ClientExtracts = recordsList.ToList();
                    View.RecordsViewShowing = $@"Showing {recordsList.Count} of {recordsList.TotalItemCount}";
                        
                    var validationList= await Task.Run(() => _tempPatientStatusExtractErrorSummaryRepository.GetAll(1, 100));
                    View.ClientExtractsValidations = validationList.ToList();

                    var notSentList = await Task.Run(() => _clientPatientStatusExtractRepository.GetAllSendErrors(1, 100));
                    View.ClientExtractsNotSent = notSentList.ToList();
                        
                    break;
                }
                case "TempPatientArtExtract":
                {
                    View.ClientExtracts =
                        await Task.Run(() => _clientPatientArtExtractRepository.GetAll(1, View.RecordsPageSize).ToList());
                    View.ClientExtractsValidations = await Task.Run(() => _tempPatientArtExtractErrorSummaryRepository
                        .GetAll(1, 100)
                        .ToList());
                    View.ClientExtractsNotSent = await Task.Run(() => _clientPatientArtExtractRepository
                        .GetAllSendErrors(1, 100)
                        .ToList());
                    break;
                }
                case "TempPatientBaselinesExtract":
                {
                    View.ClientExtracts = await Task.Run(
                        () => _clientPatientBaselinesExtractRepository.GetAll(1, View.RecordsPageSize).ToList());
                    View.ClientExtractsValidations =
                        await Task.Run(() => _tempPatientBaselinesExtractErrorSummaryRepository.GetAll(1, 100)
                            .ToList());
                    View.ClientExtractsNotSent = await Task.Run(() => _clientPatientBaselinesExtractRepository
                        .GetAllSendErrors(1, 100)
                        .ToList());


                    break;
                }

                case "TempPatientVisitExtract":
                {
                    View.ClientExtracts =
                        await Task.Run(() => _clientPatientVisitExtractRepository.GetAll(1, View.RecordsPageSize).ToList());
                    View.ClientExtractsValidations = await Task.Run(() => _tempPatientVisitExtractErrorSummaryRepository
                        .GetAll(1, 100)
                        .ToList());
                    View.ClientExtractsNotSent = await Task.Run(() => _clientPatientVisitExtractRepository
                        .GetAllSendErrors(1, 50)
                        .ToList());

                    break;
                }
                case "TempPatientPharmacyExtract":
                {
                    View.ClientExtracts =
                        await Task.Run(() => _clientPatientPharmacyExtractRepository.GetAll(1, View.RecordsPageSize).ToList());
                    View.ClientExtractsValidations =
                        await Task.Run(() => _tempPatientPharmacyExtractErrorSummaryRepository.GetAll(1, 100).ToList());
                    View.ClientExtractsNotSent = await Task.Run(() => _clientPatientPharmacyExtractRepository
                        .GetAllSendErrors(1, 50)
                        .ToList());

                    break;
                }

                case "TempPatientLaboratoryExtract":
                {
                    View.ClientExtracts = await Task.Run(
                        () => _clientPatientLaboratoryExtractRepository.GetAll(1, View.RecordsPageSize).ToList());
                    View.ClientExtractsValidations =
                        await Task.Run(
                            () => _tempPatientLaboratoryExtractErrorSummaryRepository.GetAll(1, 100).ToList());
                    View.ClientExtractsNotSent = await Task.Run(() => _clientPatientLaboratoryExtractRepository
                        .GetAllSendErrors(1, 50)
                        .ToList());

                    break;
                }
                default:
                {
                    View.ClientExtracts = await Task.Run(() => _clientPatientExtractRepository.GetAll(1, View.RecordsPageSize).ToList());
                    View.ClientExtractsValidations = await Task.Run(() => _tempPatientExtractErrorSummaryRepository
                        .GetAll(1, 100)
                        .ToList());
                    View.ClientExtractsNotSent =
                        await Task.Run(() => _clientPatientExtractRepository.GetAllSendErrors(1, 100).ToList());

                    break;
                }
            }

            //View.RecordsViewShowing=$"Showing {}of {}"
        }

        public async void GenerateSummary()
        {
            var progress=new Progress<DProgress>(ShowDProgress);

            View.CanGenerateSummary = false;
            if (null == View.ClientExtractsValidations)
            {
                View.CanGenerateSummary = true;
                return;
            }

            

            switch (View.SelectedExtractSetting)
            {
                case "TempPatientStatusExtract":
                {
                    var summary = await Task.Run(() => _tempPatientStatusExtractErrorSummaryRepository.GetAll()); // (List<TempPatientStatusExtractErrorSummary>)View.ClientExtractsValidations;
                        _summaryReport = new SummaryReport();
                        var summaryFile = await Task.Run(() => _summaryReport.CreateExcelErrorSummary(summary, "PatientStatusExtract", string.Empty, progress));
                    View.OpenFile(summaryFile);
                    break;
                }
                case "TempPatientArtExtract":
                {
                    var summary = await Task.Run(() => _tempPatientArtExtractErrorSummaryRepository.GetAll()); // (List<TempPatientArtExtractErrorSummary>)View.ClientExtractsValidations;
                        _summaryReport = new SummaryReport();
                        var summaryFile = await Task.Run(() => _summaryReport.CreateExcelErrorSummary(summary, "PatientArtExtract", string.Empty, progress));
                    View.OpenFile(summaryFile);
                        break;
                }
                case "TempPatientBaselinesExtract":
                {
                    var summary = await Task.Run(() => _tempPatientBaselinesExtractErrorSummaryRepository.GetAll()); // (List<TempPatientBaselinesExtractErrorSummary>)View.ClientExtractsValidations;
                        _summaryReport = new SummaryReport();
                        var summaryFile = await Task.Run(() => _summaryReport.CreateExcelErrorSummary(summary, "PatientBaselinesExtract", string.Empty, progress));
                    View.OpenFile(summaryFile);
                        break;
                }

                case "TempPatientVisitExtract":
                {
                    var summary = await Task.Run(() => _tempPatientVisitExtractErrorSummaryRepository.GetAll()); // (List<TempPatientVisitExtractErrorSummary>)View.ClientExtractsValidations;
                        _summaryReport = new SummaryReport();

                        var summaryFile = await Task.Run(() => _summaryReport.CreateExcelErrorSummary(summary, "PatientVisitExtract", string.Empty, progress));
                    View.OpenFile(summaryFile);
                        break;
                }
                case "TempPatientPharmacyExtract":
                {
                    var summary = await Task.Run(() => _tempPatientPharmacyExtractErrorSummaryRepository.GetAll()); // (List<TempPatientPharmacyExtractErrorSummary>)View.ClientExtractsValidations;
                        _summaryReport = new SummaryReport();
                        var summaryFile = await Task.Run(() => _summaryReport.CreateExcelErrorSummary(summary, "PatientPharmacyExtract", string.Empty, progress));
                    View.ShowMessage($"Summary Generated :{summaryFile}");
                    break;
                }

                case "TempPatientLaboratoryExtract":
                {
                    var summary = await Task.Run(() => _tempPatientLaboratoryExtractErrorSummaryRepository.GetAll()); // (List<TempPatientLaboratoryExtractErrorSummary>)View.ClientExtractsValidations;
                        _summaryReport = new SummaryReport();

                        var summaryFile = await Task.Run(() => _summaryReport.CreateExcelErrorSummary(summary, "PatientLaboratoryExtract", string.Empty, progress));
                    View.OpenFile(summaryFile);
                        break;
                }
                default:
                {
                    var summary = await Task.Run(() => _tempPatientExtractErrorSummaryRepository.GetAll()); //(List<TempPatientExtractErrorSummary>)View.ClientExtractsValidations;
                    _summaryReport = new SummaryReport();
                        var summaryFile = await Task.Run(() => _summaryReport.CreateExcelErrorSummary(summary, "PatientExtact", string.Empty, progress));
                    View.OpenFile(summaryFile);
                        break;
                }
            }

            View.CanGenerateSummary = true;
            View.ShowReady();
            //await Task.Run(() => _tempPatientExtractErrorSummaryRepository.GetAll());
        }

        public async Task<bool> CheckSpot()
        {
            bool spotOk = true;
            //SPOT
            var spotprogress = new Progress<DProgress>(ShowDProgress);
            var manifests = _clientPatientRepository.GetManifests().ToList();

            foreach (var m in manifests)
            {
                try
                {
                    await _pushService.SpotAsync(m, spotprogress);
                }
                catch (Exception ex)
                {
                    spotOk = false;
                    Log.Debug(ex);

                    View.ShowErrorMessage(
                        $"Failed to Send data! Error:{Utility.GetErrorMessage(ex)}");
                }
            }

            return spotOk;
        }

        //TODO: consider Parallel processing
        public async void SendExtracts()
        {
            

            View.Status="sending...";
            View.CanLoadCsv = View.CanSend = View.CanLoadEmr = false;

          
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var patientExtracts = _clientPatientRepository.GetAll(false).ToList();
            var total = patientExtracts.Count();
            int count = 0;
            
            foreach (var patient in patientExtracts)
            {
                count++;
                
                var extractsToSend = _profileManager.Generate(patient);
                foreach (var e in extractsToSend)
                {
                    try
                    {
                        var result = await _pushService.PushAsync(e);
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex);
                    }
                    UpdateUi($"sending Patient Profile {count} of {total}");
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            timeTaken += elapsedMs;
            var msg = $"Send Completed ({count} of {total}) Time: {elapsedMs*0.001} s !";
            UpdateUi(msg);
            

            this.View.EventSummaries = new List<string>() { msg ,$"Total time taken: {timeTaken * 0.001} s" };
            View.CanLoadCsv = View.CanSend = View.CanLoadEmr = true;
        }
        public async Task SendExtractsInParallel()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            View.Status = "sending...";
            View.CanLoadCsv = View.CanSend = View.CanLoadEmr = false;

            var proccedToSend = await CheckSpot();

            if (!proccedToSend)
            {
                View.CanLoadCsv = View.CanSend = View.CanLoadEmr = true;
                return;
            }

            var list = _clientPatientRepository.GetAll(false);

            if (null!=list)
            {
                var total = list.Count();
                int count = 0;

                //SEND
                foreach (var p in list)
                {
                    var tasks = new List<Task>();

                    count++;
                    var extractsToSend = _profileManager.Generate(p);
                    foreach (var e in extractsToSend)
                    {

                        tasks.Add(_pushService.PushAsync(e));

                    }
                    UpdateUi($"sending Patient Profile {count} of {total}");
                    try
                    {
                        await Task.WhenAll(tasks.ToArray());
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex);
                    }
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                timeTaken += elapsedMs;
                var msg = $"Send Completed ({count} of {total}) Time: {elapsedMs * 0.001} s !";

                await Task.Delay(1);

                UpdateUi(msg);

                UpdateStatistics();
                this.View.EventSummaries = new List<string>() { msg, $"Total time taken: {timeTaken * 0.001} s" };
            }
            View.Status = "Ready";
            UpdateStatistics();
            View.CanLoadCsv = View.CanSend = View.CanLoadEmr = true;
        }

        public async Task ExportExtractsAsync()
        {
            
            View.Status = "Exporting...";
            View.CanLoadCsv = View.CanSend = View.CanLoadEmr = false;

            var progress = new Progress<int>(ExportReportProgress);

            try
            {
                var location = await _exportService.ExportToJSonAsync(string.Empty,progress);
                View.OpenFile(location);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            View.Status = "Export Complete!";
            View.CanLoadCsv = View.CanSend = View.CanLoadEmr = true;
            View.ShowReady();
        }

      

        private void UpdateUi(string message)
        {
            View.Status = $"{message}";
        }

        private void ExportReportProgress(int value)
        {
            View.Status = $"Exporting {value}%";
        }

        private void ShowDProgress(DProgress value)
        {
            View.Status = $"{value.ShowProgress()}";
        }

        private void ShowDProgressViewModel(DProgress value)
        {
            
            View.Status = $"{value.ShowProgress()}";

            var vm = new ExtractsViewModel(value.ValueObject as ExtractSetting)
            {
                //Total = processStatus.Progress,
                Status = $"{value.ShowProgress()}"
            };
            View.UpdateStatus(vm);
        }


        #endregion
    }
}