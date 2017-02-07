using System;
using System.Collections.Generic;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class EMR
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<ExtractSetting> ExtractSettings { get; set; }=new List<ExtractSetting>();
        public Guid ProjectId { get; set; }

        public EMR()
        {
            Id = Guid.NewGuid();
        }

        public EMR(string name, string version, bool isDefault, Guid projectId):this()
        {
            Name = name;
            Version = version;
            IsDefault = isDefault;
            ProjectId = projectId;
        }

        public void AddExtractSetting(ExtractSetting setting)
        {
            setting.EmrId = Id;
            ExtractSettings.Add(setting);
        }
    }
}