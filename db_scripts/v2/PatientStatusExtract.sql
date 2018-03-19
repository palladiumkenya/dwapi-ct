SELECT   
	tmp_PatientMaster.PatientID, tmp_LastStatus.PatientPK, tmp_PatientMaster.SiteCode, tmp_PatientMaster.FacilityName, tmp_LastStatus.ExitDescription, tmp_LastStatus.ExitDate, tmp_LastStatus.ExitReason, 
	CAST(GETDATE() AS DATE) AS DateExtracted
FROM            
	tmp_LastStatus INNER JOIN
	tmp_PatientMaster ON tmp_LastStatus.PatientPK = tmp_PatientMaster.PatientPK
WHERE 
	tmp_PatientMaster.RegistrationAtCCC is not null