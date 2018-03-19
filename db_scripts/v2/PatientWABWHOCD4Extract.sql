SELECT
	tmp_PatientMaster.PatientPK, tmp_PatientMaster.PatientID, tmp_PatientMaster.FacilityID, tmp_PatientMaster.SiteCode, IQC_bCD4.bCD4, IQC_bCD4.bCD4Date, IQC_bWAB.bWAB, IQC_bWAB.bWABDate, 
	IQC_bWHO.bWHO, IQC_bWHO.bWHODate, IQC_eWAB.eWAB, IQC_eWAB.eWABDate, IQC_eCD4.eCD4, IQC_eCD4.eCD4Date, IQC_eWHO.eWHO, IQC_eWHO.eWHODate, IQC_lastWHO.lastWHO, 
	IQC_lastWHO.lastWHODate, IQC_lastWAB.lastWAB, IQC_lastWAB.lastWABDate, IQC_lastCD4.lastCD4, IQC_lastCD4.lastCD4Date, IQC_m12CD4.m12CD4, IQC_m12CD4.m12CD4Date, IQC_m6CD4.m6CD4, 
	IQC_m6CD4.m6CD4Date, CAST(GETDATE() AS DATE) AS DateExtracted
FROM            
	tmp_PatientMaster LEFT OUTER JOIN
    IQC_bCD4 ON tmp_PatientMaster.PatientPK = IQC_bCD4.PatientPK LEFT OUTER JOIN
    IQC_bWAB ON tmp_PatientMaster.PatientPK = IQC_bWAB.PatientPK LEFT OUTER JOIN
    IQC_bWHO ON tmp_PatientMaster.PatientPK = IQC_bWHO.PatientPK LEFT OUTER JOIN
    IQC_lastCD4 ON tmp_PatientMaster.PatientPK = IQC_lastCD4.PatientPK LEFT OUTER JOIN
    IQC_eWAB ON tmp_PatientMaster.PatientPK = IQC_eWAB.PatientPK LEFT OUTER JOIN
    IQC_eWHO ON tmp_PatientMaster.PatientPK = IQC_eWHO.PatientPK LEFT OUTER JOIN
    IQC_lastWAB ON tmp_PatientMaster.PatientPK = IQC_lastWAB.PatientPK LEFT OUTER JOIN
    IQC_eCD4 ON tmp_PatientMaster.PatientPK = IQC_eCD4.PatientPK LEFT OUTER JOIN
    IQC_lastWHO ON tmp_PatientMaster.PatientPK = IQC_lastWHO.PatientPK LEFT OUTER JOIN
    IQC_m12CD4 ON tmp_PatientMaster.PatientPK = IQC_m12CD4.PatientPK LEFT OUTER JOIN
    IQC_m6CD4 ON tmp_PatientMaster.PatientPK = IQC_m6CD4.PatientPK
WHERE 
	tmp_PatientMaster.RegistrationAtCCC is not null