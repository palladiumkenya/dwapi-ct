using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Reports
{
    public interface ISummaryReport
    {
        void CreateExcelErrorSummary(IEnumerable<IExtractErrorSummary> summaries,string file="");
    }
}