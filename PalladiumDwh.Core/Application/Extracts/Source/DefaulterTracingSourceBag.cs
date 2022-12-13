using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class DefaulterTracingSourceBag : SourceBag<DefaulterTracingSourceDto>{
        public DefaulterTracingSourceBag()
        {
        }

        public List<DefaulterTracingSourceDto> _DefaulterTracingSourceDto { get; set; }
        public DefaulterTracingSourceBag(List<DefaulterTracingSourceDto> defaulterTracingSourceDto)
        {
            _DefaulterTracingSourceDto = defaulterTracingSourceDto;
        }
    }
}