using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public class PatientVisitRepository : IPatientVisitRepository
    {
        public DwhClientEntities Context;
        public const int UploadStatus = 1;

        public PatientVisitRepository(DwhClientEntities context)
        {
            Context = context;
        }

       public IEnumerable<PatientVisitExtract> Get()
       {
           return Context.PatientVisitExtracts.ToList();
       }

       public PatientVisitExtract Get(int id)
       {
           throw new NotImplementedException();
       }

       public void Post(PatientVisitExtract entity)
       {
           throw new NotImplementedException();
       }

       public void Put(int id, PatientVisitExtract entity)
       {
           throw new NotImplementedException();
       }

       public void Delete(int id)
       {
           throw new NotImplementedException();
       }

       public int LoadFromIqTools(string iqToolsDb, string iqToolsServer)
       {
           var query = "INSERT INTO [dwh].[PatientVisitExtract]([PatientId] ,[PatientCccNumber]," +
                       "[SiteCode],[VisitID],[VisitDate],[Service],[VisitType],[WHOStage]," +
                       "[WABStage],[Pregnant],[LMP],[EDD],[Height],[Weight],[BP],[OI],[OIDate]" +
                       ",[SubstitutionFirstlineRegimenDate],[SubstitutionFirstlineRegimenReason],[SubstitutionSecondlineRegimenDate]" +
                       ",[SubstitutionSecondlineRegimenReason],[SecondlineRegimenChangeDate]," +
                       "[SecondlineRegimenChangeReason] ,[Adherence],[AdherenceCategory]," +
                       "[FamilyPlanningMethod],[PwP],[GestationAge],[NextAppointmentDate],[Emr],[Project] ,[Uploaded])" +
                       "SELECT   tmp_ClinicalEncounters.PatientPK, REPLACE(tmp_PatientMaster.PatientID, ' ', '') AS PatientID ," +
                       "tmp_PatientMaster.SiteCode,tmp_ClinicalEncounters.VisitID,tmp_ClinicalEncounters.VisitDate,tmp_ClinicalEncounters.[Service]," +
                       "tmp_ClinicalEncounters.VisitType,tmp_ClinicalEncounters.WHOStage," +
                       "tmp_ClinicalEncounters.WABStage,tmp_ClinicalEncounters.Pregnant, tmp_ClinicalEncounters.LMP,tmp_ClinicalEncounters.EDD," +
                       "tmp_ClinicalEncounters.Height,tmp_ClinicalEncounters.[Weight],tmp_ClinicalEncounters.BP,tmp_ClinicalEncounters.OI," +
                       "tmp_ClinicalEncounters.OIDate,  NULL as SubstitutionFirstlineRegimenDate,NULL as SubstitutionFirstlineRegimenReason," +
                       "NULL as SubstitutionSecondlineRegimenDate,NULL as SubstitutionSecondlineRegimenReason,NULL as SecondlineRegimenChangeDate," +
                       "NULL as SecondlineRegimenChangeReason,tmp_ClinicalEncounters.Adherence," +
                       "tmp_ClinicalEncounters.AdherenceCategory,tmp_ClinicalEncounters.FamilyPlanningMethod," +
                       "tmp_ClinicalEncounters.PwP, tmp_ClinicalEncounters.GestationAge,tmp_ClinicalEncounters.NextAppointmentDate" +
                       ",'IQCare' AS EMR" +
                       ",'HMIS' AS Project ,0 From [" + iqToolsServer + "].[" + iqToolsDb + "].dbo.tmp_ClinicalEncounters  " +
                       "INNER JOIN [" + iqToolsServer + "].[" + iqToolsDb + "].dbo.tmp_PatientMaster " +
                       "On tmp_PatientMaster.PatientPK = tmp_ClinicalEncounters.PatientPK";
           return Context.Database.ExecuteSqlCommand(query);
       }

       public void PutComposite(int id, PatientVisitExtract entity)
       {
           var patientVisitExtract = Context.PatientVisitExtracts.Find(id);
           if (patientVisitExtract == null) return;
           patientVisitExtract.Id = entity.Id;
           patientVisitExtract.PatientId = entity.PatientId;
           patientVisitExtract.PatientCccNumber = entity.PatientCccNumber;
           patientVisitExtract.SiteCode = entity.SiteCode;
           patientVisitExtract.VisitID = entity.VisitID;
           patientVisitExtract.VisitDate = entity.VisitDate;
           patientVisitExtract.Service = entity.Service;
           patientVisitExtract.VisitType = entity.VisitType;
           patientVisitExtract.WHOStage = entity.WHOStage;
           patientVisitExtract.WABStage = entity.WABStage;
           patientVisitExtract.Pregnant = entity.Pregnant;
           patientVisitExtract.LMP = entity.LMP;
           patientVisitExtract.EDD = entity.EDD;
           patientVisitExtract.Height = entity.Height;
           patientVisitExtract.Weight = entity.Weight;
           patientVisitExtract.BP = entity.BP;
           patientVisitExtract.OI = entity.OI;
           patientVisitExtract.OIDate = entity.OIDate;
           patientVisitExtract.SubstitutionFirstlineRegimenDate = entity.SubstitutionFirstlineRegimenDate;
           patientVisitExtract.SubstitutionFirstlineRegimenReason = entity.SubstitutionFirstlineRegimenReason;
           patientVisitExtract.SubstitutionSecondlineRegimenDate = entity.SubstitutionSecondlineRegimenDate;
           patientVisitExtract.SubstitutionSecondlineRegimenReason = entity.SubstitutionSecondlineRegimenReason;
           patientVisitExtract.SecondlineRegimenChangeDate = entity.SecondlineRegimenChangeDate;
           patientVisitExtract.SecondlineRegimenChangeReason = entity.SecondlineRegimenChangeReason;
           patientVisitExtract.Adherence = entity.Adherence;
           patientVisitExtract.AdherenceCategory = entity.AdherenceCategory;
           patientVisitExtract.FamilyPlanningMethod = entity.FamilyPlanningMethod;
           patientVisitExtract.PwP = entity.PwP;
           patientVisitExtract.GestationAge = entity.GestationAge;
           patientVisitExtract.NextAppointmentDate = entity.NextAppointmentDate;
           patientVisitExtract.Emr = entity.Emr;
           patientVisitExtract.Project = entity.Project;
           patientVisitExtract.Uploaded = UploadStatus;
           patientVisitExtract.DateUploaded = DateTime.Now;
           Context.SaveChanges();
       }
    }
}
