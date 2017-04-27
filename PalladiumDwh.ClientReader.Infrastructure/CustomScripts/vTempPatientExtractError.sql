create view vTempPatientExtractError as
SELECT        *
FROM            TempPatientExtract
WHERE        (CheckError = 1)