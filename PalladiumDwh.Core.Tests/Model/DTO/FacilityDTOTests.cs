using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.Core.Tests.Model.DTO
{
    [TestFixture]
    public class FacilityDTOTests
    {
        private FacilityDTO _facility;

        [SetUp]
        public void SetUp()
        {
            _facility = Builder<FacilityDTO>.CreateNew().Build();
        }

        [Test]
        public void should_Check_IsValid()
        {
            Assert.IsTrue(_facility.IsValid());
            var facility=new FacilityDTO(0,"ss","","");
            Assert.IsFalse(facility.IsValid());
        }
    }
}
