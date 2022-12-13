using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class GbvScreeningSourceBag : SourceBag<GbvScreeningSourceDto>{
        public GbvScreeningSourceBag()
        {
        }

        public List<GbvScreeningSourceDto> _GbvScreeningSourceDto { get; set; }
        public GbvScreeningSourceBag(List<GbvScreeningSourceDto> gbvScreeningSourceDto)
        {
            _GbvScreeningSourceDto = gbvScreeningSourceDto;
        }
    }
}