using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net;
using PalladiumDWh.DwapiService.JobManager;

namespace PalladiumDWh.DwapiService
{
    partial class ExtractService : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ExtractService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log.Debug("DWapiService starting...");
            SyncJobScheduler.Start();
        }

        protected override void OnStop()
        {
            Log.Debug("DWapiService stopping...");
            SyncJobScheduler.Start();
            Log.Debug("DWapiService stopped!");
        }
    }
}
