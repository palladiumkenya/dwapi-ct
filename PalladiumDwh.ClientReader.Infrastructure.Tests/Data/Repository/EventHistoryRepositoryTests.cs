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
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Repository
{


    [TestFixture]
    public class EventHistoryRepositoryTests
    {
        private DwapiRemoteContext _context;
        private Project _project;
        private IProjectRepository _repository;
        private IEventHistoryRepository _eventHistoryRepository;
        private EventHistory _eventHistory;
        private EMR _emr;
        private ExtractSetting _extractSetting;
        private IEnumerable<EventHistory> _eventHistories;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);
            var projects = Builder<Project>.CreateListOfSize(1).Build() as List<Project>;
            TestHelpers.CreateTestData(_context, projects);
            var emrs = TestHelpers.GetEMRTestProjectData(projects.First(), 2, 5);
            _emr = emrs.FirstOrDefault(x => x.IsDefault);
            _extractSetting = _emr.ExtractSettings.First();
            TestHelpers.CreateTestData(_context, emrs);


            _eventHistory = new EventHistory
            {
                Display = "Patient Status",
                SiteCode = 990099,
                ExtractSettingId = _extractSetting.Id
            };

            _eventHistories = TestHelpers.GetTestEventHistories(_extractSetting.Id);
            //TestHelpers.CreateTestData(_context, _eventHistories);

            _eventHistoryRepository = new EventHistoryRepository(_context);
        }

        [Test]
        public void should_GetStats()
        {
            _eventHistories = TestHelpers.GetTestEventHistories(_extractSetting.Id);
            TestHelpers.CreateTestData(_context, _eventHistories);

            _eventHistoryRepository = new EventHistoryRepository(_context);

            var eventId = _extractSetting.Id;

            var eventH = _eventHistoryRepository.GetStats(eventId);

            Assert.IsNotNull(eventH);
        }

        [Test]
        public void should_Update_Found_GetStats()
        {
            var foundEvent = _eventHistories.First();
            foundEvent.FoundDate=DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Found, 50);
            _eventHistoryRepository.CommitChanges();

            _eventHistoryRepository=new EventHistoryRepository(_context);

            var eventH = _eventHistoryRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.Found == 50);
            Assert.IsTrue(eventH.FoundDate.HasValue);
            Console.WriteLine(eventH.FoundInfo());

        }
        [Test]
        public void should_Update_Loaded_GetStats()
        {
            var foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Found, 50);
            _eventHistoryRepository.CommitChanges();

            foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Loaded, 10);
            _eventHistoryRepository.CommitChanges();

            _eventHistoryRepository = new EventHistoryRepository(_context);

            var eventH = _eventHistoryRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.Loaded == 10);
            Assert.IsTrue(eventH.LoadDate.HasValue);
            Console.WriteLine(eventH.LoadInfo());

        }
        [Test]
        public void should_Update_Rejeced_GetStats()
        {
            var foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Found, 50);
            _eventHistoryRepository.CommitChanges();

            foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Loaded, 10);
            _eventHistoryRepository.CommitChanges();

            foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Rejected, 40);
            _eventHistoryRepository.CommitChanges();


            _eventHistoryRepository = new EventHistoryRepository(_context);

            var eventH = _eventHistoryRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.Rejected == 40);
            Console.WriteLine(eventH.RejectedInfo());
        }

        [Test]
        public void should_Update_Sent_GetStats()
        {
            var foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Found, 50);
            _eventHistoryRepository.CommitChanges();

            foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Loaded, 10);
            _eventHistoryRepository.CommitChanges();

            foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Sent, 10);
            _eventHistoryRepository.CommitChanges();

            _eventHistoryRepository = new EventHistoryRepository(_context);

            var eventH = _eventHistoryRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.Sent == 10);
            Assert.IsTrue(eventH.SendDate.HasValue);
            Console.WriteLine(eventH.SendInfo());
        }



        [Test]
        public void should_Update_NotSent_GetStats()
        {
            var foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Found, 50);
            _eventHistoryRepository.CommitChanges();

            foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.Sent, 5);
            _eventHistoryRepository.CommitChanges();

            foundEvent = _eventHistories.First();
            foundEvent.FoundDate = DateTime.Now;
            _eventHistoryRepository.UpdateStats(foundEvent, StatAction.NotSent, 5);
            _eventHistoryRepository.CommitChanges();

            _eventHistoryRepository = new EventHistoryRepository(_context);

            var eventH = _eventHistoryRepository.GetStats(foundEvent.ExtractSettingId);
            Assert.IsNotNull(eventH);
            Assert.IsTrue(eventH.NotSent == 5);
            
            Console.WriteLine(eventH.SendInfo());
        }
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _context = null;
        }
    }
}
