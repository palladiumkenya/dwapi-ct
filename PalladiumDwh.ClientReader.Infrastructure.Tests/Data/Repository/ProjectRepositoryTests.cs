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
    public class ProjectRepositoryTests
    {
        private DwapiRemoteContext _context;
        private Project _project;
        private IProjectRepository _projectRepository;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);
            var projects = Builder<Project>.CreateListOfSize(1).Build() as List<Project>;
            TestHelpers.CreateTestData(_context, projects);
            var emrs = TestHelpers.GetEMRTestProjectData(projects.First(), 2, 5);
            TestHelpers.CreateTestData(_context, emrs);
            _projectRepository = new ProjectRepository(_context);
        }

        [Test]
        public void should_GetActiveProject()
        {
            var project = _projectRepository.GetActiveProject();
            Assert.IsNotNull(project);
            Assert.IsNotNull(project.GetDefaultEmr());

            Console.WriteLine(project);
            foreach (var emr in project.Emrs)
            {
                Console.WriteLine($"> {emr} Default:{emr.IsDefault}");
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
