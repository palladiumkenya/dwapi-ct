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
    public class ClientRepositoryTests
    {
        private DwapiRemoteContext _context;
        private Project _project;
        private IProjectRepository _repository;
        private IEMRRepository _emrRepository;

        [SetUp]
        public void SetUp()

        {
            
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);

            var projects = Builder<Project>.CreateListOfSize(1).Build() as List<Project>;
            TestHelpers.CreateTestData(_context, projects);
            _project = projects.First();
            var emrs = TestHelpers.GetEMRTestProjectData(_project, 2, 5);
            TestHelpers.CreateTestData(_context, emrs);

            _repository = new ProjectRepository(_context);
            _emrRepository = new EMRRepository(_context);
        }

        [Test]
        public void should_GetAll()
        {
            var projects = _repository.GetAll().ToList();
            Assert.IsTrue(projects.Count > 0);
            foreach (var project in projects)
            {
                Console.WriteLine(project);
                foreach (var e in project.Emrs)
                {
                    Console.WriteLine($" > {e}");
                    foreach (var es in e.ExtractSettings)
                    {
                        Console.WriteLine($"  >> {es}");
                    }
                }
            }
        }

        [Test]
        public void should_Insert()
        {
            var newProject = new Project {Code = "1", Name = "Maun Org"};

            _repository.Insert(newProject);
            _repository.CommitChanges();

            var project = _repository.GetAll().FirstOrDefault(x=>x.Id==newProject.Id);

            Assert.IsNotNull(project);
            Assert.AreEqual("1", project.Code);
            Assert.AreEqual("Maun Org", project.Name);

            Console.WriteLine(project);
        }


        [Test]
        public void should_Update()
        {
            _project.Name = "New Maun";
            _repository.Update(_project);
            _repository.CommitChanges();


            var project = _repository.GetAll().FirstOrDefault(x => x.Id == _project.Id);

            Assert.IsNotNull(project);
            Assert.AreEqual("New Maun", project.Name);
            Console.WriteLine(project);
        }

        [Test]
        public void should_Delete()
        {
        
            _repository.Delete(_project.Id);
            _repository.CommitChanges();

            var project = _repository.GetAll().FirstOrDefault(x => x.Id == _project.Id);

            Assert.IsNull(project);
        }

        /*
        [Test]
        public void should_Execute()
        {
            var facility = _projects.First();

            var _repository = new TestFacilityRepository(_context);
            _repository.Execute("delete from Facility");
            _repository.CommitChanges();

            var deletedfacility = _repository.Find(facility.Id);

            Assert.IsNull(deletedfacility);
        } 
        */

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _context = null;
        }
    }
}
