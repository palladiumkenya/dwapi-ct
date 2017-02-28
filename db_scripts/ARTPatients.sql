SET  NOCOUNT ON;
GO 
USE IQTools
GO

SELECT a.[PatientPK]
      ,a.[PatientID]
       ,c.[FacilityID]
      ,c.[SiteCode]
      ,a.[FacilityName]
      ,a.[DOB]
      ,a.[AgeEnrollment]
      ,a.[AgeARTStart]
      ,a.[AgeLastVisit]
      ,a.[RegistrationDate]
      ,a.[PatientSource]
      ,a.[Gender]
      ,[StartARTDate]
      ,a.[PreviousARTStartDate]
      ,[PreviousARTRegimen]
      ,[StartARTAtThisFacility]
      ,[StartRegimen]
      ,[StartRegimenLine]
      ,[LastARTDate]
      ,[LastRegimen]
      ,[LastRegimenLine]
      ,[Duration]
      ,[ExpectedReturn]
      ,[Provider]
      ,a.[LastVisit]
      ,[ExitReason]
      ,[ExitDate]
	  ,CAST(getdate() AS DATE) AS DateExtracted
  FROM dbo.[tmp_ARTPatients] a
  INNER JOIN dbo.[tmp_PatientMaster] c ON a.PatientPK=c.PatientPK
  