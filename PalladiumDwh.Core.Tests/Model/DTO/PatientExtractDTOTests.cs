using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.Core.Tests.Model.DTO
{
    public class PatientExtractDTOTests
    {
        private PatientExtractDTO _patient;

        [SetUp]
        public void SetUp()
        {
            _patient = Builder<PatientExtractDTO>.CreateNew().Build();
        }

        [Test]
        public void should_Check_IsValid()
        {
            Assert.IsTrue(_patient.IsValid());

            _patient.PatientCccNumber = "     ";

            Assert.IsFalse(_patient.IsValid());
        }
    }
}
