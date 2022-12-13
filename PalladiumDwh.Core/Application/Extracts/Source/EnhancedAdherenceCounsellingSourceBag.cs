using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class EnhancedAdherenceCounsellingSourceBag : SourceBag<EnhancedAdherenceCounsellingSourceDto>{
        public EnhancedAdherenceCounsellingSourceBag()
        {
        }

        public List<EnhancedAdherenceCounsellingSourceDto> _EnhancedAdherenceCounsellingSourceDto { get; set; }
        public EnhancedAdherenceCounsellingSourceBag(List<EnhancedAdherenceCounsellingSourceDto> enhancedAdherenceCounsellingSourceDto)
        {
            _EnhancedAdherenceCounsellingSourceDto = enhancedAdherenceCounsellingSourceDto;
        }
    }
}