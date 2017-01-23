using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using PalladiumDwh.Uploader.Controller;
using PalladiumDwh.Uploader.Logger;
using PalladiumDwh.Uploader.Model;
using PalladiumDwh.Uploader.Properties;
using PalladiumDwh.Uploader.Sender;

namespace PalladiumDwh.Uploader.Presentation
{
    public partial class MainForm : Form
    {
     
        private readonly ManagerController _managerController;

        private readonly ISender<PatientExtract> _patientSender;
        private readonly ISender<PatientArtExtract> _patientArtSender;
        private readonly ISender<PatientBaselinesExtract> _patientBaselineSender;
        private readonly ISender<PatientLaboratoryExtract> _patientLabSender;
        private readonly ISender<PatientPharmacyExtract> _patientPharmacySender;
        private readonly ISender<PatientVisitExtract> _patientVisitSender;
        private readonly ISender<PatientStatusExtract> _patientStatusSender; 

        private readonly PatientExtract _patientExtract;
        private readonly PatientArtExtract _patientArtExtract;
        private readonly PatientBaselinesExtract _patientBaselinesExtract;
        private readonly PatientLaboratoryExtract _patientLabExtract;
        private readonly PatientPharmacyExtract _patientPharmacyExtract;
        private readonly PatientVisitExtract _patientVisitExtract;
        private readonly PatientStatusExtract _patientStatusExtract;
        private readonly Facility _facility;
        private readonly DataSet _dataSet;
        private readonly SettingsForm _settingsForm;
        private readonly ImportForm _importForm;

        public static readonly ILog Log = LogManager.GetLogger(typeof (MainForm));
       
        public MainForm()
        {
          //TODO : Complete member initialization
            // TODO: Complete member initialization
            InitializeComponent();

            _managerController = new ManagerController();
            
            _patientSender = new  PatientSender();
            _patientArtSender = new PatientArtSender();
            _patientBaselineSender = new PatientBaselineSender();
            _patientLabSender = new PatientLabSender();
            _patientPharmacySender = new PatientPharmacySender();
            _patientVisitSender = new PatientVisitSender();
            _patientStatusSender = new PatientStatusSender();

            _patientExtract = new PatientExtract();
            _patientArtExtract = new PatientArtExtract();
            _patientBaselinesExtract = new PatientBaselinesExtract();
            _patientLabExtract = new PatientLaboratoryExtract();
            _patientPharmacyExtract = new PatientPharmacyExtract();
            _patientVisitExtract = new PatientVisitExtract();
            _patientStatusExtract = new PatientStatusExtract();
            _facility = new Facility();
            _dataSet = new DataSet();
            _settingsForm = new SettingsForm();
            _importForm = new ImportForm();
            
        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView[] gridViews =
                {
                    patientGridView, artPatientGridView, patientPharmacyGridView, patientLabGridView,
                    patientStatusGridView, patientBaseLinesGridView, patientVisitsGridView
                };
                _managerController.PopulateGridView(gridViews);

                if (dataExtractsTreeView.Nodes.Count == 0)
                {
                    _managerController.PopulateTreeView(dataExtractsTreeView);
                }
                
            }
            catch (Exception ex)
            {
                //Todo : log to logger
                MessageBox.Show(ex.Message);
            }
  
        }
        private async void uploadButton_Click(object sender, EventArgs e)
        {


            var patientProgress = new Progress<decimal>(currentPatientRecord =>
            {
                currentPatientRecord = currentPatientRecord + 1;
                var percentCompleted = decimal.Floor((currentPatientRecord / patientGridView.RowCount) * 100);
                statusLabel.Text = Resources.PatientUpload + currentPatientRecord + " of  " + patientGridView.RowCount;
                statusLabel.Text = statusLabel.Text + " " + percentCompleted.ToString() + "%";
                progressBarUpload.Maximum = patientGridView.RowCount;
                progressBarUpload.Value = (int)currentPatientRecord;

            });
            await Task.Run(() => _patientSender.CreateEntity(patientGridView, _patientExtract, patientProgress));


            var patientArtProgress = new Progress<decimal>(currentPatientArtRecord =>
            {
                currentPatientArtRecord = currentPatientArtRecord + 1;
                var percentCompleted = Decimal.Floor((currentPatientArtRecord / artPatientGridView.RowCount) * 100);
                statusLabel.Text = "Uploading  Art Patients       " + currentPatientArtRecord + " of  " + artPatientGridView.RowCount;
                statusLabel.Text = statusLabel.Text + " " + percentCompleted.ToString() + "%";
                progressBarUpload.Maximum = artPatientGridView.RowCount;
                progressBarUpload.Value = (int)currentPatientArtRecord;
            });
            await Task.Run(() => _patientArtSender.CreateEntity(artPatientGridView, _patientArtExtract, patientArtProgress));


            var patientBaselineProgress = new Progress<decimal>(currentBaselineRecord =>
            {
                currentBaselineRecord = currentBaselineRecord + 1;
                var percentCompleted = Decimal.Floor((currentBaselineRecord / patientBaseLinesGridView.RowCount) * 100);
                statusLabel.Text = "Uploading  Baseline Patients       " + currentBaselineRecord + " of  " + patientBaseLinesGridView.RowCount;
                statusLabel.Text = statusLabel.Text + " " + percentCompleted.ToString() + "%";
                progressBarUpload.Maximum = patientBaseLinesGridView.RowCount;
                progressBarUpload.Value = (int)currentBaselineRecord;
            });
            await Task.Run(() => _patientBaselineSender.CreateEntity(patientBaseLinesGridView, _patientBaselinesExtract, patientBaselineProgress));


            var patientLabProgress = new Progress<decimal>(currentLabRecord =>
            {
                currentLabRecord = currentLabRecord + 1;
                var percentCompleted = Decimal.Floor((currentLabRecord / patientBaseLinesGridView.RowCount) * 100);
                statusLabel.Text = "Uploading  Laboratory Patients       " + currentLabRecord + " of  " + patientLabGridView.RowCount;
                statusLabel.Text = statusLabel.Text + " " + percentCompleted.ToString() + "%";
                progressBarUpload.Maximum = patientLabGridView.RowCount;
                progressBarUpload.Value = (int)currentLabRecord;
            });
            await Task.Run(() => _patientLabSender.CreateEntity(patientLabGridView, _patientLabExtract, patientLabProgress));



            var patientPhamacyProgress = new Progress<decimal>(currentPharmacyRecord =>
            {
                currentPharmacyRecord = currentPharmacyRecord + 1;
                var percentCompleted = Decimal.Floor((currentPharmacyRecord / patientPharmacyGridView.RowCount) * 100);
                statusLabel.Text = "Uploading  Pharmacy Patients Data      " + currentPharmacyRecord + " of  " + patientPharmacyGridView.RowCount;
                statusLabel.Text = statusLabel.Text + " " + percentCompleted.ToString() + "%";
                progressBarUpload.Maximum = patientPharmacyGridView.RowCount;
                progressBarUpload.Value = (int)currentPharmacyRecord;
            });
            await Task.Run(() => _patientPharmacySender.CreateEntity(patientPharmacyGridView, _patientPharmacyExtract, patientPhamacyProgress));

            var patientVisitProgress = new Progress<decimal>(currentVisitRecord =>
            {
                currentVisitRecord = currentVisitRecord + 1;
                var percentCompleted = Decimal.Floor((currentVisitRecord / patientVisitsGridView.RowCount) * 100);
                statusLabel.Text = "Uploading  Visits Patients Data      " + currentVisitRecord + " of  " + patientVisitsGridView.RowCount;
                statusLabel.Text = statusLabel.Text + " " + percentCompleted.ToString() + "%";
                progressBarUpload.Maximum = patientVisitsGridView.RowCount;
                progressBarUpload.Value = (int)currentVisitRecord;
            });
            await Task.Run(() => _patientVisitSender.CreateEntity(patientVisitsGridView, _patientVisitExtract, patientVisitProgress));



            var patientStatusProgress = new Progress<decimal>(currentStatusRecord =>
            {
                currentStatusRecord = currentStatusRecord + 1;
                var percentCompleted = Decimal.Floor((currentStatusRecord / patientStatusGridView.RowCount) * 100);
                statusLabel.Text = "Uploading  Status Patients Data      " + currentStatusRecord + " of  " + patientStatusGridView.RowCount;
                statusLabel.Text = statusLabel.Text + " " + percentCompleted.ToString() + "%";
                progressBarUpload.Maximum = patientStatusGridView.RowCount;
                progressBarUpload.Value = (int)currentStatusRecord;
            });
            await Task.Run(() => _patientStatusSender.CreateEntity(patientStatusGridView, _patientStatusExtract, patientStatusProgress));
            statusLabel.Text = "Completed View Log for any issues";
            progressBarUpload.Value = 0;

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure();
            TextBoxAppender.ConfigureTextBoxAppender(loggingTextBox);
        }
    
        private void configureDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsForm.ShowDialog(this);
        }

        private void importFromIQToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _importForm.ShowDialog(this);
        }

        private void validateButton_Click(object sender, EventArgs e)
        {

        }
    }
}
