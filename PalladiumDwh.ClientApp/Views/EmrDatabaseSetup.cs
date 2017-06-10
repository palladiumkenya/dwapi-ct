using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientApp.Views
{
    public partial class EmrDatabaseSetup : Form, IEmrDatabaseSetupView
    {
        private List<string> _servers;
        private DatabaseConfig _databaseConfig;
        private List<string> _databases;
        private List<DatabaseType> _databaseTypes;
        public IEmrDatabaseSetupPresenter Presenter { get; set; }

        public Guid EmrId
        {
            get { return new Guid(labelEmrName.Text); }
            set { labelEmrId.Text = value.ToString(); }
        }
        public string EmrName
        {
            get { return labelEmrName.Text; }
            set { labelEmrName.Text = value; }
        }
        public string EmrKey
        {
            get { return labelEmrKey.Text; }
            set { labelEmrKey.Text = value; }
        }

        public List<DatabaseType> DatabaseTypes
        {
            get { return _databaseTypes; }
            set
            {
                _databaseTypes = value;
                LoadDatabaseTypes(_databaseTypes);
            }
        }

        private void LoadDatabaseTypes(List<DatabaseType> databaseTypes)
        {
            comboBoxDatabaseType.DataSource = null;
            comboBoxDatabaseType.DataSource = databaseTypes;
            comboBoxDatabaseType.DisplayMember= "Name";
            comboBoxDatabaseType.ValueMember = "Provider";
        }

        public DatabaseType SelectedDatabaseType
        {
            get
            {
                if (comboBoxDatabaseType.Items.Count > 0)
                {
                    if (comboBoxDatabaseType.SelectedIndex > 0)
                        return comboBoxDatabaseType.SelectedItem as DatabaseType;
                }
                return null;
            }
            set { comboBoxDatabaseType.SelectedItem = value; }
        }

        public List<string> Servers
        {
            get { return _servers; }
            set
            {
                _servers = value;
                LoadServers(_servers);
            }
        }

        public List<string> Databases
        {
            get { return _databases; }
            set
            {
                _databases = value;
                LoadDatabases(_databases);
            }
        }

        private void LoadDatabases(List<string> databases)
        {
            comboBoxDatabaseName.Items.Clear();
            foreach (var s in databases)
            {
                comboBoxDatabaseName.Items.Add(s);
            }
        }

        private void LoadServers(List<string> servers)
        {
            comboBoxServer.Items.Clear();
            foreach (var s in servers)
            {
                comboBoxServer.Items.Add(s);
            }
        }


        public DatabaseConfig DatabaseConfig
        {
            get
            {
                _databaseConfig = GetDatabaseConfig();
                return _databaseConfig;
            }
            set
            {
                _databaseConfig = value;
                LoadDatabaseConfig(_databaseConfig);
            }
        }

        private DatabaseConfig GetDatabaseConfig()
        {

            if (null == _databaseConfig)
                _databaseConfig = new DatabaseConfig();

            _databaseConfig.DatabaseType = comboBoxDatabaseType.SelectedItem as DatabaseType;
            _databaseConfig.Server=comboBoxServer.Text ;
            _databaseConfig.User=textBoxUser.Text;
            _databaseConfig.Port =string.IsNullOrWhiteSpace(textBoxPort.Text)?0: Convert.ToInt32(textBoxPort
                .Text);
            _databaseConfig.Password = textBoxPassword.Text;
            _databaseConfig.Database = comboBoxDatabaseName.Text;
            return _databaseConfig;
        }

        private void LoadDatabaseConfig(DatabaseConfig databaseConfig)
        {
            if (comboBoxDatabaseType.Items.Count > 0)
                comboBoxDatabaseType.SelectedIndex =-1;

            if (comboBoxServer.Items.Count>0)
                comboBoxServer.SelectedIndex = -1;
            comboBoxServer.Text = "";

            textBoxPort.Clear();          
            textBoxUser.Clear();
            textBoxPassword.Clear();

            if (comboBoxDatabaseName.Items.Count > 0)
                comboBoxServer.SelectedIndex = -1;
            comboBoxDatabaseName.Text = "";

            if (null==databaseConfig)
                return;
            
            comboBoxDatabaseType.SelectedValue = databaseConfig.DatabaseType.Provider;
            comboBoxServer.Text = databaseConfig.Server;
            textBoxPort.Text = databaseConfig.Port > 0 ? databaseConfig.Port.ToString() : string.Empty;
            textBoxUser.Text= databaseConfig.User;
            textBoxPassword.Text = databaseConfig.Password;
            comboBoxDatabaseName.Text = databaseConfig.Database;
        }

        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }
        public string Header
        {
            get { return labelHeader.Text; }
            set { labelHeader.Text = value; }
        }
        public string HeaderDescription
        {
            get { return labelHDescrption.Text; }
            set { labelHDescrption.Text = value; }
        }
        public bool CanSave
        {
            get { return buttonSave.Visible; }
            set{buttonSave.Visible = value;}
        }

        public bool CanTest
        {
            get { return buttonTest.Enabled; }
            set { buttonTest.Enabled = value; }
        }

        public bool CanEdit
        {
            get { return buttonEdit.Visible; }
            set { buttonEdit.Visible = value; }
        }

        public bool CanRefresh
        {
            get { return buttonRefresh.Enabled; }
            set { buttonRefresh.Enabled = value; }
        }

        public bool CanRefreshDatabase
        {
            get { return buttonRefreshDatabase.Enabled; }
            set { buttonRefreshDatabase.Enabled = value; }
        }

        public bool CanUsePort
        {
            get { return textBoxPort.Enabled; }
            set { textBoxPort.Enabled = value; }
        }

        public string Status
        {
            get { return labelStatus.Text; }
            set { labelStatus.Text = value; }
        }
        public int ProgessStatus {
            get { return pbSetup.Value; }
            set { pbSetup.Value = value; }
        }

        public EmrDatabaseSetup(Guid emrId, string emrName, string emrKey)
        {
            InitializeComponent();
            EmrId = emrId;
            EmrName = emrName;
            EmrKey = emrKey;
        }
        private  async void DatabaseSetup_Load(object sender, EventArgs e)
        {
            try
            {
                await StartUp();
            }
            catch (Exception ex)
            {
                Status = "Database Setup failure!";
                ShowErrorMessage($"Database Setup! \n {ex.Message}");
            }
            ShowReady();
        }
        public Task StartUp()
        {
            Cursor.Current = Cursors.WaitCursor;
            var databaseManager = Program.IOC.GetInstance<IDatabaseManager>();
            var databaseSetupService = Program.IOC.GetInstance<IDatabaseSetupService>();
            Presenter = new EmrDatabaseSetupPresenter(this, databaseManager, databaseSetupService);
            Presenter.Initialize();
            return Presenter.Load();
        }
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void Edit(bool status)
        {
            comboBoxDatabaseType.Enabled = comboBoxServer.Enabled = textBoxPort.Enabled = textBoxUser.Enabled =
                textBoxPassword.Enabled = comboBoxDatabaseName.Enabled = status;
        }

        public void ShowPleaseWait()
        {
            Cursor.Current = Cursors.WaitCursor;
            pbSetup.Style=ProgressBarStyle.Marquee;
        }

        public void ShowReady()
        {
            Cursor.Current = Cursors.Default;
            labelStatus.Text = "Ready";
            pbSetup.Style = ProgressBarStyle.Blocks;
            pbSetup.Value = 0;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowProgress(DProgress progress)
        {
            Status = progress.ShowProgress();
            if (progress.ValuePercentage.HasValue)
                pbSetup.Value = progress.ValuePercentage.Value;
        }

        private async void buttonTest_Click(object sender, EventArgs e)
        {
            await Presenter.Test();
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            await Presenter.Save();
        }

        private async void buttonRefresh_Click(object sender, EventArgs e)
        {
            await Presenter.Refresh();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
          Presenter.Change();
        }

        private async void buttonRefreshDatabase_Click(object sender, EventArgs e)
        {
            await Presenter.RefreshDatabase();
        }
    }
}
