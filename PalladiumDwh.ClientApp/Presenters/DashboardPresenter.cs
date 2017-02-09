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

        public void LoadExtracts()
        {
            var extracts = _emrmodel
                .ExtractSettings
                .OrderBy(x => x.Rank)
                .ToList();
            View.ExtractSettings = extracts;
            View.Extracts = ExtractsViewModel.CreateList(extracts);
            View.CanLoadEmr = View.CanLoadCsv = View.CanExport = View.CanSend = View.ExtractSettings.Count > 0;


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


        private void View_EmrExtractLoaded(object sender, Events.EmrExtractLoadedEvent e)
        {

            _syncService.Initialize();


            foreach (var extract in e.Extracts.OrderBy(x => x.Rank))
            {
                var summary = _syncService.Sync(extract.Destination);
                var vm = new ExtractsViewModel(extract)
                {
                    Total = summary.SyncSummary.Total,
                    Status = summary.ToString()
                };
                View.UpdateStatus(vm);
            }

            LoadExtractDetail();
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
            _clientPatientExtracts = _clientPatientRepository.GetAll();

        }

        public async void SendExtracts()
        {
            foreach (var p in _clientPatientRepository.GetAll(false))
            {
                var extractsToSend = _profileManager.Generate(p);
                Log.Debug($"sending:{p}");
                foreach (var e in extractsToSend)
                {
                    var result = await _pushService.PushAsync(e);
                    Log.Debug($"   >sent:{p} {e.GetType().Name} |{result}");
                }
            }
        }

        #endregion
    }
}