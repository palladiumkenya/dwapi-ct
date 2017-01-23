using System;
using System.Reflection;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model.Profiles;
using Quartz;

namespace PalladiumDWh.DwapiService.Job
{
    [DisallowConcurrentExecution]
    public class SyncPatientLabJob : IJob
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        

        public void Execute(IJobExecutionContext context)
        {
            var profile = typeof(PatientLabProfile).Name;
           // Log.Debug($"Execting {profile} Job...");
            try
            {
                var schedulerContext = context.Scheduler.Context;
                var reader = (IMessagingReaderService)schedulerContext.Get("myKey");
                reader.Initialize(profile);
                reader.Read(profile);
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                JobExecutionException qe = new JobExecutionException(ex);
                qe.RefireImmediately = true;  // this job will refire immediately
            }
        }
    }
}