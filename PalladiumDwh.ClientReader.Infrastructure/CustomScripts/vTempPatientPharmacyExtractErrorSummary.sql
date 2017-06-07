CREATE VIEW vTempPatientPharmacyExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientPharmacyExtractError.PatientPK,dbo.vTempPatientPharmacyExtractError.FacilityId,
                         dbo.vTempPatientPharmacyExtractError.PatientID, dbo.vTempPatientPharmacyExtractError.SiteCode,  dbo.ValidationError.RecordId
FROM            dbo.vTempPatientPharmacyExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientPharmacyExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id