namespace PalladiumDwh.ClientApp.Views
{
    partial class DatabaseSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseSetup));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
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
            this.panelMainTitle = new System.Windows.Forms.Panel();
            this.labelSectionTitle = new System.Windows.Forms.Label();
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelServer = new System.Windows.Forms.Label();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelMainTitle.SuspendLayout();
            this.groupBoxLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.pbSetup});
            this.statusStrip1.Location = new System.Drawing.Point(0, 224);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(394, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(277, 17);
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
            this.panelHeader.Size = new System.Drawing.Size(394, 10);
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
            this.panelAction.Location = new System.Drawing.Point(0, 197);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(394, 27);
            this.panelAction.TabIndex = 9;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(250, 3);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 10;
            this.buttonEdit.Text = "Change";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(250, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(136, 2);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(108, 23);
            this.buttonTest.TabIndex = 9;
            this.buttonTest.Text = "Test Connection";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelMainTitle);
            this.panelMain.Controls.Add(this.groupBoxLogin);
            this.panelMain.Controls.Add(this.buttonRefresh);
            this.panelMain.Controls.Add(this.labelServer);
            this.panelMain.Controls.Add(this.comboBoxServer);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 10);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(394, 187);
            this.panelMain.TabIndex = 11;
            // 
            // panelMainTitle
            // 
            this.panelMainTitle.Controls.Add(this.labelSectionTitle);
            this.panelMainTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMainTitle.Location = new System.Drawing.Point(0, 0);
            this.panelMainTitle.Name = "panelMainTitle";
            this.panelMainTitle.Size = new System.Drawing.Size(394, 27);
            this.panelMainTitle.TabIndex = 0;
            // 
            // labelSectionTitle
            // 
            this.labelSectionTitle.AutoSize = true;
            this.labelSectionTitle.Location = new System.Drawing.Point(9, 8);
            this.labelSectionTitle.Name = "labelSectionTitle";
            this.labelSectionTitle.Size = new System.Drawing.Size(121, 13);
            this.labelSectionTitle.TabIndex = 0;
            this.labelSectionTitle.Text = "DWAPI Client Database";
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.Controls.Add(this.textBoxDatabase);
            this.groupBoxLogin.Controls.Add(this.labelDatabase);
            this.groupBoxLogin.Controls.Add(this.textBoxUser);
            this.groupBoxLogin.Controls.Add(this.labelUser);
            this.groupBoxLogin.Controls.Add(this.labelPassword);
            this.groupBoxLogin.Controls.Add(this.textBoxPassword);
            this.groupBoxLogin.Location = new System.Drawing.Point(8, 80);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(381, 105);
            this.groupBoxLogin.TabIndex = 10;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "Provide database login credentials";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(127, 22);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(189, 20);
            this.textBoxUser.TabIndex = 6;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(92, 25);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(29, 13);
            this.labelUser.TabIndex = 2;
            this.labelUser.Text = "User";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(68, 52);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(127, 48);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(189, 20);
            this.textBoxPassword.TabIndex = 7;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(328, 39);
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
            this.labelServer.Location = new System.Drawing.Point(11, 44);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(69, 13);
            this.labelServer.TabIndex = 1;
            this.labelServer.Text = "Server Name";
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Location = new System.Drawing.Point(92, 41);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(232, 21);
            this.comboBoxServer.TabIndex = 4;
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(67, 75);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(53, 13);
            this.labelDatabase.TabIndex = 8;
            this.labelDatabase.Text = "Database";
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(128, 72);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.ReadOnly = true;
            this.textBoxDatabase.Size = new System.Drawing.Size(189, 20);
            this.textBoxDatabase.TabIndex = 9;
            // 
            // DatabaseSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 246);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Setup";
            this.Load += new System.EventHandler(this.DatabaseSetup_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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

        private System.Windows.Forms.StatusStrip statusStrip1;
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
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.Label labelDatabase;
    }
}