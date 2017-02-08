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
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.splitContainerOptions = new System.Windows.Forms.SplitContainer();
            this.listViewOptions = new System.Windows.Forms.ListView();
            this.panelOptionInfo = new System.Windows.Forms.Panel();
            this.panelOptionMain = new System.Windows.Forms.Panel();
            this.panelSubOptionHeader = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.panelSubOptionMain = new System.Windows.Forms.Panel();
            this.listViewSubOption = new System.Windows.Forms.ListView();
            this.buttonSetDefault = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOptions)).BeginInit();
            this.splitContainerOptions.Panel1.SuspendLayout();
            this.splitContainerOptions.Panel2.SuspendLayout();
            this.splitContainerOptions.SuspendLayout();
            this.panelOptionInfo.SuspendLayout();
            this.panelOptionMain.SuspendLayout();
            this.panelSubOptionHeader.SuspendLayout();
            this.panelSubOptionMain.SuspendLayout();
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
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(440, 21);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
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
            // panelOptionInfo
            // 
            this.panelOptionInfo.Controls.Add(this.labelTitle);
            this.panelOptionInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOptionInfo.Location = new System.Drawing.Point(0, 0);
            this.panelOptionInfo.Name = "panelOptionInfo";
            this.panelOptionInfo.Size = new System.Drawing.Size(504, 48);
            this.panelOptionInfo.TabIndex = 0;
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
            // panelSubOptionHeader
            // 
            this.panelSubOptionHeader.Controls.Add(this.labelHeader);
            this.panelSubOptionHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubOptionHeader.Location = new System.Drawing.Point(0, 0);
            this.panelSubOptionHeader.Name = "panelSubOptionHeader";
            this.panelSubOptionHeader.Size = new System.Drawing.Size(504, 29);
            this.panelSubOptionHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(11, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(232, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Setup connection to EMR Extracts datasources";
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Location = new System.Drawing.Point(10, 7);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(144, 13);
            this.labelHeader.TabIndex = 1;
            this.labelHeader.Text = "Choose Default EMR Source";
            // 
            // panelSubOptionMain
            // 
            this.panelSubOptionMain.Controls.Add(this.buttonSetDefault);
            this.panelSubOptionMain.Controls.Add(this.listViewSubOption);
            this.panelSubOptionMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSubOptionMain.Location = new System.Drawing.Point(0, 29);
            this.panelSubOptionMain.Name = "panelSubOptionMain";
            this.panelSubOptionMain.Size = new System.Drawing.Size(504, 350);
            this.panelSubOptionMain.TabIndex = 2;
            // 
            // listViewSubOption
            // 
            this.listViewSubOption.Location = new System.Drawing.Point(27, 6);
            this.listViewSubOption.Name = "listViewSubOption";
            this.listViewSubOption.Size = new System.Drawing.Size(465, 240);
            this.listViewSubOption.TabIndex = 0;
            this.listViewSubOption.UseCompatibleStateImageBehavior = false;
            // 
            // buttonSetDefault
            // 
            this.buttonSetDefault.Location = new System.Drawing.Point(27, 252);
            this.buttonSetDefault.Name = "buttonSetDefault";
            this.buttonSetDefault.Size = new System.Drawing.Size(140, 23);
            this.buttonSetDefault.TabIndex = 1;
            this.buttonSetDefault.Text = "Set as &Default";
            this.buttonSetDefault.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 483);
            this.Controls.Add(this.splitContainerOptions);
            this.Controls.Add(this.panel2);
            this.Name = "Options";
            this.Text = "Options";
            this.panel2.ResumeLayout(false);
            this.splitContainerOptions.Panel1.ResumeLayout(false);
            this.splitContainerOptions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOptions)).EndInit();
            this.splitContainerOptions.ResumeLayout(false);
            this.panelOptionInfo.ResumeLayout(false);
            this.panelOptionInfo.PerformLayout();
            this.panelOptionMain.ResumeLayout(false);
            this.panelSubOptionHeader.ResumeLayout(false);
            this.panelSubOptionHeader.PerformLayout();
            this.panelSubOptionMain.ResumeLayout(false);
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
        private System.Windows.Forms.ListView listViewSubOption;
        private System.Windows.Forms.Panel panelSubOptionHeader;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Panel panelOptionInfo;
        private System.Windows.Forms.Label labelTitle;
    }
}