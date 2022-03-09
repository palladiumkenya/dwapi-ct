using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using Dapper;
using FizzWare.NBuilder;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Interfaces.Extracts;
using PalladiumDwh.Shared.Interfaces.Stages;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Infrastructure.Tests
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

        public static IEnumerable<PatientExtract> GetTestPatientData(Facility facility, int patientCount,
            int visitCount)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
            }

            return patients;
        }

        public static IEnumerable<PatientExtract> GetTestPatientARTData(Facility facility, int patientCount,
            int visitCount = 1)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                var visits = Builder<PatientArtExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientArtExtracts(visits);
            }

            return patients;
        }

        public static IEnumerable<PatientExtract> GetTestPatientBaselinesData(Facility facility, int patientCount,
            int visitCount = 1)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                var visits = Builder<PatientBaselinesExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientBaselinesExtracts(visits);
            }

            return patients;
        }

        public static IEnumerable<PatientExtract> GetTestPatientStatusData(Facility facility, int patientCount,
            int visitCount = 1)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                var visits = Builder<PatientStatusExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientStatusExtracts(visits);
            }

            return patients;
        }

        public static IEnumerable<PatientExtract> GetTestPatientPharmacyData(Facility facility, int patientCount,
            int visitCount = 2)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                var visits = Builder<PatientPharmacyExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientPharmacyExtracts(visits);
            }

            return patients;
        }

        public static IEnumerable<PatientExtract> GetTestPatientLaboratoryData(Facility facility, int patientCount,
            int visitCount = 2)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                var visits = Builder<PatientLaboratoryExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientLaboratoryExtracts(visits);
            }

            return patients;
        }

        public static IEnumerable<PatientExtract> GetTestPatientVisitsData(Facility facility, int patientCount,
            int visitCount = 2)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                var visits = Builder<PatientVisitExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientVisitExtracts(visits);
            }

            return patients;
        }

        public static IEnumerable<PatientExtract> GetTestPatientAdverseEventData(Facility facility, int patientCount,
            int visitCount = 2)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                var visits = Builder<PatientAdverseEventExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientAdverseEventExtracts(visits);
            }

            return patients;
        }


        public static void AddNew(List<PatientVisitProfile> visitProfiles, int i)
        {

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
                .With(x => x.SiteCode =99990)
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

        public static List<T> CreateTestStage<T>(Guid facilityId,Guid manifestId) where T:IStage
        {
            var stages = Builder<T>.CreateListOfSize(5).All()
                .With(x => x.CurrentPatientId =null)
                .With(x => x.LiveStage =LiveStage.Rest)
                .With(x => x.FacilityId = facilityId)
                .With(x => x.SiteCode =99990)
                .With(x=>x.Voided=false)
                .With(x=>x.Processed=false)
                .With(x => x.LiveSession=manifestId).Build()
                .ToList();

            for (int i = 0; i < stages.Count; i++)
            {
                stages[i].PatientPK = i+1;
            }

            return stages;
        }

        public static void CreateTestFacilityPatient(Guid facilityId)
        {
            var patientExtracts = Builder<PatientExtract>.CreateListOfSize(3).All()
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

        public static void CreateTestExtract<T>(Guid facilityId) where T:class
        {
            CreateTestFacilityPatient(facilityId);

            var visitExtracts = Builder<T>.CreateListOfSize(3).All()
                .Build()
                .ToList();

            var context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            var patients = context.PatientExtracts.Where(x => x.FacilityId == facilityId).Take(3).ToList();

            for (int i = 0; i < visitExtracts.Count; i++)
            {
                ((dynamic) visitExtracts[i]).PatientId = patients[i].Id;
                ((dynamic) visitExtracts[i]).Created = DateTime.Now.AddDays(-2).Date;
                ((dynamic) visitExtracts[i]).Voided = false;
                ((dynamic) visitExtracts[i]).Processed = false;
            }

            visitExtracts.ForEach(s => context.Set<T>().AddOrUpdate(s));
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

        public static void CreateDb()
        {
            var context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            context.Database.Delete();
            context.Database.CreateIfNotExists();
        }
    }
}
