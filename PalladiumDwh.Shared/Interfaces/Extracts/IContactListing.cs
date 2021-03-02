using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IContactListingExtract : IExtract,IContactListing
    {
         Guid PatientId { get; set; }
    }
}
