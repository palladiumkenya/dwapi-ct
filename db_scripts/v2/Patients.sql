SELECT 
	PatientID, PatientPK, FacilityID, SiteCode, FacilityName, SatelliteName, Gender, DOB, RegistrationDate, RegistrationAtCCC, RegistrationAtPMTCT, RegistrationAtTBClinic, PatientSource, Region, District, Village, 
	ContactRelation, LastVisit, MaritalStatus, EducationLevel, DateConfirmedHIVPositive, PreviousARTExposure, PreviousARTStartDate, StatusAtCCC, StatusAtPMTCT, StatusAtTBClinic, 'IQCare' AS EMR, 
	'Kenya HMIS II' AS Project, CAST(GETDATE() AS DATE) AS DateExtracted,newid() as ID
FROM            
	tmp_PatientMaster AS a
WHERE 
	a.RegistrationAtCCC is not null