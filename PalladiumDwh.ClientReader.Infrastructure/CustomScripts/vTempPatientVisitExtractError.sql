create view vTempPatientVisitExtractError as
SELECT        *
FROM            TempPatientVisitExtract
WHERE        (CheckError = 1)