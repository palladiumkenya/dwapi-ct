using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
        private IProgress<int> _progress;
        private string _exportPath;

        [SetUp]
        public void SetUp()
        {
            _exportPath = $@"{TestContext.CurrentContext.TestDirectory.HasToEndsWith(@"\")}DWapi\Exports\Extracts\";
            if (!Directory.Exists(_exportPath))
            {
                Directory.CreateDirectory(_exportPath);
            }
            _progress = new Progress<int>(ReportProgress);
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
            var saveFolder= _exportService.ExportToJSonAsync(_exportDir, _progress).Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(saveFolder));
            Console.WriteLine($"Exported to:{saveFolder}");

            var files = TestHelpers.GetExports($"{DateTime.Today:yyyyMMMdd}");
            Assert.IsNotEmpty(files);

            if(Directory.Exists(_exportPath))
                Directory.Delete(_exportPath,true);
                
            foreach (var f in files)
            {
                ZipFile.ExtractToDirectory(f,_exportPath);
            }

            Assert.IsTrue(File.Exists($"{_exportPath}dwapi.manifest"));
            var mainifest= File.ReadAllText($"{_exportPath}dwapi.manifest");
            Assert.IsTrue(mainifest.Length > 0);
            Console.WriteLine(mainifest);

            var patients = _clientPatientExtractRepository.GetAll();
            foreach (var p in patients)
            {
                Assert.IsTrue(File.Exists($"{_exportPath}{p.Id}.dwh"));
            }
            var selectedFile = Directory.GetFiles(_exportPath, "*.dwh", SearchOption.AllDirectories).FirstOrDefault();
            Console.WriteLine(selectedFile);
            var contents = File.ReadAllText(selectedFile);
            Console.WriteLine(contents);
           
        }

        [Test]
        public void should_Decode_Exported_Json_Extracts()
        {
            var saveFolder = _exportService.ExportToJSonAsync(_exportDir).Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(saveFolder));
            Console.WriteLine($"Exported to:{saveFolder}");

            var files = TestHelpers.GetExports($"{DateTime.Today:yyyyMMMdd}");
            Assert.IsNotEmpty(files);
            foreach (var f in files)
            {
                ZipFile.ExtractToDirectory(f, _exportPath);
            }

            files = Directory.GetFiles(_exportPath, "*.dwh", SearchOption.AllDirectories);

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

        private void ReportProgress(int value)
        {
            //Update the UI to reflect the progress value that is passed back.
            Console.WriteLine($"Exporting {value}%");
        }
    }
}