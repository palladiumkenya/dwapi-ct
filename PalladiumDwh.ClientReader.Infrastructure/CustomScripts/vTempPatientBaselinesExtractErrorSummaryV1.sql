ALTER VIEW vTempPatientBaselinesExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientBaselinesExtractError.PatientPK,dbo.vTempPatientBaselinesExtractError.FacilityId,
                         dbo.vTempPatientBaselinesExtractError.PatientID, dbo.vTempPatientBaselinesExtractError.SiteCode, dbo.ValidationError.RecordId,
						 
						 dbo.vTempPatientBaselinesExtractError.bCD4, 
						 dbo.vTempPatientBaselinesExtractError.bCD4Date, 
						 dbo.vTempPatientBaselinesExtractError.bWAB, 
						 dbo.vTempPatientBaselinesExtractError.bWABDate, 
                         dbo.vTempPatientBaselinesExtractError.bWHO, 
						 dbo.vTempPatientBaselinesExtractError.bWHODate, 
						 dbo.vTempPatientBaselinesExtractError.eWAB, 
						 dbo.vTempPatientBaselinesExtractError.eWABDate, 
                         dbo.vTempPatientBaselinesExtractError.eCD4,
						 dbo.vTempPatientBaselinesExtractError.eCD4Date, 
						 dbo.vTempPatientBaselinesExtractError.eWHO, 
						 dbo.vTempPatientBaselinesExtractError.eWHODate, 
                         dbo.vTempPatientBaselinesExtractError.lastWHO, 
						 dbo.vTempPatientBaselinesExtractError.lastWHODate, 
						 dbo.vTempPatientBaselinesExtractError.lastCD4, 
						 dbo.vTempPatientBaselinesExtractError.lastCD4Date, 
                         dbo.vTempPatientBaselinesExtractError.lastWAB, 
						 dbo.vTempPatientBaselinesExtractError.lastWABDate, 
						 dbo.vTempPatientBaselinesExtractError.m12CD4, 
						 dbo.vTempPatientBaselinesExtractError.m12CD4Date, 
                         dbo.vTempPatientBaselinesExtractError.m6CD4, 
						 dbo.vTempPatientBaselinesExtractError.m6CD4Date

FROM            dbo.vTempPatientBaselinesExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientBaselinesExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id