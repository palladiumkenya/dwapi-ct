using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Model
{
    public class EmrViewModel
    {
        public string ProjectName { get; set; }
        public EMR Emr { get; set; }

        public EmrViewModel()
        {
        }

        public EmrViewModel(Project project, EMR emr)
        {
            ProjectName = project.Name;
            Emr = emr;
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