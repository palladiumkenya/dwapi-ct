using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.ClientReader.Infrastructure.Csv.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Csv.Command
{
    public class LoadPatientArtExtractCsvCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _clientConnection;
        private string _commandText;
        private ILoadPatientArtExtractCsvCommand _extractCommand;
        private int top = 5;
        private IProgress<DProgress> _dprogress;
        private IEMRRepository _emrRepository;
        private string _extractsDir;
        private string _importPath;
        private ImportCsvService _importService;


        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _emrRepository = new EMRRepository(_context);
            _dprogress = new Progress<DProgress>(ReportDProgress);
            new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _commandText = TestHelpers.GetCsv("ARTPatientExtract", "Extracts");

            _extractCommand = new LoadPatientArtExtractCsvCommand(_emrRepository);
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract");

            _extractsDir = $"{Utility.GetFolderPath(TestContext.CurrentContext.TestDirectory)}Extracts\\";
            _importPath = $@"{TestContext.CurrentContext.TestDirectory.HasToEndsWith(@"\")}";

            _importService = new ImportCsvService();
        }

        [Test]
        public void should_Execute_From_Csv()
        {
            Assert.That(_commandText, Does.Exist);

            var clearExtractsCommand = new ClearCsvExtractsCommand(_emrRepository);
            var analyzeTempExtractsCommand = new AnalyzeCsvTempExtractsCommand(_emrRepository);
            var result = clearExtractsCommand.ExecuteAsync(_dprogress).Result;

            var files = new List<string> { _commandText };
            var csvs = _importService.CopyCsvFilesAsync(files, _importPath).Result;

            var eventHistories = analyzeTempExtractsCommand.ExecuteAsync(csvs, _dprogress).Result;



            var watch = System.Diagnostics.Stopwatch.StartNew();
            var summary = _extractCommand.ExecuteAsync(_commandText, _dprogress).Result;
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientArtExtract")
                .Single();

            Assert.IsTrue(records == summary.Loaded);

            Console.WriteLine($"Summary:{summary}");
            Console.WriteLine($"Summary Error:{summary.ErrorStatus()}");

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {records} records! in {elapsedMs}ms ({elapsedMs / 1000}s)");
            Console.WriteLine(_commandText);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract");
            _context.SaveChanges();
        }
        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}
