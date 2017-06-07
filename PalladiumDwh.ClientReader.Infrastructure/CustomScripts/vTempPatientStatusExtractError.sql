create view vTempPatientStatusExtractError as
SELECT        *
FROM            TempPatientStatusExtract
WHERE        (CheckError = 1)