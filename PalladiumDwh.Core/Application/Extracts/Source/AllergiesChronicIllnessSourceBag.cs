using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class AllergiesChronicIllnessSourceBag : SourceBag<AllergiesChronicIllnessSourceDto>{
        public AllergiesChronicIllnessSourceBag()
        {
        }

        public List<AllergiesChronicIllnessSourceDto> _allergiesChronicIllnessSourceDto { get; set; }
        public AllergiesChronicIllnessSourceBag(List<AllergiesChronicIllnessSourceDto> allergiesChronicIllnessSourceDto)
        {
            _allergiesChronicIllnessSourceDto = allergiesChronicIllnessSourceDto;
        }
    }
}