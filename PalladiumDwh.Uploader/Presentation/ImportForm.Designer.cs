namespace PalladiumDwh.Uploader.Presentation
{
    partial class ImportForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonLoadDatabases = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sqlPasswordTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.buttonLoadServers = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.sqlServerComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.importButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.databaseComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.databaseComboBox);
            this.groupBox1.Controls.Add(this.buttonLoadDatabases);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.sqlPasswordTextBox);
            this.groupBox1.Controls.Add(this.userNameTextBox);
            this.groupBox1.Controls.Add(this.buttonLoadServers);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.sqlServerComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox1.Location = new System.Drawing.Point(7, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Facility IQTools Database";
            // 
            // buttonLoadDatabases
            // 
            this.buttonLoadDatabases.Location = new System.Drawing.Point(305, 87);
            this.buttonLoadDatabases.Name = "buttonLoadDatabases";
            this.buttonLoadDatabases.Size = new System.Drawing.Size(31, 23);
            this.buttonLoadDatabases.TabIndex = 9;
            this.buttonLoadDatabases.Text = "...";
            this.buttonLoadDatabases.UseVisualStyleBackColor = true;
            this.buttonLoadDatabases.Click += new System.EventHandler(this.buttonLoadDatabases_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password:";
            // 
            // sqlPasswordTextBox
            // 
            this.sqlPasswordTextBox.Location = new System.Drawing.Point(120, 66);
            this.sqlPasswordTextBox.Name = "sqlPasswordTextBox";
            this.sqlPasswordTextBox.Size = new System.Drawing.Size(179, 20);
            this.sqlPasswordTextBox.TabIndex = 5;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(120, 40);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(179, 20);
            this.userNameTextBox.TabIndex = 4;
            // 
            // buttonLoadServers
            // 
            this.buttonLoadServers.Location = new System.Drawing.Point(369, 11);
            this.buttonLoadServers.Name = "buttonLoadServers";
            this.buttonLoadServers.Size = new System.Drawing.Size(31, 23);
            this.buttonLoadServers.TabIndex = 3;
            this.buttonLoadServers.Text = "...";
            this.buttonLoadServers.UseVisualStyleBackColor = true;
            this.buttonLoadServers.Click += new System.EventHandler(this.buttonLoadServers_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Username:";
            // 
            // sqlServerComboBox
            // 
            this.sqlServerComboBox.FormattingEnabled = true;
            this.sqlServerComboBox.Location = new System.Drawing.Point(120, 13);
            this.sqlServerComboBox.Name = "sqlServerComboBox";
            this.sqlServerComboBox.Size = new System.Drawing.Size(243, 21);
            this.sqlServerComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL Server: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.importButton);
            this.groupBox2.Location = new System.Drawing.Point(7, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(543, 55);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(6, 19);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(98, 23);
            this.importButton.TabIndex = 0;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(110, 26);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(418, 10);
            this.progressBar1.TabIndex = 0;
            // 
            // databaseComboBox
            // 
            this.databaseComboBox.FormattingEnabled = true;
            this.databaseComboBox.Location = new System.Drawing.Point(120, 89);
            this.databaseComboBox.Name = "databaseComboBox";
            this.databaseComboBox.Size = new System.Drawing.Size(179, 21);
            this.databaseComboBox.TabIndex = 10;
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 193);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ImportForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonLoadDatabases;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox sqlPasswordTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Button buttonLoadServers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sqlServerComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox databaseComboBox;
    }
}