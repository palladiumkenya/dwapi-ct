using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class CovidSourceBag : SourceBag<CovidSourceDto>{
        public CovidSourceBag()
        {
        }

        public List<CovidSourceDto> _CovidSourceDto { get; set; }
        public CovidSourceBag(List<CovidSourceDto> covidSourceDto)
        {
            _CovidSourceDto = covidSourceDto;
        }
    }
}