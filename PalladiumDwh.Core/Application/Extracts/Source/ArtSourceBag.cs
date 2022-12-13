using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class ArtSourceBag : SourceBag<ArtSourceDto>{
        public ArtSourceBag()
        {
        }

        public List<ArtSourceDto> _ArtSourceDto { get; set; }
        public ArtSourceBag(List<ArtSourceDto> artSourceDto)
        {
            _ArtSourceDto = artSourceDto;
        }
    }
}