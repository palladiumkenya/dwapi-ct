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
    public partial class Dashboard : Form,IDashboardView
    {
        private IProjectRepository _projectRepository;
        private List<ExtractsViewModel> _extracts=new List<ExtractsViewModel>();

        public Dashboard()
        {
            InitializeComponent();
            _projectRepository = Program.IOC.GetInstance<IProjectRepository>();
            Presenter = new DashboardPresenter(_projectRepository, this);
            Presenter.Initialize();
            Presenter.InitializeEmrInfo();
            Presenter.InitializeExtracts();
        }
        public IDashboardPresenter Presenter { get; set; }

        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }
        public string Header { get; set; }
        public string HeaderDescription { get; set; }

        #region EMR Information

        public string EMRInfoTitle
        {
            get { return labelTopH.Text; }
            set { labelTopH.Text = value; }
        }

        public string EMR
        {
            get { return textBoxEMR.Text; }
            set { textBoxEMR.Text = value; }
        }

        public string Version
        {
            get { return textBoxEMRVersion.Text; }
            set { textBoxEMRVersion.Text = value; }
        }

        public string Project
        {
            get { return textBoxProject.Text; }
            set { textBoxProject.Text = value; }

        }
        #endregion

        #region Extracts

        public List<ExtractsViewModel> Extracts
        {
            get { return _extracts; }
            set
            {
                _extracts = value;
                LoadExtracts(_extracts);
            }
        }

        public ExtractsViewModel SelectedExtract
        {
            get
            {
                try
                {
                    if (listViewExtract.SelectedItems.Count > 0)
                    {
                        var id = listViewExtract.SelectedItems[0].SubItems[3].Text;

                        return _extracts.FirstOrDefault(x => x.Id == new Guid(id));
                    }
                }
                catch { }

                return null;
            }
        }

        public string Id
        {
            get { return labelId.Text; }
            set { labelId.Text = value; }
        }

        #endregion

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Presenter.LoadEmrInfo();
            Presenter.LoadExtracts();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options=new Options();
            options.ShowDialog(this);
            Presenter.LoadEmrInfo();
            Presenter.LoadExtracts();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        #region Extracts Actions
        public void ClearExtracts()
        {
            listViewExtract.Items.Clear();
        }
        
        private void LoadExtracts(List<ExtractsViewModel> extracts)
        {
            ClearExtracts();

            foreach (var e in extracts)
            {
                var item = new ListViewItem();
                item.Text = e.Extract;
                item.SubItems.Add(e.Total.ToString());
                item.SubItems.Add(e.Status);
                item.SubItems.Add(e.Id.ToString());
                listViewExtract.Items.Add(item);
            }
        }
        #endregion

        public bool ConfirmAction(string action, string actionTilte)
        {
            throw new NotImplementedException();
        }
        
        public void CloseView()
        {
            throw new NotImplementedException();
        }

        private void listViewExtract_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.ShowSelectedExtract();
        }
    }
}
