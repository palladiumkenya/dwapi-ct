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

namespace PalladiumDwh.ClientApp.Views
{
    public partial class Startup : Form, IStartupView
    {
        public Startup()
        {
            InitializeComponent();
        }

        private async void Startup_Load(object sender, EventArgs e)
        {
            Presenter = new StartupPresenter(this);
            bool ok = await Presenter.UpdateDatabase();
            if (!ok)
            {
                MessageBox.Show(@"Database could not be updated, A manual update will be required !", @"Startup Failure", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            Presenter.LoadDashboard();
        }

        public Task StartUp()
        {
            throw new NotImplementedException();
        }

        public string Title { get; set; }
        public string Header { get; set; }
        public string HeaderDescription { get; set; }
        public IStartupPresenter Presenter { get; set; }
        public void ShowErrorMessage(string message)
        {
            throw new NotImplementedException();
        }

        public string Status
        {
            get { return labelStatus.Text; }
            set { labelStatus.Text = value; }
        }

        public void ShowDash()
        {
            Hide();
            var dash = new Dashboard();
            dash.Show();
        }
    }
}
