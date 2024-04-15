using Report.Models;
using Report.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Report.CsvRelated
{
    public class RND_CSV
    {
        public string TargetFileName = "DataPoints.csv";
        public static string DestinationFileDirectory => $@"{AppDomain.CurrentDomain.BaseDirectory}Output";
        public string DestinationFilePath => $@"{DestinationFileDirectory}\{TargetFileName}";
        
        public RND_CSV()
        {
            if (!File.Exists(DestinationFilePath))
            {
                AppendHeaders();
                Helper.Log($"{DestinationFilePath} successfully generated.");
            }
        }


        public void AppendPoint(RNDDataPoint point)
        {
            List<string> values= new List<string>();   
            values.Add(point.Date_Time.ToString());
            values.Add(point.Current_Adc.ToString());
            values.Add(point.Voltage_Adc.ToString());
            values.Add(point.Current_Meter.ToString());
            values.Add(point.Voltage_Meter.ToString());
            AppendLine(values);
        }

        private void AppendHeaders()
        {
            List<string> headers = new List<string>();
            headers.Add("Date_Time");
            headers.Add("Current_Adc");
            headers.Add("Voltage_Adc");
            headers.Add("Current_Meter");
            headers.Add("Voltage_Meter");
            AppendLine(headers);
        }

        private void AppendLine(List<string> values)
        {
            string line = string.Join(",",values);
            try
            {
                File.AppendAllText(DestinationFilePath, $"{line}\n");
            }catch(Exception ex)
            {
                Helper.Log("CSV file being used when trying to append data.");
            }


        }

    }
}
