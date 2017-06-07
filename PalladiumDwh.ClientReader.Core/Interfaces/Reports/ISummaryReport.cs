using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Reports
{
    public interface ISummaryReport
    {
        string CreateExcelErrorSummary(IEnumerable<IExtractErrorSummary> summaries,string extract,string file="", IProgress<DProgress> progress = null);
        void CreateExcelErrorSummaryBatch(IEnumerable<IEnumerable<IExtractErrorSummary>> summariesBatch, string file = "");
    }
}