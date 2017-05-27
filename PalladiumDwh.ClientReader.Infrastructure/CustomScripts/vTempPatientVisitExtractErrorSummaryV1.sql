ALTER VIEW vTempPatientVisitExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientVisitExtractError.PatientPK,dbo.vTempPatientVisitExtractError.FacilityId,
                         dbo.vTempPatientVisitExtractError.PatientID, dbo.vTempPatientVisitExtractError.SiteCode, dbo.vTempPatientVisitExtractError.FacilityName, dbo.ValidationError.RecordId
FROM            dbo.vTempPatientVisitExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientVisitExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id