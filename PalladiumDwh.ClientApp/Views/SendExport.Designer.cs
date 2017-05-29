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
            this.panelExtractData = new System.Windows.Forms.Panel();
            this.labelEventSummary = new System.Windows.Forms.Label();
            this.listBoxEventsSummary = new System.Windows.Forms.ListBox();
            this.labelId = new System.Windows.Forms.Label();
            this.listViewExtract = new System.Windows.Forms.ListView();
            this.columnHeaderExtract = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelExtractAction = new System.Windows.Forms.Panel();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelExtractData.SuspendLayout();
            this.panelExtractAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelExtractData
            // 
            this.panelExtractData.Controls.Add(this.panel2);
            this.panelExtractData.Controls.Add(this.labelId);
            this.panelExtractData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExtractData.Location = new System.Drawing.Point(0, 123);
            this.panelExtractData.Name = "panelExtractData";
            this.panelExtractData.Size = new System.Drawing.Size(702, 283);
            this.panelExtractData.TabIndex = 9;
            // 
            // labelEventSummary
            // 
            this.labelEventSummary.AutoSize = true;
            this.labelEventSummary.Location = new System.Drawing.Point(557, 5);
            this.labelEventSummary.Name = "labelEventSummary";
            this.labelEventSummary.Size = new System.Drawing.Size(86, 13);
            this.labelEventSummary.TabIndex = 6;
            this.labelEventSummary.Text = "Events Summary";
            // 
            // listBoxEventsSummary
            // 
            this.listBoxEventsSummary.FormattingEnabled = true;
            this.listBoxEventsSummary.Location = new System.Drawing.Point(369, 5);
            this.listBoxEventsSummary.Name = "listBoxEventsSummary";
            this.listBoxEventsSummary.Size = new System.Drawing.Size(180, 69);
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
            this.listViewExtract.Location = new System.Drawing.Point(247, 5);
            this.listViewExtract.MultiSelect = false;
            this.listViewExtract.Name = "listViewExtract";
            this.listViewExtract.Size = new System.Drawing.Size(107, 101);
            this.listViewExtract.TabIndex = 0;
            this.listViewExtract.UseCompatibleStateImageBehavior = false;
            this.listViewExtract.View = System.Windows.Forms.View.Details;
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
            // panelExtractAction
            // 
            this.panelExtractAction.Controls.Add(this.listViewExtract);
            this.panelExtractAction.Controls.Add(this.buttonImport);
            this.panelExtractAction.Controls.Add(this.listBoxEventsSummary);
            this.panelExtractAction.Controls.Add(this.labelEventSummary);
            this.panelExtractAction.Controls.Add(this.buttonSend);
            this.panelExtractAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExtractAction.Location = new System.Drawing.Point(0, 0);
            this.panelExtractAction.Name = "panelExtractAction";
            this.panelExtractAction.Size = new System.Drawing.Size(702, 123);
            this.panelExtractAction.TabIndex = 8;
            this.panelExtractAction.Paint += new System.Windows.Forms.PaintEventHandler(this.panelExtractAction_Paint);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(13, 4);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(82, 31);
            this.buttonImport.TabIndex = 4;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(101, 5);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(140, 31);
            this.buttonSend.TabIndex = 3;
            this.buttonSend.Text = "Send to Data Warehouse";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(702, 283);
            this.panel2.TabIndex = 9;
            // 
            // SendExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 406);
            this.Controls.Add(this.panelExtractData);
            this.Controls.Add(this.panelExtractAction);
            this.Name = "SendExport";
            this.Text = "SendExport";
            this.panelExtractData.ResumeLayout(false);
            this.panelExtractData.PerformLayout();
            this.panelExtractAction.ResumeLayout(false);
            this.panelExtractAction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelExtractData;
        private System.Windows.Forms.Label labelEventSummary;
        private System.Windows.Forms.ListBox listBoxEventsSummary;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.ListView listViewExtract;
        private System.Windows.Forms.ColumnHeader columnHeaderExtract;
        private System.Windows.Forms.ColumnHeader columnHeaderTotal;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.Panel panelExtractAction;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Panel panel2;
    }
}