using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Crypto.Tls;
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

namespace Report.ExcelRelated.ExcelBuilder.Tests
{
    [TestClass()]
    public class ExcelReportTests
    {
        [TestMethod()]
        public void SaveTest()
        {
            ProductionExcelReport production = new ProductionExcelReport();
            for(int i=0;i<10; i++)
            {
                production.DataPoints.Add(new Models.ProductionDataPoint());
            }
            string targetFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}test3.xlsx";
            production.SaveToExcel(targetFilePath);
            Assert.IsTrue(File.Exists(targetFilePath));
        }


        [TestMethod()]
        public void SaveToExcelTest()
        {
            CSVInput input = new CSVInput(@"C:\Users\junyu\Downloads\Current2_Adc_Range200_202403150110.csv");
            CSVInput input2 = new CSVInput(@"C:\Users\junyu\Downloads\Current2_Adc_Range200_202403150110.csv");
            CertificateDataSet set= input.GetDataSet(true);
              CertificateExcelReport certificate = new CertificateExcelReport();
            List<CertificateDataSet> dataSets = new List<CertificateDataSet>();   
            List<CertificateDataPoint> points= new List<CertificateDataPoint>();
            for (int i = 0; i < 6; i++)
            {
                points.Add(GetRandomCertificateDataPoint());
            }
            for (int i = 0; i < 6; i++)
            {
                //CertificateDataSet set = new CertificateDataSet();
                //set.Points.AddRange(points);
                dataSets.Add(set);
            }
            certificate.DataSetAsLeft.AddRange(dataSets);
            certificate.DataSetAsFound.AddRange(dataSets);
            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}test2.xlsx";
            certificate.SaveToExcel(filePath);
            Helper.OpenFile(filePath);
        }

        Random Random = new Random();

        private CertificateDataPoint GetRandomCertificateDataPoint()
        {
            CertificateDataPoint certificate = new CertificateDataPoint();
            certificate.Nominal = Random.NextDouble();
            certificate.UpperLimit = Random.NextDouble();
            certificate.LowerLimit = Random.NextDouble();
            certificate.AsLeft = Random.NextDouble();
           
            return certificate;
        }
    }
}