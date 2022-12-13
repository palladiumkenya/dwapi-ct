using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class OtzSourceBag : SourceBag<OtzSourceDto>{
        public OtzSourceBag()
        {
        }

        public List<OtzSourceDto> _OtzSourceDto { get; set; }
        public OtzSourceBag(List<OtzSourceDto> otzSourceDto)
        {
            _OtzSourceDto = otzSourceDto;
        }
    }
}