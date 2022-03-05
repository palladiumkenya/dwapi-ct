﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FizzWare.NBuilder;
using PalladiumDwh.Infrastructure.Data;
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
    }
}
