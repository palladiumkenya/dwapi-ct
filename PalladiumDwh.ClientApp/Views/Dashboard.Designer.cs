namespace PalladiumDwh.ClientApp.Views
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStripDashboard = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feedbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripDashboard = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelDashboard = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarDashboard = new System.Windows.Forms.ToolStripProgressBar();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelTopH = new System.Windows.Forms.Panel();
            this.labelTopH = new System.Windows.Forms.Label();
            this.textBoxProject = new System.Windows.Forms.TextBox();
            this.labelProject = new System.Windows.Forms.Label();
            this.textBoxEMRVersion = new System.Windows.Forms.TextBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.textBoxEMR = new System.Windows.Forms.TextBox();
            this.labelEMR = new System.Windows.Forms.Label();
            this.panelExtract = new System.Windows.Forms.Panel();
            this.panelExtractData = new System.Windows.Forms.Panel();
            this.labelEventSummary = new System.Windows.Forms.Label();
            this.listBoxEventsSummary = new System.Windows.Forms.ListBox();
            this.labelId = new System.Windows.Forms.Label();
            this.listViewExtract = new System.Windows.Forms.ListView();
            this.columnHeaderExtract = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelExtractDataStatus = new System.Windows.Forms.Panel();
            this.panelExtractAction = new System.Windows.Forms.Panel();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonLoadCsv = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.panelExtractDataTop = new System.Windows.Forms.Panel();
            this.labelExtractData = new System.Windows.Forms.Label();
            this.panelExtractDetail = new System.Windows.Forms.Panel();
            this.panelExtractDataDetail = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewExtractDetail = new System.Windows.Forms.DataGridView();
            this.checkedListBoxSummary = new System.Windows.Forms.CheckedListBox();
            this.panelSummaryTop = new System.Windows.Forms.Panel();
            this.labelSummary = new System.Windows.Forms.Label();
            this.panelExtractDataDetailTop = new System.Windows.Forms.Panel();
            this.labelExtractDataDetail = new System.Windows.Forms.Label();
            this.menuStripDashboard.SuspendLayout();
            this.statusStripDashboard.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelTopH.SuspendLayout();
            this.panelExtract.SuspendLayout();
            this.panelExtractData.SuspendLayout();
            this.panelExtractAction.SuspendLayout();
            this.panelExtractDataTop.SuspendLayout();
            this.panelExtractDetail.SuspendLayout();
            this.panelExtractDataDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtractDetail)).BeginInit();
            this.panelSummaryTop.SuspendLayout();
            this.panelExtractDataDetailTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripDashboard
            // 
            this.menuStripDashboard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripDashboard.Location = new System.Drawing.Point(0, 0);
            this.menuStripDashboard.Name = "menuStripDashboard";
            this.menuStripDashboard.Size = new System.Drawing.Size(984, 24);
            this.menuStripDashboard.TabIndex = 0;
            this.menuStripDashboard.Text = "menuStripDashboard";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineHelpToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.feedbackToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // onlineHelpToolStripMenuItem
            // 
            this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
            this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.onlineHelpToolStripMenuItem.Text = "Online Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates...";
            // 
            // feedbackToolStripMenuItem
            // 
            this.feedbackToolStripMenuItem.Name = "feedbackToolStripMenuItem";
            this.feedbackToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.feedbackToolStripMenuItem.Text = "Send Feedback";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusStripDashboard
            // 
            this.statusStripDashboard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelDashboard,
            this.toolStripProgressBarDashboard});
            this.statusStripDashboard.Location = new System.Drawing.Point(0, 639);
            this.statusStripDashboard.Name = "statusStripDashboard";
            this.statusStripDashboard.Size = new System.Drawing.Size(984, 22);
            this.statusStripDashboard.TabIndex = 1;
            this.statusStripDashboard.Text = "statusStrip1";
            // 
            // toolStripStatusLabelDashboard
            // 
            this.toolStripStatusLabelDashboard.Name = "toolStripStatusLabelDashboard";
            this.toolStripStatusLabelDashboard.Size = new System.Drawing.Size(736, 17);
            this.toolStripStatusLabelDashboard.Spring = true;
            this.toolStripStatusLabelDashboard.Text = "Loading...";
            this.toolStripStatusLabelDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBarDashboard
            // 
            this.toolStripProgressBarDashboard.Name = "toolStripProgressBarDashboard";
            this.toolStripProgressBarDashboard.Size = new System.Drawing.Size(200, 16);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.panelTopH);
            this.panelTop.Controls.Add(this.textBoxProject);
            this.panelTop.Controls.Add(this.labelProject);
            this.panelTop.Controls.Add(this.textBoxEMRVersion);
            this.panelTop.Controls.Add(this.labelVersion);
            this.panelTop.Controls.Add(this.textBoxEMR);
            this.panelTop.Controls.Add(this.labelEMR);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 24);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 64);
            this.panelTop.TabIndex = 2;
            // 
            // panelTopH
            // 
            this.panelTopH.BackColor = System.Drawing.Color.Gainsboro;
            this.panelTopH.Controls.Add(this.labelTopH);
            this.panelTopH.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopH.Location = new System.Drawing.Point(0, 0);
            this.panelTopH.Name = "panelTopH";
            this.panelTopH.Size = new System.Drawing.Size(984, 32);
            this.panelTopH.TabIndex = 6;
            // 
            // labelTopH
            // 
            this.labelTopH.AutoSize = true;
            this.labelTopH.Location = new System.Drawing.Point(7, 9);
            this.labelTopH.Name = "labelTopH";
            this.labelTopH.Size = new System.Drawing.Size(86, 13);
            this.labelTopH.TabIndex = 5;
            this.labelTopH.Text = "EMR Information";
            // 
            // textBoxProject
            // 
            this.textBoxProject.Location = new System.Drawing.Point(727, 37);
            this.textBoxProject.Name = "textBoxProject";
            this.textBoxProject.ReadOnly = true;
            this.textBoxProject.Size = new System.Drawing.Size(245, 20);
            this.textBoxProject.TabIndex = 1;
            // 
            // labelProject
            // 
            this.labelProject.AutoSize = true;
            this.labelProject.Location = new System.Drawing.Point(681, 40);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(40, 13);
            this.labelProject.TabIndex = 0;
            this.labelProject.Text = "Project";
            // 
            // textBoxEMRVersion
            // 
            this.textBoxEMRVersion.Location = new System.Drawing.Point(281, 36);
            this.textBoxEMRVersion.Name = "textBoxEMRVersion";
            this.textBoxEMRVersion.ReadOnly = true;
            this.textBoxEMRVersion.Size = new System.Drawing.Size(64, 20);
            this.textBoxEMRVersion.TabIndex = 5;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(234, 40);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(41, 13);
            this.labelVersion.TabIndex = 4;
            this.labelVersion.Text = "version";
            // 
            // textBoxEMR
            // 
            this.textBoxEMR.Location = new System.Drawing.Point(72, 36);
            this.textBoxEMR.Name = "textBoxEMR";
            this.textBoxEMR.ReadOnly = true;
            this.textBoxEMR.Size = new System.Drawing.Size(158, 20);
            this.textBoxEMR.TabIndex = 3;
            // 
            // labelEMR
            // 
            this.labelEMR.AutoSize = true;
            this.labelEMR.Location = new System.Drawing.Point(26, 40);
            this.labelEMR.Name = "labelEMR";
            this.labelEMR.Size = new System.Drawing.Size(31, 13);
            this.labelEMR.TabIndex = 2;
            this.labelEMR.Text = "EMR";
            // 
            // panelExtract
            // 
            this.panelExtract.BackColor = System.Drawing.Color.White;
            this.panelExtract.Controls.Add(this.panelExtractData);
            this.panelExtract.Controls.Add(this.panelExtractDataStatus);
            this.panelExtract.Controls.Add(this.panelExtractAction);
            this.panelExtract.Controls.Add(this.panelExtractDataTop);
            this.panelExtract.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExtract.Location = new System.Drawing.Point(0, 88);
            this.panelExtract.Name = "panelExtract";
            this.panelExtract.Size = new System.Drawing.Size(984, 245);
            this.panelExtract.TabIndex = 3;
            // 
            // panelExtractData
            // 
            this.panelExtractData.Controls.Add(this.labelEventSummary);
            this.panelExtractData.Controls.Add(this.listBoxEventsSummary);
            this.panelExtractData.Controls.Add(this.labelId);
            this.panelExtractData.Controls.Add(this.listViewExtract);
            this.panelExtractData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExtractData.Location = new System.Drawing.Point(0, 72);
            this.panelExtractData.Name = "panelExtractData";
            this.panelExtractData.Size = new System.Drawing.Size(984, 163);
            this.panelExtractData.TabIndex = 7;
            // 
            // labelEventSummary
            // 
            this.labelEventSummary.AutoSize = true;
            this.labelEventSummary.Location = new System.Drawing.Point(765, 6);
            this.labelEventSummary.Name = "labelEventSummary";
            this.labelEventSummary.Size = new System.Drawing.Size(86, 13);
            this.labelEventSummary.TabIndex = 6;
            this.labelEventSummary.Text = "Events Summary";
            // 
            // listBoxEventsSummary
            // 
            this.listBoxEventsSummary.FormattingEnabled = true;
            this.listBoxEventsSummary.Location = new System.Drawing.Point(630, 25);
            this.listBoxEventsSummary.Name = "listBoxEventsSummary";
            this.listBoxEventsSummary.Size = new System.Drawing.Size(351, 134);
            this.listBoxEventsSummary.TabIndex = 2;
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelId.Location = new System.Drawing.Point(627, 6);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(16, 13);
            this.labelId.TabIndex = 1;
            this.labelId.Text = "Id";
            this.labelId.Visible = false;
            // 
            // listViewExtract
            // 
            this.listViewExtract.AutoArrange = false;
            this.listViewExtract.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderExtract,
            this.columnHeaderTotal,
            this.columnHeaderStatus,
            this.columnHeaderId});
            this.listViewExtract.FullRowSelect = true;
            this.listViewExtract.HideSelection = false;
            this.listViewExtract.Location = new System.Drawing.Point(10, 6);
            this.listViewExtract.MultiSelect = false;
            this.listViewExtract.Name = "listViewExtract";
            this.listViewExtract.Size = new System.Drawing.Size(611, 154);
            this.listViewExtract.TabIndex = 0;
            this.listViewExtract.UseCompatibleStateImageBehavior = false;
            this.listViewExtract.View = System.Windows.Forms.View.Details;
            this.listViewExtract.SelectedIndexChanged += new System.EventHandler(this.listViewExtract_SelectedIndexChanged);
            // 
            // columnHeaderExtract
            // 
            this.columnHeaderExtract.Text = "Extract";
            this.columnHeaderExtract.Width = 138;
            // 
            // columnHeaderTotal
            // 
            this.columnHeaderTotal.Text = "Total Records";
            this.columnHeaderTotal.Width = 83;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "Status";
            this.columnHeaderStatus.Width = 304;
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "Id";
            this.columnHeaderId.Width = 0;
            // 
            // panelExtractDataStatus
            // 
            this.panelExtractDataStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelExtractDataStatus.Location = new System.Drawing.Point(0, 235);
            this.panelExtractDataStatus.Name = "panelExtractDataStatus";
            this.panelExtractDataStatus.Size = new System.Drawing.Size(984, 10);
            this.panelExtractDataStatus.TabIndex = 3;
            // 
            // panelExtractAction
            // 
            this.panelExtractAction.Controls.Add(this.buttonSend);
            this.panelExtractAction.Controls.Add(this.buttonExport);
            this.panelExtractAction.Controls.Add(this.buttonLoadCsv);
            this.panelExtractAction.Controls.Add(this.buttonLoad);
            this.panelExtractAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExtractAction.Location = new System.Drawing.Point(0, 33);
            this.panelExtractAction.Name = "panelExtractAction";
            this.panelExtractAction.Size = new System.Drawing.Size(984, 39);
            this.panelExtractAction.TabIndex = 0;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(467, 8);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(140, 23);
            this.buttonSend.TabIndex = 3;
            this.buttonSend.Text = "Send to Data Warehouse";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(321, 8);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(140, 23);
            this.buttonExport.TabIndex = 2;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonLoadCsv
            // 
            this.buttonLoadCsv.Location = new System.Drawing.Point(175, 8);
            this.buttonLoadCsv.Name = "buttonLoadCsv";
            this.buttonLoadCsv.Size = new System.Drawing.Size(140, 23);
            this.buttonLoadCsv.TabIndex = 1;
            this.buttonLoadCsv.Text = "Load from CSV";
            this.buttonLoadCsv.UseVisualStyleBackColor = true;
            this.buttonLoadCsv.Click += new System.EventHandler(this.buttonLoadCsv_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(29, 8);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(140, 23);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Load from EMR";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panelExtractDataTop
            // 
            this.panelExtractDataTop.BackColor = System.Drawing.Color.Gainsboro;
            this.panelExtractDataTop.Controls.Add(this.labelExtractData);
            this.panelExtractDataTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExtractDataTop.Location = new System.Drawing.Point(0, 0);
            this.panelExtractDataTop.Name = "panelExtractDataTop";
            this.panelExtractDataTop.Size = new System.Drawing.Size(984, 33);
            this.panelExtractDataTop.TabIndex = 5;
            // 
            // labelExtractData
            // 
            this.labelExtractData.AutoSize = true;
            this.labelExtractData.Location = new System.Drawing.Point(7, 11);
            this.labelExtractData.Name = "labelExtractData";
            this.labelExtractData.Size = new System.Drawing.Size(45, 13);
            this.labelExtractData.TabIndex = 6;
            this.labelExtractData.Text = "Extracts";
            // 
            // panelExtractDetail
            // 
            this.panelExtractDetail.BackColor = System.Drawing.Color.White;
            this.panelExtractDetail.Controls.Add(this.panelExtractDataDetail);
            this.panelExtractDetail.Controls.Add(this.panelExtractDataDetailTop);
            this.panelExtractDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExtractDetail.Location = new System.Drawing.Point(0, 333);
            this.panelExtractDetail.Name = "panelExtractDetail";
            this.panelExtractDetail.Size = new System.Drawing.Size(984, 306);
            this.panelExtractDetail.TabIndex = 5;
            // 
            // panelExtractDataDetail
            // 
            this.panelExtractDataDetail.Controls.Add(this.splitContainer1);
            this.panelExtractDataDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExtractDataDetail.Location = new System.Drawing.Point(0, 35);
            this.panelExtractDataDetail.Name = "panelExtractDataDetail";
            this.panelExtractDataDetail.Size = new System.Drawing.Size(984, 271);
            this.panelExtractDataDetail.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewExtractDetail);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkedListBoxSummary);
            this.splitContainer1.Panel2.Controls.Add(this.panelSummaryTop);
            this.splitContainer1.Size = new System.Drawing.Size(984, 271);
            this.splitContainer1.SplitterDistance = 764;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridViewExtractDetail
            // 
            this.dataGridViewExtractDetail.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewExtractDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExtractDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewExtractDetail.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewExtractDetail.Name = "dataGridViewExtractDetail";
            this.dataGridViewExtractDetail.Size = new System.Drawing.Size(764, 271);
            this.dataGridViewExtractDetail.TabIndex = 0;
            // 
            // checkedListBoxSummary
            // 
            this.checkedListBoxSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxSummary.FormattingEnabled = true;
            this.checkedListBoxSummary.Location = new System.Drawing.Point(0, 32);
            this.checkedListBoxSummary.Name = "checkedListBoxSummary";
            this.checkedListBoxSummary.Size = new System.Drawing.Size(216, 239);
            this.checkedListBoxSummary.TabIndex = 8;
            // 
            // panelSummaryTop
            // 
            this.panelSummaryTop.Controls.Add(this.labelSummary);
            this.panelSummaryTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummaryTop.Location = new System.Drawing.Point(0, 0);
            this.panelSummaryTop.Name = "panelSummaryTop";
            this.panelSummaryTop.Size = new System.Drawing.Size(216, 32);
            this.panelSummaryTop.TabIndex = 7;
            // 
            // labelSummary
            // 
            this.labelSummary.AutoSize = true;
            this.labelSummary.Location = new System.Drawing.Point(12, 9);
            this.labelSummary.Name = "labelSummary";
            this.labelSummary.Size = new System.Drawing.Size(50, 13);
            this.labelSummary.TabIndex = 5;
            this.labelSummary.Text = "Summary";
            // 
            // panelExtractDataDetailTop
            // 
            this.panelExtractDataDetailTop.BackColor = System.Drawing.Color.Gainsboro;
            this.panelExtractDataDetailTop.Controls.Add(this.labelExtractDataDetail);
            this.panelExtractDataDetailTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExtractDataDetailTop.Location = new System.Drawing.Point(0, 0);
            this.panelExtractDataDetailTop.Name = "panelExtractDataDetailTop";
            this.panelExtractDataDetailTop.Size = new System.Drawing.Size(984, 35);
            this.panelExtractDataDetailTop.TabIndex = 0;
            // 
            // labelExtractDataDetail
            // 
            this.labelExtractDataDetail.AutoSize = true;
            this.labelExtractDataDetail.Location = new System.Drawing.Point(15, 10);
            this.labelExtractDataDetail.Name = "labelExtractDataDetail";
            this.labelExtractDataDetail.Size = new System.Drawing.Size(75, 13);
            this.labelExtractDataDetail.TabIndex = 0;
            this.labelExtractDataDetail.Text = "Extract Details";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.panelExtractDetail);
            this.Controls.Add(this.panelExtract);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStripDashboard);
            this.Controls.Add(this.menuStripDashboard);
            this.MainMenuStrip = this.menuStripDashboard;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.menuStripDashboard.ResumeLayout(false);
            this.menuStripDashboard.PerformLayout();
            this.statusStripDashboard.ResumeLayout(false);
            this.statusStripDashboard.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelTopH.ResumeLayout(false);
            this.panelTopH.PerformLayout();
            this.panelExtract.ResumeLayout(false);
            this.panelExtractData.ResumeLayout(false);
            this.panelExtractData.PerformLayout();
            this.panelExtractAction.ResumeLayout(false);
            this.panelExtractDataTop.ResumeLayout(false);
            this.panelExtractDataTop.PerformLayout();
            this.panelExtractDetail.ResumeLayout(false);
            this.panelExtractDataDetail.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtractDetail)).EndInit();
            this.panelSummaryTop.ResumeLayout(false);
            this.panelSummaryTop.PerformLayout();
            this.panelExtractDataDetailTop.ResumeLayout(false);
            this.panelExtractDataDetailTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripDashboard;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem feedbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripDashboard;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDashboard;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarDashboard;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelTopH;
        private System.Windows.Forms.Label labelTopH;
        private System.Windows.Forms.TextBox textBoxEMRVersion;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxEMR;
        private System.Windows.Forms.Label labelEMR;
        private System.Windows.Forms.TextBox textBoxProject;
        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.Panel panelExtract;
        private System.Windows.Forms.Panel panelExtractAction;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonLoadCsv;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Panel panelExtractData;
        private System.Windows.Forms.Panel panelExtractDataStatus;
        private System.Windows.Forms.Panel panelExtractDataTop;
        private System.Windows.Forms.Label labelExtractData;
        private System.Windows.Forms.Panel panelExtractDetail;
        private System.Windows.Forms.Panel panelExtractDataDetail;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewExtractDetail;
        private System.Windows.Forms.CheckedListBox checkedListBoxSummary;
        private System.Windows.Forms.Panel panelSummaryTop;
        private System.Windows.Forms.Label labelSummary;
        private System.Windows.Forms.Panel panelExtractDataDetailTop;
        private System.Windows.Forms.Label labelExtractDataDetail;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.ListView listViewExtract;
        private System.Windows.Forms.ColumnHeader columnHeaderExtract;
        private System.Windows.Forms.ColumnHeader columnHeaderTotal;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ListBox listBoxEventsSummary;
        private System.Windows.Forms.Label labelEventSummary;
    }
}