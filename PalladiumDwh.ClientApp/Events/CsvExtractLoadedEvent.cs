using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Events
{
    public class CsvExtractLoadedEvent: EventArgs
    {
        public List<ExtractSetting> Extracts { get; private set; }

        public CsvExtractLoadedEvent(List<ExtractSetting> extracts)
        {
            Extracts = extracts;
        }
    }
}