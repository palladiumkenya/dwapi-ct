using System;
using System.Reflection;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model.Profiles;
using Quartz;

namespace PalladiumDWh.DwapiService.JobManager
{
    //[DisallowConcurrentExecution]
    public class SyncJob : IJob
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IMessagingReaderService MessagingReaderService { get; set; }

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Doing job...{DateTime.Now}");
            /*
            MessagingReaderService.Initialize(typeof(PatientARTProfile).Name.ToLower());
            MessagingReaderService.Read();
            */
        }

    }
}
