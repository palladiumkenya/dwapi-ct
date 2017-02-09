using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PalladiumDwh.ClientReader.Core.Enums;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Model
{
    public class ExtractsViewModel
    {
        public string Extract { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }
        public Guid Id { get; set; }
        
        public ExtractsViewModel()
        {
            Status = "Not Loaded";
        }

        public ExtractsViewModel(ExtractSetting setting):this()
        {
            Extract = setting.Display;
            Id = setting.Id;
        }

        public EMR GetEmr()
        {
            return new EMR();
        }
        public static ExtractsViewModel Create(ExtractSetting setting)
        {
            return new ExtractsViewModel(setting);
        }
        internal static List<ExtractsViewModel> CreateList(List<ExtractSetting> extractSettings)
        {
            var list = new List<ExtractsViewModel>();

            foreach (var setting in extractSettings)
            {
                list.Add(new ExtractsViewModel(setting));
            }
            return list;
        }
     
        public void UpdateTotalCount(int total)
        {
            Total = total;
        }
        public void UpdateStatus(string status)
        {
            Status = status;
        }

      
    }
}