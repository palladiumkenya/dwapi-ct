using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared;

namespace PalladiumDwh.ClientReader.Core.Tests.Model
{
    [TestFixture]
    public class PatientBaselinesExtractRowTests
    {
        private List<PatientBaselinesExtractRow> _list;
        PatientBaselinesExtractRow _row;

        [SetUp]
        public void SetUp()
        {
            _list = Builder<PatientBaselinesExtractRow>.CreateListOfSize(1)
                .Build().ToList();
            _row = _list.First();
        }

        [Test]
        public void Should_Load()
        {
            var datatable = _list.ToDataTable();
            var reader = datatable.CreateDataReader();
            var extract=new PatientBaselinesExtractRow();
            
            Assert.IsTrue(reader.Read());
            extract.Load(reader);

            Assert.AreEqual(_row.PatientID,extract.PatientID);
            
        }
        [Test]
        public void Should_Handle_Nulls_On_Load()
        {
            
            var datatable = _list.ToDataTable();
            
            foreach (DataRow row in datatable.Rows)
            {
                row[nameof(PatientBaselinesExtractRow.bCD4Date)] = DBNull.Value;
            }
            datatable.AcceptChanges();

            var reader = datatable.CreateDataReader();
            var extract = new PatientBaselinesExtractRow();

            Assert.IsTrue(reader.Read());
            extract.Load(reader);

            Assert.AreEqual(1900, extract.bCD4Date.Year);
            Console.WriteLine($"{extract.bCD4Date:yyyy MMMM dd}");
        }
    }
    
}