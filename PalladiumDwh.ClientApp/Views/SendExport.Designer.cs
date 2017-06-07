namespace PalladiumDwh.ClientApp.Views
{
    partial class SendExport
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
            this.openFileDialogExports = new System.Windows.Forms.OpenFileDialog();
            this.statusStripExports = new System.Windows.Forms.StatusStrip();
            this.tabControlExports = new System.Windows.Forms.TabControl();
            this.tabPageStatus = new System.Windows.Forms.TabPage();
            this.splitContainerExports = new System.Windows.Forms.SplitContainer();
            this.listViewExports = new System.Windows.Forms.ListView();
            this.columnHeaderSite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProfiles = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listBoxEventsSummary = new System.Windows.Forms.ListBox();
            this.panelEventsH = new System.Windows.Forms.Panel();
            this.labelEventSummary = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.linkLableDelete = new System.Windows.Forms.LinkLabel();
            this.labelCount = new System.Windows.Forms.Label();
            this.tabPageErrors = new System.Windows.Forms.TabPage();
            this.panelErrors = new System.Windows.Forms.Panel();
            this.panelErrorsH = new System.Windows.Forms.Panel();
            this.panelAction = new System.Windows.Forms.Panel();
            this.buttonSendDWH = new System.Windows.Forms.Button();
            this.buttonLoadExport = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelHDescrption = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.labelExportsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbExports = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStripExports.SuspendLayout();
            this.tabControlExports.SuspendLayout();
            this.tabPageStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerExports)).BeginInit();
            this.splitContainerExports.Panel1.SuspendLayout();
            this.splitContainerExports.Panel2.SuspendLayout();
            this.splitContainerExports.SuspendLayout();
            this.panelEventsH.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.tabPageErrors.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialogExports
            // 
            this.openFileDialogExports.Multiselect = true;
            // 
            // statusStripExports
            // 
            this.statusStripExports.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelExportsStatus,
            this.pbExports});
            this.statusStripExports.Location = new System.Drawing.Point(0, 384);
            this.statusStripExports.Name = "statusStripExports";
            this.statusStripExports.Size = new System.Drawing.Size(702, 22);
            this.statusStripExports.TabIndex = 5;
            this.statusStripExports.Text = "statusStrip1";
            // 
            // tabControlExports
            // 
            this.tabControlExports.Controls.Add(this.tabPageStatus);
            this.tabControlExports.Controls.Add(this.tabPageErrors);
            this.tabControlExports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlExports.Location = new System.Drawing.Point(0, 43);
            this.tabControlExports.Name = "tabControlExports";
            this.tabControlExports.SelectedIndex = 0;
            this.tabControlExports.Size = new System.Drawing.Size(702, 341);
            this.tabControlExports.TabIndex = 8;
            // 
            // tabPageStatus
            // 
            this.tabPageStatus.Controls.Add(this.splitContainerExports);
            this.tabPageStatus.Controls.Add(this.panelInfo);
            this.tabPageStatus.Location = new System.Drawing.Point(4, 22);
            this.tabPageStatus.Name = "tabPageStatus";
            this.tabPageStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStatus.Size = new System.Drawing.Size(694, 315);
            this.tabPageStatus.TabIndex = 0;
            this.tabPageStatus.Text = "Status";
            this.tabPageStatus.UseVisualStyleBackColor = true;
            // 
            // splitContainerExports
            // 
            this.splitContainerExports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerExports.Location = new System.Drawing.Point(3, 3);
            this.splitContainerExports.Name = "splitContainerExports";
            // 
            // splitContainerExports.Panel1
            // 
            this.splitContainerExports.Panel1.Controls.Add(this.listViewExports);
            // 
            // splitContainerExports.Panel2
            // 
            this.splitContainerExports.Panel2.Controls.Add(this.listBoxEventsSummary);
            this.splitContainerExports.Panel2.Controls.Add(this.panelEventsH);
            this.splitContainerExports.Size = new System.Drawing.Size(688, 286);
            this.splitContainerExports.SplitterDistance = 458;
            this.splitContainerExports.TabIndex = 5;
            // 
            // listViewExports
            // 
            this.listViewExports.AutoArrange = false;
            this.listViewExports.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSite,
            this.columnHeaderProfiles,
            this.columnHeaderStatus,
            this.columnHeaderLocation});
            this.listViewExports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewExports.FullRowSelect = true;
            this.listViewExports.HideSelection = false;
            this.listViewExports.Location = new System.Drawing.Point(0, 0);
            this.listViewExports.MultiSelect = false;
            this.listViewExports.Name = "listViewExports";
            this.listViewExports.Size = new System.Drawing.Size(458, 286);
            this.listViewExports.TabIndex = 1;
            this.listViewExports.UseCompatibleStateImageBehavior = false;
            this.listViewExports.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderSite
            // 
            this.columnHeaderSite.Text = "Site";
            this.columnHeaderSite.Width = 63;
            // 
            // columnHeaderProfiles
            // 
            this.columnHeaderProfiles.Text = "Records";
            this.columnHeaderProfiles.Width = 53;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "Status";
            this.columnHeaderStatus.Width = 155;
            // 
            // columnHeaderLocation
            // 
            this.columnHeaderLocation.Text = "Location";
            this.columnHeaderLocation.Width = 127;
            // 
            // listBoxEventsSummary
            // 
            this.listBoxEventsSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxEventsSummary.FormattingEnabled = true;
            this.listBoxEventsSummary.Location = new System.Drawing.Point(0, 26);
            this.listBoxEventsSummary.Name = "listBoxEventsSummary";
            this.listBoxEventsSummary.Size = new System.Drawing.Size(226, 260);
            this.listBoxEventsSummary.TabIndex = 8;
            // 
            // panelEventsH
            // 
            this.panelEventsH.Controls.Add(this.labelEventSummary);
            this.panelEventsH.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEventsH.Location = new System.Drawing.Point(0, 0);
            this.panelEventsH.Name = "panelEventsH";
            this.panelEventsH.Size = new System.Drawing.Size(226, 26);
            this.panelEventsH.TabIndex = 0;
            // 
            // labelEventSummary
            // 
            this.labelEventSummary.AutoSize = true;
            this.labelEventSummary.Location = new System.Drawing.Point(3, 7);
            this.labelEventSummary.Name = "labelEventSummary";
            this.labelEventSummary.Size = new System.Drawing.Size(86, 13);
            this.labelEventSummary.TabIndex = 8;
            this.labelEventSummary.Text = "Events Summary";
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.linkLableDelete);
            this.panelInfo.Controls.Add(this.labelCount);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInfo.Location = new System.Drawing.Point(3, 289);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(688, 23);
            this.panelInfo.TabIndex = 2;
            // 
            // linkLableDelete
            // 
            this.linkLableDelete.AutoSize = true;
            this.linkLableDelete.Location = new System.Drawing.Point(2, 6);
            this.linkLableDelete.Name = "linkLableDelete";
            this.linkLableDelete.Size = new System.Drawing.Size(52, 13);
            this.linkLableDelete.TabIndex = 11;
            this.linkLableDelete.TabStop = true;
            this.linkLableDelete.Text = "Delete All";
            this.linkLableDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLableDelete_LinkClicked);
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCount.Location = new System.Drawing.Point(60, 6);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(106, 13);
            this.labelCount.TabIndex = 10;
            this.labelCount.Text = "0 Loaded Exports";
            // 
            // tabPageErrors
            // 
            this.tabPageErrors.Controls.Add(this.panelErrors);
            this.tabPageErrors.Controls.Add(this.panelErrorsH);
            this.tabPageErrors.Location = new System.Drawing.Point(4, 22);
            this.tabPageErrors.Name = "tabPageErrors";
            this.tabPageErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrors.Size = new System.Drawing.Size(694, 337);
            this.tabPageErrors.TabIndex = 1;
            this.tabPageErrors.Text = "Send Errors";
            this.tabPageErrors.UseVisualStyleBackColor = true;
            // 
            // panelErrors
            // 
            this.panelErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelErrors.Location = new System.Drawing.Point(3, 30);
            this.panelErrors.Name = "panelErrors";
            this.panelErrors.Size = new System.Drawing.Size(688, 304);
            this.panelErrors.TabIndex = 1;
            // 
            // panelErrorsH
            // 
            this.panelErrorsH.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelErrorsH.Location = new System.Drawing.Point(3, 3);
            this.panelErrorsH.Name = "panelErrorsH";
            this.panelErrorsH.Size = new System.Drawing.Size(688, 27);
            this.panelErrorsH.TabIndex = 0;
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.buttonSendDWH);
            this.panelAction.Controls.Add(this.buttonLoadExport);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAction.Location = new System.Drawing.Point(0, 10);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(702, 33);
            this.panelAction.TabIndex = 7;
            // 
            // buttonSendDWH
            // 
            this.buttonSendDWH.Location = new System.Drawing.Point(123, 5);
            this.buttonSendDWH.Name = "buttonSendDWH";
            this.buttonSendDWH.Size = new System.Drawing.Size(167, 23);
            this.buttonSendDWH.TabIndex = 1;
            this.buttonSendDWH.Text = "Send to Data Warehouse";
            this.buttonSendDWH.UseVisualStyleBackColor = true;
            this.buttonSendDWH.Click += new System.EventHandler(this.buttonSendDWH_Click);
            // 
            // buttonLoadExport
            // 
            this.buttonLoadExport.Location = new System.Drawing.Point(7, 5);
            this.buttonLoadExport.Name = "buttonLoadExport";
            this.buttonLoadExport.Size = new System.Drawing.Size(110, 23);
            this.buttonLoadExport.TabIndex = 0;
            this.buttonLoadExport.Text = "Load Exports";
            this.buttonLoadExport.UseVisualStyleBackColor = true;
            this.buttonLoadExport.Click += new System.EventHandler(this.buttonLoadExport_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panelHeader.Controls.Add(this.labelHDescrption);
            this.panelHeader.Controls.Add(this.labelHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(702, 10);
            this.panelHeader.TabIndex = 6;
            // 
            // labelHDescrption
            // 
            this.labelHDescrption.AutoSize = true;
            this.labelHDescrption.Location = new System.Drawing.Point(33, 24);
            this.labelHDescrption.Name = "labelHDescrption";
            this.labelHDescrption.Size = new System.Drawing.Size(61, 13);
            this.labelHDescrption.TabIndex = 10;
            this.labelHDescrption.Text = " Descrption";
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(12, 9);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(49, 13);
            this.labelHeader.TabIndex = 9;
            this.labelHeader.Text = "Exports";
            // 
            // labelExportsStatus
            // 
            this.labelExportsStatus.Name = "labelExportsStatus";
            this.labelExportsStatus.Size = new System.Drawing.Size(454, 17);
            this.labelExportsStatus.Spring = true;
            this.labelExportsStatus.Text = "Ready";
            this.labelExportsStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbExports
            // 
            this.pbExports.Name = "pbExports";
            this.pbExports.Size = new System.Drawing.Size(200, 16);
            // 
            // SendExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 406);
            this.Controls.Add(this.tabControlExports);
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.statusStripExports);
            this.Name = "SendExport";
            this.Text = "Manage Exports";
            this.Load += new System.EventHandler(this.SendExport_Load);
            this.statusStripExports.ResumeLayout(false);
            this.statusStripExports.PerformLayout();
            this.tabControlExports.ResumeLayout(false);
            this.tabPageStatus.ResumeLayout(false);
            this.splitContainerExports.Panel1.ResumeLayout(false);
            this.splitContainerExports.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerExports)).EndInit();
            this.splitContainerExports.ResumeLayout(false);
            this.panelEventsH.ResumeLayout(false);
            this.panelEventsH.PerformLayout();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.tabPageErrors.ResumeLayout(false);
            this.panelAction.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialogExports;
        private System.Windows.Forms.StatusStrip statusStripExports;
        private System.Windows.Forms.ToolStripStatusLabel labelExportsStatus;
        private System.Windows.Forms.ToolStripProgressBar pbExports;
        private System.Windows.Forms.TabControl tabControlExports;
        private System.Windows.Forms.TabPage tabPageStatus;
        private System.Windows.Forms.SplitContainer splitContainerExports;
        private System.Windows.Forms.ListView listViewExports;
        private System.Windows.Forms.ColumnHeader columnHeaderSite;
        private System.Windows.Forms.ColumnHeader columnHeaderProfiles;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderLocation;
        private System.Windows.Forms.ListBox listBoxEventsSummary;
        private System.Windows.Forms.Panel panelEventsH;
        private System.Windows.Forms.Label labelEventSummary;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.LinkLabel linkLableDelete;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.TabPage tabPageErrors;
        private System.Windows.Forms.Panel panelErrors;
        private System.Windows.Forms.Panel panelErrorsH;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button buttonSendDWH;
        private System.Windows.Forms.Button buttonLoadExport;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelHDescrption;
        private System.Windows.Forms.Label labelHeader;
    }
}