
SET  NOCOUNT ON;
GO 
USE IQTools
GO
Select tmp_PatientMaster.PatientID,
tmp_LastStatus.PatientPK,
 tmp_PatientMaster.[SiteCode],
  tmp_PatientMaster.FacilityName,
  tmp_LastStatus.ExitDescription,
  tmp_LastStatus.ExitDate,
  tmp_LastStatus.ExitReason
 ,CAST(getdate() AS DATE) AS DateExtracted
  
From tmp_LastStatus
 INNER JOIN  tmp_PatientMaster On tmp_LastStatus.PatientPK =  tmp_PatientMaster.PatientPK
   