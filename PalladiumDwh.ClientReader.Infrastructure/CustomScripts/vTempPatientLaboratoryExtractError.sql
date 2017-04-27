create view vTempPatientLaboratoryExtractError as
SELECT        *
FROM            TempPatientLaboratoryExtract
WHERE        (CheckError = 1)