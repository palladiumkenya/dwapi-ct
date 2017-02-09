using System.Linq;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class DashboardPresenter:IDashboardPresenter
    {
        private IProjectRepository _projectRepository;
        private EmrViewModel _emrmodel;
        public IDashboardView View { get; }

        public DashboardPresenter(IProjectRepository projectRepository, IDashboardView view)
        {
            _projectRepository = projectRepository;
            view.Presenter = this;
            View = view;
           
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

            View.Extracts = ExtractsViewModel.CreateList(extracts);
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

        #endregion
    }
}