using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class AdverseEventSourceBag : SourceBag<AdverseEventSourceDto>{
        public AdverseEventSourceBag()
        {
        }

        public List<AdverseEventSourceDto> _AdverseEventSourceDto { get; set; }
        public AdverseEventSourceBag(List<AdverseEventSourceDto> adverseEventSourceDto)
        {
            _AdverseEventSourceDto = adverseEventSourceDto;
        }
    }
}
