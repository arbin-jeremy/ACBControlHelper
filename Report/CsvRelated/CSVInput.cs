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
        public List<string> ACBValues=new List<string>();
        public List<string> NominalValues=new List<string>();
        public List<string> LowerLimit=new List<string>();
        public List<string> UpperLimit=new List<string>();
        public CSVInput(string filePath)
        {
            _Content=Helper.ReadCsvFile(filePath);
            _PointCount = GetDataPointsCount();
            Range = _Content[1][1];
            RangeType = _Content[3][1];
            RangeUnit = _Content[4][1];
        }

        private int GetDataPointsCount()
        {
            int y = 11;
            while (_Content[0][y] != "")
            {
                y++;
            }
            return y - 11;
        }

        private void GetACBValues()
        {
            for(int i = 12; i < 12 + _PointCount; i++)
            {
                ACBValues.Add(_Content[i][1]);
            }
        }

        private void GetNominalValues() 
        {   
            for(int i=21;i<21 + _PointCount;i++)
            {
                NominalValues.Add(_Content[i][1]);
            }
        }

        


    }
}
