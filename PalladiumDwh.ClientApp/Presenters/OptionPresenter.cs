using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class OptionPresenter:IOptionPresenter
    {
        private IProjectRepository _projectRepository;
        private readonly IEMRRepository _emrRepository;
        
        public IOptionView View { get; }

        public OptionPresenter(IProjectRepository projectRepository, IEMRRepository emrRepository, IOptionView view)
        {
            _projectRepository = projectRepository;
            _emrRepository = emrRepository;
            view.Presenter = this;
            View = view;
            
        }

        public void Initialize()
        {
            View.Title = "Options";
            View.Header = "Setup connection to EMR Extracts datasources";
            View.SubOptionTitle = "Choose Default EMR Source";
            View.Id = View.Info = string.Empty;
        }

        public void Load()
        {
            var list=new List<EmrViewModel>();

            _projectRepository = Program.IOC.GetInstance<IProjectRepository>();
            var projects = _projectRepository.GetAll().ToList();
            foreach (var p in projects)
            {
                list.AddRange(EmrViewModel.CreateList(p));
            }
            View.Emrs = list;
            View.CanMarkDefault = View.Emrs.Count > 0;
        }

        public void SetAsDefault()
        {
            if (null == View.SelectedEmr)
                return;

            if (View.ConfirmAction($"Are you sure you want to Change the default EMR to {View.Info}", "Set Default EMR"))
            {
                var emrId = View.SelectedEmr.Id;
                _emrRepository.SetEmrAsDefault(emrId);
                _emrRepository.CommitChanges();
                _projectRepository.CommitChanges();
            }
            Load();
        }

        public void ShowSelected()
        {
            View.CanMarkDefault = false;
            View.Id = View.Info = string.Empty;
            var emr = View.SelectedEmr;
            if (null != emr)
            {
                View.CanMarkDefault = true;
                View.Id = emr.Id.ToString();
                View.Info = $"{emr.EMR} (v {emr.Version})";
            }
        }
    }
}