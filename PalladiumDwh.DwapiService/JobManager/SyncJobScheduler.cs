using System.Reflection;
using log4net;
using PalladiumDwh.Core.Interfaces;
using Quartz;
using Quartz.Impl;

namespace PalladiumDWh.DwapiService.JobManager
{
    public class SyncJobScheduler
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static ISchedulerFactory _schedFact;
        private static IScheduler _sched;

        public static void Start()
        {
            // construct a scheduler factory
            _schedFact = new StdSchedulerFactory();
            _sched = _schedFact.GetScheduler();
            _sched.Start();

            
            

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<SyncJob>()
                .WithIdentity("dwapiEmrJ", "DWSJ01")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("dwapiEmrT", "DWST01")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(Properties.Settings.Default.QueuePoll)
                  .RepeatForever())
              .Build();

            _sched.ScheduleJob(job, trigger);
        }

        public static void Stop()
        {
            
            _sched.Shutdown(true);
            Log.Debug("shutting down compltete");
        }
    }
}