namespace PalladiumDwh.ClientApp.Views
{
    partial class Uploader
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
            this.infoControlUploader = new PalladiumDwh.ClientApp.Views.UserControls.InfoControl();
            this.splitContainerUploader = new System.Windows.Forms.SplitContainer();
            this.panelExtractAction = new System.Windows.Forms.Panel();
            this.panelAction = new System.Windows.Forms.Panel();
            this.panelInfoBottom = new System.Windows.Forms.Panel();
            this.panelInfoTop = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabControlExtract = new System.Windows.Forms.TabControl();
            this.tabPageExtract = new System.Windows.Forms.TabPage();
            this.tabPageSummary = new System.Windows.Forms.TabPage();
            this.panelExtract = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerUploader)).BeginInit();
            this.splitContainerUploader.Panel1.SuspendLayout();
            this.splitContainerUploader.Panel2.SuspendLayout();
            this.splitContainerUploader.SuspendLayout();
            this.panelExtractAction.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tabControlExtract.SuspendLayout();
            this.panelExtract.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoControlUploader
            // 
            this.infoControlUploader.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoControlUploader.Header = "Header";
            this.infoControlUploader.HeaderDescription = "Description";
            this.infoControlUploader.Location = new System.Drawing.Point(0, 0);
            this.infoControlUploader.Name = "infoControlUploader";
            this.infoControlUploader.Size = new System.Drawing.Size(1066, 102);
            this.infoControlUploader.TabIndex = 0;
            // 
            // splitContainerUploader
            // 
            this.splitContainerUploader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerUploader.Location = new System.Drawing.Point(0, 102);
            this.splitContainerUploader.Name = "splitContainerUploader";
            // 
            // splitContainerUploader.Panel1
            // 
            this.splitContainerUploader.Panel1.Controls.Add(this.panelExtract);
            this.splitContainerUploader.Panel1.Controls.Add(this.panelExtractAction);
            // 
            // splitContainerUploader.Panel2
            // 
            this.splitContainerUploader.Panel2.Controls.Add(this.panelMain);
            this.splitContainerUploader.Panel2.Controls.Add(this.panelInfoTop);
            this.splitContainerUploader.Panel2.Controls.Add(this.panelInfoBottom);
            this.splitContainerUploader.Panel2.Controls.Add(this.panelAction);
            this.splitContainerUploader.Size = new System.Drawing.Size(1066, 650);
            this.splitContainerUploader.SplitterDistance = 256;
            this.splitContainerUploader.TabIndex = 1;
            // 
            // panelExtractAction
            // 
            this.panelExtractAction.Controls.Add(this.button2);
            this.panelExtractAction.Controls.Add(this.button1);
            this.panelExtractAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExtractAction.Location = new System.Drawing.Point(0, 0);
            this.panelExtractAction.Name = "panelExtractAction";
            this.panelExtractAction.Size = new System.Drawing.Size(256, 39);
            this.panelExtractAction.TabIndex = 1;
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.button3);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAction.Location = new System.Drawing.Point(0, 550);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(806, 100);
            this.panelAction.TabIndex = 2;
            // 
            // panelInfoBottom
            // 
            this.panelInfoBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInfoBottom.Location = new System.Drawing.Point(0, 517);
            this.panelInfoBottom.Name = "panelInfoBottom";
            this.panelInfoBottom.Size = new System.Drawing.Size(806, 33);
            this.panelInfoBottom.TabIndex = 3;
            // 
            // panelInfoTop
            // 
            this.panelInfoTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfoTop.Location = new System.Drawing.Point(0, 0);
            this.panelInfoTop.Name = "panelInfoTop";
            this.panelInfoTop.Size = new System.Drawing.Size(806, 34);
            this.panelInfoTop.TabIndex = 5;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControlExtract);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 34);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(806, 483);
            this.panelMain.TabIndex = 6;
            // 
            // tabControlExtract
            // 
            this.tabControlExtract.Controls.Add(this.tabPageExtract);
            this.tabControlExtract.Controls.Add(this.tabPageSummary);
            this.tabControlExtract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlExtract.Location = new System.Drawing.Point(0, 0);
            this.tabControlExtract.Name = "tabControlExtract";
            this.tabControlExtract.SelectedIndex = 0;
            this.tabControlExtract.Size = new System.Drawing.Size(806, 483);
            this.tabControlExtract.TabIndex = 0;
            // 
            // tabPageExtract
            // 
            this.tabPageExtract.Location = new System.Drawing.Point(4, 22);
            this.tabPageExtract.Name = "tabPageExtract";
            this.tabPageExtract.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExtract.Size = new System.Drawing.Size(798, 457);
            this.tabPageExtract.TabIndex = 0;
            this.tabPageExtract.Text = "Extract";
            this.tabPageExtract.UseVisualStyleBackColor = true;
            // 
            // tabPageSummary
            // 
            this.tabPageSummary.Location = new System.Drawing.Point(4, 22);
            this.tabPageSummary.Name = "tabPageSummary";
            this.tabPageSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSummary.Size = new System.Drawing.Size(798, 457);
            this.tabPageSummary.TabIndex = 1;
            this.tabPageSummary.Text = "Validation Summary";
            this.tabPageSummary.UseVisualStyleBackColor = true;
            // 
            // panelExtract
            // 
            this.panelExtract.Controls.Add(this.listView1);
            this.panelExtract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExtract.Location = new System.Drawing.Point(0, 39);
            this.panelExtract.Name = "panelExtract";
            this.panelExtract.Size = new System.Drawing.Size(256, 611);
            this.panelExtract.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load from EMR";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(130, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Load from Csv";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(256, 611);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(607, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(195, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Send to Datawarehouse";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Uploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 752);
            this.Controls.Add(this.splitContainerUploader);
            this.Controls.Add(this.infoControlUploader);
            this.Name = "Uploader";
            this.Text = "Uploader";
            this.splitContainerUploader.Panel1.ResumeLayout(false);
            this.splitContainerUploader.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerUploader)).EndInit();
            this.splitContainerUploader.ResumeLayout(false);
            this.panelExtractAction.ResumeLayout(false);
            this.panelAction.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.tabControlExtract.ResumeLayout(false);
            this.panelExtract.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.InfoControl infoControlUploader;
        private System.Windows.Forms.SplitContainer splitContainerUploader;
        private System.Windows.Forms.Panel panelExtractAction;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelInfoTop;
        private System.Windows.Forms.Panel panelInfoBottom;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Panel panelExtract;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControlExtract;
        private System.Windows.Forms.TabPage tabPageExtract;
        private System.Windows.Forms.TabPage tabPageSummary;
        private System.Windows.Forms.Button button3;
    }
}