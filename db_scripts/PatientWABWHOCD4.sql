SET  NOCOUNT ON;
GO 
USE IQTools
GO
Select 
  tmp_PatientMaster.PatientPK,
  tmp_PatientMaster.PatientID,
  tmp_PatientMaster.[FacilityID],
  tmp_PatientMaster.[SiteCode], 
  IQC_bCD4.bCD4,
  IQC_bCD4.bCD4Date,
  IQC_bWAB.bWAB,
  IQC_bWAB.bWABDate,
  IQC_bWHO.bWHO,
  IQC_bWHO.bWHODate,
  IQC_eWAB.eWAB,
  IQC_eWAB.eWABDate,
  IQC_eCD4.eCD4,
  IQC_eCD4.eCD4Date,
  IQC_eWHO.eWHO,
  IQC_eWHO.eWHODate,
  IQC_lastWHO.lastWHO,
  IQC_lastWHO.lastWHODate,
  IQC_lastWAB.lastWAB,
  IQC_lastWAB.lastWABDate,
  IQC_lastCD4.lastCD4,
  IQC_lastCD4.lastCD4Date,
  IQC_m12CD4.m12CD4,
  IQC_m12CD4.m12CD4Date,
  IQC_m6CD4.m6CD4,
  IQC_m6CD4.m6CD4Date,
  CAST(getdate() AS DATE) AS DateExtracted
From tmp_PatientMaster 
  Left Join IQC_bCD4 On tmp_PatientMaster.PatientPK = IQC_bCD4.PatientPK
  Left Join IQC_bWAB On tmp_PatientMaster.PatientPK = IQC_bWAB.PatientPK
  Left Join IQC_bWHO On tmp_PatientMaster.PatientPK = IQC_bWHO.PatientPK
  Left Join IQC_lastCD4 On tmp_PatientMaster.PatientPK = IQC_lastCD4.PatientPK
  Left Join IQC_eWAB On tmp_PatientMaster.PatientPK = IQC_eWAB.PatientPK
  Left Join IQC_eWHO On tmp_PatientMaster.PatientPK = IQC_eWHO.PatientPK
  Left Join IQC_lastWAB On tmp_PatientMaster.PatientPK = IQC_lastWAB.PatientPK
  Left Join IQC_eCD4 On tmp_PatientMaster.PatientPK = IQC_eCD4.PatientPK
  Left Join IQC_lastWHO On tmp_PatientMaster.PatientPK = IQC_lastWHO.PatientPK
  Left Join IQC_m12CD4 On tmp_PatientMaster.PatientPK = IQC_m12CD4.PatientPK
  Left Join IQC_m6CD4 On tmp_PatientMaster.PatientPK = IQC_m6CD4.PatientPK