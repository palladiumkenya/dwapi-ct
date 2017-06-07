ALTER VIEW vTempPatientPharmacyExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientPharmacyExtractError.PatientPK,dbo.vTempPatientPharmacyExtractError.FacilityId,
                         dbo.vTempPatientPharmacyExtractError.PatientID, dbo.vTempPatientPharmacyExtractError.SiteCode,  dbo.ValidationError.RecordId,

					     dbo.vTempPatientPharmacyExtractError.Drug, 
						 dbo.vTempPatientPharmacyExtractError.DispenseDate,
						 dbo.vTempPatientPharmacyExtractError.Duration, 
						 dbo.vTempPatientPharmacyExtractError.ExpectedReturn, 
                         dbo.vTempPatientPharmacyExtractError.TreatmentType, 
						 dbo.vTempPatientPharmacyExtractError.RegimenLine, 
						 dbo.vTempPatientPharmacyExtractError.PeriodTaken, 
                         dbo.vTempPatientPharmacyExtractError.ProphylaxisType, 
						 dbo.vTempPatientPharmacyExtractError.Provider, 
						 dbo.vTempPatientPharmacyExtractError.VisitID

FROM            dbo.vTempPatientPharmacyExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientPharmacyExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id