using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.Profile;
using Quartz;

namespace PalladiumDwh.DWapi.Helpers
{
    public class MessengerJob<T> : IJob where T:IProfile
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Task Execute(IJobExecutionContext context)
        {
            var senderService = StructuremapMvc.DwapiIContainer.GetInstance<IMessagingSenderService>();
            var profiles = (List<T>) context.JobDetail.JobDataMap["profilez"];
            var gateway = context.JobDetail.JobDataMap.GetString("gateway");

            foreach (var profile in profiles)
            {
                profile.GeneratePatientRecord();
                senderService.Send(profile, gateway);
            }

            return Task.CompletedTask;
        }
    }
}
