namespace PalladiumDwh.ClientApp.Views
{
    partial class FeedBack
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
            
            this.stripStatus = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelPhone = new System.Windows.Forms.Label();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelComment = new System.Windows.Forms.Label();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.checkBoxSendLogs = new System.Windows.Forms.CheckBox();
            this.panelAction = new System.Windows.Forms.Panel();
            this.buttonSend = new System.Windows.Forms.Button();
            this.stripStatus.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoControlFeedBack
            // 
            
            // 
            // stripStatus
            // 
            this.stripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.progressBarStatus});
            this.stripStatus.Location = new System.Drawing.Point(0, 369);
            this.stripStatus.Name = "stripStatus";
            this.stripStatus.Size = new System.Drawing.Size(386, 22);
            this.stripStatus.TabIndex = 1;
            this.stripStatus.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(269, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.Text = "Ready";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBarStatus
            // 
            this.progressBarStatus.Name = "progressBarStatus";
            this.progressBarStatus.Size = new System.Drawing.Size(100, 16);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelAction);
            this.panelMain.Controls.Add(this.checkBoxSendLogs);
            this.panelMain.Controls.Add(this.textBoxComment);
            this.panelMain.Controls.Add(this.labelComment);
            this.panelMain.Controls.Add(this.textBoxEmail);
            this.panelMain.Controls.Add(this.labelEmail);
            this.panelMain.Controls.Add(this.textBoxPhone);
            this.panelMain.Controls.Add(this.labelPhone);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 79);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(386, 290);
            this.panelMain.TabIndex = 2;
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Location = new System.Drawing.Point(25, 16);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(38, 13);
            this.labelPhone.TabIndex = 0;
            this.labelPhone.Text = "Phone";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(95, 16);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(191, 20);
            this.textBoxPhone.TabIndex = 1;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(95, 49);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(191, 20);
            this.textBoxEmail.TabIndex = 3;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(25, 52);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(32, 13);
            this.labelEmail.TabIndex = 2;
            this.labelEmail.Text = "Email";
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Location = new System.Drawing.Point(25, 81);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(51, 13);
            this.labelComment.TabIndex = 4;
            this.labelComment.Text = "Comment";
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(28, 97);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(346, 92);
            this.textBoxComment.TabIndex = 5;
            // 
            // checkBoxSendLogs
            // 
            this.checkBoxSendLogs.AutoSize = true;
            this.checkBoxSendLogs.Location = new System.Drawing.Point(28, 204);
            this.checkBoxSendLogs.Name = "checkBoxSendLogs";
            this.checkBoxSendLogs.Size = new System.Drawing.Size(77, 17);
            this.checkBoxSendLogs.TabIndex = 6;
            this.checkBoxSendLogs.Text = "Send Logs";
            this.checkBoxSendLogs.UseVisualStyleBackColor = true;
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.buttonSend);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAction.Location = new System.Drawing.Point(0, 257);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(386, 33);
            this.panelAction.TabIndex = 7;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(299, 7);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // FeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 391);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.stripStatus);
            this.Name = "FeedBack";
            this.Text = "FeedBack";
            this.stripStatus.ResumeLayout(false);
            this.stripStatus.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelAction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.StatusStrip stripStatus;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripProgressBar progressBarStatus;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.CheckBox checkBoxSendLogs;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.Button buttonSend;
    }
}