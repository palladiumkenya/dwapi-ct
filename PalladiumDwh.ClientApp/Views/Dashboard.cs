﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using PalladiumDwh.ClientApp.DependencyResolution;
using PalladiumDwh.ClientApp.Events;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientApp.Views
{
    public partial class Dashboard : Form,IDashboardView
    {
        
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private List<ExtractsViewModel> _extracts=new List<ExtractsViewModel>();
        private IEMRRepository _emrRepository;
        private IDatabaseManager _databaseManager;
        private IDatabaseSetupService _databaseSetupService;
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

        private  ITempPatientArtExtractRepository _tempPatientArtExtractRepository;
        private  ITempPatientBaselinesExtractRepository _tempPatientBaselinesExtractRepository;
        private  ITempPatientExtractRepository _tempPatientExtractRepository;
        private  ITempPatientLaboratoryExtractRepository _tempPatientLaboratoryExtractRepository;
        private  ITempPatientPharmacyExtractRepository _tempPatientPharmacyExtractRepository;
        private  ITempPatientStatusExtractRepository _tempPatientStatusExtractRepository;
        private  ITempPatientVisitExtractRepository _tempPatientVisitExtractRepository;

        private  ITempPatientArtExtractErrorSummaryRepository _tempPatientArtExtractErrorSummaryRepository;
        private  ITempPatientBaselinesExtractErrorSummaryRepository _tempPatientBaselinesExtractErrorSummaryRepository;
        private  ITempPatientExtractErrorSummaryRepository _tempPatientExtractErrorSummaryRepository;
        private  ITempPatientLaboratoryExtractErrorSummaryRepository _tempPatientLaboratoryExtractErrorSummaryRepository;
        private  ITempPatientPharmacyExtractErrorSummaryRepository _tempPatientPharmacyExtractErrorSummaryRepository;
        private  ITempPatientStatusExtractErrorSummaryRepository _tempPatientStatusExtractErrorSummaryRepository;
        private  ITempPatientVisitExtractErrorSummaryRepository _tempPatientVisitExtractErrorSummaryRepository;



        private IProfileManager _profileManager;
        private  IPushProfileService _pushService;
        private IExportService _exportService;
        private IImportService _importService;
        private IImportCsvService _importCsvService;
        private ISyncCsvService _syncCsvService;

        private List<string> _eventSummaries = new List<string>();
        private List<string> _allEventSummaries=new List<string>();
        private List<ExtractSetting> _extractSettingsList;
        private string  _selectedExtractSetting;
        private  string _selectedExtractSettingDispaly;
        private object _clientExtracts;
        private object _clientExtractsValidations;
        private object _clientExtractsValidationErrors;
        private  object _selectedValidation;
        private object _clientExtractsNotSent;

        

        public Dashboard()
        {
            InitializeComponent();
         
        }
        public IDashboardPresenter Presenter { get; set; }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void OpenFile(string filename)
        {
            try
            {

                System.Diagnostics.Process.Start(filename);
                }
            catch(Exception e)
            {
                ShowErrorMessage(e.Message);
            }
        }

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
                        var id = listViewExtract.SelectedItems[0].SubItems[6].Text;

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

        public bool LoadEmrEnabled { get; set; }

        public bool CanChangeEmrSettings
        {
            get { return linkLabelEmrSetup.Enabled; }
            set { linkLabelEmrSetup.Enabled = value; }
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

        public bool CanSendExports
        {
            get { return buttonImport.Enabled; }
            set { buttonImport.Enabled = value; }
        }
        public string Status
        {
            get { return toolStripStatusLabelDashboard.Text; }
            set { toolStripStatusLabelDashboard.Text = value; }
        }


        
        public List<string> ExportFiles { get; set; }
        public List<string> CsvFiles { get; set; }=new List<string>();
        public int RecordsPage { get; set; }

        public int RecordsPageSize
        {
            get
            {
                //if (comboBoxPgSizeRecords.SelectedIndex > 0)
                //{
                //    return Convert.ToInt32(comboBoxPgSizeRecords.SelectedItem);
                //}
                //else
                //{
                    return 100;
                //}

            }
            set { comboBoxPgSizeRecords.SelectedItem = value; }
        }

        public int ValidationsPageSize { get; set; }
        public int NotSentPageSize { get; set; }

        public string RecordsViewShowing
        {
            get { return labelRecordsShowing.Text; }
            set { labelRecordsShowing.Text = value; }
        }

        public string ValidationsShowing { get; set; }
        public string NotSentShowing { get; set; }

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

        public void UpdateStats(ExtractsViewModel viewModel)
        {
            var lvitem = listViewExtract.FindItemWithText(viewModel.Id.ToString());
            if (null != lvitem)
            {
                //listViewExtract.BeginUpdate();
                lvitem.SubItems[1].Text = viewModel.Status;
                lvitem.SubItems[2].Text = viewModel.Loaded.ToString();
                lvitem.SubItems[3].Text = viewModel.Rejected.ToString();
                lvitem.SubItems[4].Text = viewModel.Queued.ToString();
                lvitem.SubItems[5].Text = viewModel.Sent.ToString();
                //listViewExtract.EndUpdate();
            }
        }

        public string SelectedExtractSetting
        {
            get { return _selectedExtractSetting; }
        }

        public string SelectedExtractSettingDispaly
        {
            get { return _selectedExtractSettingDispaly; }
        }

        public object SelectedValidation
        {
            get { return _selectedValidation; }
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

        public string SendHeader
        {
            get { return tabPageSendSummary.Text; }
            set { tabPageSendSummary.Text = value; }
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

       

        public object ClientExtractsNotSent
        {
            get { return _clientExtractsNotSent; }
            set
            {
                _clientExtractsNotSent = value;
                LoadClientExtractsNotSent(_clientExtractsNotSent);
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
            dataGridViewExtractValidations.ClearSelection();
            CanGenerateSummary = dataGridViewExtractValidations.Rows.Count > 0;
        }

       

        public void ClearClientExtractsNotSent()
        {
            dataGridViewSendSummary.DataSource = null;
            dataGridViewSendSummary.Rows.Clear();
        }

        public bool CanGenerateSummary
        {
            get { return linkLabelGenerateAllValidationSummary.Enabled; }
            set { linkLabelGenerateAllValidationSummary.Enabled = value; }
        }

     

        private void LoadClientExtractsNotSent(object clientExtractsNotSent)
        {
            ClearClientExtractsNotSent();
            dataGridViewSendSummary.DataSource = clientExtractsNotSent;
            dataGridViewSendSummary.ClearSelection();
        }


        #endregion

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            CanChangeEmrSettings = CanLoadEmr = CanLoadCsv = CanExport = CanSend = CanSendExports = false;
            
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

            CanChangeEmrSettings = CanLoadEmr = CanLoadCsv = CanExport = CanSend = CanSendExports = true;

            toolStripProgressBarDashboard.Style = ProgressBarStyle.Continuous;
            Cursor.Current = Cursors.Default;
        }
        private void InitializeOpenFileDialog()
        {
            openFileDialogCsv.Filter = "CSV-File (Macintosh) (*.csv)|*.csv|CSV-File (MS-DOS) (*.csv)|*.csv";
            openFileDialogCsv.Multiselect = true;
            openFileDialogCsv.Title = "Extracts";
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
                lvitem.SubItems[1].Text = viewModel.Status;
                lvitem.SubItems[2].Text = viewModel.Loaded.ToString();
                lvitem.SubItems[3].Text = viewModel.Rejected.ToString();
                lvitem.SubItems[4].Text = viewModel.Queued.ToString();
                lvitem.SubItems[5].Text = viewModel.Sent.ToString();
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
                item.SubItems.Add(e.Status);
                item.SubItems.Add(e.Loaded.ToString());
                item.SubItems.Add(e.Rejected.ToString());
                item.SubItems.Add(e.Queued.ToString());
                item.SubItems.Add(e.Sent.ToString());
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
            if (LoadEmrEnabled)
            {
                OnEmrExtractLoaded(new EmrExtractLoadedEvent(ExtractSettings));
                CanGenerateSummary = dataGridViewExtractValidations.Rows.Count > 0;
            }
            else
            {
                ShowErrorMessage("Configurations to read from EMR Database do not exist");
            }
        }

        private async void buttonLoadCsv_Click(object sender, EventArgs e)
        {
            openFileDialogCsv.Filter = "Data Warehouse Extracts (*.csv)|*.csv";

            DialogResult dr = this.openFileDialogCsv.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                ShowPleaseWait();
                var csvFiles = openFileDialogCsv.FileNames.ToList();
                if (csvFiles.Count > 0)
                {
                    CsvFiles = csvFiles;
                    ShowPleaseWait();
                    await Presenter.CopyCsvAsync();
                    Presenter.LoadCsvExtracts(ExtractSettings);
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            ShowPleaseWait();
            OnExtractExported(new ExtractExportedEvent(ExtractSettings));
        }
        private void buttonImport_Click(object sender, EventArgs e)
        {
            var options = new SendExport();
            options.ShowDialog(this);
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
            ShowPleaseWait();

            Program.IOC = await IoC.InitializeAsync();

            _databaseManager = Program.IOC.GetInstance<IDatabaseManager>();
            _databaseSetupService = Program.IOC.GetInstance<IDatabaseSetupService>();

            Status = "checking for updates...";

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = @"updater.exe";
                process.Start();
                
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
            }

            //CHECK DB SETUP

            Status = "checking database...";
            await Task.Delay(1);

            bool dbOnline = false;
            try
            {
                dbOnline = await _databaseSetupService.CanConnect();

            }
            catch (Exception e)
            {
                var msg = $"Could not connect to the Application Database !,\nPlease check your database connection,\n{Utility.GetErrorMessage(e)}";
                ShowErrorMessage(msg);
            }
            
            if (!dbOnline)
            {
                var databaseSetup = new DatabaseSetup();
                databaseSetup.ShowDialog(this);
            }


            var progress = new Progress<DProgress>(ViewProgress);
            await _databaseManager.RunUpdateAsync(progress,Properties.Settings.Default.seedSetting);

            _projectRepository = Program.IOC.GetInstance<IProjectRepository>();
            _emrRepository = Program.IOC.GetInstance<IEMRRepository>();
            _syncService = Program.IOC.GetInstance<ISyncService>();
            _syncCsvService = Program.IOC.GetInstance<ISyncCsvService>();

            _clientPatientRepository = Program.IOC.GetInstance<IClientPatientRepository>();

            _clientPatientArtExtractRepository = Program.IOC.GetInstance<IClientPatientArtExtractRepository>(); 
            _clientPatientBaselinesExtractRepository = Program.IOC.GetInstance<IClientPatientBaselinesExtractRepository>(); 
            _clientPatientExtractRepository = Program.IOC.GetInstance<IClientPatientExtractRepository>(); 
            _clientPatientLaboratoryExtractRepository = Program.IOC.GetInstance<IClientPatientLaboratoryExtractRepository>(); 
            _clientPatientPharmacyExtractRepository = Program.IOC.GetInstance<IClientPatientPharmacyExtractRepository>(); 
            _clientPatientStatusExtractRepository = Program.IOC.GetInstance<IClientPatientStatusExtractRepository>(); 
            _clientPatientVisitExtractRepository = Program.IOC.GetInstance<IClientPatientVisitExtractRepository>();

            
            _tempPatientArtExtractRepository = Program.IOC.GetInstance<ITempPatientArtExtractRepository>(); ;
            _tempPatientBaselinesExtractRepository = Program.IOC.GetInstance<ITempPatientBaselinesExtractRepository>(); ;
            _tempPatientExtractRepository = Program.IOC.GetInstance<ITempPatientExtractRepository>();
            _tempPatientLaboratoryExtractRepository = Program.IOC.GetInstance<ITempPatientLaboratoryExtractRepository>(); ;
            _tempPatientPharmacyExtractRepository = Program.IOC.GetInstance<ITempPatientPharmacyExtractRepository>(); ;
            _tempPatientStatusExtractRepository = Program.IOC.GetInstance<ITempPatientStatusExtractRepository>(); ;
            _tempPatientVisitExtractRepository = Program.IOC.GetInstance<ITempPatientVisitExtractRepository>(); ;


            _tempPatientArtExtractErrorSummaryRepository = Program.IOC.GetInstance<ITempPatientArtExtractErrorSummaryRepository>(); ;
            _tempPatientBaselinesExtractErrorSummaryRepository = Program.IOC.GetInstance<ITempPatientBaselinesExtractErrorSummaryRepository>(); ;
            _tempPatientExtractErrorSummaryRepository = Program.IOC.GetInstance<ITempPatientExtractErrorSummaryRepository>();
            _tempPatientLaboratoryExtractErrorSummaryRepository = Program.IOC.GetInstance<ITempPatientLaboratoryExtractErrorSummaryRepository>(); ;
            _tempPatientPharmacyExtractErrorSummaryRepository = Program.IOC.GetInstance<ITempPatientPharmacyExtractErrorSummaryRepository>(); ;
            _tempPatientStatusExtractErrorSummaryRepository = Program.IOC.GetInstance<ITempPatientStatusExtractErrorSummaryRepository>(); ;
            _tempPatientVisitExtractErrorSummaryRepository = Program.IOC.GetInstance<ITempPatientVisitExtractErrorSummaryRepository>(); ;

            _profileManager = Program.IOC.GetInstance<IProfileManager>();
            _pushService = Program.IOC.GetInstance<IPushProfileService>();
            _exportService = Program.IOC.GetInstance<IExportService>();
            _importService = Program.IOC.GetInstance<IImportService>();
            _importCsvService = Program.IOC.GetInstance<IImportCsvService>();

            Presenter = new DashboardPresenter(this,_databaseManager, _projectRepository, _syncService, _clientPatientRepository,
                _profileManager, _pushService,
                _clientPatientArtExtractRepository, _clientPatientBaselinesExtractRepository,
                _clientPatientExtractRepository, _clientPatientLaboratoryExtractRepository,
                _clientPatientPharmacyExtractRepository, _clientPatientStatusExtractRepository,
                _clientPatientVisitExtractRepository,
                _tempPatientExtractRepository, _tempPatientArtExtractRepository, _tempPatientBaselinesExtractRepository,
                _tempPatientLaboratoryExtractRepository, _tempPatientPharmacyExtractRepository,
                _tempPatientStatusExtractRepository, _tempPatientVisitExtractRepository,

                _tempPatientArtExtractErrorSummaryRepository,
            _tempPatientBaselinesExtractErrorSummaryRepository,
                _tempPatientExtractErrorSummaryRepository,
            _tempPatientLaboratoryExtractErrorSummaryRepository,
                _tempPatientPharmacyExtractErrorSummaryRepository,
            _tempPatientStatusExtractErrorSummaryRepository,
            _tempPatientVisitExtractErrorSummaryRepository,

                _exportService, _importService,_importCsvService, _syncCsvService,_emrRepository
            );

            Presenter.Initialize();
            Presenter.InitializeEmrInfo();
            Presenter.InitializeExtracts();

            

            await Presenter.LoadEmrInfoAsync();

            Presenter.LoadExtractSettings();

            Presenter.UpdateStatistics();

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
            CanGenerateSummary = false;
            _selectedExtractSettingDispaly = _selectedExtractSetting = "";

            if (listViewExtractList.SelectedItems.Count > 0)
            {
                _selectedExtractSettingDispaly = listViewExtractList.SelectedItems[0].Text;
                _selectedExtractSetting = listViewExtractList.SelectedItems[0].SubItems[1].Text;
                Presenter.LoadExtractDetail();
                CanGenerateSummary = dataGridViewExtractValidations.Rows.Count > 0;
            }
        }

      

 
        

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"updater.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void openFileDialogCsv_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void linkLabelGenerateAllValidationSummary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPleaseWait();
            Presenter.GenerateSummary();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
  


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxPgSizeRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPgSizeRecords.SelectedIndex > 0)
            {
                var size = RecordsPageSize;
                Presenter.LoadExtractDetail();
            }
        }

        private void menuStripDashboard_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ViewProgress(DProgress value)
        {
            Status = $"{value.ShowProgress()}";
        }

        private void linkLabelEmrSetup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var options = new Options();
            options.ShowDialog(this);
            Presenter.LoadEmrInfo();
            Presenter.LoadExtractSettings();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}
