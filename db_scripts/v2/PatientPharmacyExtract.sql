SELECT   
	tmp_PatientMaster.PatientID, tmp_PatientMaster.FacilityID, tmp_PatientMaster.SiteCode, tmp_Pharmacy.PatientPK, tmp_Pharmacy.VisitID, tmp_Pharmacy.Drug, tmp_Pharmacy.Provider, 
	tmp_Pharmacy.DispenseDate, tmp_Pharmacy.Duration, tmp_Pharmacy.ExpectedReturn, tmp_Pharmacy.TreatmentType, tmp_Pharmacy.RegimenLine, tmp_Pharmacy.PeriodTaken, 
	tmp_Pharmacy.ProphylaxisType, CAST(GETDATE() AS DATE) AS DateExtracted
FROM            
	tmp_Pharmacy INNER JOIN
    tmp_PatientMaster ON tmp_Pharmacy.PatientPK = tmp_PatientMaster.PatientPK
WHERE 
	tmp_PatientMaster.RegistrationAtCCC is not null