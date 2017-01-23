using System;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using PalladiumDwh.Uploader.Controller;

namespace PalladiumDwh.Uploader.Presentation
{
    public partial class ImportForm : Form
    {
        private readonly ManagerController _managerController;
        public ImportForm()
        {
            InitializeComponent();
            _managerController = new ManagerController();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            try
            {
               _managerController.LoadPatientsFromIqTools(databaseComboBox.Text,
                    sqlServerComboBox.Text);
                
                _managerController.LoadArtPatientsFromIqTools(databaseComboBox.Text,
                    sqlServerComboBox.Text);
                _managerController.LoadBaselinePatientsFromIqTools(databaseComboBox.Text,
                    sqlServerComboBox.Text);
                _managerController.LoadLaboratoryPatientsFromIqTools(databaseComboBox.Text,
                    sqlServerComboBox.Text);
                _managerController.LoadPharmacyPatientsFromIqTools(databaseComboBox.Text,
                    sqlServerComboBox.Text);
                _managerController.LoadPatientVisitsFromIqTools(databaseComboBox.Text,
                    sqlServerComboBox.Text);
                _managerController.LoadPatientStatusFromIqTools(databaseComboBox.Text,
                    sqlServerComboBox.Text);


            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void buttonLoadServers_Click(object sender, EventArgs e)
        {
            var dataTable = SmoApplication.EnumAvailableSqlServers(true);
            sqlServerComboBox.ValueMember = "Name";
            sqlServerComboBox.DataSource = dataTable;
        }

        private void buttonLoadDatabases_Click(object sender, EventArgs e)
        {
            if (sqlServerComboBox.SelectedIndex == -1) return;
            var serverName = sqlServerComboBox.Text;
            var server = new Server(serverName);
            try
            {
                foreach (Database database in server.Databases)
                {
                    databaseComboBox.Items.Add(database.Name);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
     
    }
}
