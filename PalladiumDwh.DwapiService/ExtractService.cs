using System;
using System.Reflection;
using System.ServiceProcess;
using log4net;
using PalladiumDwh.Core.Interfaces;

namespace PalladiumDWh.DwapiService
{
    partial class ExtractService : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProfileScheduler _profileScheduler;

        public ExtractService()
        {
            InitializeComponent();
        }

        public void RunSvc()
        {
            Log.Debug("DWapiService starting...");
            try
            {
                _profileScheduler = Program.IOC.GetInstance<IProfileScheduler>();
                _profileScheduler.Run();
            }
            catch (Exception se)
            {
                Log.Debug(se);
            }
        }

        public void StopSvc()
        {
            Log.Debug("DWapiService stopping...");
            _profileScheduler.Shutdown();
            Log.Debug("DWapiService stopped!");
        }

        protected override void OnStart(string[] args)
        {
            RunSvc();
        }
        protected override void OnStop()
        {
            StopSvc();
        }
    }


}
