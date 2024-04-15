using Calibration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Report.Models
{
    public class DataPoint
    {

    }

    public class ProductionDataPoint:DataPoint
    {
        public double AccurateValue;//Also Nominal Value
        public double ACBValue;
        public double Error;//Can be calculated
        public double Range;
        public double LowerLimit => AccurateValue - 2 * Range * Numbers.TOLERANCE_IN_PPM / 1000000;
        public double HigherLimit => AccurateValue + 2 * Range * Numbers.TOLERANCE_IN_PPM / 1000000;
    }

    public class CertificateDataSet
    {
        public string ParameterTested;//Voltage/Current
        public string Range; // with unit
        public List<CertificateDataPoint> Points=new List<CertificateDataPoint>(); 
    }

    public class CertificateDataPoint : DataPoint
    {

        public double Nominal;
        public double LowerLimit;
        public double? AsFound;
        public double? AsLeft;
        public double UpperLimit;
        public string Result
        {
            get
            {
                double? middleValue=AsFound!=null? AsFound: AsLeft;
                if (LowerLimit < middleValue && middleValue < UpperLimit)
                {
                    return "Passed";
                }
                return "Failed";
            }
        }
        public string Uncertainty;//with unit

    }

    public class RNDDataPoint : DataPoint
    {
        public DateTime Date_Time=DateTime.Now;
        public double Current_Adc;
        public double Voltage_Adc;
        public double Current_Meter;
        public double Voltage_Meter;
    }

}
