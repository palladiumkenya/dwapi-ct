using System.IO;
using System.Text;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using NUnit.Framework;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Shared.Tests
{
    [TestFixture]
    public class StreamExtensionsTests
    {
        private Facility _facility;
        [SetUp]
        public void SetUp()
        {
            _facility = Builder<Facility>.CreateNew().Build();
        }

        [Test]
        public void should_ReadToEnd()
        {
            var jsonFacility = JsonConvert.SerializeObject(_facility);
            
            var streamFacility = new MemoryStream(Encoding.Default.GetBytes(jsonFacility));

            var stringFaciliy = streamFacility.ReadToEnd();
            Assert.AreEqual(jsonFacility,stringFaciliy);
        }
        [Test]
        public void should_ReadFromJson_KnownType()
        {
            var jsonFacility = JsonConvert.SerializeObject(_facility);
            var streamFacility = new MemoryStream(Encoding.Default.GetBytes(jsonFacility));

            var faciliyFromStream = streamFacility.ReadFromJson<Facility>();

            Assert.That(faciliyFromStream, Is.TypeOf<Facility>());
            Assert.AreEqual(_facility.Code, faciliyFromStream.Code);
            Assert.AreEqual(_facility.Name, faciliyFromStream.Name);
            Assert.AreEqual(_facility.Id, faciliyFromStream.Id);
        }
        [Test]
        public void should_ReadFromJson_By_Type()
        {
            var jsonFacility = JsonConvert.SerializeObject(_facility);
            var streamFacility = new MemoryStream(Encoding.Default.GetBytes(jsonFacility));

            var type = Utility.GetMessageType(_facility.GetType());
            var faciliyFromStream = streamFacility.ReadFromJson(type);

            Assert.That(faciliyFromStream, Is.TypeOf<Facility>());
            
        }
    }
}