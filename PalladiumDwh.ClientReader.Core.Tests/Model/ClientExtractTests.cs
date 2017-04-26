using System;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Tests.Model
{
    [TestFixture]
    public class ClientExtractTests
    {
        private ClientPatientStatusExtract _tempPatientVisitExtract;
        private ClientPatientExtract _tempPatientExtract;

        [SetUp]
        public void SetUp()
        {
            _tempPatientVisitExtract = Builder<ClientPatientStatusExtract>.CreateNew().Build();
            _tempPatientExtract = Builder<ClientPatientExtract>.CreateNew().Build();
        }

        [Test]
        public void should_Generate_GetAddAction_SQL()
        {
            var sql = _tempPatientVisitExtract.GetAddAction("TempPatientStatusExtract");
            Assert.IsNotNull(sql);
            Console.WriteLine(sql);
        }

        [Test]
        public void should_Generate_PatientExtract_GetAddAction_SQL()
        {
            var sql2 = _tempPatientExtract.GetAddAction("TempPatientExtract");
            Assert.IsNotNull(sql2);
            Console.WriteLine(sql2);
        }
    }
}