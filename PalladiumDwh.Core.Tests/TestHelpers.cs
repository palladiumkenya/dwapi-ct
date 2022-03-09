using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using PalladiumDwh.Core.Application.Source;
using PalladiumDwh.Core.Application.Source.Dto;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Tests
{
    public static class TestHelpers
    {
        public static void CreateTestData<T>(DbContext context, IEnumerable<T> entities) where T : Entity
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }
        public static IEnumerable<T> GetTestData<T>(int count) where T : Entity
        {
            return Builder<T>.CreateListOfSize(count).Build();
        }

        public static IEnumerable<PatientExtract> GetTestPatientData(Facility facility, int patientCount)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
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
                p.AddPatientAdverseEventExtracts(Builder<PatientAdverseEventExtract>.CreateListOfSize(count).Build().ToList());

            }
            return patients;
        }

        public static List<string> GetGateways(string queueName)
        {
            var gateways = new List<string>
            {
                queueName.Substring(0, queueName.Length - 1),
                $"{queueName}{typeof(Manifest).Name.ToLower()}",
                $"{queueName}{typeof(PatientARTProfile).Name.ToLower()}",
                $"{queueName}{typeof(PatientBaselineProfile).Name.ToLower()}",
                $"{queueName}{typeof(PatientLabProfile).Name.ToLower()}",
                $"{queueName}{typeof(PatientPharmacyProfile).Name.ToLower()}",
                $"{queueName}{typeof(PatientVisitProfile).Name.ToLower()}",
                $"{queueName}{typeof(PatientStatusProfile).Name.ToLower()}",
                $"{queueName}{typeof(PatientAdverseEventProfile).Name.ToLower()}"
            };
            return gateways;
        }

        public static List<string> GetAllGateways(string queueName)
        {
            var gateways = new List<string>();

            foreach (var g in GetGateways(queueName))
            {
                gateways.Add($"{g}");
                gateways.Add($"{g}.backlog");
                gateways.Add($"{g}.backlog.dead");
                gateways.Add($"{g}.dead");
            }
            return gateways;
        }

        public static void CreateTestMasterFacility(int code = 99990, string name = "Maun Hospital (99990)",
            string county = "National")
        {
            var context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            context.MasterFacilities.AddOrUpdate(new MasterFacility() {Code = code, Name = name, County = county});
            context.SaveChanges();
        }

        public static void CreateTestFacility(Guid id, int code = 99990, string name = "Maun Hospital (99990)")
        {
            var context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            context.Facilities.AddOrUpdate(new Facility() {Id = id, Code = code, Name = name});
            context.SaveChanges();
        }

        public static void ClearDb()
        {
            var sql = @"DELETE FROM Facility;DELETE FROM FacilityManifest;TRUNCATE TABLE ActionRegister;DELETE FROM StagePatientExtract";
            var context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            context.Database.ExecuteSqlCommand(sql);
        }

        public static void ClearDb(params string[] tables)
        {
            var sql = new StringBuilder();
            foreach (var tbl in tables)
            {
                sql.AppendLine($"DELETE FROM {tbl};");
            }
            var context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            context.Database.ExecuteSqlCommand(sql.ToString());
        }

          public static void CreateTestFacilityStage(Guid facilityId)
        {
            var stages = Builder<StagePatientExtract>.CreateListOfSize(5).All()
                .With(x => x.FacilityId = facilityId)
                .With(x => x.LiveSession=Guid.NewGuid()).Build()
                .ToList();
            var context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            stages.ForEach(s => context.StagePatientExtracts.AddOrUpdate(s));
            context.SaveChanges();
        }

        public static List<StagePatientExtract> CreateTestFacilityStagePatient(Guid facilityId,Guid manifestId)
        {
            var stages = Builder<StagePatientExtract>.CreateListOfSize(5).All()
                .With(x => x.CurrentPatientId =null)
                .With(x => x.LiveStage =LiveStage.Rest)
                .With(x => x.FacilityId = facilityId)
                .With(x=>x.Voided=false)
                .With(x=>x.Processed=false)
                .With(x => x.LiveSession=manifestId).Build()
                .ToList();

            for (int i = 0; i < stages.Count; i++)
            {
                stages[i].PatientPID = i+1;
                stages[i].PatientCccNumber = $"999900000{stages[i].PatientPID}";
            }

            return stages;
        }

        public static List<PatientSourceDto> CreateSourcePatient(int count=5,int siteCode=99990)
        {
            var sources = Builder<PatientSourceDto>.CreateListOfSize(count).All()
                .With(x => x.SiteCode =siteCode)
                .Build()
                .ToList();

            for (int i = 0; i < sources.Count; i++)
            {
                sources[i].PatientPK = i+1;
                sources[i].PatientID = $"{siteCode}0000{sources[i].PatientPK}";
            }

            return sources;
        }

        public static void CreatePatientExtracts(Guid facilityId,int count=3)
        {
            var patientExtracts = Builder<PatientExtract>.CreateListOfSize(count).All()
                .With(x => x.FacilityId = facilityId)
                .With(x=>x.Created=DateTime.Now.AddDays(-2).Date)
                .With(x=>x.Updated=null)
                .With(x=>x.Voided=false)
                .With(x=>x.Processed=false)
                .Build()
                .ToList();

            for (int i = 0; i < patientExtracts.Count; i++)
            {
                patientExtracts[i].PatientPID = i+1;
                patientExtracts[i].PatientCccNumber = $"999900000{patientExtracts[i].PatientPID}";
            }

            var context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            patientExtracts.ForEach(s => context.PatientExtracts.AddOrUpdate(s));
            context.SaveChanges();
        }

        public static void CreateDb()
        {
            var context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            context.Database.Delete();
            context.Database.CreateIfNotExists();
        }

        public static PatientSourceBag GeneratePatientBag(Guid facilityId,Guid manifestId,int count=5,int siteCode=99990)
        {
            var bag = Builder<PatientSourceBag>.CreateNew()
                .With(x => x.EmrSetup = EmrSetup.SingleFacility)
                .With(x => x.FacilityId = facilityId)
                .With(x => x.ManifestId = manifestId)
                .With(x => x.SiteCode=siteCode)
                .With(x => x.Facility="Maun Hospital (99990)")
                .With(x=>x.Extracts=CreateSourcePatient(count,siteCode))
                .Build();

            return bag;
        }
    }
}
