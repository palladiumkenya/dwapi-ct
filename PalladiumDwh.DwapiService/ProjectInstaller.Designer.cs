namespace PalladiumDWh.DwapiService
{
    partial class ProjectInstaller
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
            this.dwapiServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.dwapiServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // dwapiServiceProcessInstaller
            // 
            this.dwapiServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.dwapiServiceProcessInstaller.Password = null;
            this.dwapiServiceProcessInstaller.Username = null;
            // 
            // dwapiServiceInstaller
            // 
            this.dwapiServiceInstaller.Description = "DWapi Service";
            this.dwapiServiceInstaller.DisplayName = "DWapi Service";
            this.dwapiServiceInstaller.ServiceName = "DWapi Service";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.dwapiServiceProcessInstaller,
            this.dwapiServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller dwapiServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller dwapiServiceInstaller;
    }
}