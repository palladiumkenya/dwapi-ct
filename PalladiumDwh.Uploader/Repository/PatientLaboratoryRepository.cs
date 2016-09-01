using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public class PatientLaboratoryRepository : IPatientLaboratoryRepository
    {

        public DwhClientEntities Context;
        public const int UploadStatus = 1;
        public PatientLaboratoryRepository(DwhClientEntities context)
        {
            this.Context = context;
        }
        public IEnumerable<PatientLaboratoryExtract> Get()
        {
            return Context.PatientLaboratoryExtracts.ToList();
        }

        public PatientLaboratoryExtract Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(PatientLaboratoryExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Put(int id, PatientLaboratoryExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int LoadFromIqTools(string iqToolsDb, string iqToolsServer)
        {
            var query = "INSERT INTO [dwh].[PatientLaboratoryExtract] ([PatientId]," +
                        "[PatientCccNumber],[SiteCode] ," +
                        "[VisitId],[OrderedByDate],[ReportedByDate],[TestName],[TestResult]," +
                        "[Emr],[Project] ,[Uploaded]) Select tmp_Labs.PatientPK," +
                        "tmp_PatientMaster.PatientID,tmp_PatientMaster.SiteCode,tmp_Labs.VisitID," +
                        "tmp_Labs.OrderedbyDate,tmp_Labs.ReportedbyDate,tmp_Labs.TestName," +
                        "tmp_Labs.TestResult,'IQCare' AS EMR ,'HMIS' AS Project,0 " +
                        "From [" + iqToolsServer + "].[" + iqToolsDb + "].dbo.tmp_Labs " +
                        "inner Join [" + iqToolsServer + "].[" + iqToolsDb + "].dbo.tmp_PatientMaster " +
                        "On tmp_Labs.PatientPK = tmp_PatientMaster.PatientPK";
            return Context.Database.ExecuteSqlCommand(query);
        }

        public void PutComposite(int id, PatientLaboratoryExtract entity)
        {
            var patientLaboratoryExtract = Context.PatientLaboratoryExtracts.Find(id);
            if (patientLaboratoryExtract == null) return;
            patientLaboratoryExtract.Id = entity.Id;
            patientLaboratoryExtract.PatientId = entity.PatientId;
            patientLaboratoryExtract.PatientCccNumber = entity.PatientCccNumber;
            patientLaboratoryExtract.SiteCode = entity.SiteCode;
            patientLaboratoryExtract.VisitId = entity.VisitId;
            patientLaboratoryExtract.OrderedByDate = entity.OrderedByDate;
            patientLaboratoryExtract.ReportedByDate = entity.ReportedByDate;
            patientLaboratoryExtract.TestName = entity.TestName;
            patientLaboratoryExtract.TestResult = entity.TestResult;
            patientLaboratoryExtract.Emr = entity.Emr;
            patientLaboratoryExtract.Project = entity.Project;
            patientLaboratoryExtract.Uploaded = UploadStatus;
            patientLaboratoryExtract.DateUploaded = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
