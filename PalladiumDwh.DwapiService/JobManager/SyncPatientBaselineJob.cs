using System.Reflection;
using System.Threading;
using log4net;
using PalladiumDwh.Core.Model.Profiles;
using Quartz;

namespace PalladiumDWh.DwapiService.JobManager
{
    public class SyncPatientBaselineJob : IJob
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void Execute(IJobExecutionContext context)
        {
            Log.Debug($"Execting {typeof(PatientBaselineProfile).Name} Job...");
            Thread.Sleep(1000);
        }
    }
}