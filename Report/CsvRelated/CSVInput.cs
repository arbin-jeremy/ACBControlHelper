using Calibration;
using Microsoft.Office.Interop.Excel;
using Report.Models;
using Report.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.CsvRelated
{
    public class CSVInput
    {
        private int _PointCount;
        private string[][] _Content;
        public string Range;
        public string RangeType;
        public string RangeUnit;
        public string CalibrationTime;
        public string RangeString
        {
            get
            {
                return $"{Range}{RangeUnit}";
            }
        }
        public List<string> ACBValues=new List<string>();
        public List<string> NominalValues=new List<string>();
        public List<string> LowerLimitValues=new List<string>();
        public List<string> UpperLimitValues=new List<string>();
        public CSVInput(string filePath)
        {
            _Content=Helper.ReadCsvFile(filePath);
            _PointCount = GetDataPointsCount();
            Range = _Content[1][1];
            RangeType = _Content[3][1];
            RangeUnit = _Content[4][1];
            CalibrationTime = _Content[0][1];
            GetACBValues();
            GetNominalValues();
            GetLowerLimitValues();
            GetUpperLimitValues();
        }

        private int GetDataPointsCount()
        {
            int y = 12;
            while (_Content[y][0] != "")
            {
                y++;
            }
            return y - 12;
        }

        private void GetACBValues()
        {
            GetValues(12, 1, ACBValues);
        }

        private void GetNominalValues() 
        {   
            GetValues(21,1, NominalValues);
        }

        private void GetLowerLimitValues()
        {
            GetValues(21, 2, LowerLimitValues);
        }

        private void GetUpperLimitValues()
        {
            GetValues(21, 3, UpperLimitValues);
        }

        private void GetValues(int row, int col, List<string> values)
        {
            for(int i=row;i<row+_PointCount;i++)
            {
                values.Add(_Content[i][col]);
            }
        }

        public CertificateDataSet GetDataSets(List<string> acbValues=null)
        {
            var result= new CertificateDataSet();
            result.ParameterTested = RangeType;
            result.Range = RangeString;
            for(int i = 0; i < _PointCount; i++)
            {
                CertificateDataPoint point = new CertificateDataPoint();
                point.UpperLimit =Double.Parse(UpperLimitValues[i]);
                point.LowerLimit = Double.Parse(LowerLimitValues[i]);
                point.Nominal = Double.Parse(NominalValues[i]);
                point.AsLeft = Double.Parse(ACBValues[i]);
                point.Uncertainty = Numbers.GetUncertaintiyString(RangeString);
                if (acbValues != null)
                {
                    point.AsFound = Double.Parse(acbValues[i]);
                }
                result.Points.Add(point);
            }

            return result;
        }

        public CertificateDataSet GetDataSets(CSVInput asFound)
        {
            return GetDataSets(asFound.ACBValues);
        }

    }
}
