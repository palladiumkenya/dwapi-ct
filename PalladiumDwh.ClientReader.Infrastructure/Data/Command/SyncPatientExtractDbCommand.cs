using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class SyncPatientExtractDbCommand : SyncCommand<TempPatientExtract,ClientPatientExtract>, ISyncPatientExtractCommand
  {
      public SyncPatientExtractDbCommand(string connectionString) : base(connectionString)
      {
            
      }

        //TODO Sync Facilities
      public override void Execute()
      {
          base.Execute();
            /*
             * SELECT [SiteCode],max([Emr])Emr,max([Project])Project,max([FacilityName])FacilityName
  FROM [DWAPIRemote].[dbo].[PatientExtract]
  group by [SiteCode],[Emr],[Project],[FacilityName]
             */
        }
    }
}
