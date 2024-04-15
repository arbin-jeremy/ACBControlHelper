using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Util
{
    public class Helper
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void OpenFile(string filePath)
        {
            string argument = $"/select, \"{filePath}\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        public static void CreateDirectoryIfNotExist(string filePath)
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }
            }
            catch
            {
            }
        }

        public static string[][] ReadCsvFile(string filePath)
        {
            // List to hold row arrays
            List<string[]> rows = new List<string[]>();

            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Process each line
            foreach (string line in lines)
            {
                // Split the line into parts based on commas
                // This assumes that your CSV file does not have quoted fields that can contain commas.
                // If it does, consider using a more robust CSV parsing library
                string[] parts = line.Split(',');

                // Add the parts array to the rows list
                rows.Add(parts);
            }

            // Convert the list to a 2D array
            return rows.ToArray();
        }
    }
}
