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
            this.infoControl1 = new PalladiumDwh.ClientApp.Views.UserControls.InfoControl();
            this.splitContainerOptions = new System.Windows.Forms.SplitContainer();
            this.panelAction = new System.Windows.Forms.Panel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOptions)).BeginInit();
            this.splitContainerOptions.Panel2.SuspendLayout();
            this.splitContainerOptions.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoControl1
            // 
            this.infoControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoControl1.Header = "Header";
            this.infoControl1.HeaderDescription = "Description";
            this.infoControl1.Location = new System.Drawing.Point(0, 0);
            this.infoControl1.Name = "infoControl1";
            this.infoControl1.Size = new System.Drawing.Size(926, 102);
            this.infoControl1.TabIndex = 0;
            // 
            // splitContainerOptions
            // 
            this.splitContainerOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOptions.Location = new System.Drawing.Point(0, 102);
            this.splitContainerOptions.Name = "splitContainerOptions";
            // 
            // splitContainerOptions.Panel2
            // 
            this.splitContainerOptions.Panel2.Controls.Add(this.panelMain);
            this.splitContainerOptions.Panel2.Controls.Add(this.panelAction);
            this.splitContainerOptions.Size = new System.Drawing.Size(926, 544);
            this.splitContainerOptions.SplitterDistance = 271;
            this.splitContainerOptions.TabIndex = 1;
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.buttonCancel);
            this.panelAction.Controls.Add(this.buttonOk);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAction.Location = new System.Drawing.Point(0, 488);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(651, 56);
            this.panelAction.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(492, 21);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(573, 21);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(651, 488);
            this.panelMain.TabIndex = 2;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 646);
            this.Controls.Add(this.splitContainerOptions);
            this.Controls.Add(this.infoControl1);
            this.Name = "Options";
            this.Text = "Options";
            this.splitContainerOptions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOptions)).EndInit();
            this.splitContainerOptions.ResumeLayout(false);
            this.panelAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.InfoControl infoControl1;
        private System.Windows.Forms.SplitContainer splitContainerOptions;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
    }
}