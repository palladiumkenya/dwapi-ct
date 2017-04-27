create view vTempPatientPharmacyExtractError as
SELECT        *
FROM            TempPatientPharmacyExtract
WHERE        (CheckError = 1)