using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Shared.Tests.Model
{
    [TestFixture]
    public class ManifestTests
    {
        private Manifest _manifest;

        [SetUp]
        public void SetUp()
        {
            _manifest = Builder<Manifest>.CreateNew().With(x=>x.PatientPKs=new List<int>(){1,2,4,5,6}).Build();
        }

        [Test]
        public void should_Get_Items()
        {
            var joined = _manifest.Items;
            Assert.False(string.IsNullOrWhiteSpace(joined));
            Console.WriteLine(joined);
        }

        [Test]
        public void should_Get_PersonExtracts()
        {
            var joined = _manifest.GetInitExtracts(Guid.NewGuid());
            Assert.False(string.IsNullOrWhiteSpace(joined));
            Console.WriteLine(joined);
        }

        [Test]
        public void should_Get_BatchPatientPKsJoined()
        {
            var joined = _manifest.GetBatchPatientPKsJoined(2);
            Assert.True(joined.Any());
            joined.ForEach(Console.WriteLine);
        }
    }
}
