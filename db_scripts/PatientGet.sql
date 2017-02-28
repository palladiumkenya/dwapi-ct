SET  NOCOUNT ON;
GO 
USE IQTools
GO
SELECT
	 [PatientID]
      ,[PatientPK]
      ,a.[FacilityID]
      ,a.[SiteCode]
      ,a.[FacilityName]
      ,[SatelliteName]
      ,[Gender]
      ,[DOB]
      ,[RegistrationDate]
      ,[RegistrationAtCCC]
      ,[RegistrationAtPMTCT]
      ,[RegistrationAtTBClinic]
      ,[PatientSource]
      ,[Region]
      ,[District]
      ,[Village]
      ,[ContactRelation]
      ,[LastVisit]
      ,[MaritalStatus]
      ,[EducationLevel]
      ,[DateConfirmedHIVPositive]
      ,[PreviousARTExposure]
      ,[PreviousARTStartDate]
      ,[StatusAtCCC]
      ,[StatusAtPMTCT]
      ,[StatusAtTBClinic]
	  ,'IQCare' AS EMR
	  ,'Kenya HMIS II' AS Project
	  ,CAST(getdate() AS DATE) AS DateExtracted
  FROM  
	dbo.tmp_PatientMaster a
  