using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public class PatientExtractRepository : IPatientExtractRepository
    {
        public DwhClientEntities Context;
        public const int UploadStatus = 1;
   
        public PatientExtractRepository(DwhClientEntities context)
        {
            this.Context = context;
        
        }

        public IEnumerable<PatientExtract> Get()
        {
            return Context.PatientExtracts.ToList();
        }

        public PatientExtract Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(PatientExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Put(int id, PatientExtract entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int LoadFromIqTools(string iqToolsDb, string iqToolsServer)
        {
            var query =
                "INSERT INTO [dwh].[PatientExtract]([Id],[SiteCode] ,[PatientCccNumber],[FacilityName],[Gender] ," +
                "[DOB],[RegistrationDate],[RegistrationAtCCC],[RegistrationATPMTCT],[RegistrationAtTBClinic]," +
                "[PatientSource],[Region] ,[District],[Village],[ContactRelation],[LastVisit],[MaritalStatus]," +
                "[EducationLevel],[DateConfirmedHIVPositive],[PreviousARTExposure],[PreviousARTStartDate],[Emr] " +
                ",[Project],[Uploaded]) SELECT [PatientPK],[SiteCode],[PatientID],[FacilityName] ,[Gender],[DOB] ," +
                "[RegistrationDate],[RegistrationAtCCC],[RegistrationAtPMTCT],[RegistrationAtTBClinic] ,a.[PatientSource]," +
                "[Region] ,[District],[Village] ,[ContactRelation],[LastVisit],[MaritalStatus],[EducationLevel]," +
                "[DateConfirmedHIVPositive],[PreviousARTExposure],[PreviousARTStartDate],'IQCare' AS EMR,'HMIS' AS Project ,0 " +
                "FROM  [" + iqToolsServer + "].[" + iqToolsDb + "].dbo.tmp_PatientMaster a " +
                "INSERT INTO [dwh].[Facility]([FacilityCode],[FacilityName],[DateLoaded],[UploadStatus],[DateUploaded])" +
                "SELECT Distinct SiteCode,FacilityName,GETDATE(),0,'1900-01-01 00:00:00.000' FROM [dwh].[PatientExtract]" +
                " WHERE SiteCode = (SELECT Max(SiteCode) FROM [dwh].[PatientExtract])"
 
                 ;
           return Context.Database.ExecuteSqlCommand(query);
        }
        public void PutComposite(int patientId, int siteCode, PatientExtract entity)
        {
            var patientExtract = Context.PatientExtracts.Find(patientId,siteCode);
            if (patientExtract != null)
            {
                patientExtract.PatientCccNumber = entity.PatientCccNumber;
                patientExtract.SiteCode = entity.SiteCode;
                patientExtract.FacilityName = entity.FacilityName;
                patientExtract.Gender = entity.Gender;
                patientExtract.DOB = entity.DOB;
                patientExtract.RegistrationDate = entity.RegistrationDate;
                patientExtract.RegistrationAtCCC = entity.RegistrationAtCCC;
                patientExtract.RegistrationATPMTCT = entity.RegistrationATPMTCT;
                patientExtract.RegistrationAtTBClinic = entity.RegistrationAtTBClinic;
                patientExtract.PatientSource = entity.PatientSource;
                patientExtract.Region = entity.Region;
                patientExtract.District = entity.District;
                patientExtract.Village = entity.Village;
                patientExtract.ContactRelation = entity.ContactRelation;
                patientExtract.LastVisit = entity.LastVisit;
                patientExtract.MaritalStatus = entity.MaritalStatus;
                patientExtract.EducationLevel = entity.EducationLevel;
                patientExtract.DateConfirmedHIVPositive = entity.DateConfirmedHIVPositive;
                patientExtract.PreviousARTExposure = entity.PreviousARTExposure;
                patientExtract.PreviousARTStartDate = entity.PreviousARTStartDate;
                patientExtract.Emr = entity.Emr;
                patientExtract.Project = entity.Project;
                patientExtract.Uploaded = UploadStatus;
                patientExtract.DateUploaded = DateTime.Now;
                
                Context.SaveChanges();
            }
        }
    }
}
