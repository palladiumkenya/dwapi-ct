using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using PalladiumDWh.DwapiService.JobManager;
using Quartz;
using Quartz.Impl;

namespace PalladiumDWh.DwapiService
{
    partial class ExtractService : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ProfileScheduler _profileScheduler;
        public ExtractService()
        {
            InitializeComponent();
            _profileScheduler=new ProfileScheduler();
        }


        protected override void OnStart(string[] args)
        {
            Log.Debug("DWapiService starting...");

            try
            {
                _profileScheduler.Run();
            }
            catch (SchedulerException se)
            {
                Log.Debug(se);
            }

        }

        protected override void OnStop()
        {
            Log.Debug("DWapiService stopping...");
            _profileScheduler.Shutdown();
            Log.Debug("DWapiService stopped!");
        }
    }
    public class HelloJob : IJob
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void Execute(IJobExecutionContext context)
        {
            Log.Debug("Greetings from HelloJob!");
        }
    }
}
