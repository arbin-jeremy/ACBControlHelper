using Report.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Calibration
{
    public struct PreCalibrationValues
    {
        public double ShuntValue=Numbers.NOT_AVAILABLE;
        public double ShuntOffset= Numbers.NOT_AVAILABLE;
        public double ADC_noHp_GAIN=Numbers.NOT_AVAILABLE;
        public double ADC_noHp_Offset= Numbers.NOT_AVAILABLE;

        public PreCalibrationValues()
        {
        }
    }

    public class Numbers
    {
        public const double STANDARD_RESISTOR = 0.99998898;//changelater put in a setting (or as an input parameter)
        public const double TOLERANCE_IN_PPM = 40;
        public const double NOT_AVAILABLE = double.MinValue;

        public static PreCalibrationValues GetCurrentPreCalValues(double acbRange)
        {
            switch(acbRange)
            {
                case 1000:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 0.001,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 1500,
                        ADC_noHp_Offset = 0
                    };
                case 600:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 0.000667,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 900,
                        ADC_noHp_Offset = 0
                    };
                case 200:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 0.005,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 125,
                        ADC_noHp_Offset = 0
                    };
                case 60:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 0.01667,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 37.5,
                        ADC_noHp_Offset = 0
                    };
                case 10:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 0.1,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 6.25,
                        ADC_noHp_Offset = 0
                    };
                case 1:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 1,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 0.612,
                        ADC_noHp_Offset = 0
                    };
                case 0.2:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 5,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 0.12,
                        ADC_noHp_Offset = 0
                    };
                case 0.02:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 50,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 0.012,
                        ADC_noHp_Offset = 0
                    };
                case 0.002:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 500,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 0.0012,
                        ADC_noHp_Offset = 0
                    };
                case 0.0001:
                    return new PreCalibrationValues
                    {
                        ShuntValue = 10000,
                        ShuntOffset = 0,
                        ADC_noHp_GAIN = 0.0000612,
                        ADC_noHp_Offset = 0
                    };
            }
            Helper.Log($"Current precal {acbRange}A is not vaild");
            return new PreCalibrationValues();
        }

        public static PreCalibrationValues GetVoltagePreCalValues(double acbRange)
        {
            switch (acbRange)
            {
                case 100:
                    return new PreCalibrationValues
                    {
                        ShuntValue = NOT_AVAILABLE,
                        ShuntOffset = NOT_AVAILABLE,
                        ADC_noHp_GAIN = 117,
                        ADC_noHp_Offset = 0
                    };
                case 10:
                    return new PreCalibrationValues
                    {
                        ShuntValue = NOT_AVAILABLE,
                        ShuntOffset = NOT_AVAILABLE,
                        ADC_noHp_GAIN = 9.9,
                        ADC_noHp_Offset = 0
                    };
            }
            Helper.Log($"Voltage precal {acbRange}V is not vaild");
            return new PreCalibrationValues();
        }

        /// <summary>
        /// Unit in A  NOT USED SINCE 6/4/2024
        /// </summary>
        /// <param name="acbRange"></param>
        /// <returns></returns>
        public static double GetCurrentUncertainty(double acbRange)
        {
            switch (acbRange)
            {
                case 0.0001:
                    return 8.39 / 1000 / 1000 / 1000;
                case 0.002:
                    return 60 / 1000 / 1000 / 1000;
                case 0.02:
                    return 16 / 1000 / 1000;
                case 0.2:
                    return 62 / 1000 / 1000;
                case 1:
                    return 62 / 1000 / 1000;
                case 10:
                    return 0.386/ 1000;
                case 60:
                    return 3.862 / 1000;
                case 200:
                    return 3.862 / 1000;
                case 600:
                    return NOT_AVAILABLE;
                case 1000:
                    return NOT_AVAILABLE;
            }
            Helper.Log($"ACB Range {acbRange}A is not available");//changelater
            return NOT_AVAILABLE;


        }

        /// <summary>
        /// Unit in V  NOT USED SINCE 6/4/2024
        /// </summary>
        /// <param name="acbRange"></param>
        public static double GetVoltageUncertainty(double acbRange)
        {
            switch (acbRange)
            {
                case 10:
                    return 151.5 / 1000 / 1000;
                case 100:
                    return 2.063 / 1000;
            }
            Helper.Log($"ACB Range {acbRange}V is not available");
            return NOT_AVAILABLE;
        }

        public static string GetUncertaintiyString(string input)
        {
            switch (input)
            {
                case "100V":
                    return "1.1mV";
                case "10V":
                case "5V":
                    return "76uV";
                case "200A":
                case "60A":
                    return "1.9mA";
                case "10A":
                case "5A":
                    return "0.19mA";
                case "1A":
                case "0.5A":
                case "0.4A":
                case "0.2A":
                    return "31uA";
                case "0.1A":
                case "0.05A":
                    return "8.2uA";
                case "0.02A":
                case "0.01A":
                    return "0.32uA";
                case "0.002A":
                case "0.001A":
                    return "30nA";
                case "0.0001A":
                    return "4.2nA";
            }
            return "N/A";
        }


        //not used since 6/4/2024
        public static string GetUncertaintiyStringOld(string input)
        {
            switch (input)
            {
                case "100V":
                    return "2.063mV";
                case "10V":
                    return "151.5uV";
                case "200A":
                    return "3.862mA";
                case "60A":
                    return "3.862mA";
                case "10A":
                    return "0.386mA";
                case "1A":
                    return "62uA";
                case "0.5A":
                    return "62uA";
                case "0.2A":
                    return "62uA";
                case "0.02A":
                    return "16uA";  

                case "0.002A":
                    return "60nA";
                case "0.001A":
                    return "60nA";
                case "0.0001A":
                    return "8.39nA";    
            }
            return "N/A";
        }
    }
}