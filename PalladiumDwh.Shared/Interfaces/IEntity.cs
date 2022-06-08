using System;

namespace PalladiumDwh.Shared.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
        bool Voided { get; set; }
        bool Processed { get; set; }
    }
}