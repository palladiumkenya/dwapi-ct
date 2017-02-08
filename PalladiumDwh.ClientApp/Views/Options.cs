using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;

namespace PalladiumDwh.ClientApp.Views
{
    public partial class Options : Form,IOptionView
    {
        private IProjectRepository _projectRepository;
        private IEMRRepository _emrRepository;
        private List<EmrViewModel> _emrs;

        public Options()
        {
            InitializeComponent();
            _projectRepository=Program.IOC.GetInstance<IProjectRepository>();
            _emrRepository = Program.IOC.GetInstance<IEMRRepository>();
            Presenter=new OptionPresenter(_projectRepository,_emrRepository,this);
            Presenter.Initialize();
            
        }

        public IOptionPresenter Presenter { get; set; }

        public string Title
        {
            get { return Text; }
            set { Text = value;}
        }

        public string Header
        {
            get { return labelHeader.Text; }
            set { labelHeader.Text = value; }
        }
        public string HeaderDescription { get; set; }

        public string SubOptionTitle
        {
            get { return labelSubOptionTitle.Text; }
            set { labelSubOptionTitle.Text = value; }
        }
        public string Id
        {
            get { return labelId.Text; }
            set { labelId.Text = value; }
        }
        public string Info
        {
            get { return labelExtractInfo.Text; }
            set { labelExtractInfo.Text = value; }
        }

        public List<EmrViewModel> Emrs
        {
            get { return _emrs; }
            set
            {
                _emrs = value;
                LoadListView(_emrs);
            }
        }

        public EmrViewModel SelectedEmr
        {
            get { return dataGridViewOptions.SelectedRows.Cast<EmrViewModel>().ToList().FirstOrDefault(); }
        }

        private void LoadListView(List<EmrViewModel> emrs)
        {
            dataGridViewOptions.DataSource = emrs;
            dataGridViewOptions.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        }

        private void Options_Load(object sender, EventArgs e)
        {
            Presenter.Load();
        }

        private void dataGridViewOptions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void dataGridViewOptions_SelectionChanged(object sender, EventArgs e)
        {
            Presenter.ShowSelected();
        }
    }
}
