create view vTempPatientArtExtractError as
SELECT        *
FROM            TempPatientArtExtract
WHERE        (CheckError = 1)