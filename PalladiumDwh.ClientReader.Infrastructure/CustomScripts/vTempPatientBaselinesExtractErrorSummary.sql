CREATE VIEW vTempPatientBaselinesExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientBaselinesExtractError.PatientPK,dbo.vTempPatientBaselinesExtractError.FacilityId,
                         dbo.vTempPatientBaselinesExtractError.PatientID, dbo.vTempPatientBaselinesExtractError.SiteCode, dbo.ValidationError.RecordId
FROM            dbo.vTempPatientBaselinesExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientBaselinesExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id