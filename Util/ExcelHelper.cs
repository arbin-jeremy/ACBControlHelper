//using System;
//using System.Collections.Generic;
//using System.Data.SqlTypes;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;
//using System.Text;
//using System.Threading.Tasks;
//using Exception = System.Exception;
//using System.Diagnostics;
//using System;
//using System.IO;

//namespace Report.ExcelRelated
//{
//    public class ExcelHelper
//    {
//        public static void SaveExcelFileToPdf(string excelFilePath, string pdfFilePath)
//        {
//            Excel.Application excelApp = new Excel.Application();
//            try
//            {
//                Excel.Application s = new();

//                //Excel.Workbook workbook = excelApp.wo.Open(excelFilePath);
//               // Excel.Workbook workbook = excelApp.ActivateMicrosoftApp;
//              //  workbook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, pdfFilePath);
//              //  workbook.Close(false);
//            }
//            finally
//            {
//                KillExcelProcess(excelApp);
//            }
//            Console.WriteLine("Excel file converted to PDF.");
//        }



//        [DllImport("user32.dll")]
//        private static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

//        public static void KillExcelProcess(Excel.Application excelApp)
//        {
//            try
//            {
//               //GetWindowThreadProcessId(excelApp.get_Hwnd(), out int id);
//                Process excelProcesses = Process.GetProcessById(id);
//                excelProcesses.Kill();
//            }
//            catch
//            {
//            }
//        }

//    }

//}