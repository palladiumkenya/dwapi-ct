using System.Reflection;
using log4net;
using PalladiumDwh.Core.Model.Profiles;
using Quartz;

namespace PalladiumDWh.DwapiService.JobManager
{
    public class SyncPatientARTJob:IJob
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void Execute(IJobExecutionContext context)
        {
            Log.Debug($"Execting {typeof(PatientARTProfile).Name} Job...");
        }
    }
}