ALTER VIEW vTempPatientLaboratoryExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientLaboratoryExtractError.PatientPK,dbo.vTempPatientLaboratoryExtractError.FacilityId,
                         dbo.vTempPatientLaboratoryExtractError.PatientID, dbo.vTempPatientLaboratoryExtractError.SiteCode, dbo.vTempPatientLaboratoryExtractError.FacilityName, dbo.ValidationError.RecordId,

						 dbo.vTempPatientLaboratoryExtractError.OrderedByDate, 
						 dbo.vTempPatientLaboratoryExtractError.ReportedByDate, 
						 dbo.vTempPatientLaboratoryExtractError.TestName, 
                         dbo.vTempPatientLaboratoryExtractError.EnrollmentTest, 
						 dbo.vTempPatientLaboratoryExtractError.TestResult, 
						 dbo.vTempPatientLaboratoryExtractError.VisitId

FROM            dbo.vTempPatientLaboratoryExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientLaboratoryExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id