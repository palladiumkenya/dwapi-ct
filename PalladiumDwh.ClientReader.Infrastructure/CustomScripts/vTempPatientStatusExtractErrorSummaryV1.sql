ALTER VIEW vTempPatientStatusExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientStatusExtractError.PatientPK,dbo.vTempPatientStatusExtractError.FacilityId,
                         dbo.vTempPatientStatusExtractError.PatientID, dbo.vTempPatientStatusExtractError.SiteCode, dbo.vTempPatientStatusExtractError.FacilityName, dbo.ValidationError.RecordId,

						 dbo.vTempPatientStatusExtractError.ExitDescription, 
						 dbo.vTempPatientStatusExtractError.ExitDate, 
						 dbo.vTempPatientStatusExtractError.ExitReason

FROM            dbo.vTempPatientStatusExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientStatusExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id