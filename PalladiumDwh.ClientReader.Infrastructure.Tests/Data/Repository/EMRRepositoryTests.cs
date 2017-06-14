using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Enums;
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
        
        private EventHistory _eventHistory;
        private IEnumerable<EventHistory> _eventHistories;
        private DwapiRemoteContext _dbcontext;

        [SetUp]
        public void SetUp()
        {
            _dbcontext = new DwapiRemoteContext();
            _dbcontext.Database.ExecuteSqlCommand("DELETE FROM EventHistory");

            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);

            var projects = Builder<Project>.CreateListOfSize(1).Build() as List<Project>;
            TestHelpers.CreateTestData(_context, projects);
            var emrs = TestHelpers.GetEMRTestProjectData(projects.First(), 2, 5);
            _emr = emrs.FirstOrDefault(x=>x.IsDefault);
            _extractSetting = _emr.ExtractSettings.First();
            TestHelpers.CreateTestData(_context, emrs);
            _emrRepository = new EMRRepository(_context);


            _eventHistory = new EventHistory
            {
                Display = "Patient Status",
                SiteCode = 990099,
                ExtractSettingId = _extractSetting.Id
            };

            _eventHistories = TestHelpers.GetTestEventHistories(_extractSetting.Id);
        }

        [Test]
        public void should_SetDefaultEMR()
        {
            var emrs = _emrRepository.GetAll().ToList();
            var emrDefault = emrs.First(x => x.IsDefault);
            Assert.IsNotNull(emrDefault);

            var emrtoUpdate = emrs.First(x => x.IsDefault==false);
            _emrRepository.SetEmrAsDefault(emrtoUpdate.Id);
            _emrRepository.CommitChanges();

            _emrRepository=new EMRRepository(_context);
            var savedEmrs = _emrRepository.GetAll().ToList();

            var noDefualt = savedEmrs.Where(x => x.IsDefault).ToList().Count;
            var defaultEmr=savedEmrs.First(x => x.IsDefault);
            
            Assert.IsTrue(noDefualt==1);
            Assert.AreEqual(emrtoUpdate.Id,defaultEmr.Id);
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
        [Test]
        public void should_Update_Found_GetStats()
        {
            _extractSetting = _dbcontext.ExtractSettings.FirstOrDefault();
            var foundEvent = _eventHistories.First();
            foundEvent.ExtractSettingId = _extractSetting.Id;
            foundEvent.FoundDate = DateTime.Now;
            foundEvent.Found = 50;

            _emrRepository = new EMRRepository(_dbcontext);
            _emrRepository.CreateStats(foundEvent, StatAction.Found);
            
            var eventH = _emrRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.Found == 50);
            Assert.IsTrue(eventH.FoundDate.HasValue);
            Console.WriteLine(eventH.FoundInfo());
        }

        [Test]
        public void should_Update_Loaded_GetStats()
        {
            _extractSetting = _dbcontext.ExtractSettings.FirstOrDefault();
            var foundEvent = _eventHistories.First();
            foundEvent.ExtractSettingId = _extractSetting.Id;
            foundEvent.FoundDate = DateTime.Now;
            foundEvent.Found = 50;
            _emrRepository = new EMRRepository(_dbcontext);
            _emrRepository.CreateStats(foundEvent, StatAction.Found);

            foundEvent.LoadDate = DateTime.Now;
            _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Loaded, 10);
                       
            var eventH = _emrRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.Loaded == 10);
            Assert.IsTrue(eventH.LoadDate.HasValue);
            Console.WriteLine(eventH.LoadInfo());
        }

        [Test]
        public void should_Update_Rejected_GetStats()
        {
            _extractSetting = _dbcontext.ExtractSettings.FirstOrDefault();
            var foundEvent = _eventHistories.First();
            foundEvent.ExtractSettingId = _extractSetting.Id;
            foundEvent.FoundDate = DateTime.Now;
            foundEvent.Found = 50;
            _emrRepository = new EMRRepository(_dbcontext);
            _emrRepository.CreateStats(foundEvent, StatAction.Found);
            foundEvent.LoadDate = DateTime.Now;
            _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Loaded, 10);
            
            _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Rejected, 40);

            var eventH = _emrRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.Rejected == 40);
            Assert.IsTrue(eventH.Loaded == 10);
            Assert.IsTrue(eventH.LoadDate.HasValue);
            Console.WriteLine(eventH.RejectedInfo());
        }

        [Test]
        public void should_Update_Sent_GetStats()
        {
            _extractSetting = _dbcontext.ExtractSettings.FirstOrDefault();
            var foundEvent = _eventHistories.First();
            foundEvent.ExtractSettingId = _extractSetting.Id;
            foundEvent.FoundDate = DateTime.Now;
            foundEvent.Found = 50;
            _emrRepository = new EMRRepository(_dbcontext);
            _emrRepository.CreateStats(foundEvent, StatAction.Found);
            foundEvent.LoadDate = DateTime.Now;
            _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Loaded, 10);

            _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Sent, 4);

            var eventH = _emrRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.Sent == 4);
            Assert.IsTrue(eventH.Loaded == 10);
            Assert.IsTrue(eventH.LoadDate.HasValue);
            Console.WriteLine(eventH.SendInfo());
        }

        [Test]
        public void should_Update_NotSent_GetStats()
        {
            _extractSetting = _dbcontext.ExtractSettings.FirstOrDefault();
            var foundEvent = _eventHistories.First();
            foundEvent.ExtractSettingId = _extractSetting.Id;
            foundEvent.FoundDate = DateTime.Now;
            foundEvent.Found = 50;
            _emrRepository = new EMRRepository(_dbcontext);
            _emrRepository.CreateStats(foundEvent, StatAction.Found);
            foundEvent.LoadDate = DateTime.Now;
            _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Loaded, 10);
            _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Sent, 4);

            _emrRepository.UpdateStats(_extractSetting.Id, StatAction.NotSent, 1);

            var eventH = _emrRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.NotSent == 1);
            Assert.IsTrue(eventH.Sent == 4);
            Assert.IsTrue(eventH.Loaded == 10);
            Assert.IsTrue(eventH.LoadDate.HasValue);
            Console.WriteLine(eventH.NotSenTInfo());
        }

        [TearDown]
        public void TearDown()
        {
            _dbcontext = new DwapiRemoteContext();
            _dbcontext.Database.ExecuteSqlCommand("DELETE FROM EventHistory");
            _dbcontext.Dispose();
            _dbcontext = null;

            _context.Dispose();
            _context = null;
        }
    }
}
