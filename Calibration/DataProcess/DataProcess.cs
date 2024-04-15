using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calibration.DataProcess
{
    public abstract class DataProcess
    {
        protected List<Point> MeterPoints;
        protected List<Point> CalculationPoints;
        protected double B=int.MinValue;
        protected double AcbRange;
        protected double OffsetX;
        protected double OffsetY;
        protected double _CalibrationXAverage => CalculationPoints.Average(p => p.X);
        protected double _CalibrationYAverage => CalculationPoints.Average(p => p.Y);
        protected DataProcess(List<Point> meterPoints,double acbRange, double offsetX=0,double offsetY = 0)
        {
            MeterPoints = meterPoints;
            AcbRange= acbRange; 
            OffsetX= offsetX;
            OffsetY= offsetY;  
        }

        protected abstract void FillCalibrationPoints();
        protected abstract void FillVerificationPoints();

        protected abstract double GetGain();
        protected abstract double GetOffset();
        public abstract double GetUncertainty();

        public void Calibration(out double gain, out double offset)
        {
            FillCalibrationPoints();
            gain = GetGain();
            offset = GetOffset();
        }

        public List<double> Verification()
        {
            List<double> result= new List<double>();
            FillVerificationPoints();
            foreach(Point point in CalculationPoints) 
            { 
                result.Add(GetError(point));
            }
            return result;
        }

        private double GetError(Point p)
        {
            return (p.Y - p.X) * 1000 * 1000 / 2 / AcbRange;
        }


        protected double GetB()
        {
            if (B == int.MinValue)
            {
                double correlation = GetCorrelation();
                double sumOfXSquares = GetSumOfXSquares();
                B = correlation / sumOfXSquares;
            }
            return B;
        }

        private double GetCorrelation()
        {
            double result = 0;
            double xSum=0;
            double ySum=0;
            foreach(Point point in CalculationPoints) 
            {
                double x = point.X;
                double y = point.Y;
                result += x * y;
                xSum += x;
                ySum += y;
            }
            result -= xSum * ySum / CalculationPoints.Count;
            return result;
        }

        private double GetSumOfXSquares()
        {
            double result = 0;
            double sum = 0;
            foreach(var  point in CalculationPoints)
            {
                double x = point.X;
                result += x * x;
                sum += x;
            }
            result-=sum*sum/CalculationPoints.Count;
            return result;
        }

        
    }
}
