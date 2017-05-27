ALTER VIEW vTempPatientExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientExtractError.PatientPK,dbo.vTempPatientExtractError.FacilityId,
                         dbo.vTempPatientExtractError.PatientID, dbo.vTempPatientExtractError.SiteCode, dbo.vTempPatientExtractError.FacilityName, dbo.ValidationError.RecordId,

                         dbo.vTempPatientExtractError.Gender, 
						 dbo.vTempPatientExtractError.DOB, 
						 dbo.vTempPatientExtractError.RegistrationDate, 
						 dbo.vTempPatientExtractError.RegistrationAtCCC, 
						 dbo.vTempPatientExtractError.PatientSource, 						 
                         dbo.vTempPatientExtractError.MaritalStatus, 
						 dbo.vTempPatientExtractError.EducationLevel,
						 dbo.vTempPatientExtractError.DateConfirmedHIVPositive, 
						 dbo.vTempPatientExtractError.PreviousARTExposure, 
                         dbo.vTempPatientExtractError.PreviousARTStartDate,
						 dbo.vTempPatientExtractError.LastVisit
						 
FROM            dbo.vTempPatientExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id