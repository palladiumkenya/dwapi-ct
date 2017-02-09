using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Model
{
    public class EmrViewModel
    {
        public string Project { get; set; }
        public string EMR { get; set; }
        public string Version { get; set; }
        public bool IsDefault { get; set; }
        public Guid Id { get; set; }
        public EmrViewModel()
        {
        }

        public EmrViewModel(Project project, EMR emr)
        {
            Project = project.Name;
            EMR = emr.Name;
            Version = emr.Version;
            IsDefault = emr.IsDefault;
            Id = emr.Id;
        }

        public EMR GetEmr()
        {
            return new EMR();
        }
        public static List<EmrViewModel> CreateList(Project project)
        {
            var list = new List<EmrViewModel>();

            foreach (var emr in project.Emrs)
            {
                list.Add(new EmrViewModel(project,emr));
            }
            
            return list;
        }

       
    }
}