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
        private ITempPatientExtractRepository _tempPatientExtractRepository;

        private List<Core.Model.Validator> _validators;
        private List<ValidationError> _validationErrors;
        private string[] _ids;


        [SetUp]
        public void SetUp()
        {
            _validators = Builder<Core.Model.Validator>.CreateListOfSize(2).Build().ToList();

            _tempPatientExtracts = Builder<TempPatientExtract>.CreateListOfSize(5)
                .All()
                .With(x => x.CheckError = false)
                .Build()
                .ToList();

            var p1 = _tempPatientExtracts.First();
            p1.CheckError = true;
            var p2 = _tempPatientExtracts.Last();
            p2.CheckError = true;

            _validationErrors = Builder<ValidationError>.CreateListOfSize(2).Build().ToList();

            var v1 = _validationErrors.First();
            v1.ValidatorId = _validators.First().Id;
            v1.RecordId = p1.Id;
            var v2 = _validationErrors.Last();
            v2.ValidatorId = _validators.Last().Id;
            v2.RecordId = p2.Id;

            _context = new DwapiRemoteContext();
            _context.Database.ExecuteSqlCommand($@"DELETE FROM TempPatientExtract;DELETE FROM ValidationError");
            _ids = _validators.Select(x => $"'{x.Id}'").ToArray();
            _context.Database.ExecuteSqlCommand($@"DELETE FROM Validator WHERE Id IN({string.Join(",", _ids)})");

            
            TestHelpers.CreateTestData(_context,_validators);
            TestHelpers.CreateTestData(_context, _tempPatientExtracts);
            TestHelpers.CreateTestData(_context, _validationErrors);

            _tempPatientExtractRepository =new TempPatientExtractRepository(_context);

        }

        [Test]
        public void should_GetAll_Paged()
        {
            var pagedlist = _tempPatientExtractRepository.GetAll(1, 5);
            var extracts = pagedlist.ToList();
            Assert.That(extracts, Is.Not.Empty);
            Assert.That(extracts.Count, Is.EqualTo(5));

            Assert.That(pagedlist.PageNumber, Is.EqualTo(1));
            Assert.That(pagedlist.PageSize, Is.EqualTo(5));
            Assert.That(pagedlist.PageCount, Is.EqualTo(2));
            Assert.That(pagedlist.Count, Is.EqualTo(5));
            Assert.That(pagedlist.TotalItemCount, Is.EqualTo(10));
        }

        [TearDown]
        public void TearDown()
        {
            _context = new DwapiRemoteContext();
            _context.Database.ExecuteSqlCommand($@"DELETE FROM TempPatientExtract;DELETE FROM ValidationError");
            _ids = _validators.Select(x => $"'{x.Id}'").ToArray();
            _context.Database.ExecuteSqlCommand($@"DELETE FROM Validator WHERE Id IN({string.Join(",", _ids)})");
            _context.Dispose();
            _context = null;
        }
    }
}
