--		delete FROM [DWAPICentral].[dbo].[Facility]


SELECT * FROM [DWAPICentral].[dbo].[Facility]

SELECT        Facility.Id, Facility.Code, Facility.Name, PatientExtract.Id AS IDD,PatientExtract.PatientPID, PatientExtract.PatientCccNumber, PatientExtract.Gender, PatientPharmacyExtract.DispenseDate, PatientPharmacyExtract.RegimenLine
FROM            Facility INNER JOIN
                         PatientExtract ON Facility.Id = PatientExtract.FacilityId LEFT OUTER JOIN
                         PatientPharmacyExtract ON PatientExtract.Id = PatientPharmacyExtract.PatientId
		
				

