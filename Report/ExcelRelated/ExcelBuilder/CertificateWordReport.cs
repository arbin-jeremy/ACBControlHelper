using Microsoft.Office.Interop.Word;
using Report.Models;
using System;

namespace Report.ExcelRelated.ExcelBuilder
{
    public class CertificateWordReport
    {
        public List<CertificateDataSet> certificateDataSet = new List<CertificateDataSet>();
        private static object oMissing = System.Reflection.Missing.Value;
        Application wordApp;
        private string outputPath;
        Document mainDocument;
        bool blankDocument;
        private int pageCnt = 0;
        private CertificateExcelReport report;
        
        private CertificateWordReport() { }
        public CertificateWordReport(string outputPath, CertificateExcelReport report)
        {
            wordApp = new Application();
            wordApp.Visible = false;
            this.outputPath = outputPath;
            mainDocument = wordApp.Documents.Add();
            mainDocument.SaveAs(outputPath);
            blankDocument = true;
            //tempFileList = new List<string>();
            this.report = report;
            Console.WriteLine(ExcelReport.SourceFileDirectory);
            Console.WriteLine(report.Customer);
            pageCnt = 0;
        }
        ~CertificateWordReport()
        {
        }
        public void SaveToWord()
        {
            Page1();
            Page2();
            Page3();
            if(report.DataSetAsFound.Count != 0)
            {
                certificateDataSet = report.DataSetAsFound;
                PageData();
            }
            if(report.DataSetAsLeft.Count != 0)
            {
                certificateDataSet = report.DataSetAsLeft;
                PageData();
            }
            mainDocument = wordApp.Documents.Open(outputPath);
            wordApp.Visible = false;
            FindAndReplaceContentBox(mainDocument, "Title", "Certificate of Calibration");
            FindAndReplaceContentBox(mainDocument, "Subject", "Arbin Instruments Calibration Laboratory");
            FindAndReplaceContentBox(mainDocument, "Certificate Number", report.CertificateNumber);
            mainDocument.Save();
            mainDocument.Close();
            wordApp.Quit();
        }

        public void AppendDocument(Document docToAppend)
        {
            mainDocument = wordApp.Documents.Open(outputPath);
            wordApp.Visible = false;
            if (!blankDocument)
            {

                Microsoft.Office.Interop.Word.WdStatistic stat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
                int totalPages = mainDocument.ComputeStatistics(stat, ref oMissing);
                if(totalPages == pageCnt)
                    mainDocument.Range(mainDocument.Content.End - 1).InsertBreak(WdBreakType.wdPageBreak);
            }
            else blankDocument = false;
            ++pageCnt;
            docToAppend.Content.Copy();
            mainDocument.Range(mainDocument.Content.End - 1).Paste();
            docToAppend.Close(SaveChanges: WdSaveOptions.wdDoNotSaveChanges);
            mainDocument.Save();
            mainDocument.Close();
        }
        public void PageData()
        {
            string templatePath = ExcelReport.SourceFileDirectory + @"\pageData.docx";
            //string templatePath = @"C:\Users\will.w\Downloads\pageData.docx";
            Document doc = wordApp.Documents.Open(templatePath, ReadOnly: true);
            wordApp.Visible = false;
            //wordApp.Visible = true;
            //doc.Activate();
            
            //assume there is only one table in the document
            if(doc.Tables.Count != 1)
            {
                Console.WriteLine("There can only be one table in the file");
            }
            Console.WriteLine(doc.Tables.Count);
            Table t = doc.Tables[1];
            int row = 0;
            int page = 1;
            foreach(CertificateDataSet set in certificateDataSet)
            {
                if (EnoughForAnotherDataSet(row, set))
                {
                    WriteDataSetToDoc(ref t, ref row, set);
                }
                else
                {
                    AppendDocument(doc);
                    //doc.Close(SaveChanges: WdSaveOptions.wdDoNotSaveChanges);
                    //doc.SaveAs(@"C:\Users\will.w\Downloads\pageData-"+page+".docx");
                    //SaveFile(doc, "pageData-" + page + ".docx");
                    doc = wordApp.Documents.Open(templatePath, ReadOnly: true);
                    wordApp.Visible = false;
                    //doc.Activate();
                    t = doc.Tables[1];
                    row = 0;
                    page++;
                    WriteDataSetToDoc(ref t, ref row, set);
                }
            }
            //doc.SaveAs(@"C:\Users\will.w\Downloads\pageData-"+page+".docx");
            //SaveFile(doc, "pageData-" + page + ".docx");
            AppendDocument(doc);
            //doc.Close();
        }
        /*
        public void MergeDocument()
        {
            Document mainDocument = wordApp.Documents.Add();
            for(int i = 0; i < tempFileList.Count; ++i)
            {
                //Document docToAppend = wordApp.Documents.Open(path + "pageData-" + i + ".docx", ReadOnly: true);
            wordApp.Visible = false;
                Document docToAppend = wordApp.Documents.Open(tempFileList[i]);
            wordApp.Visible = false;
                docToAppend.Content.Copy();
                mainDocument.Range(mainDocument.Content.End - 1).Paste();
                if(i != tempFileList.Count - 1)
                    mainDocument.Range(mainDocument.Content.End - 1).InsertBreak(WdBreakType.wdPageBreak);
                docToAppend.Close();
            }
            FindAndReplaceContentBox(mainDocument, "Title", "Certificate of Calibration");
            FindAndReplaceContentBox(mainDocument, "Subject", "Arbin Instruments Calibration Laboratory");
            mainDocument.SaveAs(directory + fileName);

        }
        */
        private void FormatBorder(Row row, int rn)
        {
            foreach(Cell cell in row.Cells)
            {
                cell.Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
                cell.Range.Borders[WdBorderType.wdBorderBottom].Color = WdColor.wdColorGray50;
                cell.Range.Borders[WdBorderType.wdBorderBottom].LineWidth = WdLineWidth.wdLineWidth050pt;
                if (rn != 0)
                {
                    cell.Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
                    cell.Range.Borders[WdBorderType.wdBorderTop].Color = WdColor.wdColorGray50;
                    cell.Range.Borders[WdBorderType.wdBorderTop].LineWidth = WdLineWidth.wdLineWidth050pt;
                }
            }
        }
        private int WriteDataSetToDoc(ref Table table, ref int row, CertificateDataSet set)
        {
            if(row != 0)
            {
                //if not at the top of the doc, then 
                table.Rows.Add(ref oMissing);
                //formatBorder(table.Rows[table.Rows.Count], row);
                ++row;
            }
            for(int i = 0; i < set.Points.Count; i++, row++)
            {
                table.Rows.Add(ref oMissing);
                if(row == 1)
                {
                    FormatBorder(table.Rows[table.Rows.Count], row);
                }
                Row r = table.Rows[table.Rows.Count];
                if(i == 0)
                {
                    r.Cells[1].Range.Text = set.ParameterTested;
                    r.Cells[1].Range.Font.Bold = 0;
                    r.Cells[2].Range.Text = set.Range;
                }
                //Nominal
                r.Cells[3].Range.Text = "" + set.Points[i].Nominal.ToString($"F8");
                //Lower Limit
                r.Cells[4].Range.Text = "" + set.Points[i].LowerLimit.ToString($"F8");
                //Upper Limit
                r.Cells[7].Range.Text = "" + set.Points[i].UpperLimit.ToString($"F8");
                //Pass/Fail
                r.Cells[8].Range.Text = set.Points[i].Result;
                //Uncertainty
                r.Cells[9].Range.Text = set.Points[i].Uncertainty;
                if(set.Points[i].AsFound != null) 
                {
                    r.Cells[5].Range.Text = "" + $"{set.Points[i].AsFound:F8}";
                }
                else
                {
                    r.Cells[5].Range.Text = "-";
                }
                if(set.Points[i].AsLeft != null) 
                { 
                    r.Cells[6].Range.Text = "" + $"{set.Points[i].AsLeft:F8}";
                }
                else
                {
                    r.Cells[6].Range.Text = "-";
                }
            }
            return row;
        }
        private bool EnoughForAnotherDataSet(int row, CertificateDataSet set)
        {
            //around 40 is the maximum amount of lines
            if (row != 0)
            {
                return set.Points.Count + row + 1 <= 40;
            }
            return set.Points.Count + row <= 40;
        }
        public void Page3()
        {
            string templatePath = ExcelReport.SourceFileDirectory + @"\page3.docx";
            //string templatePath = @"C:\Users\will.w\Downloads\page3.docx";
            Document doc = wordApp.Documents.Open(templatePath, ReadOnly: true);
            wordApp.Visible = false;

            //more operations here if needed

            //doc.SaveAs(@"C:\Users\will.w\Downloads\page3-mod.docx");
            //SaveFile(doc, "page3-mod.docx");
            AppendDocument(doc);
        }
        public void Page2()
        {
            string templatePath = ExcelReport.SourceFileDirectory + @"\page2.docx";
            //string templatePath = @"C:\Users\will.w\Downloads\page2.docx";
            Document doc = wordApp.Documents.Open(templatePath, ReadOnly: true);
            wordApp.Visible = false;

            FindAndReplaceContentBox(doc, "Calibration Engineer Title", report.CalibrationEngineerFull);
            FindAndReplaceContentBox(doc, "QC Approval Title", report.QcApprovalEngineerFull);

            //doc.SaveAs(@"C:\Users\will.w\Downloads\page2-mod.docx");
            //SaveFile(doc, "page2-mod.docx");
            AppendDocument(doc);
        }
        public void Page1()
        {
            string templatePath = ExcelReport.SourceFileDirectory + @"\page1.docx";
            //string templatePath = @"C:\Users\will.w\Downloads\page1.docx";
            Document doc = wordApp.Documents.Open(templatePath, ReadOnly: true);
            wordApp.Visible = false;
            //FindAndReplaceContentBox(doc, "Certificate Number", "Cert ###");
            FindAndReplaceContentBox(doc, "Model Number", report.ModelNumber);
            FindAndReplaceContentBox(doc, "Serial Number", report.SerialNumber);
            FindAndReplaceContentBox(doc, "Manufacturer", "Arbin Instruments");
            FindAndReplaceContentBox(doc, "Description", report.Description);
            FindAndReplaceContentBox(doc, "Date of Calibration", report.DateOfCalibration.ToString("MMMM dd, yyyy"));
            FindAndReplaceContentBox(doc, "Calibration Interval", report.CalibrationInterval);
            FindAndReplaceContentBox(doc, "Calibration Due Date", report.CalibrationDueDate.ToString("MMMM dd, yyyy"));
            //FindAndReplaceContentBox(doc, "Date Type", "???");
            //FindAndReplaceContentBox(doc, "Temperature/RH", "???");
            //FindAndReplaceContentBox(doc, "Procedure", "???");
            FindAndReplaceContentBox(doc, "Customer", report.Customer);
            //FindAndReplaceContentBox(doc, "Location of Calibration", "???");
            //doc.SaveAs(@"C:\Users\will.w\Downloads\page1-mod.docx");
            AppendDocument(doc);
        }
        /*
        private void SaveFile(Document doc, string name)
        {
            doc.SaveAs(directory + name);
            tempFileList.Add(directory + name);
        }
        */
        static int FindAndReplaceContentBox(Document doc, string boxName, string replaceText)
        {
            ContentControls ccList = doc.SelectContentControlsByTitle(boxName);
            if (ccList == null) return 0;
            int replaceCnt = 0;
            foreach(ContentControl ccl in ccList)
            {
                ccl.Range.Text = replaceText;
                ++replaceCnt;
            }
            return replaceCnt;
        }
    }
}
