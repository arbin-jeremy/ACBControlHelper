using Report;
using Report.CsvRelated;
using System.Text.RegularExpressions;

namespace ACBControlHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _Customer
        {
            get
            {
                return RichTextBox_Customer.Text.Replace("\v", "\n");
            }
        }

        private string _SN
        {
            get
            {
                return TextBox_SN.Text;
            }
        }

        private void ButtonCertificate_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> mainCsvFiles = SelectCsvs_Main.GetSelectedCsvFiles();
                List<string> verificationCsvFiles = SelectCsvs_Verfication.GetSelectedCsvFiles();

                API.GenerateCertificate(mainCsvFiles, verificationCsvFiles, _Customer, _SN);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FillTextBoxWithSN(string sn)
        {
            TextBox_SN.Text = sn;
        }


        private void HandleEvent(object sender, MyEventArgs e)
        {
            TextBox_SN.Text = e.Message;
        }

        private bool ContainsSixConsecutiveDigits(string str)
        {
            return Regex.IsMatch(str, @"\d{6}");
        }





        private void Form1_Load(object sender, EventArgs e)
        {
            SelectCsvs_Main.FolderPathChanged += HandleEvent;
        }

        public const string arbin_production = "Arbin Production";
        public const string arbin_stock = "Arbin Stock";

        private void TextBox_SN_TextChanged(object sender, EventArgs e)
        {
            string sn = TextBox_SN.Text;
            if (sn.Length > 5 && sn.Contains("ACB"))
            {
                // RichTextBox_Customer.Text = arbin_production;
            }
            else if (ContainsSixConsecutiveDigits(sn))
            {
                // RichTextBox_Customer.Text = "";
            }
            else
            {
                // RichTextBox_Customer.Text = arbin_stock;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            API.word = wordCheckBox.Checked;
        }
    }
}