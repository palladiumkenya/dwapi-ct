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
    public partial class DatabaseSetup : Form, IDatabaseSetupView
    {
        private List<string> _servers;
        private DatabaseConfig _databaseConfig;
        public IDatabaseSetupPresenter Presenter { get; set; }

        public List<DatabaseType> DatabaseTypes{get;set;}
        public DatabaseType SelectedDatabaseType { get; set; }

        public List<string> Servers
        {
            get { return _servers; }
            set
            {
                _servers = value;
                LoadServers(_servers);
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

            _databaseConfig.Server=comboBoxServer.Text ;
            _databaseConfig.User=textBoxUser.Text;
            _databaseConfig.Password = textBoxPassword.Text;
            return _databaseConfig;
        }

        private void LoadDatabaseConfig(DatabaseConfig databaseConfig)
        {
            if(comboBoxServer.Items.Count>0)
                comboBoxServer.SelectedIndex = 0;

            comboBoxServer.Text = "";
            textBoxUser.Clear();
            textBoxPassword.Clear();

            if (null==databaseConfig)
                return;
            
            comboBoxServer.Text = databaseConfig.Server;
            textBoxUser.Text= databaseConfig.User;
            textBoxPassword.Text = databaseConfig.Password;
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

        public string Status
        {
            get { return labelStatus.Text; }
            set { labelStatus.Text = value; }
        }
        public int ProgessStatus {
            get { return pbSetup.Value; }
            set { pbSetup.Value = value; }
        }
        public DatabaseSetup()
        {
            InitializeComponent();
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
            Presenter = new DatabaseSetupPresenter(this, databaseManager, databaseSetupService);
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
            comboBoxServer.Enabled = textBoxUser.Enabled = textBoxPassword.Enabled = status;
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
    }
}
