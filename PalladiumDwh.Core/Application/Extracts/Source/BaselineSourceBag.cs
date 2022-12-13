using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class BaselineSourceBag : SourceBag<BaselineSourceDto>{
        public BaselineSourceBag()
        {
        }

        public List<BaselineSourceDto> _BaselineSourceDto { get; set; }
        public BaselineSourceBag(List<BaselineSourceDto> baselineSourceDto)
        {
            _BaselineSourceDto = baselineSourceDto;
        }
    }
}