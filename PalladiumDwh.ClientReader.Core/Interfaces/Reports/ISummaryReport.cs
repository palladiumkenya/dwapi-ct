using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Reports
{
    public interface ISummaryReport
    {
        string CreateExcelErrorSummary(IEnumerable<IExtractErrorSummary> summaries,string extract,string file="");
        void CreateExcelErrorSummaryBatch(IEnumerable<IEnumerable<IExtractErrorSummary>> summariesBatch, string file = "");
    }
}