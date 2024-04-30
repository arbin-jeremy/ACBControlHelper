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
                CheckedListBox_Files.SetItemChecked(i++, true);
            }
            //Parent.FillTextBoxWithSN(Helper.FindConsecutiveDigits(_FolderPath) + "-X");
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
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    TextBox_Path.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}