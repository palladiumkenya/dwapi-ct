using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.Profile;
using Quartz;
using Quartz.Impl;

namespace PalladiumDwh.DWapi.Helpers
{
    public class ProfileScheduler
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public async Task Run<T>(List<T> patientProfile) where T :IProfile
        {

            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<MessengerJob>()
                .WithIdentity("job1", "group1")
                .Build();

            job.JobDataMap["profilez"] = patientProfile;

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);

            // and last shut down the scheduler when you are ready to close your program
            await scheduler.Shutdown();
        }
 }
}
