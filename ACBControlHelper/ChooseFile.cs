﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Report;
using Report.Util;
using System.Globalization;

namespace ACBControlHelper
{
    public class MyEventArgs : EventArgs
    {
        public string Message { get; }

        public MyEventArgs(string message)
        {
            Message = message;
        }
    }

    public partial class ChooseFile : UserControl
    {
        public delegate void EventHandler(object sender, MyEventArgs e);

        public event EventHandler FolderPathChanged;

        public void RaiseEvent(string message)
        {
            OnMyEvent(new MyEventArgs(message));
        }

        protected virtual void OnMyEvent(MyEventArgs e)
        {
            FolderPathChanged?.Invoke(this, e);
        }

        private string _FolderPath
        {
            get
            {
                return TextBox_Path.Text;
            }
        }

        public ChooseFile()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<string> fileList = API.GetAllCsvFilesContains(_FolderPath, "adc");
            CheckedListBox_Files.Items.Clear();
            int i = 0;
            foreach (var fullName in fileList)
            {
                string fileName = Path.GetFileName(fullName);
                CheckedListBox_Files.Items.Add(fileName);
                // CheckedListBox_Files.SetItemChecked(i++, true);
            }

            ArrangeCheckListBox();

            RaiseEvent(GetLastFolderName(_FolderPath));
            //Parent.FillTextBoxWithSN(Helper.FindConsecutiveDigits(_FolderPath) + "-X");
        }


        //Dominic
        //only check the boxes of the latest files 
        private void ArrangeCheckListBox()
        {
            Dictionary<string,string> latestFiles = new Dictionary<string, string>(); 
            foreach (var item in CheckedListBox_Files.Items)
            {
                if (item != null) 
                {
                    string fileName = item.ToString();
                    string range = ExtractTestSetup(fileName); //  extract range from the file name
                    DateTime fileDateTime = ExtractDateTime(fileName); // extract date/time from the file name

                    if (!latestFiles.ContainsKey(range) || fileDateTime > ExtractDateTime(latestFiles[range]))
                    {
                        latestFiles[range] = fileName;
                    }
                }
            }

            CheckboxLatestFiles(latestFiles); 
        }

        private void CheckboxLatestFiles(Dictionary<string, string> latestFiles)
        {
            foreach (var fileName in latestFiles.Values)
            {
                int index = CheckedListBox_Files.Items.IndexOf(fileName);
                if (index != -1)
                {
                    CheckedListBox_Files.SetItemChecked(index, true);
                }
            }
        }
        private string ExtractTestSetup(string filename)
        {
            string[] parts = filename.Split('_'); 
            return parts[0]; 
        }

        private DateTime ExtractDateTime(string filename)
        {
            string time_s = filename.Split('_').Last(); 
            time_s = time_s.Split('.').First(); 
            return DateTime.ParseExact(time_s, "yyyyMMddHHmm", CultureInfo.InvariantCulture); 
        }

        // 

        private static string GetLastFolderName(string path)
        {
            // Normalize the path
            string normalizedPath = Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            
            return GetSerialNumber(normalizedPath);
            
        }
        private static string GetSerialNumber(string folderPath)
        {
            string[] parts = folderPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar); 
            string[] filteredParts = parts.Where(part => 
            !part.Equals("Data", StringComparison.OrdinalIgnoreCase) &&
            !part.StartsWith("ACB", StringComparison.OrdinalIgnoreCase) 
            ).ToArray();

            return filteredParts.Last(); 
        }

        public List<string> GetSelectedCsvFiles()
        {
            List<string> result = new();
            foreach (int index in CheckedListBox_Files.CheckedIndices)
            {
                string fileName = CheckedListBox_Files.Items[index].ToString();
                result.Add(Path.Combine(_FolderPath, fileName));
            }
            return result;
        }

        private void Button_BrowseFolder_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.ValidateNames = false;
            //    openFileDialog.CheckFileExists = false;
            //    openFileDialog.CheckPathExists = true;
            //    openFileDialog.FileName = "Select Folder"; // This text will appear in the filename box to guide the user.
            //    openFileDialog.Filter = "All files (*.*)|*.*"; // This will show all files in the dialog.
            //    openFileDialog.Title = "Select a folder";

            //    DialogResult result = openFileDialog.ShowDialog();
            //    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
            //    {
            //        // Return the directory path
            //        TextBox_Path.Text = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            //    }
            //}

            // previsous code 
            /*using (var folderDialog = new FolderBrowserDialog())
            {
               DialogResult result = folderDialog.ShowDialog();
               if (result == DialogResult.OK)
               {
                   TextBox_Path.Text = folderDialog.SelectedPath;
               }
            }*/ 

            using(OpenFileDialog openFileDialog = new OpenFileDialog { 
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Select Folder"
            })
            {
                //openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    TextBox_Path.Text = Path.GetDirectoryName(openFileDialog.FileName);
                    // RichTextBox_Customer.Text = "GetSerialNumber(TextBox_Path.Text)"; 
                }

            }
        }
    }
}
