namespace PalladiumDwh.ClientApp.Views
{
    partial class ExtractListControl
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
            this.panelExTop = new System.Windows.Forms.Panel();
            this.panelExBottom = new System.Windows.Forms.Panel();
            this.panelExtContent = new System.Windows.Forms.Panel();
            this.listViewExtractList = new System.Windows.Forms.ListView();
            this.labelHeader = new System.Windows.Forms.Label();
            this.panelExTop.SuspendLayout();
            this.panelExtContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelExTop
            // 
            this.panelExTop.Controls.Add(this.labelHeader);
            this.panelExTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExTop.Location = new System.Drawing.Point(0, 0);
            this.panelExTop.Name = "panelExTop";
            this.panelExTop.Size = new System.Drawing.Size(285, 25);
            this.panelExTop.TabIndex = 0;
            // 
            // panelExBottom
            // 
            this.panelExBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelExBottom.Location = new System.Drawing.Point(0, 212);
            this.panelExBottom.Name = "panelExBottom";
            this.panelExBottom.Size = new System.Drawing.Size(285, 24);
            this.panelExBottom.TabIndex = 2;
            // 
            // panelExtContent
            // 
            this.panelExtContent.Controls.Add(this.listViewExtractList);
            this.panelExtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExtContent.Location = new System.Drawing.Point(0, 25);
            this.panelExtContent.Name = "panelExtContent";
            this.panelExtContent.Padding = new System.Windows.Forms.Padding(5);
            this.panelExtContent.Size = new System.Drawing.Size(285, 187);
            this.panelExtContent.TabIndex = 3;
            // 
            // listViewExtractList
            // 
            this.listViewExtractList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewExtractList.Location = new System.Drawing.Point(5, 5);
            this.listViewExtractList.Name = "listViewExtractList";
            this.listViewExtractList.Size = new System.Drawing.Size(275, 177);
            this.listViewExtractList.TabIndex = 1;
            this.listViewExtractList.UseCompatibleStateImageBehavior = false;
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Location = new System.Drawing.Point(12, 6);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(0, 13);
            this.labelHeader.TabIndex = 0;
            // 
            // ExtractListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelExtContent);
            this.Controls.Add(this.panelExTop);
            this.Controls.Add(this.panelExBottom);
            this.Name = "ExtractListControl";
            this.Size = new System.Drawing.Size(285, 236);
            this.panelExTop.ResumeLayout(false);
            this.panelExTop.PerformLayout();
            this.panelExtContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelExTop;
        private System.Windows.Forms.Panel panelExBottom;
        private System.Windows.Forms.Panel panelExtContent;
        private System.Windows.Forms.ListView listViewExtractList;
        private System.Windows.Forms.Label labelHeader;
    }
}
