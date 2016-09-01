using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
   public class PatientBaselineRepository : IPatientBaselineRepository
    {
       public DwhClientEntities Context;
       public const int UploadStatus = 1;
       public PatientBaselineRepository(DwhClientEntities context)
        {
            this.Context = context;
        }
        public IEnumerable<PatientBaselinesExtract> Get()
        {
            return Context.PatientBaselinesExtracts.ToList();
        }

        public PatientBaselinesExtract Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(PatientBaselinesExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Put(int id, PatientBaselinesExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int LoadFromIqTools(string iqToolsDb, string iqToolsServer)
        {
            var query = "INSERT INTO [dwh].[PatientBaselinesExtract] ([PatientCccNumber]," +
                        "[SiteCode] ,[eCD4],[eCD4Date],[eWHO] ,[eWHODate],[bCD4],[bCD4Date],[bWHO],[bWHODate],[lastWHO]," +
                        "[lastWHODate] ,[lastCD4],[lastCD4Date] ,[m12CD4] ,[m12CD4Date] ,[m6CD4]" +
                        " ,[m6CD4Date] ,[Emr],[Project] ,[PatientId],[Uploaded]) Select tmp_PatientMaster.PatientID," +
                        "tmp_PatientMaster.SiteCode,IQC_eCD4.eCD4,IQC_eCD4.eCD4Date,IQC_eWHO.eWHO,IQC_eWHO.eWHODate," +
                        "IQC_bCD4.bCD4,IQC_bCD4.bCD4Date,IQC_bWHO.bWHO,IQC_bWHO.bWHODate," +
                        "IQC_lastWHO.lastWHO,IQC_lastWHO.lastWHODate,IQC_lastCD4.lastCD4,IQC_lastCD4.lastCD4Date,IQC_m12CD4.m12CD4," +
                        "IQC_m12CD4.m12CD4Date, IQC_m6CD4.m6CD4,IQC_m6CD4.m6CD4Date,'IQCare' AS EMR,'HMIS' AS Project" +
                        " ,tmp_PatientMaster.PatientPK,0 From [" + iqToolsServer + "].[" + iqToolsDb + "].dbo.tmp_PatientMaster" +
                        " Left Join [" + iqToolsDb + "].dbo.IQC_bCD4 On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK = [" + iqToolsDb + "].dbo.IQC_bCD4.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_bWAB On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK = [" + iqToolsDb + "].dbo.IQC_bWAB.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_bWHO On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK =[" + iqToolsDb + "].dbo.IQC_bWHO.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_lastCD4 On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK = [" + iqToolsDb + "].dbo.IQC_lastCD4.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_eWAB On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK = [" + iqToolsDb + "].dbo.IQC_eWAB.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_eWHO On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK = [" + iqToolsDb + "].dbo.IQC_eWHO.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_lastWAB On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK = [" + iqToolsDb + "].dbo.IQC_lastWAB.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_eCD4 On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK = [" + iqToolsDb + "].dbo.IQC_eCD4.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_lastWHO On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK =[" + iqToolsDb + "].dbo.IQC_lastWHO.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_m12CD4 On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK =[" + iqToolsDb + "].dbo.IQC_m12CD4.PatientPK " +
                        "Left Join [" + iqToolsDb + "].dbo.IQC_m6CD4 On [" + iqToolsDb + "].dbo.tmp_PatientMaster.PatientPK = [" + iqToolsDb + "].dbo.IQC_m6CD4.PatientPK ";
            return Context.Database.ExecuteSqlCommand(query);
        }

        public void PutComposite(int id,  PatientBaselinesExtract entity)
        {
          var patientBaselineExtract = Context.PatientBaselinesExtracts.Find(id);
            if (patientBaselineExtract != null)
            {
                patientBaselineExtract.Id = entity.Id;
                patientBaselineExtract.PatientCccNumber = entity.PatientCccNumber;
                patientBaselineExtract.SiteCode = entity.SiteCode;
                patientBaselineExtract.eCD4 = entity.eCD4;
                patientBaselineExtract.eCD4Date = entity.eCD4Date;
                patientBaselineExtract.eWHO = entity.eWHO;
                patientBaselineExtract.eWHODate = entity.eWHODate;
                patientBaselineExtract.lastCD4 = entity.lastCD4;
                patientBaselineExtract.lastCD4Date = entity.lastCD4Date;
                patientBaselineExtract.m12CD4 = entity.m12CD4;
                patientBaselineExtract.m12CD4Date = entity.m12CD4Date;
                patientBaselineExtract.m6CD4 = entity.m6CD4;
                patientBaselineExtract.m6CD4Date = entity.m6CD4Date;
                patientBaselineExtract.Emr = entity.Emr;
                patientBaselineExtract.Project = entity.Project;
                patientBaselineExtract.PatientId = entity.PatientId;
                patientBaselineExtract.Uploaded = UploadStatus;
                patientBaselineExtract.DateUploaded = DateTime.Now;
                Context.SaveChanges();
            }
        }
    }
}
