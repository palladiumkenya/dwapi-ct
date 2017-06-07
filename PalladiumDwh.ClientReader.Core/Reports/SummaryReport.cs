using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PalladiumDwh.ClientReader.Core.Interfaces.Reports;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Reports
{
    public class SummaryReport:ISummaryReport
    {
        public string CreateExcelErrorSummary(IEnumerable<IExtractErrorSummary> summaries, string extract, string file = "",IProgress<DProgress> progress = null)
        {
            progress?.ReportStatus("Generating Summary...");
            
            string fileName = "";

            if (string.IsNullOrWhiteSpace(file))
            {
                var folderToSaveTo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderToSaveTo = folderToSaveTo.EndsWith("\\") ? folderToSaveTo : $"{folderToSaveTo}\\";
                var destination = $@"{folderToSaveTo}Dwapi\Summary\{extract}ValidationErrors.xlsx";
                fileName = destination;
            }
            else
            {
                fileName = file;
            }

            var folder = Path.GetDirectoryName(fileName);
            bool exists = Directory.Exists(folder);

            if (!exists)
                Directory.CreateDirectory(folder);

            using (SpreadsheetDocument document =
                SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                UInt32Value sheetId = 1;
               
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());


                Sheet sheet = new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = sheetId,
                    Name = $"{extract}"
                };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();



                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                // Constructing header
                Row row = new Row();
                summaries.First().AddHeader(row);
                
                // Insert the header row to the Sheet Data
                sheetData.AppendChild(row);

                var extractErrorSummaries = summaries as IList<IExtractErrorSummary> ?? summaries.ToList();
                var total = extractErrorSummaries.Count();
                int count = 0;

                // Inserting each employee
                foreach (var summary in extractErrorSummaries)
                {
                    row = new Row();

                    summary.AddRow(row);

                    sheetData.AppendChild(row);
                    count++;
                    progress?.ReportStatus("Generating Summary",count,total);
                }

                worksheetPart.Worksheet.Save();

            }

            return fileName;
        }

        public string CreateExcelErrorSummary<T>(IEnumerable<IExtractErrorSummary> summaries, string extract, string file = "", IProgress<DProgress> progress = null) where T : TempExtractErrorSummary
        {
            throw new NotImplementedException();
        }

        

        public void CreateExcelErrorSummaryBatch(IEnumerable<IEnumerable<IExtractErrorSummary>> summariesBatch, string file = "")
        {
            string fileName = "";

            if (string.IsNullOrWhiteSpace(file))
            {
                var folderToSaveTo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderToSaveTo = folderToSaveTo.EndsWith("\\") ? folderToSaveTo : $"{folderToSaveTo}\\";
                var destination = $@"{folderToSaveTo}Dwapi\Summary\ValidationErrors.xlsx";
                fileName = destination;
            }
            else
            {
                fileName = file;
            }

            using (SpreadsheetDocument document =
                SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                UInt32Value sheetId = 1;

                foreach (var summaries in summariesBatch)
                {

                    Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());


                    Sheet sheet = new Sheet()
                    {
                        Id = workbookPart.GetIdOfPart(worksheetPart),
                        SheetId = sheetId,
                        Name = $"sheet{sheetId}"
                    };

                    sheets.Append(sheet);

                    workbookPart.Workbook.Save();



                    SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                    // Constructing header
                    Row row = new Row();
                    /*
    FacilityId
    Id
    RecordId
                     */
                    row.Append(
                        ConstructCell("SiteCode", CellValues.String),
                        ConstructCell("PatientPK", CellValues.String),
                        ConstructCell("PatientID", CellValues.String),
                        ConstructCell("Type", CellValues.String),
                        ConstructCell("Field", CellValues.String),
                        ConstructCell("Summary", CellValues.String),
                        ConstructCell("DateGenerated", CellValues.String),
                        ConstructCell("RecordId", CellValues.String)
                    );

                    // Insert the header row to the Sheet Data
                    sheetData.AppendChild(row);

                    // Inserting each employee
                    foreach (var summary in summaries)
                    {
                        row = new Row();

                        row.Append(
                            ConstructCell(summary.SiteCode.ToString(), CellValues.Number),
                            ConstructCell(summary.PatientPK.ToString(), CellValues.String),
                            ConstructCell(summary.PatientID, CellValues.String),
                            ConstructCell(summary.Type, CellValues.String),
                            ConstructCell(summary.Field, CellValues.String),
                            ConstructCell(summary.Summary, CellValues.String),
                            ConstructCell(summary.DateGenerated.Value.ToString("ddMMMyyyy"), CellValues.Date),
                            ConstructCell(summary.RecordId.ToString(), CellValues.String)
                        );

                        sheetData.AppendChild(row);
                    }

                    
                    sheetId++;
                }
                worksheetPart.Worksheet.Save();
            }
        }

        private Cell ConstructCell(string value, CellValues dataType)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType)
            };
        }
    }
}