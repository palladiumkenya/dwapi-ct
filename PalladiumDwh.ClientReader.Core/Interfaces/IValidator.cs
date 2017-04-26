using System;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IValidator
    {
        Guid Id { get; set; }
        string Extract { get; set; }
        string Field { get; set; }
        string Type { get; set; }
        string Logic { get; set; }
        string Summary { get; set; }
        string GenerateValidateSql();
    }
}