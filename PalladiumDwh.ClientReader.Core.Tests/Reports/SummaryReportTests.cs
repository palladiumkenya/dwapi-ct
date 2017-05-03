using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Reports;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Core.Reports;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Tests.Reports
{
    [TestFixture]
    public class SummaryReportTests
    {
        private ISummaryReport _summaryReport;
        private string _excelFile;
        private string _excelFileBatch;
        [SetUp]
        public void SetUp()
        {
            _summaryReport = new SummaryReport();
            _excelFile = $"{Utility.GetFolderPath(TestContext.CurrentContext.TestDirectory)}ValidationErrors.xlsx";
            _excelFileBatch = $"{Utility.GetFolderPath(TestContext.CurrentContext.TestDirectory)}ValidationErrorsBatch.xlsx";
        }

        [Test]
        public void should_create_ExcelErrorSummary()
        {
            var patientExtractsErrors = Builder<TempPatientExtractErrorSummary>.CreateListOfSize(10).Build().ToList();
            var artExtractsErrors = Builder<TempPatientArtExtractErrorSummary>.CreateListOfSize(10).Build().ToList();
            var summaryFile=_summaryReport.CreateExcelErrorSummary(patientExtractsErrors, "PatientExtract", _excelFile);
            var file = TestHelpers.GetExcel("ValidationErrors");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(summaryFile));
            Assert.IsTrue(File.Exists(summaryFile));
            Console.WriteLine(summaryFile);
        }
        [Test]
        public void should_create_ExcelErrorSummary_From_Batch()
        {
            var patientExtractsErrors = Builder<TempPatientExtractErrorSummary>.CreateListOfSize(10).Build().ToList();
            var artExtractsErrors = Builder<TempPatientArtExtractErrorSummary>.CreateListOfSize(10).Build().ToList();
            var list = new List<IEnumerable<IExtractErrorSummary>>
            {
                patientExtractsErrors,artExtractsErrors
            };

            _summaryReport.CreateExcelErrorSummaryBatch(list, _excelFileBatch);
            var file = TestHelpers.GetExcel("ValidationErrorsBatch");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(file));
            Console.WriteLine(file);
        }
    }
}