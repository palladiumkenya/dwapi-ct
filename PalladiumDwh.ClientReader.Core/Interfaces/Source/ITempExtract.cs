using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source
{
    public interface ITempExtract
    {
        Guid Id { get; set; }
        int? PatientPK { get; set; }
        string PatientID { get; set; }
        int? FacilityId { get; set; }
        int? SiteCode { get; set; }
        DateTime DateExtracted { get; set; }
        bool CheckError { get; set; }
        bool HasError { get; set; }
        void Load(IDataReader reader);
        void Load(IDataReader reader, PropertyInfo[] propertyInfos);
        Task LoadAsync(IDataReader reader);
        bool IsValid();
        string GetAddAction();
        
    }
}