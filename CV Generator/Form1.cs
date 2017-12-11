using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CV_Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            //instal in NutGet Console https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.Winforms/140.340.80
            //
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.LocalReport.ReportPath = Path.GetFullPath(@"..\..\Templates\CV_template1.rdlc");
            
            //Set parameters
            List<ReportParameter> data = new List<ReportParameter>();
            data.Add(new ReportParameter("Name", txb_Name.Text.ToString()));
            data.Add(new ReportParameter("Surname", txb_Surname.Text.ToString()));
            data.Add(new ReportParameter("Phone", txb_Phone.Text.ToString()));

            ReportViewer1.LocalReport.SetParameters(data);
            
            //Save file as pdf
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = "CV";
            saveFileDialog1.Filter = "Pdf Files|*.pdf";
            saveFileDialog1.Title = "Save CV as PDF File";
            DialogResult result = saveFileDialog1.ShowDialog();//open dialog
            string fileName = saveFileDialog1.FileName;
            if (result == DialogResult.OK)
            {
                MemoryStream stream = new MemoryStream();
                byte[] fileBytes = ReportViewer1.LocalReport.Render("PDF");
                FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Write(fileBytes, 0, fileBytes.Length);
                fileStream.Flush();
                fileStream.Close();
                stream.Close();
                MessageBox.Show("You have first saved this PDF docuemnt as memory stream,\nthen write the memory stream in a file :\n" + 
                    fileName, "Spire.PdfViewer Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
    }
}
