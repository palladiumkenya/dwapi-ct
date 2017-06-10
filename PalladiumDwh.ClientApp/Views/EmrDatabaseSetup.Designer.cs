namespace PalladiumDwh.ClientApp.Views
{
    partial class EmrDatabaseSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmrDatabaseSetup));
            this.statusStripEmrDatabase = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbSetup = new System.Windows.Forms.ToolStripProgressBar();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelHDescrption = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.panelAction = new System.Windows.Forms.Panel();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonRefreshDatabase = new System.Windows.Forms.Button();
            this.labelEmrKey = new System.Windows.Forms.Label();
            this.labelEmrName = new System.Windows.Forms.Label();
            this.comboBoxDatabaseName = new System.Windows.Forms.ComboBox();
            this.labelDatabaseName = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelDatabaseType = new System.Windows.Forms.Label();
            this.comboBoxDatabaseType = new System.Windows.Forms.ComboBox();
            this.panelMainTitle = new System.Windows.Forms.Panel();
            this.labelEmrId = new System.Windows.Forms.Label();
            this.labelSectionTitle = new System.Windows.Forms.Label();
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelServer = new System.Windows.Forms.Label();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.statusStripEmrDatabase.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelMainTitle.SuspendLayout();
            this.groupBoxLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripEmrDatabase
            // 
            this.statusStripEmrDatabase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.pbSetup});
            this.statusStripEmrDatabase.Location = new System.Drawing.Point(0, 341);
            this.statusStripEmrDatabase.Name = "statusStripEmrDatabase";
            this.statusStripEmrDatabase.Size = new System.Drawing.Size(415, 22);
            this.statusStripEmrDatabase.TabIndex = 0;
            this.statusStripEmrDatabase.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(298, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.Text = "Ready";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbSetup
            // 
            this.pbSetup.Name = "pbSetup";
            this.pbSetup.Size = new System.Drawing.Size(100, 16);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panelHeader.Controls.Add(this.labelHDescrption);
            this.panelHeader.Controls.Add(this.labelHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(415, 10);
            this.panelHeader.TabIndex = 7;
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
            // panelAction
            // 
            this.panelAction.Controls.Add(this.buttonEdit);
            this.panelAction.Controls.Add(this.buttonSave);
            this.panelAction.Controls.Add(this.buttonTest);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAction.Location = new System.Drawing.Point(0, 314);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(415, 27);
            this.panelAction.TabIndex = 9;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(218, 2);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 10;
            this.buttonEdit.Text = "Change";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(218, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(104, 2);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(108, 23);
            this.buttonTest.TabIndex = 9;
            this.buttonTest.Text = "Test Connection";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonRefreshDatabase);
            this.panelMain.Controls.Add(this.labelEmrKey);
            this.panelMain.Controls.Add(this.labelEmrName);
            this.panelMain.Controls.Add(this.comboBoxDatabaseName);
            this.panelMain.Controls.Add(this.labelDatabaseName);
            this.panelMain.Controls.Add(this.textBoxPort);
            this.panelMain.Controls.Add(this.labelPort);
            this.panelMain.Controls.Add(this.labelDatabaseType);
            this.panelMain.Controls.Add(this.comboBoxDatabaseType);
            this.panelMain.Controls.Add(this.panelMainTitle);
            this.panelMain.Controls.Add(this.groupBoxLogin);
            this.panelMain.Controls.Add(this.buttonRefresh);
            this.panelMain.Controls.Add(this.labelServer);
            this.panelMain.Controls.Add(this.comboBoxServer);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 10);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(415, 304);
            this.panelMain.TabIndex = 11;
            // 
            // buttonRefreshDatabase
            // 
            this.buttonRefreshDatabase.Location = new System.Drawing.Point(345, 263);
            this.buttonRefreshDatabase.Name = "buttonRefreshDatabase";
            this.buttonRefreshDatabase.Size = new System.Drawing.Size(61, 23);
            this.buttonRefreshDatabase.TabIndex = 18;
            this.buttonRefreshDatabase.Text = "Refresh";
            this.buttonRefreshDatabase.UseVisualStyleBackColor = true;
            this.buttonRefreshDatabase.Click += new System.EventHandler(this.buttonRefreshDatabase_Click);
            // 
            // labelEmrKey
            // 
            this.labelEmrKey.AutoSize = true;
            this.labelEmrKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmrKey.ForeColor = System.Drawing.Color.Gray;
            this.labelEmrKey.Location = new System.Drawing.Point(255, 36);
            this.labelEmrKey.Name = "labelEmrKey";
            this.labelEmrKey.Size = new System.Drawing.Size(43, 13);
            this.labelEmrKey.TabIndex = 17;
            this.labelEmrKey.Text = "EmrKey";
            // 
            // labelEmrName
            // 
            this.labelEmrName.AutoSize = true;
            this.labelEmrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmrName.Location = new System.Drawing.Point(104, 36);
            this.labelEmrName.Name = "labelEmrName";
            this.labelEmrName.Size = new System.Drawing.Size(34, 13);
            this.labelEmrName.TabIndex = 15;
            this.labelEmrName.Text = "EMR";
            // 
            // comboBoxDatabaseName
            // 
            this.comboBoxDatabaseName.FormattingEnabled = true;
            this.comboBoxDatabaseName.Location = new System.Drawing.Point(12, 264);
            this.comboBoxDatabaseName.Name = "comboBoxDatabaseName";
            this.comboBoxDatabaseName.Size = new System.Drawing.Size(327, 21);
            this.comboBoxDatabaseName.TabIndex = 9;
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(20, 248);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(126, 13);
            this.labelDatabaseName.TabIndex = 8;
            this.labelDatabaseName.Text = "Enter or Select Database";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(107, 117);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(55, 20);
            this.textBoxPort.TabIndex = 14;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(74, 117);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(26, 13);
            this.labelPort.TabIndex = 13;
            this.labelPort.Text = "Port";
            // 
            // labelDatabaseType
            // 
            this.labelDatabaseType.AutoSize = true;
            this.labelDatabaseType.Location = new System.Drawing.Point(20, 64);
            this.labelDatabaseType.Name = "labelDatabaseType";
            this.labelDatabaseType.Size = new System.Drawing.Size(80, 13);
            this.labelDatabaseType.TabIndex = 11;
            this.labelDatabaseType.Text = "Database Type";
            // 
            // comboBoxDatabaseType
            // 
            this.comboBoxDatabaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabaseType.FormattingEnabled = true;
            this.comboBoxDatabaseType.Location = new System.Drawing.Point(107, 61);
            this.comboBoxDatabaseType.Name = "comboBoxDatabaseType";
            this.comboBoxDatabaseType.Size = new System.Drawing.Size(157, 21);
            this.comboBoxDatabaseType.TabIndex = 12;
            this.comboBoxDatabaseType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabaseType_SelectedIndexChanged);
            // 
            // panelMainTitle
            // 
            this.panelMainTitle.Controls.Add(this.labelEmrId);
            this.panelMainTitle.Controls.Add(this.labelSectionTitle);
            this.panelMainTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMainTitle.Location = new System.Drawing.Point(0, 0);
            this.panelMainTitle.Name = "panelMainTitle";
            this.panelMainTitle.Size = new System.Drawing.Size(415, 27);
            this.panelMainTitle.TabIndex = 0;
            // 
            // labelEmrId
            // 
            this.labelEmrId.AutoSize = true;
            this.labelEmrId.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmrId.ForeColor = System.Drawing.Color.Gray;
            this.labelEmrId.Location = new System.Drawing.Point(104, 8);
            this.labelEmrId.Name = "labelEmrId";
            this.labelEmrId.Size = new System.Drawing.Size(16, 13);
            this.labelEmrId.TabIndex = 16;
            this.labelEmrId.Text = "Id";
            // 
            // labelSectionTitle
            // 
            this.labelSectionTitle.AutoSize = true;
            this.labelSectionTitle.Location = new System.Drawing.Point(9, 8);
            this.labelSectionTitle.Name = "labelSectionTitle";
            this.labelSectionTitle.Size = new System.Drawing.Size(80, 13);
            this.labelSectionTitle.TabIndex = 0;
            this.labelSectionTitle.Text = "EMR Database";
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.Controls.Add(this.textBoxUser);
            this.groupBoxLogin.Controls.Add(this.labelUser);
            this.groupBoxLogin.Controls.Add(this.labelPassword);
            this.groupBoxLogin.Controls.Add(this.textBoxPassword);
            this.groupBoxLogin.Location = new System.Drawing.Point(6, 152);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(400, 81);
            this.groupBoxLogin.TabIndex = 10;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "Provide database login credentials";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(144, 21);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(189, 20);
            this.textBoxUser.TabIndex = 6;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(109, 24);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(29, 13);
            this.labelUser.TabIndex = 2;
            this.labelUser.Text = "User";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(85, 51);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(144, 47);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(189, 20);
            this.textBoxPassword.TabIndex = 7;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(345, 86);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(61, 23);
            this.buttonRefresh.TabIndex = 5;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(31, 91);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(69, 13);
            this.labelServer.TabIndex = 1;
            this.labelServer.Text = "Server Name";
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Location = new System.Drawing.Point(107, 88);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(232, 21);
            this.comboBoxServer.TabIndex = 4;
            // 
            // EmrDatabaseSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 363);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.statusStripEmrDatabase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmrDatabaseSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EMR Database Setup";
            this.Load += new System.EventHandler(this.DatabaseSetup_Load);
            this.statusStripEmrDatabase.ResumeLayout(false);
            this.statusStripEmrDatabase.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelAction.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelMainTitle.ResumeLayout(false);
            this.panelMainTitle.PerformLayout();
            this.groupBoxLogin.ResumeLayout(false);
            this.groupBoxLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripEmrDatabase;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripProgressBar pbSetup;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelHDescrption;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelMainTitle;
        private System.Windows.Forms.Label labelSectionTitle;
        private System.Windows.Forms.GroupBox groupBoxLogin;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.ComboBox comboBoxServer;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label labelDatabaseType;
        private System.Windows.Forms.ComboBox comboBoxDatabaseType;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.ComboBox comboBoxDatabaseName;
        private System.Windows.Forms.Label labelDatabaseName;
        private System.Windows.Forms.Label labelEmrName;
        private System.Windows.Forms.Label labelEmrId;
        private System.Windows.Forms.Label labelEmrKey;
        private System.Windows.Forms.Button buttonRefreshDatabase;
    }
}