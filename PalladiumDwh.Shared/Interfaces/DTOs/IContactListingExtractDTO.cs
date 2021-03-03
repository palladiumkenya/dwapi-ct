using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IContactListingExtractDTO : IExtractDTO,IContactListing
    {
        Guid PatientId { get; set; }
    }
}