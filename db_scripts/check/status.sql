--		delete FROM [DWAPICentral].[dbo].[Facility]


SELECT * FROM [DWAPICentral].[dbo].[Facility]

SELECT        Facility.Id, Facility.Code, Facility.Name, PatientExtract.Id AS IDD,PatientExtract.PatientPID, PatientExtract.PatientCccNumber, PatientExtract.Gender, PatientStatusExtract.ExitDate, PatientStatusExtract.ExitReason
FROM            Facility INNER JOIN
                         PatientExtract ON Facility.Id = PatientExtract.FacilityId LEFT OUTER JOIN
                         PatientStatusExtract ON PatientExtract.Id = PatientStatusExtract.PatientId
						 WHERE PatientId='{47d43c2c-cc2c-490c-9949-a86500969a69}'
				

