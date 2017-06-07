create view vTempPatientBaselinesExtractError as
SELECT        *
FROM            TempPatientBaselinesExtract
WHERE        (CheckError = 1)