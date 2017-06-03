using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using PalladiumDwh.ClientApp.Model;
using PalladiumDwh.ClientApp.Presenters;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientApp.Views
{
    public partial class SendExport : Form,IManageExportsView
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private List<ExportsViewModel> _exports=new List<ExportsViewModel>();
        private int _exportsCount;
        private List<string> _eventsSummary=new List<string>();

        public SendExport()
        {
            InitializeComponent();
        }
        public IManageExportsPresenter Presenter { get; set; }

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

        public bool CanLoadExports
        {
            get { return buttonLoadExport.Enabled; }
            set { buttonLoadExport.Enabled = value; }
        }
        public bool CanSend
        {
            get { return buttonSendDWH.Enabled; }
            set { buttonSendDWH.Enabled = value; }
        }

        public bool CanDeleteAll
        {
            get { return linkLableDelete.Enabled; }
            set { linkLableDelete.Enabled = value; }
        }

        public List<string> ExportFiles { get; set; }

        public List<ExportsViewModel> Exports
        {
            get { return _exports; }
            set
            {
                _exports = value;
                ShowExports(_exports);
            }
        }

        public int ExportsCount
        {
            get { return _exportsCount; }
            set
            {
                _exportsCount = value;
                labelCount.Text = $"{_exportsCount} Exports";
            }
        }

        public List<string> EventsSummary
        {
            get { return _eventsSummary; }
            set { ShowEventsSummary(value); }
        }

        public string Status
        {
            get { return labelExportsStatus.Text; }
            set{labelExportsStatus.Text= value;}
        }

        public int ProgessStatus
        {
            get { return pbExports.Value; }
            set { pbExports.Value = value; }
        }

        public Task StartUp()
        {
            Cursor.Current = Cursors.WaitCursor;
            var importService  = Program.IOC.GetInstance<IImportService>();
            var pushProfileService = Program.IOC.GetInstance<IPushProfileService>();
            Presenter =new ManageExportsPresenter(this, importService, pushProfileService);
            Presenter.Initialize();
            return Presenter.LoadExportsAsync(true);
         
        }
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void ShowPleaseWait()
        {
            Cursor.Current = Cursors.WaitCursor;
            pbExports.Value = 0;
        }

        public void ShowReady()
        {
            Cursor.Current = Cursors.Default;
            labelExportsStatus.Text = "Ready";
            pbExports.Value = 0;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowProgress(DProgress progress)
        {
            Status = progress.ShowProgress();
            if(progress.ValuePercentage.HasValue)
                pbExports.Value = progress.ValuePercentage.Value;
        }

        private void ShowExports(List<ExportsViewModel> extracts)
        {
            ClearExports();

            foreach (var e in extracts)
            {
                var item = new ListViewItem {Text = e.SiteCode.ToString()};
                item.SubItems.Add(e.Records.ToString());
                item.SubItems.Add(e.Status);
                item.SubItems.Add(e.Location);
                listViewExports.Items.Add(item);
            }

            listViewExports.VirtualListSize = listViewExports.Items.Count;
        }

        public void ClearExports()
        {
            
            listViewExports.Items.Clear();
        }

        private void ShowEventsSummary(List<string> events)
        {
            AddEvents(events);
            listBoxEventsSummary.DataSource = null;
            listBoxEventsSummary.DataSource = _eventsSummary;
        }
        private void AddEvents(List<string> events)
        {
            _eventsSummary.AddRange(events);
        }

        public void ClearEvents()
        {
            listBoxEventsSummary.DataSource = null;
            listBoxEventsSummary.Items.Clear();
        }

        private async void SendExport_Load(object sender, EventArgs e)
        {            
            try
            {                
                await StartUp();
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                Status = "Manage Exports failure!";
                ShowErrorMessage($"Manage Exports! \n {ex.Message}");
            }
            
            Cursor.Current = Cursors.Default;
        }

        private async void buttonLoadExport_Click(object sender, EventArgs e)
        {

            openFileDialogExports.Filter = "DWapi Exports (*.zip)|*.zip";

            DialogResult dr = this.openFileDialogExports.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                ShowPleaseWait();
                var imports = openFileDialogExports.FileNames.ToList();
                if (imports.Count > 0)
                {
                    ExportFiles = imports;
                    ShowPleaseWait();
                    bool cansend = await Presenter.ExtractExportsAsync();
                    if (cansend)
                        await Presenter.LoadExportsAsync(false);
                }
            }
        }

        private async void linkLableDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure ?", "Confirm Delete", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                await Presenter.DeleteAllExportsAsync();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void buttonSendDWH_Click(object sender, EventArgs e)
        {

        }
    }
}
