using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Tests.Model
{
    [TestFixture]
    public class ValidatorTests
    {
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = Builder<Validator>.CreateNew()
                .With(x => x.Extract = "TempPatientExtract")
                .With(x => x.Field = "Gender")
                .With(x => x.Type = "Required")
                .With(x => x.Logic = "Gender IS NULL")
                .Build();
        }

        [Test]
        public void should_GenerateValidateSql()
        {
            var sql = _validator.GenerateValidateSql();
            Assert.IsNotNull(sql);
            Console.WriteLine(sql);
        }
    }
}