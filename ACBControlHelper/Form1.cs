using Report;
using Report.CsvRelated;

namespace ACBControlHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonCertificate_Click(object sender, EventArgs e)
        {
            List<string> mainCsvFiles=SelectCsvs_Main.GetSelectedCsvFiles();
            List<string> verificationCsvFiles=SelectCsvs_Verfication.GetSelectedCsvFiles();

            API.GenerateCertificate(mainCsvFiles, verificationCsvFiles);
        }
    }
}