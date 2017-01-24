using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FizzWare.NBuilder;
using PalladiumDwh.Shared;

namespace PalladiumDwh.ClientReader.Core.Tests
{
    [TestFixture]
    public class PatientExtractRowTests
    {
        private List<PatientExtractRow> _list;
        PatientExtractRow _row;

        [SetUp]
        public void SetUp()
        {
            _list = Builder<PatientExtractRow>.CreateListOfSize(1).Build().ToList();
            _row = _list.First();
        }

        [Test]
        public void Should_Load()
        {
            var datatable = _list.ToDataTable();

            var reader = datatable.CreateDataReader();
            var extract=new PatientExtractRow();
            
            Assert.IsTrue(reader.Read());
            extract.Load(reader);

            Assert.AreEqual(_row.PatientCccNumber,extract.PatientCccNumber);
        }
    }
    
}