using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class DepressionScreeningSourceBag : SourceBag<DepressionScreeningSourceDto>{
        public DepressionScreeningSourceBag()
        {
        }

        public List<DepressionScreeningSourceDto> _DepressionScreeningSourceDto { get; set; }
        public DepressionScreeningSourceBag(List<DepressionScreeningSourceDto> depressionScreeningSourceDto)
        {
            _DepressionScreeningSourceDto = depressionScreeningSourceDto;
        }
    }
}