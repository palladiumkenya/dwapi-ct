using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class OptionPresenter:IOptionPresenter
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEMRRepository _emrRepository;
        
        public IOptionView View { get; }

        public OptionPresenter(IProjectRepository projectRepository, IEMRRepository emrRepository, IOptionView view)
        {
            _projectRepository = projectRepository;
            _emrRepository = emrRepository;
            View = view;
        }

        public void Initialize()
        {
            View.Title = "Options";
            View.Header = "Setup connection to EMR Extracts datasources";
            View.SubOptionTitle = "Choose Default EMR Source";
        }

        public void Load()
        {
            var list=new List<EmrViewModel>();
            var projects = _projectRepository.GetAll();
            foreach (var p in projects)
            {
                list.AddRange(EmrViewModel.CreateList(p));
            }
            View.Emrs = list;
        }

        public void SetAsDefault()
        {
            var emr = View.SelectedEmr.Emr;

            _emrRepository.Update(emr);
            _emrRepository.CommitChanges();

            Load();
        }

        public void ShowSelected()
        {
            var emr = View.SelectedEmr;
            View.Id = emr.Emr.Id.ToString();
            View.Info = "";
        }
    }
}