using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Application.Extracts.Source;

namespace PalladiumDwh.Core.Tests.Application.Source
{
    [TestFixture]
    public class SourceBagTests
    {
        [TestCase(22222,11111)]
        public void Generate_TestData(int siteCode)
        {
            var patients = Builder<PatientSourceBag>.CreateNew()
                .With(x=>x.SiteCode=siteCode)
                .Build()
                ;
        }
    }
}
