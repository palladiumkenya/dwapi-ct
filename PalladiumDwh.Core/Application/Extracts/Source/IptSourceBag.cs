using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class IptSourceBag : SourceBag<IptSourceDto>{
        public IptSourceBag()
        {
        }

        public List<IptSourceDto> _IptSourceDto { get; set; }
        public IptSourceBag(List<IptSourceDto> iptSourceDto)
        {
            _IptSourceDto = iptSourceDto;
        }
    }
}