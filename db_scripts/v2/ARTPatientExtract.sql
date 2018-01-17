SELECT   
	a.PatientPK, a.PatientID, c.FacilityID, c.SiteCode, a.FacilityName, a.DOB, a.AgeEnrollment, a.AgeARTStart, a.AgeLastVisit, a.RegistrationDate, a.PatientSource, a.Gender, a.StartARTDate, a.PreviousARTStartDate, 
	a.PreviousARTRegimen, a.StartARTAtThisFacility, a.StartRegimen, a.StartRegimenLine, a.LastARTDate, a.LastRegimen, a.LastRegimenLine, a.Duration, a.ExpectedReturn, a.Provider, a.LastVisit, a.ExitReason, 
	a.ExitDate, CAST(GETDATE() AS DATE) AS DateExtracted
FROM            
	tmp_ARTPatients AS a INNER JOIN
	tmp_PatientMaster AS c ON a.PatientPK = c.PatientPK
WHERE 
	c.RegistrationAtCCC is not null