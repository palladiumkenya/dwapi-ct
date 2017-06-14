using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class AnalyzeTempExtractsCommandTests
    {
        private DwapiRemoteContext _context;
        private IAnalyzeTempExtractsCommand _extractCommand;
        private DatabaseManager _databaseManager;
        private IProgress<DProgress> _dprogress;

        [SetUp]
        public void SetUp()
        {

            _dprogress = new Progress<DProgress>(ReportDProgress);

            _context = new DwapiRemoteContext();
            _databaseManager = new DatabaseManager(_context);
            _extractCommand = new AnalyzeTempExtractsCommand(new EMRRepository(_context),_databaseManager);
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory");
            
            /*
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM  PatientExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract");
            */
        }

      
        [Test]
        public void should_Execute_Load_AnalyzeTempExtracts_Find_DbCommand()
        {
            var events= _extractCommand.ExecuteAsync( _dprogress).Result.ToList();

            _context=new  DwapiRemoteContext();
            var updatedEventHistories = _context.EventHistories.ToList();
            Assert.IsNotEmpty(updatedEventHistories);
            Assert.IsTrue(updatedEventHistories.Count> 0);
            foreach (var e in updatedEventHistories)
            {
                Console.WriteLine($"Site:{e.SiteCode}, {e.FoundInfo()}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory");
            /*
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM  PatientExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract");
            */
        }

        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}
