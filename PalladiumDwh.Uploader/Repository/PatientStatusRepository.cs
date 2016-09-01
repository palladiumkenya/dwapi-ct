using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public class PatientStatusRepository : IPatientStatusRepository
    {
        public DwhClientEntities Context;

        public const int UploadStatus = 1;
        public PatientStatusRepository(DwhClientEntities context)
        {
            Context = context;
        }
        public IEnumerable<PatientStatusExtract> Get()
        {
            return Context.PatientStatusExtracts.ToList();
        }

        public PatientStatusExtract Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(PatientStatusExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Put(int id, PatientStatusExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int LoadFromIqTools(string iqToolsDb, string iqToolsServer)
        {
            var query = "INSERT INTO [dwh].[PatientStatusExtract] ([PatientId],[PatientCccNumber]," +
                        "[SiteCode],[FacilityName],[ExitDescription],[ExitDate],[ExitReason],[Emr]," +
                        "[Project],[Uploaded]) Select tmp_LastStatus.PatientPK," +
                        "tmp_PatientMaster.PatientID,tmp_PatientMaster.SiteCode, " +
                        "tmp_PatientMaster.FacilityName, tmp_LastStatus.ExitDescription," +
                        "tmp_LastStatus.ExitDate,tmp_LastStatus.ExitReason,'IQCare' AS EMR " +
                        ",'HMIS' AS Project,0 From " +
                        "[" + iqToolsServer + "].[" + iqToolsDb + "].dbo.tmp_LastStatus " +
                        "INNER JOIN  [" + iqToolsServer + "].[" + iqToolsDb + "].dbo.tmp_PatientMaster On " +
                        "tmp_LastStatus.PatientPK =  tmp_PatientMaster.PatientPK";
            return Context.Database.ExecuteSqlCommand(query);
        }

        public void PutComposite(int id,  PatientStatusExtract entity)
        {
            var patientStatusExtract = Context.PatientStatusExtracts.Find(id);
            if (patientStatusExtract == null) return;
            patientStatusExtract.Id = entity.Id;
            patientStatusExtract.PatientId = entity.PatientId;
            patientStatusExtract.PatientCccNumber = entity.PatientCccNumber;
            patientStatusExtract.SiteCode = entity.SiteCode;
            patientStatusExtract.FacilityName = entity.FacilityName;
            patientStatusExtract.ExitDescription = entity.ExitDescription;
            patientStatusExtract.ExitDate = entity.ExitDate;
            patientStatusExtract.ExitReason = entity.ExitReason;
            patientStatusExtract.Emr = entity.Emr;
            patientStatusExtract.Project = entity.Project;
            patientStatusExtract.Uploaded = UploadStatus;
            patientStatusExtract.DateUploaded = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
