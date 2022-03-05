using System.Reflection;
using AutoMapper;
using FizzWare.NBuilder;
using log4net;
using NUnit.Framework;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Tests.Model.MapProfiles
{
    [TestFixture]
    public class CtProfileTests
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = TestInitializer.Container.GetInstance<IMapper>();
        }

        [Test]
        public void should_Map_PatientExtract()
        {
            var dto = Builder<PatientSourceDto>.CreateNew().Build();

            var extract= _mapper.Map<StagePatientExtract>(dto);

            Assert.AreEqual(dto.PatientPK,extract.PatientPID);
            Assert.AreEqual(dto.PatientID,extract.PatientCccNumber);
            Log.Debug($"{dto.PatientPK} > {extract.PatientPID}");
            Log.Debug($"{dto.PatientID} > {extract.PatientCccNumber}");
        }
    }
}
