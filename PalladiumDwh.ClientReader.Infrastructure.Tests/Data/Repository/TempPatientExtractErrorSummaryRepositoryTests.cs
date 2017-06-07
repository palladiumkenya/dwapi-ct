using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FizzWare.NBuilder;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;


namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Repository
{


    [TestFixture]
    public class TempPatientExtractErrorSummaryRepositoryTests

    {
        private DwapiRemoteContext _context;
        private List<TempPatientExtract> _tempPatientExtracts;
        private ITempPatientExtractErrorSummaryRepository _tempPatientExtractErrorSummaryRepository;

        private List<Core.Model.Validator> _validators;
        private List<ValidationError> _validationErrors;
        private string[] _ids;


        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _context.Database.ExecuteSqlCommand($@"DELETE FROM TempPatientExtract;DELETE FROM ValidationError;DELETE FROM Validator WHERE Type='TEST'");

            TestHelpers.CreateTestPatientWithValidations(_context);
            _tempPatientExtractErrorSummaryRepository =new TempPatientExtractErrorSummaryRepository(_context);
        }

        [Test]
        public void should_GetAll_Error_Summary()
        {
            var pagedlist = _tempPatientExtractErrorSummaryRepository.GetAll(1, 5);
            var extracts = pagedlist.ToList();
            Assert.That(extracts, Is.Not.Empty);

            foreach (var e in extracts)
            {
                Console.WriteLine(e);
            }
        }

        [TearDown]
        public void TearDown()
        {
            _context = new DwapiRemoteContext();
            _context.Database.ExecuteSqlCommand($@"DELETE FROM TempPatientExtract;DELETE FROM ValidationError;DELETE FROM Validator WHERE Type='TEST'");
            _context.Dispose();
            _context = null;
        }
    }
}
