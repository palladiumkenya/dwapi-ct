using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model
{
    public interface IExtract
    {
        string Emr { get; set; }
        string Project { get; set; }
    }
}