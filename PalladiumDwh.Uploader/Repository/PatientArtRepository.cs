using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public class PatientArtRepository : IPatientArtExtractRepository
    {
        public DwhClientEntities Context;
         public const int UploadStatus = 1;

        public PatientArtRepository(DwhClientEntities context)
        {
            Context = context;
        }
        public IEnumerable<PatientArtExtract> Get()
        {
           return Context.PatientArtExtracts.ToList();
        }
        public PatientArtExtract Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(PatientArtExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Put(int id, PatientArtExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int LoadFromIqTools(string iqToolsDb, string iqToolsServer)
        {
            var query = "INSERT INTO [dwh].[PatientArtExtract]([SiteCode] ,[PatientId],[PatientCccNumber]," +
                        "[AgeEnrollment],[AgeARTStart],[AgeLastVisit],[FacilityName],[RegistrationDate]" +
                        ",[PatientSource],[Gender],[StartARTDate],[PreviousARTStartDate],[PreviousARTRegimen]" +
                        ",[StartARTAtThisFacility],[StartRegimen],[StartRegimenLine],[LastARTDate]" +
                        ",[LastRegimen],[LastRegimenLine],[Duration] ,[ExpectedReturn],[LastVisit]," +
                        "[ExitReason],[ExitDate],[Emr],[Project],[Uploaded])(SELECT pa.[SiteCode],pa.[PatientPK],pa.[PatientID]" +
                        ",pa.[AgeEnrollment],pa.[AgeARTStart],pa.[AgeLastVisit],pa.[FacilityName]" +
                        ",pa.[RegistrationDate],pa.[PatientSource],pa.[Gender],[StartARTDate]" +
                        ",pa.[PreviousARTStartDate],[PreviousARTRegimen] ,[StartARTAtThisFacility],[StartRegimen],[StartRegimenLine]" +
                        ",[LastARTDate],[LastRegimen],[LastRegimenLine],[Duration],[ExpectedReturn],pa.[LastVisit],[ExitReason]" +
                        " ,[ExitDate],'IQCare' AS EMR,'HMIS' AS Project, 0 FROM " +
                        "[" + iqToolsServer + "].[" + iqToolsDb + "].[dbo].[tmp_ARTPatients] pa INNER JOIN " +
                        "[" + iqToolsServer + "].[" + iqToolsDb + "].[dbo].[tmp_PatientMaster] p ON pa.PatientPK =p.PatientPK )";

            return Context.Database.ExecuteSqlCommand(query);
        }

        public void PutComposite(int id, PatientArtExtract entity)
        {
          var patientArtExtract = Context.PatientArtExtracts.Find(id);
            if (patientArtExtract == null) return;
            patientArtExtract.PatientCccNumber = entity.PatientCccNumber;
            patientArtExtract.AgeEnrollment = entity.AgeEnrollment;
            patientArtExtract.AgeARTStart = entity.AgeARTStart;
            patientArtExtract.AgeLastVisit = entity.AgeLastVisit;
            patientArtExtract.SiteCode = entity.SiteCode;
            patientArtExtract.FacilityName = entity.FacilityName;
            patientArtExtract.RegistrationDate = entity.RegistrationDate;
            patientArtExtract.PatientSource = entity.PatientSource;
            patientArtExtract.Gender = entity.Gender;
            patientArtExtract.StartARTDate = entity.StartARTDate;
            patientArtExtract.PreviousARTStartDate = entity.PreviousARTStartDate;
            patientArtExtract.PreviousARTRegimen = entity.PreviousARTRegimen;
            patientArtExtract.StartARTAtThisFacility = entity.StartARTAtThisFacility;
            patientArtExtract.StartRegimen = entity.StartRegimen;
            patientArtExtract.StartRegimenLine = entity.StartRegimenLine;
            patientArtExtract.LastARTDate = entity.LastARTDate;
            patientArtExtract.LastRegimen = entity.LastRegimen;
            patientArtExtract.LastRegimenLine = entity.LastRegimenLine;
            patientArtExtract.Duration = entity.Duration;
            patientArtExtract.ExpectedReturn = entity.ExpectedReturn;
            patientArtExtract.LastVisit = entity.LastVisit;
            patientArtExtract.ExitReason = entity.ExitReason;
            patientArtExtract.ExitDate = entity.ExitDate;
            patientArtExtract.Emr = entity.Emr;
            patientArtExtract.Project = entity.Project;
            patientArtExtract.PatientId = entity.PatientId;
            patientArtExtract.Uploaded = UploadStatus;
            patientArtExtract.DateUploaded = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
