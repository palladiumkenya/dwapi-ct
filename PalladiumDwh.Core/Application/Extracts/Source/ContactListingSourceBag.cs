using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class ContactListingSourceBag : SourceBag<ContactListingSourceDto>{
        public ContactListingSourceBag()
        {
        }

        public List<ContactListingSourceDto> _ContactListingSourceDto { get; set; }
        public ContactListingSourceBag(List<ContactListingSourceDto> contactListingSourceDto)
        {
            _ContactListingSourceDto = contactListingSourceDto;
        }
    }
}