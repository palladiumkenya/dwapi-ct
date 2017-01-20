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
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            Log.Debug("DWapiService stopping...");
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
