using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Repository
{


    [TestFixture]
    public class ClientExtractRepositoryTests
    {
        private DwapiRemoteContext _context;
        private List<ClientPatientExtract> _clientPatientExtracts;
        private IClientPatientExtractRepository _repository;

        [SetUp]
        public void SetUp()
        {           
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);
            _clientPatientExtracts = Builder<ClientPatientExtract>.CreateListOfSize(10).Build().ToList();
            TestHelpers.CreateTestData(_context, _clientPatientExtracts);

            _repository = new ClientPatientExtractRepository(_context);
        }

        [Test]
        public void should_GetAll_Paged()
        {
            var pagedlist = _repository.GetAll(1, 5);
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
            _context.Dispose();
            _context = null;
        }
    }
}
