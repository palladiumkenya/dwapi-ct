using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Extracts
{
    public interface IPatientStatusExtractRow:IExtractRow,IStatus
    {
        string FacilityName { get; set; }
    }
}