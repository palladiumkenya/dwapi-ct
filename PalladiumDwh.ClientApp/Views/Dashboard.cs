using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using PalladiumDwh.ClientApp.DependencyResolution;
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
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private List<ExtractsViewModel> _extracts=new List<ExtractsViewModel>();

        private  IProjectRepository _projectRepository;
        private  ISyncService _syncService;
        private  IClientPatientRepository _clientPatientRepository;

        private  IClientPatientArtExtractRepository _clientPatientArtExtractRepository;
        private  IClientPatientBaselinesExtractRepository _clientPatientBaselinesExtractRepository;
        private  IClientPatientExtractRepository _clientPatientExtractRepository;
        private  IClientPatientLaboratoryExtractRepository _clientPatientLaboratoryExtractRepository;
        private  IClientPatientPharmacyExtractRepository _clientPatientPharmacyExtractRepository;
        private  IClientPatientStatusExtractRepository _clientPatientStatusExtractRepository;
        private  IClientPatientVisitExtractRepository _clientPatientVisitExtractRepository;

        private ITempPatientExtractRepository _tempPatientExtractRepository;

        private  IProfileManager _profileManager;
        private  IPushProfileService _pushService;
        private List<string> _eventSummaries = new List<string>();
        private List<string> _allEventSummaries=new List<string>();
        private List<ExtractSetting> _extractSettingsList;
        private string  _selectedExtractSetting;
        private  string _selectedExtractSettingDispaly;
        private object _clientExtracts;
        private object _clientExtractsValidations;

        public Dashboard()
        {
            InitializeComponent();
         
        }
        public IDashboardPresenter Presenter { get; set; }
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

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

        public bool CanLoadEmr
        {
            get { return buttonLoad.Enabled; }
            set { buttonLoad.Enabled = value; }
        }

        public bool CanLoadCsv
        {
            get { return buttonLoadCsv.Enabled; }
            set { buttonLoadCsv.Enabled = value; }
        }

        public bool CanExport
        {
            get { return buttonExport.Enabled; }
            set { buttonExport.Enabled = value; }
        }

        public bool CanSend
        {
            get { return buttonSend.Enabled; }
            set { buttonSend.Enabled = value; }
        }


        public string Status
        {
            get { return toolStripStatusLabelDashboard.Text; }
            set { toolStripStatusLabelDashboard.Text = value; }

        }


       

        public List<string> EventSummaries
        {
            get { return _eventSummaries; }
            set
            {
                _eventSummaries = value;
                LoadEventSummary(_eventSummaries);
            }
        }

       

        public event EventHandler<EmrExtractLoadedEvent> EmrExtractLoaded;
        public event EventHandler<CsvExtractLoadedEvent> CsvExtractLoaded;
        public event EventHandler<ExtractExportedEvent> ExtractExported;
        public event EventHandler<ExtractSentEvent> ExtractSent;

        #endregion

        #region ExtractSettingsList

        public string SelectedExtractSetting
        {
            get { return _selectedExtractSetting; }
        }

        public string SelectedExtractSettingDispaly
        {
            get { return _selectedExtractSettingDispaly; }
        }

        public List<ExtractSetting> ExtractSettingsList
        {
            get { return _extractSettingsList; }
            set
            {
                _extractSettingsList = value;
                LoadExtractExtractSettingList(_extractSettingsList);
            }
        }

        public string RecordsHeader
        {
            get { return tabPageRecords.Text; }
            set {  tabPageRecords.Text = value; }
        }

        public string ValidtionHeader
        {
            get { return tabPageValidationSummary.Text; }
            set { tabPageValidationSummary.Text = value; }
        }

        public object ClientExtracts
        {
            get { return _clientExtracts; }
            set
            {
                _clientExtracts = value; 
                LoadClientExtracts(_clientExtracts);
            }
        }

        public object ClientExtractsValidations
        {
            get { return _clientExtractsValidations; }
            set
            {
                _clientExtractsValidations = value;
                LoadClientExtractsValidations(_clientExtractsValidations);
            }
        }

        public void ClearExtractSettingsList()
        {
            listViewExtractList.Items.Clear();
        }


        private void LoadExtractExtractSettingList(List<ExtractSetting> extractSettings)
        {
            ClearExtractSettingsList();
            foreach (var e in extractSettings)
            {
                var item = new ListViewItem {Text = e.Display};
                item.SubItems.Add(e.Destination);
                listViewExtractList.Items.Add(item);
            }
        }

        public void ClearClientExtracts()
        {
            dataGridViewExtractDetail.DataSource = null;
            dataGridViewExtractDetail.Rows.Clear();
        }
        private void LoadClientExtracts(object clientExtracts)
        {
            ClearClientExtracts();
            dataGridViewExtractDetail.DataSource = clientExtracts;
        }

        public void ClearClientExtractsValidations()
        {
            dataGridViewExtractValidations.DataSource = null;
            dataGridViewExtractValidations.Rows.Clear();
        }
        private void LoadClientExtractsValidations(object clientExtractsValidations)
        {
            ClearClientExtractsValidations();
            dataGridViewExtractValidations.DataSource = clientExtractsValidations;
        }
        #endregion

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            toolStripProgressBarDashboard.Style=ProgressBarStyle.Marquee;
            //toolStripProgressBarDashboard.MarqueeAnimationSpeed = 10;

            try
            {
                await StartUp();
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                Status = "Startup failure!";
                ShowErrorMessage($"Startup failure! \n {ex.Message}");

            }



            toolStripProgressBarDashboard.Style = ProgressBarStyle.Continuous;
            Cursor.Current = Cursors.Default;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options = new Options();
            options.ShowDialog(this);
            Presenter.LoadEmrInfo();
            Presenter.LoadExtractSettings();
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
            
            var lvitem= listViewExtract.FindItemWithText(viewModel.Id.ToString());
            if (null != lvitem)
            {
                //listViewExtract.BeginUpdate();
                lvitem.SubItems[1].Text = viewModel.Total.ToString();
                lvitem.SubItems[2].Text = viewModel.Status;
                //listViewExtract.EndUpdate();
            }

            /*
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
            */
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
            listViewExtract.VirtualListSize = listViewExtract.Items.Count;
            //listViewExtract.VirtualMode = true;
        }

        protected virtual  void OnEmrExtractLoaded(EmrExtractLoadedEvent e)
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

        private void LoadEventSummary(List<string> eventSummaries)
        {
            _allEventSummaries.AddRange(eventSummaries);
            listBoxEventsSummary.DataSource = null;
            listBoxEventsSummary.DataSource = _allEventSummaries;
        }

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

        private  void buttonLoad_Click(object sender, EventArgs e)
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

        public void UpdateProgress(int i)
        {
            
        }

        public async Task StartUp()
        {
            Program.IOC = await IoC.InitializeAsync();
            _projectRepository = Program.IOC.GetInstance<IProjectRepository>();
            _syncService = Program.IOC.GetInstance<ISyncService>();

            _clientPatientRepository = Program.IOC.GetInstance<IClientPatientRepository>();

            _clientPatientArtExtractRepository = Program.IOC.GetInstance<IClientPatientArtExtractRepository>(); 
            _clientPatientBaselinesExtractRepository = Program.IOC.GetInstance<IClientPatientBaselinesExtractRepository>(); 
            _clientPatientExtractRepository = Program.IOC.GetInstance<IClientPatientExtractRepository>(); 
            _clientPatientLaboratoryExtractRepository = Program.IOC.GetInstance<IClientPatientLaboratoryExtractRepository>(); 
            _clientPatientPharmacyExtractRepository = Program.IOC.GetInstance<IClientPatientPharmacyExtractRepository>(); 
            _clientPatientStatusExtractRepository = Program.IOC.GetInstance<IClientPatientStatusExtractRepository>(); 
            _clientPatientVisitExtractRepository = Program.IOC.GetInstance<IClientPatientVisitExtractRepository>();

            _tempPatientExtractRepository = Program.IOC.GetInstance<ITempPatientExtractRepository>();

            _profileManager = Program.IOC.GetInstance<IProfileManager>();
            _pushService = Program.IOC.GetInstance<IPushProfileService>();


            Presenter = new DashboardPresenter(this, _projectRepository, _syncService, _clientPatientRepository,
                _profileManager, _pushService,
                _clientPatientArtExtractRepository,_clientPatientBaselinesExtractRepository,_clientPatientExtractRepository,_clientPatientLaboratoryExtractRepository,_clientPatientPharmacyExtractRepository,_clientPatientStatusExtractRepository,_clientPatientVisitExtractRepository,
                _tempPatientExtractRepository);

            Presenter.Initialize();
            Presenter.InitializeEmrInfo();
            Presenter.InitializeExtracts();

            await Presenter.LoadEmrInfoAsync();

            Presenter.LoadExtractSettings();
            Presenter.LoadExtractList();

        }

        public void ShowPleaseWait()
        {
            Cursor.Current = Cursors.WaitCursor;
            toolStripProgressBarDashboard.Style = ProgressBarStyle.Marquee;
        }

        public void ShowReady()
        {
            toolStripProgressBarDashboard.Style = ProgressBarStyle.Continuous;
            Cursor.Current = Cursors.Default;
        }

        private void listViewExtractList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewExtractList.SelectedItems.Count > 0)
            {
                _selectedExtractSettingDispaly = listViewExtractList.SelectedItems[0].Text;
                _selectedExtractSetting = listViewExtractList.SelectedItems[0].SubItems[1].Text;
                Presenter.LoadExtractDetail();
            }
        }

        private void dataGridViewExtractValidations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
