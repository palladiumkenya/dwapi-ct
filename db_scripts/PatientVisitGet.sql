SET  NOCOUNT ON;
GO 
USE IQTools
GO

SELECT 
  REPLACE(tmp_PatientMaster.PatientID, ' ', '') AS PatientID ,
  tmp_PatientMaster.FacilityName,
  tmp_PatientMaster.[SiteCode],
  tmp_ClinicalEncounters.PatientPK,
  tmp_ClinicalEncounters.VisitID,
  tmp_ClinicalEncounters.VisitDate,
  tmp_ClinicalEncounters.Service,
  tmp_ClinicalEncounters.VisitType,
  tmp_ClinicalEncounters.WHOStage,
  tmp_ClinicalEncounters.WABStage,
  tmp_ClinicalEncounters.Pregnant,
  tmp_ClinicalEncounters.LMP,
  tmp_ClinicalEncounters.EDD,
  tmp_ClinicalEncounters.Height,
  tmp_ClinicalEncounters.Weight,
  tmp_ClinicalEncounters.BP,
  tmp_ClinicalEncounters.OI,
  tmp_ClinicalEncounters.OIDate,
  tmp_ClinicalEncounters.Adherence,
  tmp_ClinicalEncounters.AdherenceCategory,
  NULL as SubstitutionFirstlineRegimenDate,
  NULL as SubstitutionFirstlineRegimenReason,
  NULL as SubstitutionSecondlineRegimenDate,
  NULL as SubstitutionSecondlineRegimenReason,
  NULL as SecondlineRegimenChangeDate,
  NULL as SecondlineRegimenChangeReason,
  tmp_ClinicalEncounters.FamilyPlanningMethod,
  tmp_ClinicalEncounters.PwP,
  tmp_ClinicalEncounters.GestationAge,
  tmp_ClinicalEncounters.NextAppointmentDate,
  CAST(getdate() AS DATE) AS DateExtracted
From tmp_ClinicalEncounters
  INNER JOIN tmp_PatientMaster On tmp_PatientMaster.PatientPK = tmp_ClinicalEncounters.PatientPK
   