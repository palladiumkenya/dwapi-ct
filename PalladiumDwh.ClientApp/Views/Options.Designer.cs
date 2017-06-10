namespace PalladiumDwh.ClientApp.Views
{
    partial class Options
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("EMR Setup");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.splitContainerOptions = new System.Windows.Forms.SplitContainer();
            this.listViewOptions = new System.Windows.Forms.ListView();
            this.panelOptionMain = new System.Windows.Forms.Panel();
            this.panelSubOptionMain = new System.Windows.Forms.Panel();
            this.buttonDatabase = new System.Windows.Forms.Button();
            this.labelExtractInfo = new System.Windows.Forms.Label();
            this.labelId = new System.Windows.Forms.Label();
            this.dataGridViewOptions = new System.Windows.Forms.DataGridView();
            this.buttonSetDefault = new System.Windows.Forms.Button();
            this.panelSubOptionHeader = new System.Windows.Forms.Panel();
            this.labelSubOptionTitle = new System.Windows.Forms.Label();
            this.panelOptionInfo = new System.Windows.Forms.Panel();
            this.labelHeader = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOptions)).BeginInit();
            this.splitContainerOptions.Panel1.SuspendLayout();
            this.splitContainerOptions.Panel2.SuspendLayout();
            this.splitContainerOptions.SuspendLayout();
            this.panelOptionMain.SuspendLayout();
            this.panelSubOptionMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOptions)).BeginInit();
            this.panelSubOptionHeader.SuspendLayout();
            this.panelOptionInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.buttonOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 427);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(684, 56);
            this.panel2.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(521, 21);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(440, 21);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // splitContainerOptions
            // 
            this.splitContainerOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOptions.Location = new System.Drawing.Point(0, 0);
            this.splitContainerOptions.Name = "splitContainerOptions";
            // 
            // splitContainerOptions.Panel1
            // 
            this.splitContainerOptions.Panel1.Controls.Add(this.listViewOptions);
            this.splitContainerOptions.Panel1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 3);
            // 
            // splitContainerOptions.Panel2
            // 
            this.splitContainerOptions.Panel2.Controls.Add(this.panelOptionMain);
            this.splitContainerOptions.Panel2.Controls.Add(this.panelOptionInfo);
            this.splitContainerOptions.Size = new System.Drawing.Size(684, 427);
            this.splitContainerOptions.SplitterDistance = 176;
            this.splitContainerOptions.TabIndex = 3;
            // 
            // listViewOptions
            // 
            this.listViewOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewOptions.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewOptions.Location = new System.Drawing.Point(3, 5);
            this.listViewOptions.Name = "listViewOptions";
            this.listViewOptions.Size = new System.Drawing.Size(170, 419);
            this.listViewOptions.TabIndex = 0;
            this.listViewOptions.UseCompatibleStateImageBehavior = false;
            this.listViewOptions.View = System.Windows.Forms.View.List;
            // 
            // panelOptionMain
            // 
            this.panelOptionMain.Controls.Add(this.panelSubOptionMain);
            this.panelOptionMain.Controls.Add(this.panelSubOptionHeader);
            this.panelOptionMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOptionMain.Location = new System.Drawing.Point(0, 48);
            this.panelOptionMain.Name = "panelOptionMain";
            this.panelOptionMain.Size = new System.Drawing.Size(504, 379);
            this.panelOptionMain.TabIndex = 2;
            // 
            // panelSubOptionMain
            // 
            this.panelSubOptionMain.Controls.Add(this.buttonDatabase);
            this.panelSubOptionMain.Controls.Add(this.labelExtractInfo);
            this.panelSubOptionMain.Controls.Add(this.labelId);
            this.panelSubOptionMain.Controls.Add(this.dataGridViewOptions);
            this.panelSubOptionMain.Controls.Add(this.buttonSetDefault);
            this.panelSubOptionMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSubOptionMain.Location = new System.Drawing.Point(0, 29);
            this.panelSubOptionMain.Name = "panelSubOptionMain";
            this.panelSubOptionMain.Size = new System.Drawing.Size(504, 350);
            this.panelSubOptionMain.TabIndex = 2;
            // 
            // buttonDatabase
            // 
            this.buttonDatabase.Location = new System.Drawing.Point(10, 252);
            this.buttonDatabase.Name = "buttonDatabase";
            this.buttonDatabase.Size = new System.Drawing.Size(141, 23);
            this.buttonDatabase.TabIndex = 5;
            this.buttonDatabase.Text = "Database Settings";
            this.buttonDatabase.UseVisualStyleBackColor = true;
            this.buttonDatabase.Click += new System.EventHandler(this.buttonDatabase_Click);
            // 
            // labelExtractInfo
            // 
            this.labelExtractInfo.AutoSize = true;
            this.labelExtractInfo.Location = new System.Drawing.Point(172, 257);
            this.labelExtractInfo.Name = "labelExtractInfo";
            this.labelExtractInfo.Size = new System.Drawing.Size(67, 13);
            this.labelExtractInfo.TabIndex = 4;
            this.labelExtractInfo.Text = "emr (version)";
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelId.ForeColor = System.Drawing.Color.Gray;
            this.labelId.Location = new System.Drawing.Point(476, 290);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(16, 13);
            this.labelId.TabIndex = 3;
            this.labelId.Text = "Id";
            this.labelId.Visible = false;
            // 
            // dataGridViewOptions
            // 
            this.dataGridViewOptions.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewOptions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridViewOptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOptions.Location = new System.Drawing.Point(10, 6);
            this.dataGridViewOptions.Name = "dataGridViewOptions";
            this.dataGridViewOptions.ReadOnly = true;
            this.dataGridViewOptions.RowHeadersVisible = false;
            this.dataGridViewOptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOptions.Size = new System.Drawing.Size(482, 240);
            this.dataGridViewOptions.TabIndex = 2;
            this.dataGridViewOptions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOptions_CellContentClick);
            this.dataGridViewOptions.SelectionChanged += new System.EventHandler(this.dataGridViewOptions_SelectionChanged);
            // 
            // buttonSetDefault
            // 
            this.buttonSetDefault.Location = new System.Drawing.Point(377, 251);
            this.buttonSetDefault.Name = "buttonSetDefault";
            this.buttonSetDefault.Size = new System.Drawing.Size(115, 23);
            this.buttonSetDefault.TabIndex = 1;
            this.buttonSetDefault.Text = "Set as &Default";
            this.buttonSetDefault.UseVisualStyleBackColor = true;
            this.buttonSetDefault.Click += new System.EventHandler(this.buttonSetDefault_Click);
            // 
            // panelSubOptionHeader
            // 
            this.panelSubOptionHeader.Controls.Add(this.labelSubOptionTitle);
            this.panelSubOptionHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubOptionHeader.Location = new System.Drawing.Point(0, 0);
            this.panelSubOptionHeader.Name = "panelSubOptionHeader";
            this.panelSubOptionHeader.Size = new System.Drawing.Size(504, 29);
            this.panelSubOptionHeader.TabIndex = 0;
            // 
            // labelSubOptionTitle
            // 
            this.labelSubOptionTitle.AutoSize = true;
            this.labelSubOptionTitle.Location = new System.Drawing.Point(7, 7);
            this.labelSubOptionTitle.Name = "labelSubOptionTitle";
            this.labelSubOptionTitle.Size = new System.Drawing.Size(144, 13);
            this.labelSubOptionTitle.TabIndex = 1;
            this.labelSubOptionTitle.Text = "Choose Default EMR Source";
            // 
            // panelOptionInfo
            // 
            this.panelOptionInfo.Controls.Add(this.labelHeader);
            this.panelOptionInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOptionInfo.Location = new System.Drawing.Point(0, 0);
            this.panelOptionInfo.Name = "panelOptionInfo";
            this.panelOptionInfo.Size = new System.Drawing.Size(504, 48);
            this.panelOptionInfo.TabIndex = 0;
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Location = new System.Drawing.Point(7, 19);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(232, 13);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "Setup connection to EMR Extracts datasources";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 483);
            this.Controls.Add(this.splitContainerOptions);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.panel2.ResumeLayout(false);
            this.splitContainerOptions.Panel1.ResumeLayout(false);
            this.splitContainerOptions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOptions)).EndInit();
            this.splitContainerOptions.ResumeLayout(false);
            this.panelOptionMain.ResumeLayout(false);
            this.panelSubOptionMain.ResumeLayout(false);
            this.panelSubOptionMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOptions)).EndInit();
            this.panelSubOptionHeader.ResumeLayout(false);
            this.panelSubOptionHeader.PerformLayout();
            this.panelOptionInfo.ResumeLayout(false);
            this.panelOptionInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.SplitContainer splitContainerOptions;
        private System.Windows.Forms.ListView listViewOptions;
        private System.Windows.Forms.Panel panelOptionMain;
        private System.Windows.Forms.Panel panelSubOptionMain;
        private System.Windows.Forms.Button buttonSetDefault;
        private System.Windows.Forms.Panel panelSubOptionHeader;
        private System.Windows.Forms.Label labelSubOptionTitle;
        private System.Windows.Forms.Panel panelOptionInfo;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.DataGridView dataGridViewOptions;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label labelExtractInfo;
        private System.Windows.Forms.Button buttonDatabase;
    }
}