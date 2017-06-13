using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.ClientReader.Infrastructure.Csv.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Csv.Command
{
    public class AnalyzeCsvTempExtractsCommandTests
    {
        private DwapiRemoteContext _context;
        private IAnalyzeCsvTempExtractsCommand _extractCommand;
        private IProgress<DProgress> _dprogress;
        private string _extractsDir;
        private string _importPath;
        private ImportCsvService _importService;

        [SetUp]
        public void SetUp()
        {

            _dprogress = new Progress<DProgress>(ReportDProgress);
            _context = new DwapiRemoteContext();
            _extractCommand = new AnalyzeCsvTempExtractsCommand(_context);
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory");


            _extractsDir = $"{Utility.GetFolderPath(TestContext.CurrentContext.TestDirectory)}Extracts\\";
            _importPath = $@"{TestContext.CurrentContext.TestDirectory.HasToEndsWith(@"\")}";

            _importService = new ImportCsvService();

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
        public void should_Generate_Found_EventHistory_from_DiscoverSources()
        {
            /*
                    Sites|Nums
                    1000 |1
                    2000 |3
                         |2
            */

            var csv = TestHelpers.GetCsv("ARTPatientExtract");

            var summaries = _extractCommand.GenerateFoundEventHistory(csv).ToList();
            Assert.IsNotEmpty(summaries);

            foreach (var s in summaries)
            {
                Console.WriteLine($"Site:{s.SiteCode} | Found:{s.Found}");
            }
        }

        [Test]
        public void should_Execute_Load_AnalyzeCSVTempExtracts_Find_Command()
        {

            var files = Directory.GetFiles(_extractsDir, "*.csv").ToList();
            var csvs = _importService.CopyCsvFilesAsync(files, _importPath).Result;

            var emr = _context.Emrs.FirstOrDefault(x => x.Name.ToLower().Equals("IQCare".ToLower()));
            

            var events = _extractCommand.ExecuteAsync(emr,csvs, _dprogress).Result.ToList();

            _context = new DwapiRemoteContext();
            var updatedEventHistories = _context.EventHistories.ToList();
            Assert.IsNotEmpty(updatedEventHistories);
            Assert.IsTrue(updatedEventHistories.Count > 0);
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
