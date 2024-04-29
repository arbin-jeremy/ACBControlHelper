using OfficeOpenXml;
using Report.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using Report.Util;

namespace Report.ExcelRelated.ExcelBuilder
{
    public class ExcelReport
    {
        static ExcelReport()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        protected Dictionary<string, string> LocationValuePairs = new Dictionary<string, string>();

        public static string SourceFileDirectory = $@"{AppDomain.CurrentDomain.BaseDirectory}ExcelRelated\Templates";
        public virtual string SourceFileName { get; } 
        public virtual string TargetFileName { get; }
        public string SourceFilePath => $@"{SourceFileDirectory}\{SourceFileName}";

        public static string DestinationFileDirectory => $@"{AppDomain.CurrentDomain.BaseDirectory}Output";
        public string DestinationFileName => TargetFileName;
        public string DestinationFilePath => $@"{DestinationFileDirectory}\{DestinationFileName}";


       
        public void SaveToPdf(string pdfFilePath)
        {
            string temp = $"{AppDomain.CurrentDomain.BaseDirectory}temp.xlsx";
            int i = 1;
            while(IsFileBeingUsed(temp))
            {
                temp = $"{AppDomain.CurrentDomain.BaseDirectory}temp ({i}).xlsx";
                i++;
            }
            SaveToExcel(temp);
            //ExcelHelper.SaveExcelFileToPdf(temp, pdfFilePath);
            Helper.Log($"{pdfFilePath} is successfully generated!");
        }

        protected virtual void ReplaceWithData(ExcelWorkbook book)
        {
            
        }


        protected void FillWorksheet(ExcelWorksheet excelWorksheet)
        {
            foreach (string location in LocationValuePairs.Keys)
            {
                excelWorksheet.Cells[location].Value = LocationValuePairs[location];
            }
        }


        public bool SaveToExcel(string destinationFilePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(destinationFilePath))
                {
                    destinationFilePath = DestinationFilePath;
                }
                Directory.CreateDirectory(Path.GetDirectoryName(destinationFilePath));
                using (ExcelPackage excelPackage = new ExcelPackage(SourceFilePath))
                {
                    ReplaceWithData(excelPackage.Workbook);
                    try
                    {
                        excelPackage.SaveAs(destinationFilePath); //SaveAs is more compatiable (with the input string, and etc.)
                        Helper.Log($"{destinationFilePath} is successfully generated!");
                        return true;
                    }
                    catch (Exception e)
                    {
                        Helper.Log($"Save excel file to {destinationFilePath} Failed!");
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.Log($"Generate excel file from {SourceFileDirectory} Failed!");
                return false;
            }

        }

        private bool IsFileBeingUsed(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            try
            {
                File.OpenRead(filePath);
            }catch (Exception e)
            {
                return true;
            }
            return false;
        }


        protected void LoadEntryToDictionary(string location, object o)
        {
            if (LocationValuePairs.ContainsKey(location))
            {
                Helper.Log($"Dictionary already contains {location}");
                return;
            }
            string value = o != null ? o.ToString() : "N/A";
            if (o is DateTime)
            {
                value = FormatWithDaySuffix((DateTime)o);
            }
           
            LocationValuePairs.Add(location, value);
        }
        private static string FormatWithDaySuffix(DateTime date)
        {
            string formattedDate = date.ToString("MMMM d, yyyy");
            string dayWithSuffix = $"{date.Day}{GetDaySuffix(date.Day)}";
            var regex = new Regex(Regex.Escape(date.Day.ToString()));
            return regex.Replace(formattedDate, dayWithSuffix, 1);
        }

        private static string GetDaySuffix(int day)
        {
            if (day >= 11 && day <= 13)
            {
                return "th";
            }

            switch (day % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }


    }
}
