using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Report;
using System.Globalization; 

namespace ACBControlHelper
{
    public partial class UserControl_chooseFile : UserControl
    {

        public delegate void EventHandler(object sender, MyEventArgs e);

        public event EventHandler FolderPathChanged;

        public UserControl_chooseFile()
        {
            InitializeComponent();
        }

        private void Button_BrowseFolder_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Select Folder"
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    TextBox_Path.Text = Path.GetDirectoryName(openFileDialog.FileName); 
                }

            }
        }

        private string _FolderPath
        {
            get
            {
                return TextBox_Path.Text;
            }
        }

        private void TextBox_Path_TextChanged(object sender, EventArgs e)
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
            
            //raise event to notify the parent form
            FolderPathChanged?.Invoke(this, new MyEventArgs(GetLastFolderName(_FolderPath)));

        }
        private void ArrangeCheckListBox()
        {
            Dictionary<string, string> latestFiles = new Dictionary<string, string>();
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
    }
}
