using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class OvcSourceBag : SourceBag<OvcSourceDto>{
        public OvcSourceBag()
        {
        }

        public List<OvcSourceDto> _OvcSourceDto { get; set; }
        public OvcSourceBag(List<OvcSourceDto> ovcSourceDto)
        {
            _OvcSourceDto = ovcSourceDto;
        }
    }
}