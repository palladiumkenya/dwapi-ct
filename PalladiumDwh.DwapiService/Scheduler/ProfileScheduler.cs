using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Interfaces;
using PalladiumDWh.DwapiService.Job;
using Quartz;
using Quartz.Impl;

namespace PalladiumDWh.DwapiService.Scheduler
{


    public class ProfileScheduler : IProfileScheduler
    {
        private IScheduler _scheduler;
        
        public void Run()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
            _scheduler.Start();

            var jobs = new List<Type>
            {              
                typeof(SyncPatientARTJob),
                typeof(SyncPatientBaselineJob),
                typeof(SyncPatientPharmacyJob),
                typeof(SyncPatientLabJob),
                typeof(SyncPatientVisitJob),
                typeof(SyncPatientStatusJob)
            };

            foreach (var job in jobs)
                _scheduler.ScheduleJob(GetJobDetail(job), GeTrigger(job));
        }

        private IJobDetail GetJobDetail(Type jobType)
        {
            string jobName = $"j{jobType.Name}";
            string jobGroup = $"j{jobType.Name}group";

            var job = JobBuilder.Create(jobType)
                 .WithIdentity($"{jobName}", $"{jobGroup}")
                 .Build();

            return job;
        }
        private ITrigger GeTrigger(Type jobType)
        {
            string triggerName = $"t{jobType.Name}";
            string triggerGroup = $"t{jobType.Name}group";

            var trigger = TriggerBuilder.Create()
              .WithIdentity($"{triggerName}", $"{triggerGroup}")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(Properties.Settings.Default.QueuePoll)
                  .RepeatForever())
              .Build();

            return trigger;
        }

        public void Shutdown()
        {
            _scheduler.Shutdown(true);
        }
    } 
}