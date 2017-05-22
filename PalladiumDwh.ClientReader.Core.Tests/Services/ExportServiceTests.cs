using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using FizzWare.NBuilder;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Tests.Services
{
    [TestFixture]
    public class ExportServiceTests
    {
        private DwapiRemoteContext _context;
        private IClientPatientExtractRepository _clientPatientExtractRepository;
        private IExportService _exportService;
        private List<ClientPatientExtract> _clientPatientExtracts;
        private string _exportDir;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);
            _clientPatientExtracts = Builder<ClientPatientExtract>.CreateListOfSize(3).Build().ToList();
            TestHelpers.CreateTestData(_context, _clientPatientExtracts);

            _clientPatientExtractRepository = new ClientPatientExtractRepository(_context);
            _exportDir = TestContext.CurrentContext.TestDirectory;
            _exportService =new ExportService(_clientPatientExtractRepository);
        }

        [Test]
        public void should_Export_Extracts_ToJSon()
        {
            var saveFolder= _exportService.ExportToJSonAsync(_exportDir).Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(saveFolder));
            Console.WriteLine(saveFolder);
            var files = TestHelpers.GetExports("-");
            Assert.IsNotEmpty(files);
            
            var patients = _clientPatientExtractRepository.GetAll();
            foreach (var p in patients)
            {
                Assert.IsTrue(File.Exists($"{saveFolder}{p.Id}.dwh"));
            }
            var selectedFile = files.FirstOrDefault();
            Console.WriteLine(selectedFile);
            var contents = File.ReadAllText(selectedFile);
            Console.WriteLine(contents);
        }
        [Test]
        public void should_Decode_Exported_Json_Extracts()
        {
            var saveFolder = _exportService.ExportToJSonAsync(_exportDir).Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(saveFolder));
            Console.WriteLine(saveFolder);
            var files = TestHelpers.GetExports("-");
            var selectedFile = files.FirstOrDefault();
            Assert.IsNotNull(selectedFile);
            Console.WriteLine(selectedFile);

            var contents = File.ReadAllText(selectedFile);
            Console.WriteLine(new string('-',30));

            Console.WriteLine(contents);
            var decoded = _exportService.Base64Decode(contents);

            Assert.IsTrue(decoded.ToLower().Contains("PatientPK".ToLower()));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(decoded);
        }
    }
}