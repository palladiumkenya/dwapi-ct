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
using PalladiumDwh.Core.Interfaces;
using PalladiumDWh.DwapiService.Scheduler;
using Quartz;
using Quartz.Impl;
using StructureMap.Attributes;

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


        protected override void OnStart(string[] args)
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

        protected override void OnStop()
        {
            Log.Debug("DWapiService stopping...");
            _profileScheduler.Shutdown();
            Log.Debug("DWapiService stopped!");
        }
    }
  
    
}
