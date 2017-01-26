using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model.DTO
{
    public interface IExtractDTO
    {
        
        string Emr { get; set; }
        string Project { get; set; }
    }
}