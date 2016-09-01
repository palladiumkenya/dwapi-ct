USE [master]
GO
/****** Object:  Database [DwhClient]    Script Date: 8/29/2016 12:38:38 PM ******/
CREATE DATABASE [DwhClient]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DwhClient', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DwhClient.mdf' , SIZE = 9216KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DwhClient_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DwhClient_log.ldf' , SIZE = 69760KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DwhClient] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DwhClient].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DwhClient] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DwhClient] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DwhClient] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DwhClient] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DwhClient] SET ARITHABORT OFF 
GO
ALTER DATABASE [DwhClient] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DwhClient] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DwhClient] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DwhClient] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DwhClient] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DwhClient] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DwhClient] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DwhClient] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DwhClient] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DwhClient] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DwhClient] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DwhClient] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DwhClient] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DwhClient] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DwhClient] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DwhClient] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DwhClient] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DwhClient] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DwhClient] SET RECOVERY FULL 
GO
ALTER DATABASE [DwhClient] SET  MULTI_USER 
GO
ALTER DATABASE [DwhClient] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DwhClient] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DwhClient] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DwhClient] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DwhClient', N'ON'
GO
USE [DwhClient]
GO
/****** Object:  Schema [dwh]    Script Date: 8/29/2016 12:38:38 PM ******/
CREATE SCHEMA [dwh]
GO
/****** Object:  StoredProcedure [dwh].[pr_CheckForPatientsWhoDoNotHaveVisits]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu	
-- Create date: 27/06/2016
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckForPatientsWhoDoNotHaveVisits]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 SELECT *  FROM 
	 [dwh].[PatientExtract]  P 
	 INNER JOIN 
     [dwh].[PatientVisitExtract] PV 
	 ON P.PatientID = PV.PatientID 
	 AND PV.PatientID IS NULL OR PV.PatientID = ' '
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckforPatientsWhoseCccRegistrationDateIsGreaterThanRequired]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckforPatientsWhoseCccRegistrationDateIsGreaterThanRequired]
	-- Add the parameters for the stored procedure here
@SetDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dwh].[PatientExtract] P INNER JOIN
	[dwh].[PatientArtExtract] AP ON P.PatientID = AP.PatientID 
	 WHERE p.RegistrationAtCCC > @SetDate
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhereDOBIsNull]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhereDOBIsNull]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  * FROM [dwh].[PatientExtract] WHERE DOB IS NULL OR DOB = ' '   
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhereGenderIsNull]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2017
-- Description:	Checks the Patients in the Extract whose gender is Null
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhereGenderIsNull]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dwh].[PatientExtract] WHERE Gender IS NULL OR GENDER = ' '  
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoAreInArtButDoNotHaveARTStartDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoAreInArtButDoNotHaveARTStartDate]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 SELECT * FROM [dwh].[PatientExtract] P 
INNER JOIN [dwh].[PatientArtExtract] PA
ON p.PatientID = PA.PatientID WHERE PA.StartARTDate IS NULL   
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoARTStartDateIsGreatorThanLastVisitDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoARTStartDateIsGreatorThanLastVisitDate]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 SELECT * FROM [dwh].[PatientExtract] P  INNER JOIN 
[dwh].[PatientArtExtract] PA ON p.PatientID = PA.PatientID
 WHERE PA.StartARTDate > PA.LastVisit                                                                                                        
 
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoseAgeArtStartIsLessThanSetDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2017
-- Description:	
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoseAgeArtStartIsLessThanSetDate]
	-- Add the parameters for the stored procedure here
	 @SetDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
	SELECT * FROM  [dwh].[PatientExtract] P 
INNER JOIN 
 [dwh].[PatientArtExtract] AP ON P.PatientID = AP.PatientID WHERE 
 AP.AgeARTStart < @SetDate 
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoseDateOfBirthIsGreaterThanRegistrationDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2017
-- Description:	Check for patients whose date of birth is greater than the registration date
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoseDateOfBirthIsGreaterThanRegistrationDate]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT P.PatientID,P.DOB ,PA.RegistrationDate FROM
	 [dwh].[PatientExtract] P 
    INNER JOIN [dwh].[PatientArtExtract] PA 
     ON P.PatientID = PA.PatientID
     WHERE P.DOB > PA.RegistrationDate
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoseDateOfBirthIsGreatorThanRegistrationDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoseDateOfBirthIsGreatorThanRegistrationDate]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT P.PatientID,P.DOB ,PA.RegistrationDate FROM [dwh].[PatientExtract] P 
INNER JOIN [dwh].[PatientArtExtract]  PA 
ON P.PatientID = PA.PatientID
WHERE P.DOB > PA.RegistrationDate
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoseDateOfBirthIsLessThanSetDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	Eliminate patients based on given Date
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoseDateOfBirthIsLessThanSetDate]
	-- Add the parameters for the stored procedure here
    @SetDate DATETIME 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT * FROM [dwh].[PatientExtract] P INNER JOIN
		[dwh].[PatientArtExtract] AP ON P.PatientID = AP.PatientID 
		WHERE p.RegistrationAtCCC < @SetDate
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoseExitReasonIsDeathAndExitDateIsLessThanStartArtDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoseExitReasonIsDeathAndExitDateIsLessThanStartArtDate]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 SELECT Pv.PatientID, 
	        PV.ExitDate, 
			PA.StartARTDate
 FROM [dwh].[PatientArtExtract] PA INNER JOIN 
      [dwh].[PatientStatusExtract]  PV
ON PA.PatientID = PV.PatientID 
WHERE PV.ExitReason = 'Death' 
AND PV.ExitDate < PA.StartARTDate 
                       
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoseExitReasonIsDeathandExitDateIsSetDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoseExitReasonIsDeathandExitDateIsSetDate]
	-- Add the parameters for the stored procedure here
	@SetDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dwh].[PatientStatusExtract] WHERE ExitReason = 'Death' and ExitDate < @SetDate
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoseLastVisitIsNull]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoseLastVisitIsNull]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 SELECT * FROM [dwh].[PatientExtract] P
INNER JOIN   PA ON P.PatientID = PA.PatientID 
WHERE p.LastVisit IS NULL 
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoseLastVistIsLessThanRegistrationDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoseLastVistIsLessThanRegistrationDate]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dwh].[PatientExtract] WHERE  LastVisit< RegistrationAtCCC 
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWhoseRegistrationDateatCCCIsLessThanSetDate]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWhoseRegistrationDateatCCCIsLessThanSetDate]
	-- Add the parameters for the stored procedure here
	@SetDate DATETIME
AS
 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dwh].[PatientExtract] P 
LEFT JOIN  [dwh].[PatientArtExtract] PA ON P.PatientID = PA.PatientID
WHERE P.RegistrationAtCCC IS NOT NULL AND PA.PatientID IS NOT NULL AND P.RegistrationAtCCC < @SetDate
END


GO
/****** Object:  StoredProcedure [dwh].[pr_CheckPatientsWithNoPatientId]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_CheckPatientsWithNoPatientId]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dwh].PatientExtract WHERE PatientID IS NULL AND RegistrationAtCCC IS NOT NULL OR PatientID = ' '
	
END


GO
/****** Object:  StoredProcedure [dwh].[pr_GetArtPatients]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	Get ART Patients From IQtools database based on the Datawarehouse Dictionary
-- =============================================
CREATE PROCEDURE [dwh].[pr_GetArtPatients]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;



    -- Insert statements for procedure here
TRUNCATE TABLE [dwh].[PatientArtExtract]

INSERT INTO [dwh].[PatientArtExtract]
           (
           [PatientCccNumber]
           ,[AgeEnrollment]
           ,[AgeARTStart]
           ,[AgeLastVisit]
           ,[SiteCode]
           ,[FacilityName]
           ,[RegistrationDate]
           ,[PatientSource]
           ,[Gender]
           ,[StartARTDate]
           ,[PreviousARTStartDate]
           ,[PreviousARTRegimen]
           ,[StartARTAtThisFacility]
           ,[StartRegimen]
           ,[StartRegimenLine]
           ,[LastARTDate]
           ,[LastRegimen]
           ,[LastRegimenLine]
           ,[Duration]
           ,[ExpectedReturn]
           ,[LastVisit]
           ,[ExitReason]
           ,[ExitDate]
           ,[Emr]
           ,[Project]
		   ,[PatientId]
		   ,[Uploaded]
		   )	
SELECT 
       pa.[PatientID]
	  ,pa.[AgeEnrollment]
	   ,pa.[AgeARTStart]
	   ,pa.[AgeLastVisit]
      ,pa.[SiteCode]
      ,pa.[FacilityName]
      ,pa.[RegistrationDate]
      ,pa.[PatientSource]
      ,pa.[Gender]
      ,[StartARTDate]
      ,pa.[PreviousARTStartDate]
      ,[PreviousARTRegimen]
      ,[StartARTAtThisFacility]
      ,[StartRegimen]
      ,[StartRegimenLine]
      ,[LastARTDate]
      ,[LastRegimen]
      ,[LastRegimenLine]
      ,[Duration]
      ,[ExpectedReturn]
      ,pa.[LastVisit]
      ,[ExitReason]
      ,[ExitDate]
	   ,'IQCare' AS EMR
	  ,'HMIS' AS Project
	  ,pa.[PatientPK]
	  ,0
  FROM IQTools_IQCareKE_HMIS_NgaoDistrictHospital.[dbo].[tmp_ARTPatients] pa INNER JOIN 
  IQTools_IQCareKE_HMIS_NgaoDistrictHospital.[dbo].[tmp_PatientMaster] p ON pa.PatientPK =p.PatientPK  
END


GO
/****** Object:  StoredProcedure [dwh].[pr_GetPatient]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	Query to Retrive Patient Data from IQtools Database based on the Data warehouse
-- Data Dictionary
-- =============================================
CREATE PROCEDURE  [dwh].[pr_GetPatient]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--DELETE  FROM [dwh].[PatientExtract]
	
	INSERT INTO [dwh].[PatientExtract]
           ([Id]
		   ,[SiteCode]
		   ,[PatientCccNumber]
           ,[FacilityName]
           ,[Gender]
           ,[DOB]
           ,[RegistrationDate]
           ,[RegistrationAtCCC]
           ,[RegistrationATPMTCT]
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
           ,[Emr]
           ,[Project]
		   ,[Uploaded])
	SELECT
	 [PatientPK]
   , [SiteCode]
	,[PatientID]
    ,[FacilityName]
    ,[Gender]
    ,[DOB]
      ,[RegistrationDate]
      ,[RegistrationAtCCC]
      ,[RegistrationAtPMTCT]
      ,[RegistrationAtTBClinic]
      ,a.[PatientSource]
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
	  ,'IQCare' AS EMR
	  ,'HMIS' AS Project
	  ,0
  FROM  IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster a
INSERT INTO [dwh].[Facility]
           ([FacilityCode]
           ,[FacilityName]
           ,[DateLoaded]
           ,[UploadStatus]
           ,[DateUploaded])
SELECT Distinct SiteCode,FacilityName,GETDATE(),0,'1900-01-01 00:00:00.000' FROM [dwh].[PatientExtract] WHere SiteCode = (SELECT Max(SiteCode) From [dwh].[PatientExtract])
 
END


GO
/****** Object:  StoredProcedure [dwh].[pr_GetPatientBaselines]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	Get patient baseline data from IQTools based on the Datawarehouse data dictionary
-- =============================================
CREATE PROCEDURE [dwh].[pr_GetPatientBaselines]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	TRUNCATE TABLE [dwh].[PatientBaselinesExtract]

	INSERT INTO [dwh].[PatientBaselinesExtract]
           ([PatientCccNumber]
           ,[SiteCode]
           ,[eCD4]
           ,[eCD4Date]
           ,[eWHO]
           ,[eWHODate]
           ,[bCD4]
           ,[bCD4Date]
           ,[bWHO]
           ,[bWHODate]
           ,[lastWHO]
           ,[lastWHODate]
           ,[lastCD4]
           ,[lastCD4Date]
           ,[m12CD4]
           ,[m12CD4Date]
           ,[m6CD4]
           ,[m6CD4Date]
           ,[Emr]
           ,[Project]
		   ,[PatientId]
		   ,[Uploaded])
    -- Insert statements for procedure here
	Select 
		tmp_PatientMaster.PatientID,
	
		tmp_PatientMaster.SiteCode,
		IQC_eCD4.eCD4,
		IQC_eCD4.eCD4Date,
		IQC_eWHO.eWHO,
		IQC_eWHO.eWHODate,
		IQC_bCD4.bCD4,
		IQC_bCD4.bCD4Date,
		IQC_bWHO.bWHO,
		IQC_bWHO.bWHODate,
		IQC_lastWHO.lastWHO,
		IQC_lastWHO.lastWHODate,
        IQC_lastCD4.lastCD4,
		IQC_lastCD4.lastCD4Date,
		IQC_m12CD4.m12CD4,
		IQC_m12CD4.m12CD4Date,
	    IQC_m6CD4.m6CD4,
		IQC_m6CD4.m6CD4Date
		,'IQCare' AS EMR
		,'HMIS' AS Project
	     ,tmp_PatientMaster.PatientPK
		 ,0
		From IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster 

  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_bCD4 On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_bCD4.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_bWAB On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_bWAB.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_bWHO On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_bWHO.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_lastCD4 On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_lastCD4.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_eWAB On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_eWAB.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_eWHO On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_eWHO.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_lastWAB On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_lastWAB.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_eCD4 On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_eCD4.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_lastWHO On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_lastWHO.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_m12CD4 On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_m12CD4.PatientPK
  Left Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_m6CD4 On IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster.PatientPK = IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.IQC_m6CD4.PatientPK
END


GO
/****** Object:  StoredProcedure [dwh].[pr_GetPatientLabs]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dwh].[pr_GetPatientLabs]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

TRUNCATE TABLE [dwh].[PatientLaboratoryExtract]

INSERT INTO [dwh].[PatientLaboratoryExtract]
           ([PatientId]
           ,[PatientCccNumber]
           ,[SiteCode] 
           ,[VisitId]
           ,[OrderedByDate]
           ,[ReportedByDate]
           ,[TestName]
           ,[TestResult]
           ,[Emr]
           ,[Project]
		   ,[Uploaded])	
Select 
  tmp_Labs.PatientPK,
  tmp_PatientMaster.PatientID,
  tmp_PatientMaster.SiteCode,
  tmp_Labs.VisitID,
  tmp_Labs.OrderedbyDate,
  tmp_Labs.ReportedbyDate,
  tmp_Labs.TestName,
  tmp_Labs.TestResult
    ,'IQCare' AS EMR
	  ,'HMIS' AS Project
	  ,0
From IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_Labs
  inner Join IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster 
  On tmp_Labs.PatientPK = tmp_PatientMaster.PatientPK
END


GO
/****** Object:  StoredProcedure [dwh].[pr_GetPatientPharmacy]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description: Get Patient Pharmacy Information from IQTools Database based on the 
-- Datawarehouse 
-- =============================================
CREATE PROCEDURE [dwh].[pr_GetPatientPharmacy]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
TRUNCATE TABLE [dwh].[PatientPharmacyExtract]

INSERT INTO [dwh].[PatientPharmacyExtract]
           ([PatientID]
           ,[PatientCccNumber]
           ,[SiteCode]
           ,[VisitID]
           ,[Drug]
           ,[DispenseDate]
           ,[Duration]
           ,[ExpectedReturn]
           ,[TreatmentType]
           ,[PeriodTaken]
           ,[ProphylaxisType]
           ,[Emr]
           ,[Project]
		   ,[Uploaded])
Select 
  tmp_Pharmacy.PatientPK,
  tmp_PatientMaster.PatientID,
  tmp_PatientMaster.SiteCode,
  tmp_Pharmacy.VisitID,
  tmp_Pharmacy.Drug,

  tmp_Pharmacy.DispenseDate,
  tmp_Pharmacy.Duration,
  tmp_Pharmacy.ExpectedReturn,
  tmp_Pharmacy.TreatmentType,

  tmp_Pharmacy.PeriodTaken,
  tmp_Pharmacy.ProphylaxisType
    ,'IQCare' AS EMR
	  ,'HMIS' AS Project
	  ,0
From IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_Pharmacy
 INNER JOIN  IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster On tmp_Pharmacy.PatientPK =    tmp_PatientMaster.PatientPK
  
END


GO
/****** Object:  StoredProcedure [dwh].[pr_GetPatientStatus]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	Get Patient Status from IQtools, based on the Datawarehouse data dictionary

-- =============================================
CREATE PROCEDURE  [dwh].[pr_GetPatientStatus]
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	TRUNCATE TABLE [dwh].[PatientStatusExtract]

	INSERT INTO [dwh].[PatientStatusExtract]
           ([PatientId]
           ,[PatientCccNumber]
           ,[SiteCode]
           ,[FacilityName]
           ,[ExitDescription]
           ,[ExitDate]
           ,[ExitReason]
           ,[Emr]
           ,[Project]
		   ,[Uploaded])
	Select 
  tmp_LastStatus.PatientPK,
  tmp_PatientMaster.PatientID,
  tmp_PatientMaster.SiteCode,
  tmp_PatientMaster.FacilityName,
  tmp_LastStatus.ExitDescription,
  tmp_LastStatus.ExitDate,
  tmp_LastStatus.ExitReason
    ,'IQCare' AS EMR
	  ,'HMIS' AS Project
	  ,0
From IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_LastStatus
 INNER JOIN  IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster On 
 tmp_LastStatus.PatientPK =  tmp_PatientMaster.PatientPK
END


GO
/****** Object:  StoredProcedure [dwh].[pr_GetPatientVisits]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brian Wakhutu
-- Create date: 27/06/2016
-- Description:	Get patient visit information from the IQtools database based on the datawarehouse extract
-- Dictionary
-- =============================================
CREATE PROCEDURE [dwh].[pr_GetPatientVisits]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	TRUNCATE TABLE [dwh].[PatientVisitExtract]

	INSERT INTO [dwh].[PatientVisitExtract]
           (
             [PatientId]
            ,[PatientCccNumber]
           ,[SiteCode]
           ,[VisitID]
           ,[VisitDate]
           ,[Service]
           ,[VisitType]
           ,[WHOStage]
           ,[WABStage]
           ,[Pregnant]
           ,[LMP]
           ,[EDD]
           ,[Height]
           ,[Weight]
           ,[BP]
           ,[OI]
           ,[OIDate]
           ,[SubstitutionFirstlineRegimenDate]
           ,[SubstitutionFirstlineRegimenReason]
           ,[SubstitutionSecondlineRegimenDate]
           ,[SubstitutionSecondlineRegimenReason]
           ,[SecondlineRegimenChangeDate]
           ,[SecondlineRegimenChangeReason]
           ,[Adherence]
           ,[AdherenceCategory]
           ,[FamilyPlanningMethod]
           ,[PwP]
           ,[GestationAge]
           ,[NextAppointmentDate]
           ,[Emr]
           ,[Project]
		   ,[Uploaded])

	SELECT   tmp_ClinicalEncounters.PatientPK, 
	         REPLACE(tmp_PatientMaster.PatientID, ' ', '') AS PatientID ,
  tmp_PatientMaster.SiteCode,
  tmp_ClinicalEncounters.VisitID,
  tmp_ClinicalEncounters.VisitDate,
  tmp_ClinicalEncounters.[Service],
  tmp_ClinicalEncounters.VisitType,
  tmp_ClinicalEncounters.WHOStage,
  tmp_ClinicalEncounters.WABStage,
  tmp_ClinicalEncounters.Pregnant,
  tmp_ClinicalEncounters.LMP,
  tmp_ClinicalEncounters.EDD,
  tmp_ClinicalEncounters.Height,
  tmp_ClinicalEncounters.[Weight],
  tmp_ClinicalEncounters.BP,
  tmp_ClinicalEncounters.OI,
  tmp_ClinicalEncounters.OIDate,

  NULL as SubstitutionFirstlineRegimenDate,
  NULL as SubstitutionFirstlineRegimenReason,
  NULL as SubstitutionSecondlineRegimenDate,
  NULL as SubstitutionSecondlineRegimenReason,
  NULL as SecondlineRegimenChangeDate,
  NULL as SecondlineRegimenChangeReason,
    tmp_ClinicalEncounters.Adherence,
  tmp_ClinicalEncounters.AdherenceCategory,
  tmp_ClinicalEncounters.FamilyPlanningMethod,
  tmp_ClinicalEncounters.PwP,
  tmp_ClinicalEncounters.GestationAge,
  tmp_ClinicalEncounters.NextAppointmentDate
    ,'IQCare' AS EMR
	  ,'HMIS' AS Project
	  ,0
From IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_ClinicalEncounters
  INNER JOIN IQTools_IQCareKE_HMIS_NgaoDistrictHospital.dbo.tmp_PatientMaster On tmp_PatientMaster.PatientPK = tmp_ClinicalEncounters.PatientPK
  
END


GO
/****** Object:  Table [dwh].[DataSet]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[DataSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataSetName] [varchar](50) NULL,
 CONSTRAINT [PK_DataSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dwh].[Facility]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[Facility](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FacilityCode] [int] NULL,
	[FacilityName] [varchar](100) NULL,
	[DateLoaded] [datetime] NULL,
	[UploadStatus] [int] NULL,
	[DateUploaded] [datetime] NULL,
 CONSTRAINT [PK_FacilityLoad] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dwh].[PatientArtExtract]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[PatientArtExtract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteCode] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientCccNumber] [varchar](100) NULL,
	[AgeEnrollment] [decimal](5, 1) NULL,
	[AgeARTStart] [decimal](5, 1) NULL,
	[AgeLastVisit] [decimal](5, 1) NULL,
	[FacilityName] [varchar](50) NULL,
	[RegistrationDate] [datetime] NULL,
	[PatientSource] [varchar](100) NULL,
	[Gender] [varchar](100) NULL,
	[StartARTDate] [datetime] NULL,
	[PreviousARTStartDate] [datetime] NULL,
	[PreviousARTRegimen] [varchar](50) NULL,
	[StartARTAtThisFacility] [datetime] NULL,
	[StartRegimen] [varchar](100) NULL,
	[StartRegimenLine] [varchar](200) NULL,
	[LastARTDate] [datetime] NULL,
	[LastRegimen] [varchar](200) NULL,
	[LastRegimenLine] [varchar](200) NULL,
	[Duration] [numeric](24, 0) NULL,
	[ExpectedReturn] [datetime] NULL,
	[LastVisit] [datetime] NULL,
	[ExitReason] [varchar](100) NULL,
	[ExitDate] [datetime] NULL,
	[Emr] [varchar](50) NULL,
	[Project] [varchar](50) NULL,
	[Uploaded] [int] NULL,
 CONSTRAINT [PK_PatientArtExtract_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dwh].[PatientBaselinesExtract]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[PatientBaselinesExtract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[SiteCode] [int] NOT NULL,
	[PatientCccNumber] [varchar](100) NULL,
	[eCD4] [int] NULL,
	[eCD4Date] [datetime] NULL,
	[eWHO] [int] NULL,
	[eWHODate] [datetime] NULL,
	[bCD4] [int] NULL,
	[bCD4Date] [datetime] NULL,
	[bWHO] [int] NULL,
	[bWHODate] [datetime] NULL,
	[lastWHO] [int] NULL,
	[lastWHODate] [datetime] NULL,
	[lastCD4] [int] NULL,
	[lastCD4Date] [datetime] NULL,
	[m12CD4] [int] NULL,
	[m12CD4Date] [datetime] NULL,
	[m6CD4] [int] NULL,
	[m6CD4Date] [datetime] NULL,
	[Emr] [varchar](50) NULL,
	[Project] [varchar](50) NULL,
	[Uploaded] [int] NULL,
 CONSTRAINT [PK_PatientBaselinesExtract_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_PatientBaselinesExtract] UNIQUE NONCLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dwh].[PatientExtract]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[PatientExtract](
	[Id] [int] NOT NULL,
	[SiteCode] [int] NOT NULL,
	[PatientCccNumber] [varchar](100) NULL,
	[FacilityName] [varchar](50) NULL,
	[Gender] [varchar](10) NULL,
	[DOB] [datetime] NULL,
	[RegistrationDate] [datetime] NULL,
	[RegistrationAtCCC] [datetime] NULL,
	[RegistrationATPMTCT] [datetime] NULL,
	[RegistrationAtTBClinic] [datetime] NULL,
	[PatientSource] [varchar](100) NULL,
	[Region] [varchar](50) NULL,
	[District] [varchar](50) NULL,
	[Village] [varchar](50) NULL,
	[ContactRelation] [varchar](250) NULL,
	[LastVisit] [datetime] NULL,
	[MaritalStatus] [varchar](100) NULL,
	[EducationLevel] [varchar](50) NULL,
	[DateConfirmedHIVPositive] [datetime] NULL,
	[PreviousARTExposure] [varchar](50) NULL,
	[PreviousARTStartDate] [datetime] NULL,
	[Emr] [varchar](50) NULL,
	[Project] [varchar](50) NULL,
	[Uploaded] [int] NULL,
 CONSTRAINT [PK_PatientExtract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[SiteCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dwh].[PatientLaboratoryExtract]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[PatientLaboratoryExtract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[SiteCode] [int] NOT NULL,
	[PatientCccNumber] [varchar](100) NULL,
	[VisitId] [int] NULL,
	[OrderedByDate] [datetime] NULL,
	[ReportedByDate] [datetime] NULL,
	[TestName] [varchar](200) NULL,
	[TestResult] [varchar](200) NULL,
	[Emr] [varchar](50) NULL,
	[Project] [varchar](50) NULL,
	[Uploaded] [int] NULL,
 CONSTRAINT [PK_PatientLaboratoryExtract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dwh].[PatientPharmacyExtract]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[PatientPharmacyExtract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[SiteCode] [int] NULL,
	[PatientCccNumber] [varchar](100) NULL,
	[VisitID] [int] NULL,
	[Drug] [varchar](100) NULL,
	[DispenseDate] [datetime] NULL,
	[Duration] [numeric](24, 2) NULL,
	[ExpectedReturn] [nchar](10) NULL,
	[TreatmentType] [varchar](100) NULL,
	[PeriodTaken] [varchar](100) NULL,
	[ProphylaxisType] [varchar](100) NULL,
	[Emr] [varchar](50) NULL,
	[Project] [varchar](50) NULL,
	[Uploaded] [int] NULL,
 CONSTRAINT [PK_PatientPharmacyExtract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dwh].[PatientStatusExtract]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[PatientStatusExtract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[SiteCode] [int] NOT NULL,
	[PatientCccNumber] [varchar](100) NULL,
	[FacilityName] [varchar](50) NULL,
	[ExitDescription] [varchar](100) NULL,
	[ExitDate] [datetime] NULL,
	[ExitReason] [varchar](100) NULL,
	[Emr] [varchar](50) NULL,
	[Project] [varchar](50) NULL,
	[Uploaded] [int] NULL,
 CONSTRAINT [PK_PatientStatusExtract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dwh].[PatientVisitExtract]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[PatientVisitExtract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[SiteCode] [int] NOT NULL,
	[PatientCccNumber] [varchar](100) NULL,
	[VisitID] [int] NULL,
	[VisitDate] [datetime] NULL,
	[Service] [varchar](50) NULL,
	[VisitType] [varchar](50) NULL,
	[WHOStage] [int] NULL,
	[WABStage] [varchar](100) NULL,
	[Pregnant] [varchar](20) NULL,
	[LMP] [datetime] NULL,
	[EDD] [datetime] NULL,
	[Height] [numeric](18, 1) NULL,
	[Weight] [numeric](18, 1) NULL,
	[BP] [varchar](10) NULL,
	[OI] [varchar](200) NULL,
	[OIDate] [datetime] NULL,
	[SubstitutionFirstlineRegimenDate] [datetime] NULL,
	[SubstitutionFirstlineRegimenReason] [varchar](max) NULL,
	[SubstitutionSecondlineRegimenDate] [datetime] NULL,
	[SubstitutionSecondlineRegimenReason] [varchar](max) NULL,
	[SecondlineRegimenChangeDate] [datetime] NULL,
	[SecondlineRegimenChangeReason] [varchar](max) NULL,
	[Adherence] [varchar](200) NULL,
	[AdherenceCategory] [varchar](200) NULL,
	[FamilyPlanningMethod] [varchar](200) NULL,
	[PwP] [varchar](200) NULL,
	[GestationAge] [numeric](18, 1) NULL,
	[NextAppointmentDate] [datetime] NULL,
	[Emr] [varchar](50) NULL,
	[Project] [varchar](50) NULL,
	[Uploaded] [int] NULL,
 CONSTRAINT [PK_PatientVisitExtract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dwh].[UploadErrorLog]    Script Date: 8/29/2016 12:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dwh].[UploadErrorLog](
	[Id] [int] NOT NULL,
	[ErrorDescription] [varchar](max) NULL,
	[FacilityId] [int] NULL,
 CONSTRAINT [PK_UploadLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dwh].[PatientArtExtract]  WITH CHECK ADD  CONSTRAINT [FK_PatientArtExtract_PatientExtract] FOREIGN KEY([PatientId], [SiteCode])
REFERENCES [dwh].[PatientExtract] ([Id], [SiteCode])
GO
ALTER TABLE [dwh].[PatientArtExtract] CHECK CONSTRAINT [FK_PatientArtExtract_PatientExtract]
GO
ALTER TABLE [dwh].[PatientBaselinesExtract]  WITH CHECK ADD  CONSTRAINT [FK_PatientBaselinesExtract_PatientExtract] FOREIGN KEY([PatientId], [SiteCode])
REFERENCES [dwh].[PatientExtract] ([Id], [SiteCode])
GO
ALTER TABLE [dwh].[PatientBaselinesExtract] CHECK CONSTRAINT [FK_PatientBaselinesExtract_PatientExtract]
GO
ALTER TABLE [dwh].[PatientLaboratoryExtract]  WITH CHECK ADD  CONSTRAINT [FK_PatientLaboratoryExtract_PatientExtract] FOREIGN KEY([PatientId], [SiteCode])
REFERENCES [dwh].[PatientExtract] ([Id], [SiteCode])
GO
ALTER TABLE [dwh].[PatientLaboratoryExtract] CHECK CONSTRAINT [FK_PatientLaboratoryExtract_PatientExtract]
GO
ALTER TABLE [dwh].[PatientPharmacyExtract]  WITH CHECK ADD  CONSTRAINT [FK_PatientPharmacyExtract_PatientExtract] FOREIGN KEY([PatientId], [SiteCode])
REFERENCES [dwh].[PatientExtract] ([Id], [SiteCode])
GO
ALTER TABLE [dwh].[PatientPharmacyExtract] CHECK CONSTRAINT [FK_PatientPharmacyExtract_PatientExtract]
GO
ALTER TABLE [dwh].[PatientStatusExtract]  WITH CHECK ADD  CONSTRAINT [FK_PatientStatusExtract_PatientExtract] FOREIGN KEY([PatientId], [SiteCode])
REFERENCES [dwh].[PatientExtract] ([Id], [SiteCode])
GO
ALTER TABLE [dwh].[PatientStatusExtract] CHECK CONSTRAINT [FK_PatientStatusExtract_PatientExtract]
GO
ALTER TABLE [dwh].[PatientVisitExtract]  WITH CHECK ADD  CONSTRAINT [FK_PatientVisitExtract_PatientExtract] FOREIGN KEY([PatientId], [SiteCode])
REFERENCES [dwh].[PatientExtract] ([Id], [SiteCode])
GO
ALTER TABLE [dwh].[PatientVisitExtract] CHECK CONSTRAINT [FK_PatientVisitExtract_PatientExtract]
GO
ALTER TABLE [dwh].[UploadErrorLog]  WITH CHECK ADD  CONSTRAINT [FK_UploadErrorLog_Facility] FOREIGN KEY([FacilityId])
REFERENCES [dwh].[Facility] ([Id])
GO
ALTER TABLE [dwh].[UploadErrorLog] CHECK CONSTRAINT [FK_UploadErrorLog_Facility]
GO
USE [master]
GO
ALTER DATABASE [DwhClient] SET  READ_WRITE 
GO
