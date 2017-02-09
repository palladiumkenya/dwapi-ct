﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PalladiumDwh.ClientApp.Events;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core.Interfaces;

namespace PalladiumDwh.ClientApp.Views
{
    public partial class Dashboard : Form,IDashboardView
    {
        private List<ExtractsViewModel> _extracts=new List<ExtractsViewModel>();

        private readonly IProjectRepository _projectRepository;
        private readonly ISyncService _syncService;
        private readonly IClientPatientRepository _clientPatientRepository;
        private readonly IProfileManager _profileManager;
        private readonly IPushProfileService _pushService;


        public Dashboard()
        {
            InitializeComponent();

            
            _projectRepository = Program.IOC.GetInstance<IProjectRepository>();
            _syncService = Program.IOC.GetInstance<ISyncService>();

            _clientPatientRepository = Program.IOC.GetInstance<IClientPatientRepository>();
            _profileManager = Program.IOC.GetInstance<IProfileManager>();
            _pushService = Program.IOC.GetInstance<IPushProfileService>();

            Presenter = new DashboardPresenter(this, _projectRepository, _syncService,_clientPatientRepository,_profileManager,_pushService);

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
        public List<ExtractSetting> ExtractSettings { get; set; }

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

        public bool CanLoadEmr { get; set; }
        public bool CanLoadCsv { get; set; }
        public bool CanExport { get; set; }
        public bool CanSend { get; set; }

        public event EventHandler<EmrExtractLoadedEvent> EmrExtractLoaded;
        public event EventHandler<CsvExtractLoadedEvent> CsvExtractLoaded;
        public event EventHandler<ExtractExportedEvent> ExtractExported;
        public event EventHandler<ExtractSentEvent> ExtractSent;

        #endregion

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Presenter.LoadEmrInfo();
            Presenter.LoadExtracts();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options = new Options();
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

        public void UpdateStatus(ExtractsViewModel viewModel)
        {
            foreach (ListViewItem item in listViewExtract.Items)
            {
                var extractId = item.SubItems[3];
                if (extractId.Text.ToLower() == viewModel.Id.ToString().ToLower())
                {
                    listViewExtract.Items[item.Index].SubItems[1].Text = viewModel.Total.ToString();
                    listViewExtract.Items[item.Index].SubItems[2].Text = viewModel.Status;
                    break;
                }
            }
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

        protected virtual void OnEmrExtractLoaded(EmrExtractLoadedEvent e)
        {
            EmrExtractLoaded?.Invoke(this, e);
        }
        protected virtual void OnCsvExtractLoaded(CsvExtractLoadedEvent e)
        {
            CsvExtractLoaded?.Invoke(this, e);
        }
        protected virtual void OnExtractExported(ExtractExportedEvent e)
        {
            ExtractExported?.Invoke(this, e);
        }
        protected virtual void OnExtractSent(ExtractSentEvent e)
        {
            ExtractSent?.Invoke(this, e);
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

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OnEmrExtractLoaded(new EmrExtractLoadedEvent(ExtractSettings));
        }

        private void buttonLoadCsv_Click(object sender, EventArgs e)
        {
            OnCsvExtractLoaded(new CsvExtractLoadedEvent(ExtractSettings));
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            OnExtractExported(new ExtractExportedEvent(ExtractSettings));
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            OnExtractSent(new ExtractSentEvent());
        }
    }
}
