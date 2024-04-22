using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using Report.Models;

namespace Report.ExcelRelated.ExcelBuilder
{
    public class CertificateExcelReport : ExcelReport
    {
        public override string SourceFileName => "Certificate.xlsx";

        //global
        public string CertificateNumber
        {
            get
            {
                string date = DateOfCalibration.ToString("MMddyyyy");
                return $"{SerialNumber}-{date}";
            }
        }

        public string CertificateNumberCellValue => $"Certificate Number: {CertificateNumber}";

        //page1
        public string ModelNumber="ACB";//e.g. OCA425554
        public string SerialNumber;//e.g. 215183-X2
        public string Description;

        public DateTime DateOfCalibration=DateTime.Today;
        public string CalibrationInterval="12 MONTHS";//e.g. 12MONTHS
        public DateTime CalibrationDueDate
        {
            get
            {
                return DateOfCalibration.AddYears(1);
            }
        }

        public string Customer;

        //page2
        public string CalibrationEngineerName="Junyu Lu";

        public string CalibrationEngineerFull
        {
            get
            {
                return $"{CalibrationEngineerName}, Calibration Engineer";
            }
        }

        public string QcAprovalEngineerName="Sarah Teague";

        public string QcApprovalEngineerFull
        {
            get
            {
                return $"{QcAprovalEngineerName}, QC Approval";
            }
        }

        //page3
        public string[] CalDates = new string[4]; //not used

        public string[] CalDueDates = new string[4];//not used

        //page4
        public bool IsAnnual;

        public List<CertificateDataSet> DataSetAnnual = new List<CertificateDataSet>();

        //page5
        public List<CertificateDataSet> DataSetInitial = new List<CertificateDataSet>();

        protected override void ReplaceWithData(ExcelWorkbook book)
        {
            LoadPage1Data(book.Worksheets[0]);
            LoadPage2Data(book.Worksheets[1]);
            LoadPage3Data(book.Worksheets[2]);
            if (IsAnnual)
            {
                LoadDatasets(DataSetAnnual, book);
            }
            LoadDatasets(DataSetInitial, book);
            book.Worksheets.Delete(3);
        }

        private ExcelWorksheet DuplicateResultPage(ExcelWorkbook book)
        {
            int index = book.Worksheets.Count;
            var newSheet=book.Worksheets.Add($"Page{index}", book.Worksheets[3]);
            UpdateFootNotePageNumber(newSheet,index);
            return newSheet;
        }

        private void UpdateFootNotePageNumber(ExcelWorksheet sheet,int pageNumber)
        {
            sheet.Cells["R89"].Value= $"Page{pageNumber}";

        }


        private void LoadPageGlobalDictionary()
        {
            LocationValuePairs = new Dictionary<string, string>();
            LoadEntryToDictionary("A6", CertificateNumberCellValue);
        }

        private void LoadPage1Data(ExcelWorksheet page1)
        {
            LoadPage1Dictionary();
            FillWorksheet(page1);
        }

        private void LoadPage1Dictionary()
        {
            LoadPageGlobalDictionary();
            LoadEntryToDictionary("F11", ModelNumber);
            LoadEntryToDictionary("F13", SerialNumber);
            LoadEntryToDictionary("F17", Description);
            LoadEntryToDictionary("F26", DateOfCalibration);
            LoadEntryToDictionary("F28", CalibrationInterval);
            LoadEntryToDictionary("F30", CalibrationDueDate);
            LoadEntryToDictionary("K13", Customer);
        }

        private void LoadPage2Data(ExcelWorksheet page2)
        {
            LoadPage2Dictionary();
            FillWorksheet(page2);
        }

        private void LoadPage2Dictionary()
        {
            LoadPageGlobalDictionary();
            LoadEntryToDictionary("A49", CalibrationEngineerFull);
            LoadEntryToDictionary("K49", QcApprovalEngineerFull);
        }

        private void LoadPage3Data(ExcelWorksheet page3)
        {
            LoadPage3Dictionary();
            FillWorksheet(page3);
        }

        private void LoadPage3Dictionary()
        {
            LoadPageGlobalDictionary();
            //for (int i = 0; i < 4; i++)
            //{
            //    LoadEntryToDictionary($"L{15 + i*2}", CalDates[i]);
            //    LoadEntryToDictionary($"N{15 + i*2}", CalDueDates[i]);
            //}
        }


        private void LoadDatasets(List<CertificateDataSet> datasets,ExcelWorkbook book)
        {
            var sheet = DuplicateResultPage(book);
            LoadPageGlobalDictionary();

            int row = 13;
            foreach (var set in datasets)
            {
                if (!EnoughForAnotherDataSet(row, set))
                {
                    FillWorksheet(sheet);
                    sheet = DuplicateResultPage(book);
                    LoadPageGlobalDictionary();
                    row = 13;
                }

                LoadDataSet(set, row);
                row += set.Points.Count*2 + 2;
                
            }
            FillWorksheet(sheet);

        }

        private bool EnoughForAnotherDataSet(int row,CertificateDataSet set)
        {
            return (set.Points.Count * 2 + row) <= 80;
        }

        private void LoadDataSet(CertificateDataSet set, int row)
        {
            LoadEntryToDictionary($"A{row}", set.ParameterTested);
            LoadEntryToDictionary($"C{row}", set.Range);
            int i = row;
            foreach (var point in set.Points)
            {
                LoadPoint(point, i);
                i+=2;
            }
        }

        private void LoadPoint(CertificateDataPoint point, int row)
        {
            LoadEntryToDictionary($"E{row}", FormatDoubleValueString(point.Nominal));
            LoadEntryToDictionary($"G{row}", FormatDoubleValueString(point.LowerLimit));
            LoadEntryToDictionary($"I{row}", FormatDoubleValueString(point.AsFound));
            LoadEntryToDictionary($"K{row}", FormatDoubleValueString(point.AsLeft));
            LoadEntryToDictionary($"M{row}", FormatDoubleValueString(point.UpperLimit));
            LoadEntryToDictionary($"O{row}", point.Result);
            LoadEntryToDictionary($"Q{row}", point.Uncertainty);
        }

        private string FormatDoubleValueString(double? value)
        {
            if (value == null)
            {
                return "-";
            }
            else
            {
                return ((double)value).ToString("F8");
            }
        }
    }
}