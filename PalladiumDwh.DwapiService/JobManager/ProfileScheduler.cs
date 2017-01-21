using System;
using System.Collections.Generic;
using Quartz;
using Quartz.Impl;

namespace PalladiumDWh.DwapiService.JobManager
{
    public class ProfileScheduler
    {
        private IScheduler _scheduler;

        public void Run()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
            _scheduler.Start();

           
            int n = 1;

            IJobDetail job = JobBuilder.Create<SyncPatientARTJob>()
                .WithIdentity($"job{n}", $"group{n}")
                .Build();

            _scheduler.ScheduleJob(job, GeTrigger(n,n));

            n++;

            IJobDetail job2 = JobBuilder.Create<SyncPatientBaselineJob>()
                .WithIdentity($"job{n}", $"group{n}")
                .Build();

            _scheduler.ScheduleJob(job2, GeTrigger(n, n));

        }

        public ITrigger GeTrigger(int id,int group)
        {
            ITrigger trigger2 = TriggerBuilder.Create()
              .WithIdentity($"trigger{id}", $"group{group}")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(5)
                  .RepeatForever())
              .Build();

            return trigger2;
        }

        public void Shutdown()
        {
            _scheduler.Shutdown(true);
        }
    }

  
}