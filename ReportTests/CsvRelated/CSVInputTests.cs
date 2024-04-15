using Microsoft.VisualStudio.TestTools.UnitTesting;
using Report.CsvRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.CsvRelated.Tests
{
    [TestClass()]
    public class CSVInputTests
    {
        [TestMethod()]
        public void CSVInputTest()
        {
            CSVInput input = new CSVInput(@"C:\Users\junyu\Downloads\Current2_Adc_Range200_202403150110.csv");
            
        }
    }
}