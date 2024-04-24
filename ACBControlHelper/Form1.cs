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
                return RichTextBox_Customer.Text.Replace("\v","\n");
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
            List<string> mainCsvFiles = SelectCsvs_Main.GetSelectedCsvFiles();
            List<string> verificationCsvFiles = SelectCsvs_Verfication.GetSelectedCsvFiles();
            
            API.GenerateCertificate(mainCsvFiles, verificationCsvFiles, _Customer, _SN);
        }
        
        public void FillTextBoxWithSN(string sn)
        {
            TextBox_SN.Text = sn;
        }
    }
}