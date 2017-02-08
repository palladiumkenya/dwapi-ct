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
        public string Destination { get; set; }
        public bool IsActive { get; set; }
        public Guid EmrId { get; set; }

        public ExtractSetting()
        {
            Id = Guid.NewGuid();
        }

        public ExtractSetting(string name, string extractCsv, string extractSql, string destination, bool isActive, Guid emrId):this()
        {
            Name = name;
            ExtractCsv = extractCsv;
            ExtractSql = extractSql;
            Destination = destination;
            IsActive = isActive;
            EmrId = emrId;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public void UpdateTo(ExtractSetting setting)
        {
            Name = setting.Name;
            ExtractCsv = setting.ExtractCsv;
            ExtractSql = setting.ExtractSql;
            Destination = setting.Destination;
            IsActive = setting.IsActive;
        }
    }
}