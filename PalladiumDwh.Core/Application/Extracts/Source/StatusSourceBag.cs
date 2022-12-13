using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class StatusSourceBag : SourceBag<StatusSourceDto>{
        public StatusSourceBag()
        {
        }

        public List<StatusSourceDto> _StatusSourceDto { get; set; }
        public StatusSourceBag(List<StatusSourceDto> statusSourceDto)
        {
            _StatusSourceDto = statusSourceDto;
        }
    }
}