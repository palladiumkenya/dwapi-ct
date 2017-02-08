using System;
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
    public class EMRRepositoryTests
    {
        private DwapiRemoteContext _context;
        private EMR _emr;
        private IEMRRepository _emrRepository;
        private ExtractSetting _extractSetting;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);

            var projects = Builder<Project>.CreateListOfSize(1).Build() as List<Project>;
            TestHelpers.CreateTestData(_context, projects);
            var emrs = TestHelpers.GetEMRTestProjectData(projects.First(), 2, 5);
            _emr = emrs.FirstOrDefault(x=>x.IsDefault);
            _extractSetting = _emr.ExtractSettings.First();
            TestHelpers.CreateTestData(_context, emrs);
            _emrRepository = new EMRRepository(_context);
        }

        [Test]
        public void should_GetDefaultEMR()
        {
            var emr = _emrRepository.GetDefault();
            Assert.IsNotNull(emr);
            Assert.AreEqual(_emr.Id,emr.Id);

            var extractSetting = _emr.GetActiveExtractSetting(_extractSetting.Destination);
            Assert.IsNotNull(extractSetting);

            Console.WriteLine(emr);
            foreach (var s in emr.ExtractSettings)
            {
                Console.WriteLine($"> {s}");
            }           
        }

      
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _context = null;
        }
    }
}
