using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calibration.DataProcess
{
    internal class Shunt : DataProcess
    {
        private double _CurrentComparatorMultiplier = 1.0;//changelater
        private double ShuntValue;
        private double ShuntOffset;

        public Shunt(List<Point> points,double acbRange, double offsetX=0, double offsetY=0) :base(points, acbRange,offsetX, offsetY)   
        {
            
        }

        protected override void FillCalibrationPoints()
        {
            CalculationPoints = new List<Point>();
            for (int i = 0; i < CalculationPoints.Count; i++)
            {
                double x = CalculationPoints[i].X - OffsetX;
                double y = (CalculationPoints[i].Y - OffsetY)*(_CurrentComparatorMultiplier/Numbers.STANDARD_RESISTOR);
                CalculationPoints.Add(new Point(x, y));
            }
        }

        protected override void FillVerificationPoints()
        {
            CalculationPoints = new List<Point>();
            foreach(Point point in MeterPoints)
            {
                double x = point.X / ShuntValue + ShuntOffset;
                double y = point.Y;
                CalculationPoints.Add(new Point(x, y));
            }
        }
        public void FillShuntValue()
        {
            double b = GetB();
            ShuntValue= 1 / b;
        }

        public void FillShuntOffset()
        {
            ShuntOffset = this.GetOffset();
        }

        protected override double GetOffset()
        {
            double b = GetB();
            double offset = _CalibrationYAverage - b * _CalibrationXAverage;
            return offset;
        }


        public override double GetUncertainty()
        {
            return Numbers.GetCurrentUncertainty(AcbRange);
        }

        protected override double GetGain()
        {
            return ShuntValue;
        }
    }
}
