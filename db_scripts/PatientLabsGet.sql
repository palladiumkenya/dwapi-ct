SET  NOCOUNT ON;
GO 
USE IQTools
GO

Select tmp_PatientMaster.PatientID,
  tmp_Labs.PatientPK,
	tmp_PatientMaster.[FacilityID],
	tmp_PatientMaster.[SiteCode],
  tmp_PatientMaster.FacilityName,
  tmp_PatientMaster.SatelliteName,
  tmp_Labs.VisitID,
  tmp_Labs.OrderedbyDate,
  tmp_Labs.ReportedbyDate,
  tmp_Labs.TestName,
  tmp_Labs.EnrollmentTest,
  tmp_Labs.TestResult
  ,CAST(getdate() AS DATE) AS DateExtracted
From tmp_Labs
  inner Join tmp_PatientMaster    On tmp_Labs.PatientPK = tmp_PatientMaster.PatientPK
  