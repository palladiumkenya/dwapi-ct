CREATE VIEW vTempPatientArtExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientArtExtractError.PatientPK,dbo.vTempPatientArtExtractError.FacilityId,
                         dbo.vTempPatientArtExtractError.PatientID, dbo.vTempPatientArtExtractError.SiteCode, dbo.vTempPatientArtExtractError.FacilityName, dbo.ValidationError.RecordId
FROM            dbo.vTempPatientArtExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientArtExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id