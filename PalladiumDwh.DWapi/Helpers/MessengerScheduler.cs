﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Interfaces.Profiles;
using Quartz;
using Quartz.Impl;

namespace PalladiumDwh.DWapi.Helpers
{
    public class MessengerScheduler:IMessengerScheduler
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IScheduler scheduler;
        private StdSchedulerFactory factory;

        public MessengerScheduler()
        {
            factory = new StdSchedulerFactory();
        }

        public async void Start()
        {
            scheduler = await factory.GetScheduler();
            // and start it off
            await scheduler.Start();
        }

        public async Task Run<T>(List<T> patientProfile,string gateway) where T :IProfile
        {
            IJobDetail job = JobBuilder
                .Create<MessengerJob<T>>()
                .Build();

            job.JobDataMap["profilez"] = patientProfile;
            job.JobDataMap["gateway"] =gateway;

            ITrigger trigger = TriggerBuilder
                .Create()
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
        public async void Shutdown()
        {
            await scheduler.Shutdown(true);
        }
    }
}
