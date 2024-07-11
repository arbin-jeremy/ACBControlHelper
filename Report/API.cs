using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Ocsp;
using Report.CsvRelated;
using Report.ExcelRelated.ExcelBuilder;
using Report.Models;
using Report.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    public class API
    {
        public static bool word = false;

        public static void GenerateCertificate(string mainFolderPath,string asFoundFolderPath="",string sn="")
        {
            if (sn == "")
            {
                sn = GetSNFromPath(mainFolderPath);
            }
            List<string> main= GetAllCsvFilesContains(mainFolderPath);
            List<string> asFound = GetAllCsvFilesContains(asFoundFolderPath);
            GenerateCertificate(main, asFound,sn);
        }

        public static void GenerateCertificate(List<string> mainCsvPaths, List<string> verifCsvPaths,string customer="",string sn="")
        {
            string mainFolder = Path.GetDirectoryName(mainCsvPaths[0]);
 
            
            var mainCsvInputs= GetCsvInputDict(mainCsvPaths);
            var verifCsvInputs= GetCsvInputDict(verifCsvPaths);
            CertificateExcelReport certificate = new CertificateExcelReport();
            var main=GetSets(mainCsvInputs,false);
            var veri=GetSets(verifCsvInputs,true);
            certificate.Customer = customer;
            certificate.DataSetAsLeft.AddRange(main);//change later
            certificate.DataSetAsFound.AddRange(veri);
            certificate.SerialNumber = sn;
            certificate.DateOfCalibration = GetCalibrationDate(mainCsvInputs);
            certificate.Description = GetAllRanges(mainCsvInputs);
            string filePath = $"{Path.GetDirectoryName(mainCsvPaths[0])}\\Certificate_{sn}_{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.xlsx";
            certificate.SaveToExcel(filePath);
            if(word)
            {
                string filePathWord = $"{Path.GetDirectoryName(mainCsvPaths[0])}\\Certificate_{sn}_{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.docx";
                CertificateWordReport certificateWordReport = new CertificateWordReport(filePathWord, certificate);
                certificateWordReport.SaveToWord();
            }

            Helper.OpenFile(filePath);
        }

        private static List<CertificateDataSet> GetSets(Dictionary<string,CSVInput> CsvInputs,bool asFound)
        {
            var result = new List<CertificateDataSet>();
            foreach (var r in CsvInputs.Keys)
            {
                CSVInput input = CsvInputs[r];
                result.Add(input.GetDataSet(asFound));
            }
            return result;
        }

        private static string GetAllRanges(Dictionary<string, CSVInput> csvInputs)
        {
            StringBuilder voltage = new StringBuilder("Voltage Range\n ");
            StringBuilder current = new StringBuilder("\nCurrent Range ");
            foreach (var input in csvInputs)
            {
                var csvInput = input.Value;
                if (csvInput.RangeType == "Voltage")
                {
                    voltage.Append(csvInput.RangeString + "/");
                }
                else//current
                {
                    current.Append(csvInput.RangeString + "/");
                }
            }
            voltage.Remove(voltage.Length - 1, 1);
            current.Remove(current.Length - 1, 1);
            return voltage.Append(current).ToString();
        }


        private static string GetSNFromPath(string path)
        {
            return Helper.FindConsecutiveDigits(path);
        }

   

        private static Dictionary<string,CSVInput> GetCsvInputDict(List<string> csvPaths)
        {
            var result= new Dictionary<string,CSVInput>();   
            foreach (var csvPath in csvPaths)
            {
                var csvInput = new CSVInput(csvPath);
                result.Add(csvInput.RangeString, csvInput);
            }
            return result;
        }

        private static DateTime GetCalibrationDate(Dictionary<string,CSVInput> csvInputs)
        {
            DateTime latest= DateTime.MinValue;
            foreach(var input in csvInputs)
            {
                DateTime date = ExtractDate(input.Value.CalibrationTime);
                latest = GetLatestDateTime(latest, date);
            }
            return latest;
        }

        private static DateTime ExtractDate(string inputString)
        {
            DateTime dateTime;
            if (DateTime.TryParse(inputString, out dateTime))
            {
                // Extract only the date part
                return dateTime.Date;
            }
            else
            {
                throw new ArgumentException("Invalid date format.");
            }
        }

        private static DateTime GetLatestDateTime(DateTime dateTime1, DateTime dateTime2)
        {
            return DateTime.Compare(dateTime1, dateTime2) > 0 ? dateTime1 : dateTime2;
        }
        /// <summary>
        /// Retrieves a list of CSV files from the specified directory that contain a certain substring in their filenames.
        /// </summary>
        /// <param name="folderPath">The path to the directory to search within.</param>
        /// <param name="contains">The substring to look for in file names (default is "adc").</param>
        /// <returns>A list of file paths matching the criteria.</returns>
        public static List<string> GetAllCsvFilesContains(string folderPath, string contains = "adc")
        {
            if (folderPath == "")
            {
                return new List<string>();
            }
            List<string> result = new List<string>();
            try
            {
                if (Directory.Exists(folderPath))
                {
                    // Get all CSV files in the directory
                    string[] csvFiles = Directory.GetFiles(folderPath, "*.csv");

                    // Loop through each CSV file and check if it contains the specified substring
                    foreach (string file in csvFiles)
                    {
                        if (file.ToLower().Contains(contains.ToLower()))
                        {
                            result.Add(file);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return result;
        }
    }
}
