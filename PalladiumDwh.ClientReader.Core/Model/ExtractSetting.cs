using System;
using System.ComponentModel.DataAnnotations;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class ExtractSetting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ExtractCsv { get; set; }
        [MaxLength(8000)]
        public string ExtractSql { get; set; }
        public Guid EmrId { get; set; }

        public ExtractSetting()
        {
            Id = Guid.NewGuid();
        }

        public ExtractSetting(string name, string extractCsv, string extractSql, Guid emrId):this()
        {
            Name = name;
            ExtractCsv = extractCsv;
            ExtractSql = extractSql;
            EmrId = emrId;
        }
    }
}