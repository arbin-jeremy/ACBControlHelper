using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calibration.DataProcess
{
    public class CurrentADC:DataProcess
    {
        private double CurrentComparatorMultiplier;
        private double OldGain;
        private double OldOffset;
        public CurrentADC(List<Point> meterPoints, double acbRange, double currentComparatorMultiplier,double oldGain, double oldOffset,double offsetX=0,double offsetY=0):base(meterPoints, acbRange,offsetX, offsetY)
        {
            CurrentComparatorMultiplier = currentComparatorMultiplier;
            OldGain = oldGain;
            OldOffset = oldOffset;
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
            return Numbers.GetCurrentUncertainty(AcbRange);
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
                double y = GetAccurateValue(meterPoint.Y, OffsetY, CurrentComparatorMultiplier);
                CalculationPoints.Add(new Point(x, y));
            }
        }

        private double GetAccurateValue( double currentMeterReading, double offset, double mi6011dRange)
        {
            if (AcbRange > 0.2f) //meter using DCV mode
            {
                return GetSpecialCaseAccurateValue(currentMeterReading, offset) * mi6011dRange * 10;
            }
            else if (AcbRange == 0.2f) //meter range extender in standby mode
            {
                return GetSpecialCaseAccurateValue(currentMeterReading, offset);
            }
            else //meter range extender in standby mode
            {
                return currentMeterReading - offset;
            }
        }

        private double GetSpecialCaseAccurateValue(double currentMeterReading, double offset)
        {
            return (currentMeterReading - offset) / Numbers.STANDARD_RESISTOR;
        }
    }
}
