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
    public class ImportServiceTests
    {
        private DwapiRemoteContext _context;
        private IClientPatientExtractRepository _clientPatientExtractRepository;
        private IImportService _importService;
        private List<ClientPatientExtract> _clientPatientExtracts;
        private string _exportDir;
        private IProgress<int> _progress;
        private string _importPath;

        [SetUp]
        public void SetUp()
        {
            _progress = new Progress<int>(ReportProgress);
            _exportDir = $"{Utility.GetFolderPath(TestContext.CurrentContext.TestDirectory)}Exports\\";
            _importPath = $@"{TestContext.CurrentContext.TestDirectory.HasToEndsWith(@"\")}";

            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);
            _clientPatientExtracts = Builder<ClientPatientExtract>.CreateListOfSize(3).Build().ToList();
            TestHelpers.CreateTestData(_context, _clientPatientExtracts);
            _clientPatientExtractRepository = new ClientPatientExtractRepository(_context);

            _importService = new ImportService(_clientPatientExtractRepository);
        }

        [Test]
        public void should_Extract_Exports_To_ImportFolder()
        {
            var files = Directory.GetFiles(_exportDir, "*.zip").ToList();

            var imports = _importService.ExtractExportsAsync(files, _importPath).Result;

            Assert.IsNotEmpty(imports);
            Console.WriteLine($"Extracted TO:{_importPath}");
            foreach (var i in imports)
            {
                Console.WriteLine(i);
                foreach (var p in i.Profiles)
                {
                    Console.WriteLine($" >.{p}");
                }
            }
        }

        /*
        [Test]
        public void should_Read_Exports()
        {
            var imports= _importService.ReadExportsAsync(_exportDir).Result;
            Assert.IsNotEmpty(imports);
            foreach (var i in imports)
            {
                Console.WriteLine(i);
            }
        }
        
        [Test]
        public void should_Decode_Imported_Json_Extracts()
        {
            var saveFolder = _ImportService.ImportToJSonAsync(_ImportDir).Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(saveFolder));
            Console.WriteLine(saveFolder);
            var files = TestHelpers.GetImports("-");
            var selectedFile = files.FirstOrDefault();
            Assert.IsNotNull(selectedFile);
            Console.WriteLine(selectedFile);

            var contents = File.ReadAllText(selectedFile);
            Console.WriteLine(new string('-',30));

            Console.WriteLine(contents);
            var decoded = _ImportService.Base64Decode(contents);

            Assert.IsTrue(decoded.ToLower().Contains("PatientPK".ToLower()));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(decoded);
        }
        */

        private void ReportProgress(int value)
        {
            //Update the UI to reflect the progress value that is passed back.
            Console.WriteLine($"Extracting files {value}%");
        }
    }
}