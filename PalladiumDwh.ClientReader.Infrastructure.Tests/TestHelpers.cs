using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests
{
    public static class TestHelpers
    {
        public static void CreateTestData<T>(DbContext context, IEnumerable<T> entities) where T : class 
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }
        public static IEnumerable<T> GetTestData<T>(int count) where T : Entity
        {
            return Builder<T>.CreateListOfSize(count).Build();
        }

        public static IEnumerable<EMR> GetEMRTestProjectData(Project project,int emrCount, int extractSettingCount)
        {
            var emrs = Builder<EMR>.CreateListOfSize(emrCount).Build().ToList();
            foreach (var emr in emrs)
            {
                emr.ProjectId = project.Id;
                var extractSettings = Builder<ExtractSetting>.CreateListOfSize(extractSettingCount).Build().ToList();
                foreach (var e in extractSettings)
                {
                    e.IsActive = true;
                    emr.AddExtractSetting(e);
                }
                
                emr.IsDefault=false;
            }
            emrs.First().IsDefault = true;
            return emrs;
        }

        public static IEnumerable<PatientExtract> GetTestPatientData(Facility facility, int patientCount, int visitCount)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
            }
            return patients;
        }

        public static IEnumerable<PatientExtract> GetTestPatientVisitsData(Facility facility,int patientCount,int visitCount)
        {
            var patients=Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId=facility.Id;
                var visits = Builder<PatientVisitExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientVisitExtracts(visits);
            }
            return patients;
        }
        public static IEnumerable<PatientExtract> GetTestPatientWithExtracts(Facility facility, int patientCount, int count)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                p.AddPatientArtExtracts(Builder<PatientArtExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientBaselinesExtracts(Builder<PatientBaselinesExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientLaboratoryExtracts(Builder<PatientLaboratoryExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientPharmacyExtracts(Builder<PatientPharmacyExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientStatusExtracts(Builder<PatientStatusExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientVisitExtracts(Builder<PatientVisitExtract>.CreateListOfSize(count).Build().ToList());

            }
            return patients;
        }

        public static void CreateTestPatientWithValidations(DwapiRemoteContext context)
        {
            var validators = Builder<Core.Model.Validator>.CreateListOfSize(2)
                .All()
                .With(x => x.Type = "TEST")
                .Build()
                .ToList();

            var tempPatientExtracts = Builder<TempPatientExtract>.CreateListOfSize(5)
                .All()
                .With(x => x.CheckError = false)
                .Build().ToList();

            var p1 = tempPatientExtracts.First();
            p1.CheckError = true;
            var p2 = tempPatientExtracts.Last();
            p2.CheckError = true;

            var validationErrors = Builder<ValidationError>.CreateListOfSize(2).Build().ToList();

            var v1 = validationErrors.First();
            v1.ValidatorId = validators.First().Id;
            v1.RecordId = p1.Id;
            var v2 = validationErrors.Last();
            v2.ValidatorId = validators.Last().Id;
            v2.RecordId = p2.Id;

            CreateTestData(context, validators);
            CreateTestData(context, tempPatientExtracts);
            CreateTestData(context, validationErrors);
        }

        public static void RefershConnect(string key = "EMRDatabase")
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings[key].ConnectionString = GetConnection(key).ConnectionString;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }
        public static string GetCsv(string name)
        {
            string path = TestContext.CurrentContext.TestDirectory;
            var files = Directory.GetFiles(path, "*.csv", SearchOption.AllDirectories);
            return files.FirstOrDefault(x => x.Contains(name));
        }

        public static DbConnection GetConnection(string key= "EMRDatabase")
        {
            string path = TestContext.CurrentContext.TestDirectory;
            path=path.EndsWith(@"\")?path:$@"{path}\";
            var cn = ConfigurationManager.ConnectionStrings[key];
            var connectionString = cn.ConnectionString.Replace($"AttachDbFilename=",$"AttachDbFilename={path}");

           return new SqlConnection(connectionString);
        }

        public static string GetPatientsSql(int top=-1)
        {
            string selecttop = top > 0 ? $"TOP {top} " : "";

            return $@"
              SELECT {selecttop}  
	            PatientID, PatientPK, FacilityID, SiteCode, FacilityName, SatelliteName, Gender, DOB, RegistrationDate, RegistrationAtCCC, RegistrationAtPMTCT, RegistrationAtTBClinic, PatientSource, Region, District, Village, 
	            ContactRelation, LastVisit, MaritalStatus, EducationLevel, DateConfirmedHIVPositive, PreviousARTExposure, PreviousARTStartDate, StatusAtCCC, StatusAtPMTCT, StatusAtTBClinic, 'IQCare' AS EMR, 
	            'Kenya HMIS II' AS Project, CAST(GETDATE() AS DATE) AS DateExtracted,newid() as ID
            FROM            
	            tmp_PatientMaster AS a
            ";
        }

        public static string GetPatientsArtSql(int top = -1)
        {
            string selecttop = top > 0 ? $"TOP {top} " : "";

            return $@"
              SELECT {selecttop}  
	                a.PatientPK, a.PatientID, c.FacilityID, c.SiteCode, a.FacilityName, a.DOB, a.AgeEnrollment, a.AgeARTStart, a.AgeLastVisit, a.RegistrationDate, a.PatientSource, a.Gender, a.StartARTDate, a.PreviousARTStartDate, 
	                a.PreviousARTRegimen, a.StartARTAtThisFacility, a.StartRegimen, a.StartRegimenLine, a.LastARTDate, a.LastRegimen, a.LastRegimenLine, a.Duration, a.ExpectedReturn, a.Provider, a.LastVisit, a.ExitReason, 
	                a.ExitDate, CAST(GETDATE() AS DATE) AS DateExtracted
                FROM            
	                tmp_ARTPatients AS a INNER JOIN
	                tmp_PatientMaster AS c ON a.PatientPK = c.PatientPK
            ";
        }

        public static string GetPatientsPharmacySql(int top = -1)
        {
            string selecttop = top > 0 ? $"TOP {top} " : "";

            return $@"
              SELECT {selecttop}  
	                tmp_PatientMaster.PatientID, tmp_PatientMaster.FacilityID, tmp_PatientMaster.SiteCode, tmp_Pharmacy.PatientPK, tmp_Pharmacy.VisitID, tmp_Pharmacy.Drug, tmp_Pharmacy.Provider, 
	                tmp_Pharmacy.DispenseDate, tmp_Pharmacy.Duration, tmp_Pharmacy.ExpectedReturn, tmp_Pharmacy.TreatmentType, tmp_Pharmacy.RegimenLine, tmp_Pharmacy.PeriodTaken, 
	                tmp_Pharmacy.ProphylaxisType, CAST(GETDATE() AS DATE) AS DateExtracted
                FROM            
	                tmp_Pharmacy INNER JOIN
                    tmp_PatientMaster ON tmp_Pharmacy.PatientPK = tmp_PatientMaster.PatientPK 
            ";
        }

        public static string GetPatientVisitsSql(int top = -1)
        {
            string selecttop = top > 0 ? $"TOP {top} " : "";

            return $@"
              SELECT {selecttop}  
	                REPLACE(tmp_PatientMaster.PatientID, ' ', '') AS PatientID, tmp_PatientMaster.FacilityName, tmp_PatientMaster.SiteCode, tmp_ClinicalEncounters.PatientPK, tmp_ClinicalEncounters.VisitID, 
	                tmp_ClinicalEncounters.VisitDate, tmp_ClinicalEncounters.Service, tmp_ClinicalEncounters.VisitType, tmp_ClinicalEncounters.WHOStage, tmp_ClinicalEncounters.WABStage, tmp_ClinicalEncounters.Pregnant, 
	                tmp_ClinicalEncounters.LMP, tmp_ClinicalEncounters.EDD, tmp_ClinicalEncounters.Height, tmp_ClinicalEncounters.Weight, tmp_ClinicalEncounters.BP, tmp_ClinicalEncounters.OI, 
	                tmp_ClinicalEncounters.OIDate, tmp_ClinicalEncounters.Adherence, tmp_ClinicalEncounters.AdherenceCategory, NULL AS SubstitutionFirstlineRegimenDate, NULL AS SubstitutionFirstlineRegimenReason, NULL 
	                AS SubstitutionSecondlineRegimenDate, NULL AS SubstitutionSecondlineRegimenReason, NULL AS SecondlineRegimenChangeDate, NULL AS SecondlineRegimenChangeReason, 
	                tmp_ClinicalEncounters.FamilyPlanningMethod, tmp_ClinicalEncounters.PwP, tmp_ClinicalEncounters.GestationAge, tmp_ClinicalEncounters.NextAppointmentDate, CAST(GETDATE() AS DATE) 
	                AS DateExtracted
                FROM            
	                tmp_ClinicalEncounters INNER JOIN
	                tmp_PatientMaster ON tmp_PatientMaster.PatientPK = tmp_ClinicalEncounters.PatientPK
            ";
        }

        public static string GetPatientStatusSql(int top = -1)
        {
            string selecttop = top > 0 ? $"TOP {top} " : "";

            return $@"
              SELECT {selecttop}  
	                tmp_PatientMaster.PatientID, tmp_LastStatus.PatientPK, tmp_PatientMaster.SiteCode, tmp_PatientMaster.FacilityName, tmp_LastStatus.ExitDescription, tmp_LastStatus.ExitDate, tmp_LastStatus.ExitReason, 
	                CAST(GETDATE() AS DATE) AS DateExtracted
                FROM            
	                tmp_LastStatus INNER JOIN
	                tmp_PatientMaster ON tmp_LastStatus.PatientPK = tmp_PatientMaster.PatientPK
            ";
        }

        public static string GetPatientLabsSql(int top = -1)
        {
            string selecttop = top > 0 ? $"TOP {top} " : "";

            return $@"
              SELECT {selecttop}  
	                tmp_PatientMaster.PatientID, tmp_Labs.PatientPK, tmp_PatientMaster.FacilityID, tmp_PatientMaster.SiteCode, tmp_PatientMaster.FacilityName, tmp_PatientMaster.SatelliteName, tmp_Labs.VisitID, 
	                tmp_Labs.OrderedbyDate, tmp_Labs.ReportedbyDate, tmp_Labs.TestName, tmp_Labs.EnrollmentTest, tmp_Labs.TestResult, CAST(GETDATE() AS DATE) AS DateExtracted
                FROM           
	                tmp_Labs INNER JOIN
	                tmp_PatientMaster ON tmp_Labs.PatientPK = tmp_PatientMaster.PatientPK
            ";
        }

        public static string GetPatientBaselinesSql(int top = -1)
        {
            string selecttop = top > 0 ? $"TOP {top} " : "";

            return $@"
              SELECT {selecttop}  
	                tmp_PatientMaster.PatientPK, tmp_PatientMaster.PatientID, tmp_PatientMaster.FacilityID, tmp_PatientMaster.SiteCode, IQC_bCD4.bCD4, IQC_bCD4.bCD4Date, IQC_bWAB.bWAB, IQC_bWAB.bWABDate, 
	                IQC_bWHO.bWHO, IQC_bWHO.bWHODate, IQC_eWAB.eWAB, IQC_eWAB.eWABDate, IQC_eCD4.eCD4, IQC_eCD4.eCD4Date, IQC_eWHO.eWHO, IQC_eWHO.eWHODate, IQC_lastWHO.lastWHO, 
	                IQC_lastWHO.lastWHODate, IQC_lastWAB.lastWAB, IQC_lastWAB.lastWABDate, IQC_lastCD4.lastCD4, IQC_lastCD4.lastCD4Date, IQC_m12CD4.m12CD4, IQC_m12CD4.m12CD4Date, IQC_m6CD4.m6CD4, 
	                IQC_m6CD4.m6CD4Date, CAST(GETDATE() AS DATE) AS DateExtracted
                FROM            
	                tmp_PatientMaster LEFT OUTER JOIN
                    IQC_bCD4 ON tmp_PatientMaster.PatientPK = IQC_bCD4.PatientPK LEFT OUTER JOIN
                    IQC_bWAB ON tmp_PatientMaster.PatientPK = IQC_bWAB.PatientPK LEFT OUTER JOIN
                    IQC_bWHO ON tmp_PatientMaster.PatientPK = IQC_bWHO.PatientPK LEFT OUTER JOIN
                    IQC_lastCD4 ON tmp_PatientMaster.PatientPK = IQC_lastCD4.PatientPK LEFT OUTER JOIN
                    IQC_eWAB ON tmp_PatientMaster.PatientPK = IQC_eWAB.PatientPK LEFT OUTER JOIN
                    IQC_eWHO ON tmp_PatientMaster.PatientPK = IQC_eWHO.PatientPK LEFT OUTER JOIN
                    IQC_lastWAB ON tmp_PatientMaster.PatientPK = IQC_lastWAB.PatientPK LEFT OUTER JOIN
                    IQC_eCD4 ON tmp_PatientMaster.PatientPK = IQC_eCD4.PatientPK LEFT OUTER JOIN
                    IQC_lastWHO ON tmp_PatientMaster.PatientPK = IQC_lastWHO.PatientPK LEFT OUTER JOIN
                    IQC_m12CD4 ON tmp_PatientMaster.PatientPK = IQC_m12CD4.PatientPK LEFT OUTER JOIN
                    IQC_m6CD4 ON tmp_PatientMaster.PatientPK = IQC_m6CD4.PatientPK
            ";
        }
    }
}