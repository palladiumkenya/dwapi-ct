--delete FROM [DWAPICentral].[dbo].[Facility]

SELECT        Facility.Id, Facility.Code, Facility.Name, PatientExtract.PatientPID, PatientExtract.PatientCccNumber, PatientExtract.Gender, PatientVisitExtract.VisitId, PatientVisitExtract.VisitDate, PatientVisitExtract.Service
FROM            Facility INNER JOIN
                         PatientExtract ON Facility.Id = PatientExtract.FacilityId INNER JOIN
                         PatientVisitExtract ON PatientExtract.Id = PatientVisitExtract.PatientId
						 WHERE PatientPID=1000


