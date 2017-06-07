using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Events
{
    public class ExtractImportedEvent: EventArgs
    {
        public List<string> Exports { get; }

        public ExtractImportedEvent(List<string> exports)
        {
            Exports = exports;
        }
    }
}