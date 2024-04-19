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
    public partial class ChooseFile : UserControl
    {
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
                string fileName=Path.GetFileName(fullName);
                CheckedListBox_Files.Items.Add(fileName);
                CheckedListBox_Files.SetItemChecked(i++, true);
            }
        }

        public List<string> GetSelectedCsvFiles()
        {
            List<string> result = new();
            foreach(int index in CheckedListBox_Files.CheckedIndices)
            {
                string fileName = CheckedListBox_Files.Items[index].ToString();
                result.Add(Path.Combine(_FolderPath, fileName));    
            }
            return result;
        }
        private void Button_BrowseFolder_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a folder";

                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    TextBox_Path.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
