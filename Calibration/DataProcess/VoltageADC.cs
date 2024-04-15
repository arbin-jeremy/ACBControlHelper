using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calibration.DataProcess
{
    public class VoltageADC : DataProcess
    {
        private double OldGain;
        private double OldOffset;
        public VoltageADC(List<Point> meterPoints, double acbRange, double oldGain,double oldOffset,double offsetX = 0, double offsetY = 0) : base(meterPoints, acbRange, offsetX, offsetY)
        {
            OldGain= oldGain;
            OldOffset= oldOffset;
        }



        protected override double GetGain()
        {
            double b = GetB();
            return OldGain * b;
        }
        protected override double GetOffset()
        {
            double b = GetB();
            return OldOffset + (_CalibrationYAverage - b * _CalibrationXAverage) * b;
        }
        public override double GetUncertainty()
        {
            return Numbers.GetVoltageUncertainty(AcbRange);
        }

        protected override void FillCalibrationPoints()
        {
            FillPoints();
        }

        protected override void FillVerificationPoints()
        {
            FillPoints();
        }


        private void FillPoints()
        {
            CalculationPoints = new List<Point>();
            foreach (var meterPoint in MeterPoints)
            {
                double x = meterPoint.X;
                double y = meterPoint.Y - OffsetY;
                CalculationPoints.Add(new Point(x, y));
            }
        }
    }
}
