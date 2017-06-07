ALTER VIEW vTempPatientVisitExtractErrorSummary
AS
SELECT        dbo.ValidationError.Id, dbo.Validator.Extract, dbo.Validator.Field, dbo.Validator.Type, dbo.Validator.Summary, dbo.ValidationError.DateGenerated, dbo.vTempPatientVisitExtractError.PatientPK,dbo.vTempPatientVisitExtractError.FacilityId,
                         dbo.vTempPatientVisitExtractError.PatientID, dbo.vTempPatientVisitExtractError.SiteCode, dbo.vTempPatientVisitExtractError.FacilityName, dbo.ValidationError.RecordId,

						 dbo.vTempPatientVisitExtractError.VisitDate, 
						 dbo.vTempPatientVisitExtractError.Service, 
						 dbo.vTempPatientVisitExtractError.VisitType, 
						 dbo.vTempPatientVisitExtractError.WHOStage, 
                         dbo.vTempPatientVisitExtractError.WABStage, 
						 dbo.vTempPatientVisitExtractError.Pregnant, 
						 dbo.vTempPatientVisitExtractError.LMP, 
						 dbo.vTempPatientVisitExtractError.EDD, 
                         dbo.vTempPatientVisitExtractError.Height, 
						 dbo.vTempPatientVisitExtractError.Weight, 
						 dbo.vTempPatientVisitExtractError.BP, 
						 dbo.vTempPatientVisitExtractError.OI, 
						 dbo.vTempPatientVisitExtractError.OIDate, 
                         dbo.vTempPatientVisitExtractError.Adherence, 
						 dbo.vTempPatientVisitExtractError.AdherenceCategory, 
						 dbo.vTempPatientVisitExtractError.SubstitutionFirstlineRegimenDate, 
                         dbo.vTempPatientVisitExtractError.SubstitutionFirstlineRegimenReason, 
						 dbo.vTempPatientVisitExtractError.SubstitutionSecondlineRegimenDate, 
                         dbo.vTempPatientVisitExtractError.SubstitutionSecondlineRegimenReason, 
						 dbo.vTempPatientVisitExtractError.SecondlineRegimenChangeDate, 
                         dbo.vTempPatientVisitExtractError.SecondlineRegimenChangeReason, 
						 dbo.vTempPatientVisitExtractError.FamilyPlanningMethod, 
						 dbo.vTempPatientVisitExtractError.PwP, 
                         dbo.vTempPatientVisitExtractError.GestationAge, 
						 dbo.vTempPatientVisitExtractError.NextAppointmentDate, 
						 dbo.vTempPatientVisitExtractError.VisitId

FROM            dbo.vTempPatientVisitExtractError INNER JOIN
                         dbo.ValidationError ON dbo.vTempPatientVisitExtractError.Id = dbo.ValidationError.RecordId INNER JOIN
                         dbo.Validator ON dbo.ValidationError.ValidatorId = dbo.Validator.Id