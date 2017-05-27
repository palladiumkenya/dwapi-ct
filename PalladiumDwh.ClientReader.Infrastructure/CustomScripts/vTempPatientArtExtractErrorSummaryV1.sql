ALTER VIEW vTempPatientArtExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientArtExtractError.PatientPK,dbo.vTempPatientArtExtractError.FacilityId,
                         dbo.vTempPatientArtExtractError.PatientID, dbo.vTempPatientArtExtractError.SiteCode, dbo.vTempPatientArtExtractError.FacilityName, dbo.ValidationError.RecordId,
						 
						 dbo.vTempPatientArtExtractError.DOB, 
						 dbo.vTempPatientArtExtractError.Gender, 
                         dbo.vTempPatientArtExtractError.PatientSource, 
						 dbo.vTempPatientArtExtractError.RegistrationDate, 
						 dbo.vTempPatientArtExtractError.AgeLastVisit, 
						 dbo.vTempPatientArtExtractError.PreviousARTStartDate, 
                         dbo.vTempPatientArtExtractError.PreviousARTRegimen, 
						 dbo.vTempPatientArtExtractError.StartARTAtThisFacility, 
						 dbo.vTempPatientArtExtractError.StartARTDate, 
						 dbo.vTempPatientArtExtractError.StartRegimen, 
                         dbo.vTempPatientArtExtractError.StartRegimenLine, 
						 dbo.vTempPatientArtExtractError.LastARTDate, 
						 dbo.vTempPatientArtExtractError.LastRegimen, 
						 dbo.vTempPatientArtExtractError.LastRegimenLine, 
						 dbo.vTempPatientArtExtractError.LastVisit, 
                         dbo.vTempPatientArtExtractError.ExitReason, 
						 dbo.vTempPatientArtExtractError.ExitDate
FROM            dbo.vTempPatientArtExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientArtExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id