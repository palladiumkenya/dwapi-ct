namespace PalladiumDwh.Uploader.Presentation
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.importFromIQToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataExtractsTreeView = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.previousUploadsTreeView = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.patientGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.artPatientGridView = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.patientLabGridView = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.patientPharmacyGridView = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.patientBaseLinesGridView = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.patientVisitsGridView = new System.Windows.Forms.DataGridView();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.patientStatusGridView = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.loggingTextBox = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.loadButton = new System.Windows.Forms.Button();
            this.validateButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artPatientGridView)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientLabGridView)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientPharmacyGridView)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientBaseLinesGridView)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientVisitsGridView)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientStatusGridView)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1034, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureDatabaseToolStripMenuItem,
            this.toolStripMenuItem2,
            this.importFromIQToolsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // configureDatabaseToolStripMenuItem
            // 
            this.configureDatabaseToolStripMenuItem.Name = "configureDatabaseToolStripMenuItem";
            this.configureDatabaseToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.configureDatabaseToolStripMenuItem.Text = "Settings";
            this.configureDatabaseToolStripMenuItem.Click += new System.EventHandler(this.configureDatabaseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(178, 6);
            // 
            // importFromIQToolsToolStripMenuItem
            // 
            this.importFromIQToolsToolStripMenuItem.Name = "importFromIQToolsToolStripMenuItem";
            this.importFromIQToolsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.importFromIQToolsToolStripMenuItem.Text = "Import from IQTools";
            this.importFromIQToolsToolStripMenuItem.Click += new System.EventHandler(this.importFromIQToolsToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.69633F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.45261F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.417795F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.736944F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.59961F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.statusLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.progressBarUpload, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.69768F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.302325F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 235F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1034, 668);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataExtractsTreeView);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(208, 377);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Data Extracts";
            // 
            // dataExtractsTreeView
            // 
            this.dataExtractsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataExtractsTreeView.Location = new System.Drawing.Point(10, 20);
            this.dataExtractsTreeView.Name = "dataExtractsTreeView";
            this.dataExtractsTreeView.Size = new System.Drawing.Size(185, 351);
            this.dataExtractsTreeView.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.previousUploadsTreeView);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Location = new System.Drawing.Point(3, 386);
            this.groupBox2.Name = "groupBox2";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox2, 3);
            this.groupBox2.Size = new System.Drawing.Size(208, 279);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Previous Uploads";
            // 
            // previousUploadsTreeView
            // 
            this.previousUploadsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previousUploadsTreeView.Location = new System.Drawing.Point(10, 19);
            this.previousUploadsTreeView.Name = "previousUploadsTreeView";
            this.previousUploadsTreeView.Size = new System.Drawing.Size(185, 250);
            this.previousUploadsTreeView.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tabControl1, 4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(217, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(814, 342);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.patientGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(806, 316);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Patients";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // patientGridView
            // 
            this.patientGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientGridView.Location = new System.Drawing.Point(3, 3);
            this.patientGridView.Name = "patientGridView";
            this.patientGridView.Size = new System.Drawing.Size(800, 310);
            this.patientGridView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.artPatientGridView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(806, 316);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Art Patients";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // artPatientGridView
            // 
            this.artPatientGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.artPatientGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.artPatientGridView.Location = new System.Drawing.Point(3, 3);
            this.artPatientGridView.Name = "artPatientGridView";
            this.artPatientGridView.Size = new System.Drawing.Size(800, 310);
            this.artPatientGridView.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.patientLabGridView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(806, 316);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Patient Labs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // patientLabGridView
            // 
            this.patientLabGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientLabGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientLabGridView.Location = new System.Drawing.Point(3, 3);
            this.patientLabGridView.Name = "patientLabGridView";
            this.patientLabGridView.Size = new System.Drawing.Size(800, 310);
            this.patientLabGridView.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.patientPharmacyGridView);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(806, 316);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Patient Pharmacy";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // patientPharmacyGridView
            // 
            this.patientPharmacyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientPharmacyGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientPharmacyGridView.Location = new System.Drawing.Point(3, 3);
            this.patientPharmacyGridView.Name = "patientPharmacyGridView";
            this.patientPharmacyGridView.Size = new System.Drawing.Size(800, 310);
            this.patientPharmacyGridView.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.patientBaseLinesGridView);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(806, 316);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Patient Baselines";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // patientBaseLinesGridView
            // 
            this.patientBaseLinesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientBaseLinesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientBaseLinesGridView.Location = new System.Drawing.Point(3, 3);
            this.patientBaseLinesGridView.Name = "patientBaseLinesGridView";
            this.patientBaseLinesGridView.Size = new System.Drawing.Size(800, 310);
            this.patientBaseLinesGridView.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.patientVisitsGridView);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(806, 316);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Patient Visits";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // patientVisitsGridView
            // 
            this.patientVisitsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientVisitsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientVisitsGridView.Location = new System.Drawing.Point(3, 3);
            this.patientVisitsGridView.Name = "patientVisitsGridView";
            this.patientVisitsGridView.Size = new System.Drawing.Size(800, 310);
            this.patientVisitsGridView.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.patientStatusGridView);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(806, 316);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Patient Status";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // patientStatusGridView
            // 
            this.patientStatusGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientStatusGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientStatusGridView.Location = new System.Drawing.Point(3, 3);
            this.patientStatusGridView.Name = "patientStatusGridView";
            this.patientStatusGridView.Size = new System.Drawing.Size(800, 310);
            this.patientStatusGridView.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox3, 4);
            this.groupBox3.Controls.Add(this.loggingTextBox);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox3.Location = new System.Drawing.Point(217, 386);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(814, 229);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // loggingTextBox
            // 
            this.loggingTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loggingTextBox.Location = new System.Drawing.Point(7, 19);
            this.loggingTextBox.Multiline = true;
            this.loggingTextBox.Name = "loggingTextBox";
            this.loggingTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.loggingTextBox.Size = new System.Drawing.Size(801, 204);
            this.loggingTextBox.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.statusLabel, 4);
            this.statusLabel.Location = new System.Drawing.Point(217, 618);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(88, 13);
            this.statusLabel.TabIndex = 7;
            this.statusLabel.Text = "Status Strip Text ";
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.progressBarUpload, 4);
            this.progressBarUpload.Location = new System.Drawing.Point(217, 644);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(814, 18);
            this.progressBarUpload.Step = 1;
            this.progressBarUpload.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.loadButton);
            this.flowLayoutPanel1.Controls.Add(this.validateButton);
            this.flowLayoutPanel1.Controls.Add(this.uploadButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(217, 351);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(493, 29);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(3, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(123, 26);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Load ";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(132, 3);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(120, 26);
            this.validateButton.TabIndex = 3;
            this.validateButton.Text = "Validate";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(258, 3);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(121, 26);
            this.uploadButton.TabIndex = 4;
            this.uploadButton.Text = "Upload";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 698);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1034, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 720);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Upload Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.artPatientGridView)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientLabGridView)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientPharmacyGridView)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientBaseLinesGridView)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientVisitsGridView)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientStatusGridView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView dataExtractsTreeView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView previousUploadsTreeView;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox loggingTextBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.DataGridView patientGridView;
        private System.Windows.Forms.DataGridView artPatientGridView;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView patientLabGridView;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView patientPharmacyGridView;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView patientBaseLinesGridView;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView patientVisitsGridView;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.DataGridView patientStatusGridView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem importFromIQToolsToolStripMenuItem;



    }
}

