using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class DrugAlcoholScreeningSourceBag : SourceBag<DrugAlcoholScreeningSourceDto>{
        public DrugAlcoholScreeningSourceBag()
        {
        }

        public List<DrugAlcoholScreeningSourceDto> _DrugAlcoholScreeningSourceDto { get; set; }
        public DrugAlcoholScreeningSourceBag(List<DrugAlcoholScreeningSourceDto> drugAlcoholScreeningSourceDto)
        {
            _DrugAlcoholScreeningSourceDto = drugAlcoholScreeningSourceDto;
        }
    }
}