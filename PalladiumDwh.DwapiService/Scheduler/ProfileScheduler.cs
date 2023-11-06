using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDWh.DwapiService.Job;
using Quartz;
using Quartz.Impl;

namespace PalladiumDWh.DwapiService.Scheduler
{


    public class ProfileScheduler : IProfileScheduler
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IScheduler _scheduler;

        public void Run()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
            _scheduler.Start();

            Log.Debug("DWapiService started !");

            var jobs = new List<Type>
            {
                typeof(SyncManifestJob),
                typeof(SyncPatientARTJob),
                typeof(SyncPatientBaselineJob),
                typeof(SyncPatientPharmacyJob),
                typeof(SyncPatientLabJob),
                typeof(SyncPatientVisitJob),
                typeof(SyncPatientAdverseEventJob),
                typeof(SyncPatientStatusJob),
                typeof(SyncAllergiesChronicIllnessJob),
                typeof(SyncIptJob),
                typeof(SyncDepressionScreeningJob),
                typeof(SyncContactListingJob),
                typeof(SyncGbvScreeningJob),
                typeof(SyncEnhancedAdherenceCounsellingJob),
                typeof(SyncDrugAlcoholScreeningJob),
                typeof(SyncOvcJob),
                typeof(SyncOtzJob),

                typeof(SyncCovidJob),
                typeof(SyncDefaulterTracingJob),
                typeof(SyncCancerScreeningJob),
                typeof(SyncIITRiskScoresJob),
                typeof(SyncArtFastTrackJob),
                typeof(SyncCervicalCancerScreeningJob)


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
