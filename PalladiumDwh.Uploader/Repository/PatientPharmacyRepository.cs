using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public class PatientPharmacyRepository : IPatientPharamacyRepository
    {

        public DwhClientEntities Context;
        public const int UploadStatus = 1;
        public PatientPharmacyRepository(DwhClientEntities context)
        {
            this.Context = context;
        }
        public IEnumerable<PatientPharmacyExtract> Get()
        {
            return Context.PatientPharmacyExtracts.ToList();
        }

        public PatientPharmacyExtract Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(PatientPharmacyExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Put(int id, PatientPharmacyExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int LoadFromIqTools(string iqToolsDb, string iqToolsServer)
        {
            var query = "INSERT INTO [dwh].[PatientPharmacyExtract] ([PatientID],[PatientCccNumber],[SiteCode]" +
                        ",[VisitID],[Drug],[DispenseDate],[Duration],[ExpectedReturn],[TreatmentType]," +
                        "[PeriodTaken],[ProphylaxisType],[Emr],[Project],[Uploaded]) Select tmp_Pharmacy.PatientPK," +
                        "tmp_PatientMaster.PatientID,tmp_PatientMaster.SiteCode,tmp_Pharmacy.VisitID,tmp_Pharmacy.Drug," +
                        "tmp_Pharmacy.DispenseDate,tmp_Pharmacy.Duration,tmp_Pharmacy.ExpectedReturn," +
                        "tmp_Pharmacy.TreatmentType,tmp_Pharmacy.PeriodTaken,tmp_Pharmacy.ProphylaxisType,'IQCare' AS EMR," +
                        "'HMIS' AS Project,0 From [" + iqToolsServer + "].[" + iqToolsDb + "].dbo.tmp_Pharmacy " +
                        "INNER JOIN  [" + iqToolsDb + "].dbo.tmp_PatientMaster On tmp_Pharmacy.PatientPK = tmp_PatientMaster.PatientPK";
            return Context.Database.ExecuteSqlCommand(query);
        }

        public void PutComposite(int id,  PatientPharmacyExtract entity)
        {
            var patientPharmacyExtract = Context.PatientPharmacyExtracts.Find(id);
            if (patientPharmacyExtract == null) return;
            patientPharmacyExtract.Id = entity.Id;
            patientPharmacyExtract.PatientId = entity.PatientId;
            patientPharmacyExtract.PatientCccNumber = entity.PatientCccNumber;
            patientPharmacyExtract.SiteCode = entity.SiteCode;
            patientPharmacyExtract.VisitID = entity.VisitID;
            patientPharmacyExtract.Drug = entity.Drug;
            patientPharmacyExtract.DispenseDate = entity.DispenseDate;
            patientPharmacyExtract.Duration = entity.Duration;
            patientPharmacyExtract.ExpectedReturn = entity.ExpectedReturn;
            patientPharmacyExtract.TreatmentType = entity.TreatmentType;
            patientPharmacyExtract.PeriodTaken = entity.PeriodTaken;
            patientPharmacyExtract.ProphylaxisType = entity.ProphylaxisType;
            patientPharmacyExtract.Emr = entity.Emr;
            patientPharmacyExtract.Project = entity.Project;
            patientPharmacyExtract.Uploaded = UploadStatus;
            patientPharmacyExtract.DateUploaded = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
