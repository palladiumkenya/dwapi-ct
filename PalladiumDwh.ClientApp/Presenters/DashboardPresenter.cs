using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core.Interfaces;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class DashboardPresenter : IDashboardPresenter
    {
        
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IProjectRepository _projectRepository;
        private readonly ISyncService _syncService;
        private readonly IClientPatientRepository _clientPatientRepository;
        private readonly IProfileManager _profileManager;
        private readonly IPushProfileService _pushService;
        private long timeTaken = 0;

        private EmrViewModel _emrmodel;
        private IEnumerable<ClientPatientExtract> _clientPatientExtracts;
        public IDashboardView View { get; }



        public DashboardPresenter(IDashboardView view, IProjectRepository projectRepository, ISyncService syncService,
            IClientPatientRepository clientPatientRepository, IProfileManager profileManager,
            IPushProfileService pushService)
        {
            view.Presenter = this;
            View = view;

            _projectRepository = projectRepository;
            _syncService = syncService;
            _clientPatientRepository = clientPatientRepository;
            _profileManager = profileManager;
            _pushService = pushService;


            View.EmrExtractLoaded += View_EmrExtractLoaded;
            View.CsvExtractLoaded += View_CsvExtractLoaded;
            View.ExtractExported += View_ExtractExported;
            View.ExtractSent += View_ExtractSent;
        }

        public void Initialize()
        {
            View.Title = "Dashboard";
        }

        #region EMR Information

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

        #endregion

        #region Extracts

        public void InitializeExtracts()
        {
            View.ClearExtracts();
        }

        public void LoadExtractSettings()
        {
            var extracts = _emrmodel
                .ExtractSettings
                .OrderBy(x => x.Rank)
                .ToList();
            View.ExtractSettings = extracts;
            View.Extracts = ExtractsViewModel.CreateList(extracts);
            View.CanLoadEmr = View.CanLoadCsv = View.CanExport = View.CanSend = View.ExtractSettings.Count > 0;


        }

        //TODO: consider Parallel processing
        public async void LoadExtracts(List<ExtractSetting> extracts)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            View.Status = "loading...";
            int total = extracts.Count;
            int count = 0;

            await _syncService.InitializeAsync();

            foreach (var extract in extracts.OrderBy(x => x.Rank))
            {
                
                count++;
                var vm = new ExtractsViewModel(extract) {Status = "loading..."};
                View.UpdateStatus(vm);

                View.Status = $"loading {count}/{total} ({extract.Display}) ...";
                var summary = await _syncService.SyncAsync(extract.Destination);

                vm = new ExtractsViewModel(extract)
                {
                    Total = summary.SyncSummary.Total,
                    Status = summary.ToString()
                };
                View.UpdateStatus(vm);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            timeTaken += elapsedMs;
            var msg = $"Load Completed ({count} of {total}) Extracts Time: {elapsedMs*0.001} s !";
            UpdateUi(msg);
            
            this.View.EventSummaries=new List<string>() {msg};

            LoadExtractDetail();
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


        private  void View_EmrExtractLoaded(object sender, Events.EmrExtractLoadedEvent e)
        {
           LoadExtracts(e.Extracts);
        }

        private void View_ExtractSent(object sender, Events.ExtractSentEvent e)
        {
            SendExtracts();
        }

        private void View_ExtractExported(object sender, Events.ExtractExportedEvent e)
        {
            throw new System.NotImplementedException();
        }

        private void View_CsvExtractLoaded(object sender, Events.CsvExtractLoadedEvent e)
        {
            throw new System.NotImplementedException();
        }



        #endregion

        #region Extracts Detail

        public void LoadExtractDetail()
        {
           // _clientPatientExtracts = _clientPatientRepository.GetAll();

        }

        //TODO: consider Parallel processing
        public async void SendExtracts()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            View.Status="sending...";
            View.CanLoadCsv = View.CanSend = View.CanLoadEmr = false;

            var list = _clientPatientRepository.GetAll(false).ToList();
            var total = list.Count();
            int count = 0;
            foreach (var p in list)
            {
                count++;
                
                var extractsToSend = _profileManager.Generate(p);
                foreach (var e in extractsToSend)
                {
                    var result = await _pushService.PushAsync(e);
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

        private void UpdateUi(string message)
        {
            View.Status = $"{message}";
        }

        #endregion
    }
}