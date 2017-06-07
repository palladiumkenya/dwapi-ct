using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Tests.Model
{
    [TestFixture]
    public class PatientBaselinesExtractRowTests
    {
        private List<TempPatientBaselinesExtract> _list;
        TempPatientBaselinesExtract _row;

        [SetUp]
        public void SetUp()
        {
            _list = Builder<TempPatientBaselinesExtract>.CreateListOfSize(1)
                .Build().ToList();
            _row = _list.First();
        }

        [Test]
        public void Should_Load()
        {
            var datatable = _list.ToDataTable();
            var reader = datatable.CreateDataReader();
            var extract=new TempPatientBaselinesExtract();
            
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
                row[nameof(TempPatientBaselinesExtract.bCD4Date)] = DBNull.Value;
            }
            datatable.AcceptChanges();

            var reader = datatable.CreateDataReader();
            var extract = new TempPatientBaselinesExtract();

            Assert.IsTrue(reader.Read());
            extract.Load(reader);

            Assert.IsNull(extract.bCD4Date);
        }
    }
    
}