using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;
namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class PharmacySourceBag : SourceBag<PharmacySourceDto>{
        public PharmacySourceBag()
        {
        }

        public List<PharmacySourceDto> _PharmacySourceDto { get; set; }
        public PharmacySourceBag(List<PharmacySourceDto> pharmacySourceDto)
        {
            _PharmacySourceDto = pharmacySourceDto;
        }
    }
}