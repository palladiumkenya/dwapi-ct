using System;
using System.Linq;
using System.Security;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class DashboardPresenter:IDashboardPresenter
    {
        private IProjectRepository _projectRepository;
        private EmrViewModel _emrmodel;
        public IDashboardView View { get; }

        private ISyncService _syncService;

        public DashboardPresenter(IDashboardView view,IProjectRepository projectRepository, ISyncService syncService)
        {
            view.Presenter = this;
            View = view;

            _projectRepository = projectRepository;
            _syncService = syncService;
            


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


            foreach (var extract in e.Extracts.OrderBy(x=>x.Rank))
            {
                var summary= _syncService.Sync(extract.Destination);
                var vm = new ExtractsViewModel(extract)
                {
                    Total = summary.SyncSummary.Total,
                    Status = summary.ToString()
                };
                View.UpdateStatus(vm);
            }
        }

        private void View_ExtractSent(object sender, Events.ExtractSentEvent e)
        {
            throw new System.NotImplementedException();
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
    }
}