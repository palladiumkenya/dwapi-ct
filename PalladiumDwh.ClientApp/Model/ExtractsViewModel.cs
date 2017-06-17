using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientApp.Model
{
    public class ExtractsViewModel
    {
        public string Extract { get; set; }
        public string ExtractName { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }
        public int Loaded { get; set; }
        public int Rejected { get; set; }
        public int Sent { get; set; }
        public int Queued { get; set; }
        public Guid Id { get; set; }
        
        public ExtractsViewModel()
        {
            Status = "Not Loaded";
        }

        private ExtractsViewModel(string status, int? total, int? loaded, int? rejected, int? sent, int? queued, Guid id)
        {
            Status = status;
            Total = total ?? 0;
            Loaded = loaded ?? 0;
            Rejected = rejected ?? 0;
            Sent = sent ?? 0;
            Queued = queued ?? 0;
            Id = id;
        }


        public ExtractsViewModel(ExtractSetting setting):this()
        {
            Extract = setting.Display;
            ExtractName = setting.Name;
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


        public static ExtractsViewModel CreateHistory(EventHistory eventHistory,Guid id)
        {
            /*
            
            Status = status;
            Total = total ?? 0;
            Loaded = loaded ?? 0;
            Rejected = rejected ?? 0;
            Sent = sent ?? 0;
            Queued = queued ?? 0;
            Id = id;

               MAX(FoundDate) AS FoundDate, 
               MAX(LoadDate) AS LoadDate, 
	           MAX(ImportDate) AS ImportDate, 
	           MAX(SendDate) AS SendDate
            */
            string status = "Not Loaded";
            if (eventHistory.FoundDate.HasValue)
            {
                status = $"Updated, {eventHistory.FoundDate.GetTiming()}";
            }
            if (eventHistory.LoadDate.HasValue)
            {
                status = $"Loaded, {eventHistory.LoadDate.GetTiming()}";
            }
            if (eventHistory.ImportDate.HasValue)
            {
                status = $"Saved, {eventHistory.ImportDate.GetTiming()}";
            }
            if (eventHistory.SendDate.HasValue)
            {
                status = $"Sent, {eventHistory.SendDate.GetTiming()}";
            }
            
            return new ExtractsViewModel(status, eventHistory.Found, eventHistory.Imported, eventHistory.Rejected, eventHistory.Sent, eventHistory.NotSent,id);
        }
    }
}