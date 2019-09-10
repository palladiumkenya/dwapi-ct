using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Tests.Services
{
    [TestFixture]
    public class LiveSyncServiceTests
    {
        private ILiveSyncService _liveSyncService;
        private List<ManifestDto> _manifestDtos=new List<ManifestDto>();
        [SetUp]
        public void SetUp()
        {
            _liveSyncService=new LiveSyncService("http://localhost:4777/stages/");
            _manifestDtos.Add(new ManifestDto(10000,"Demo Fac 00",new FacilityManifest(10000,120)));
           // _manifestDtos.Add(new ManifestDto(10001, "Demo Fac 01", new FacilityManifest(10001, 1100)));
        }

        [Test]
        public void should_Sync_Manifest()
        {
            _manifestDtos.ForEach(m =>
            {
                var result = _liveSyncService.SyncManifest(m).Result;
                Assert.True(result.IsSuccess);
            });
        }
    }
}