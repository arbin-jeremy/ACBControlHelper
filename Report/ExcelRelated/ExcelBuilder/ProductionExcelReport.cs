using iText.Layout.Minmaxwidth;
using OfficeOpenXml;
using Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.ExcelRelated.ExcelBuilder
{
    public class ProductionExcelReport : ExcelReport
    {
        public override string SourceFileName => "Production.xlsx";

        public DateTime CalibrationDate;
        public double Range;
        public double RangeIndex;
        public string RangeType="Current";//Voltage or Current
        public char RangeUnit
        {
            get
            {
                return RangeType == "Voltage" ? 'V' : 'A';
            }
        }//
        public string CalibrationType="ADC";//ADC or Shunt
        public double MaxError;//
        public double MaxUncertainty;
        public double GainValue;
        public double OffsetValue;
        public double MeterOneOffset;//offset of voltage meter measured with short circuit
        public double MeterTwoOffset;//


        public List<ProductionDataPoint> DataPoints= new List<ProductionDataPoint>();

        protected void LoadAllDataToDictionary()
        {
            LoadEntryToDictionary("B1", CalibrationDate);
            LoadEntryToDictionary("B2", Range);
            LoadEntryToDictionary("B3", RangeIndex);
            LoadEntryToDictionary("B4", RangeType);
            LoadEntryToDictionary("B5", RangeUnit);
            LoadEntryToDictionary("B6", CalibrationType);
            LoadEntryToDictionary("B7", MaxError);
            LoadEntryToDictionary("B8", MaxUncertainty);
            LoadEntryToDictionary("B9", GainValue);
            LoadEntryToDictionary("B10", OffsetValue);
            LoadEntryToDictionary("D10", MeterOneOffset);
            LoadEntryToDictionary("F10", MeterTwoOffset);

            AddPointsToDictionary(DataPoints);

        }

        protected override void ReplaceWithData(ExcelWorkbook book)
        {
            var sheet = book.Worksheets[0];
            LoadAllDataToDictionary();
            FillWorksheet(sheet);
        }

        private void AddPointsToDictionary(List<ProductionDataPoint> points)
        {
            int row = 13;
            foreach(ProductionDataPoint point in points) {
                AddPointToDictionary(point, row);
                row++;
            }
        }

        private void AddPointToDictionary(ProductionDataPoint point,int row)
        {
            LoadEntryToDictionary($"A{row}", point.Range);
            LoadEntryToDictionary($"B{row}", point.AccurateValue);
            LoadEntryToDictionary($"C{row}", point.ACBValue);
            LoadEntryToDictionary($"D{row}", point.Error);
            LoadEntryToDictionary($"E{row}", point.LowerLimit);
            LoadEntryToDictionary($"F{row}", point.HigherLimit);
        }
    }
}
