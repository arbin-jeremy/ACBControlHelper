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
    public class RND_CSVTests
    {
        [TestMethod()]
        public void RND_CSVTest()
        {
            RND_CSV csv= new RND_CSV();
            for(int i = 0; i < 10; i++)
            {
                csv.AppendPoint(new Models.RNDDataPoint());
            }
        }


    }
}