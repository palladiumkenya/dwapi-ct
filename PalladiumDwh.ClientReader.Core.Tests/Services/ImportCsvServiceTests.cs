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
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Tests.Services
{
    [TestFixture]
    public class ImportCsvServiceTests
    {
      
        private IImportCsvService _importService;
        private string _extractsDir;
        private string _importPath;
        private IProgress<DProgress> _dprogress;
      

        [SetUp]
        public void SetUp()
        {
            _dprogress=new Progress<DProgress>(ReportDProgress);

            _extractsDir = $"{Utility.GetFolderPath(TestContext.CurrentContext.TestDirectory)}Extracts\\";
            _importPath = $@"{TestContext.CurrentContext.TestDirectory.HasToEndsWith(@"\")}";

            _importService = new ImportCsvService();
        }

        
        [Test]
        public void should_Copy_Csvs_To_ImportFolder()
        {
            var files = Directory.GetFiles(_extractsDir, "*.csv").ToList();

            var imports = _importService.CopyCsvFilesAsync(files, _importPath, _dprogress).Result;

            Assert.IsNotEmpty(imports);
            Console.WriteLine($"Copied TO:{_importPath}");
            foreach (var i in imports)
            {
                Console.WriteLine(i);
            }
        }

        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}