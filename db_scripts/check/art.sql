--		delete FROM [DWAPICentral].[dbo].[Facility]


SELECT * FROM [DWAPICentral].[dbo].[Facility]

SELECT        Facility.Id, Facility.Code, Facility.Name, PatientExtract.Id AS IDD,PatientExtract.PatientPID, PatientExtract.PatientCccNumber, PatientExtract.Gender, PatientArtExtract.LastRegimen, PatientArtExtract.LastVisit
FROM            Facility INNER JOIN
                         PatientExtract ON Facility.Id = PatientExtract.FacilityId LEFT OUTER JOIN
                         PatientArtExtract ON PatientExtract.Id = PatientArtExtract.PatientId
						 WHERE
                         PatientId='{56a41758-c4d2-4ff6-a3cf-a865008f3ff5}'
					
					

