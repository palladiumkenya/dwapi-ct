namespace PalladiumDwh.ClientApp.Views.UserControls
{
    partial class InfoControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelInfoMain = new System.Windows.Forms.Panel();
            this.panelInfoH = new System.Windows.Forms.Panel();
            this.panelHD = new System.Windows.Forms.Panel();
            this.labelH = new System.Windows.Forms.Label();
            this.labelHD = new System.Windows.Forms.Label();
            this.panelInfoMain.SuspendLayout();
            this.panelInfoH.SuspendLayout();
            this.panelHD.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInfoMain
            // 
            this.panelInfoMain.Controls.Add(this.panelHD);
            this.panelInfoMain.Controls.Add(this.panelInfoH);
            this.panelInfoMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfoMain.Location = new System.Drawing.Point(0, 0);
            this.panelInfoMain.Name = "panelInfoMain";
            this.panelInfoMain.Padding = new System.Windows.Forms.Padding(3);
            this.panelInfoMain.Size = new System.Drawing.Size(582, 102);
            this.panelInfoMain.TabIndex = 0;
            // 
            // panelInfoH
            // 
            this.panelInfoH.BackColor = System.Drawing.Color.White;
            this.panelInfoH.Controls.Add(this.labelH);
            this.panelInfoH.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfoH.Location = new System.Drawing.Point(3, 3);
            this.panelInfoH.Name = "panelInfoH";
            this.panelInfoH.Size = new System.Drawing.Size(576, 33);
            this.panelInfoH.TabIndex = 0;
            // 
            // panelHD
            // 
            this.panelHD.Controls.Add(this.labelHD);
            this.panelHD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHD.Location = new System.Drawing.Point(3, 36);
            this.panelHD.Name = "panelHD";
            this.panelHD.Size = new System.Drawing.Size(576, 63);
            this.panelHD.TabIndex = 2;
            // 
            // labelH
            // 
            this.labelH.AutoSize = true;
            this.labelH.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelH.Location = new System.Drawing.Point(16, 10);
            this.labelH.Name = "labelH";
            this.labelH.Size = new System.Drawing.Size(48, 13);
            this.labelH.TabIndex = 0;
            this.labelH.Text = "Header";
            // 
            // labelHD
            // 
            this.labelHD.AutoSize = true;
            this.labelHD.Location = new System.Drawing.Point(27, 9);
            this.labelHD.Name = "labelHD";
            this.labelHD.Size = new System.Drawing.Size(60, 13);
            this.labelHD.TabIndex = 1;
            this.labelHD.Text = "Description";
            // 
            // InfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelInfoMain);
            this.Name = "InfoControl";
            this.Size = new System.Drawing.Size(582, 102);
            this.panelInfoMain.ResumeLayout(false);
            this.panelInfoH.ResumeLayout(false);
            this.panelInfoH.PerformLayout();
            this.panelHD.ResumeLayout(false);
            this.panelHD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelInfoMain;
        private System.Windows.Forms.Panel panelHD;
        private System.Windows.Forms.Label labelHD;
        private System.Windows.Forms.Panel panelInfoH;
        private System.Windows.Forms.Label labelH;
    }
}
