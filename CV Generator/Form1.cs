using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Resources;
using System.Collections;
using System.Reflection;
using System.Globalization;

namespace CV_Generator
{
    public partial class Form1 : Form
    {
        CV cv;
        Dictionary<String, Color> listaKolorówLoguSystemu = new Dictionary<string, Color>();

        String str_klauzula = "Wyrażam zgodę na przetwarzanie moich danych osobowych dla potrzeb niezbędnych do realizacji procesu rekrutacji (zgodnie z Ustawą z dnia 29.08.1997 roku o Ochronie Danych Osobowych; tekst jednolity: Dz. U. 2016 r. poz. 922).";
        
        private String lang = "pl";

        public String Language
        {
            get { return lang; }
            set { lang = value; }
        }

        List<String> sex_option = new List<string> { "nieznana", "męska", "żenska" };
        List<String> listLanguages = new List<String> { "Angielski" ,"Niemiecki", "Francuski", "Hiszpański", "Chiński", "Rosyjski" };
        
        public Form1()
        {
            CreateNewData();

            InitializeComponent();
            lbl_logSys.Text = "";
            txb_ConfidentialityClause.Text = str_klauzula;

            //dodanie do comboboxa   
            ActuallyComboBox();

            //Image load
            pBx_photo.Image = ResizeImage(Properties.Resources.image_null, pBx_photo.Width, pBx_photo.Height);

            //Language Version
            ChangeLanguage(lang);//pl or en
        }

        public void ActuallyComboBox() {
            foreach (String sex_option_ in sex_option)
                cBx_sex.Items.Add(sex_option_);
        }

        public struct AllUseControls
        {
            public Control control;
            public ToolStripMenuItem item;
            public string resx;
        }

        public void ChangeLanguage(String lang) {

            Language = lang;

            ResourceManager rm = new ResourceManager("CV_Generator.Resources.lang_" + lang,
                Assembly.GetExecutingAssembly());
            //banał - wprowadzać tłumaczenia po polsku i angielsku + dać do kontrolek

            List<AllUseControls> c_tab = new List<AllUseControls>{
                { new AllUseControls { control = gBx_PersonalData, resx = "PersonalData" } },
                { new AllUseControls { control = tPg_PersonalData, resx = "PersonalData" } },
                { new AllUseControls { control = gBx_TeleaddressData, resx = "PersonalData" } },
                { new AllUseControls { control = label1, resx = "Name" } },
                { new AllUseControls { control = label2, resx = "Surname" } },
                { new AllUseControls { control = label24, resx = "Profession" } },
                { new AllUseControls { control = label25, resx = "Sex" } },
                { new AllUseControls { control = label36, resx = "Picture" } },
                { new AllUseControls { control = label18, resx = "ConfidentialityClause" } },
                { new AllUseControls { control = label19, resx = "Interests" } },
                { new AllUseControls { control = label28, resx = "Linkedin" } },
                { new AllUseControls { control = label29, resx = "Github" } },
                { new AllUseControls { control = label3, resx = "Phone" } },
                { new AllUseControls { control = label23, resx = "Email" } },
                { new AllUseControls { control = label22, resx = "Address" } },
                { new AllUseControls { control = label27, resx = "MaritalStatus" } },                
                { new AllUseControls { control = label26, resx = "Citizenship" } },
                { new AllUseControls { control = label20, resx = "PlaceOfBirth" } },
                { new AllUseControls { control = label21, resx = "DateOfBirth" } },
                { new AllUseControls { control = tPg_Education, resx = "Education" } },
                { new AllUseControls { control = tPg_Skills, resx = "Skills" } },
                { new AllUseControls { control = tPg_Languages, resx = "Languages" } },
                { new AllUseControls { control = tPg_ProfesionalExperience, resx = "ProfesionalExperience" } },
                { new AllUseControls { control = label31, resx = "Description" } },
                { new AllUseControls { control = label32, resx = "Knowledge" } },
                { new AllUseControls { control = label33, resx = "Language" } },
                { new AllUseControls { control = label34, resx = "Knowledge" } },
                { new AllUseControls { control = label35, resx = "Description" } },
                { new AllUseControls { control = label30, resx = "Name_" } },
                { new AllUseControls { control = btn_UpListExperiences, resx = "GoHigher" } },
                { new AllUseControls { control = btn_UpListEducations, resx = "GoHigher" } },
                { new AllUseControls { control = btn_UpListSkills, resx = "GoHigher" } },
                { new AllUseControls { control = btn_UpListLanguage, resx = "GoHigher" } },
                { new AllUseControls { control = btn_DownListExperiences, resx = "GoBelow" } },
                { new AllUseControls { control = btn_DownListEducations, resx = "GoBelow" } },
                { new AllUseControls { control = btn_DownListSkills, resx = "GoBelow" } },
                { new AllUseControls { control = btn_DownListLanguage, resx = "GoBelow" } },
                { new AllUseControls { control = btn_AddExperience, resx = "AddExperience" } },
                { new AllUseControls { control = btn_RemoveExperience, resx = "RemoveExperience" } },
                { new AllUseControls { control = btn_AddEducation, resx = "AddEducation" } },
                { new AllUseControls { control = btn_RemoveEducation, resx = "RemoveEducation" } },
                { new AllUseControls { control = btn_AddSkill, resx = "AddSkill" } },
                { new AllUseControls { control = btn_RemoveSkill, resx = "RemoveSkill" } },
                { new AllUseControls { control = btn_AddLanguage, resx = "AddLanguage" } },
                { new AllUseControls { control = btn_RemoveLanguage, resx = "RemoveLanguage" } },
                { new AllUseControls { control = label4, resx = "CompanyName" } },
                { new AllUseControls { control = label5, resx = "Position" } },
                { new AllUseControls { control = label6, resx = "Description" } },
                { new AllUseControls { control = label7, resx = "Period" } },
                { new AllUseControls { control = label8, resx = "From" } },
                { new AllUseControls { control = label9, resx = "To" } },
                { new AllUseControls { control = label10, resx = "UniversityName" } },
                { new AllUseControls { control = label11, resx = "Direction" } },
                { new AllUseControls { control = label12, resx = "Degree" } },
                { new AllUseControls { control = label13, resx = "Specialty" } },
                { new AllUseControls { control = label14, resx = "Description" } },
                { new AllUseControls { control = label15, resx = "Period" } },
                { new AllUseControls { control = label16, resx = "From" } },
                { new AllUseControls { control = label17, resx = "To" } },
                { new AllUseControls { item = openDataFromFileToolStripMenuItem, resx = "OpenDataFromFile" } },
                { new AllUseControls { item = saveDataToFileToolStripMenuItem, resx = "SaveDataToFile" } },
                { new AllUseControls { item = ClearStripMenuItem, resx = "ClearStrip" } },
                { new AllUseControls { item = generatePdfToolStripMenuItem, resx = "GeneratePdf" } },
                { new AllUseControls { item = settingsToolStripMenuItem, resx = "Settings" } },
                { new AllUseControls { item = authorToolStripMenuItem, resx = "Author" } },
                { new AllUseControls { item = exitToolStripMenuItem, resx = "Exit" } }

            }; //new ToolStripMenuItem().te


            foreach (AllUseControls c in c_tab) {
                if (c.control != null)
                {
                    c.control.Text = rm.GetString(c.resx);
                }
                else {
                    c.item.Text = rm.GetString(c.resx);
                }               
            }

            //Sex
            sex_option[0] = rm.GetString("Unknow");
            sex_option[1] = rm.GetString("Male");
            sex_option[2] = rm.GetString("Female");

            cBx_sex.Items.Clear();
            ActuallyComboBox();

            //Languages
            listLanguages[0] = rm.GetString("English");
            listLanguages[1] = rm.GetString("German");
            listLanguages[2] = rm.GetString("French");
            listLanguages[3] = rm.GetString("Spanish");
            listLanguages[4] = rm.GetString("Chinese");
            listLanguages[5] = rm.GetString("Russian");
            
           // cBx_Languages.Items.Clear();
            LoadLanguagesToComboBox();
        }
        
        private void LoadLanguagesToComboBox() {
            //dodanie języków
            List<String> tmp_lang = new List<string>();

            foreach (var lang__ in listLanguages) {
                tmp_lang.Add(lang__);
            }

            cBx_Languages.DataSource = tmp_lang;
        }

        private void CreateNewData()
        {
            //create cv object
            cv = new CV();

            //Set colors
            listaKolorówLoguSystemu.Add("sukces", Color.DarkOliveGreen);
            listaKolorówLoguSystemu.Add("fail", Color.OrangeRed);
            listaKolorówLoguSystemu.Add("info", Color.DodgerBlue);
            listaKolorówLoguSystemu.Add("warming", Color.Goldenrod);
            listaKolorówLoguSystemu.Add("error", Color.Crimson);
        }

        String _imageUrl = "";
        String[] UserColors = { "#ffffff" , "#0059b3", "#000a1a" };

        private void Wygeneruj_pdf()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            //instal in NutGet Console https://www.nuget.org/packages/Microsoft.ReportingServices.ReportViewerControl.Winforms/140.340.80
            //
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.LocalReport.ReportPath = Path.GetFullPath(@"..\..\Templates\CV_template1.rdlc");

            //Set Parameters
            List<ReportParameter> data = new List<ReportParameter>();
            data.Add(new ReportParameter("Name_Surname", txb_Name.Text.ToString() + " " + txb_Surname.Text.ToString()));
            data.Add(new ReportParameter("Sex", cBx_sex.SelectedItem.ToString()));

            List<Control> elem = new List<Control>(new Control[] {
                txb_ConfidentialityClause, txb_Interests, txb_Github, txb_Linkedin, txb_BirthdayPlace, dTP_BirthdayDate,
                txb_Phone, txb_Email, txb_Address, txb_MaritalStatus, txb_Citizienship, txb_LastWork});
            foreach (Control elem_var in elem) {
                data.Add(new ReportParameter(elem_var.Name.Substring(4), elem_var.Text.ToString()));
            }

            //Set Image
            if (_imageUrl != null && _imageUrl != "")
            { 
                data.Add(new ReportParameter("Image", new Uri(_imageUrl).AbsoluteUri.ToString()));
            }
            ReportViewer1.LocalReport.EnableExternalImages = true;

            //Set colors
            data.Add(new ReportParameter("Color_0", UserColors[0]));
            data.Add(new ReportParameter("Color_1", UserColors[1]));
            data.Add(new ReportParameter("Color_2", UserColors[2]));

            //Set DataSet   
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Experience", cv.Doświadczenie));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Education", cv.Edukacja));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Skills", cv.Umiejętności));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Language", cv.Języki));

            //możliwość załączenia zdjęcia, zmiany kolorów, czcionki

            //ustawić parametry
            //Dane - imię nazwisko, nazwa ostatniego stanowiska, dane kontaktowe (e-mail, telefon, linkedin, github, bitbucken=r, 
            //adres korespondencyjny, płeć, stan cywilny, data ur, miejsce ur, obywatelstwo

            //Doświadczenie - 1 pozycja = okres (od do) nazwa firmy, stanowisko, opis, 
            //Edukacja - 1 pozycja = okres (od do) nazwa uczelni, kierunek, stopień, specjalność, opis, 
            //Umiejętności - nazwa, poziom, krótki opis + przykładowy projekt
            //Języki obce - nazwa języka, poziom znajomości, krótki opis,
            //Zainteresowania opis
            //Kursy - okres (od do) nazwa kursu
            //na koniec klauzura

            //1. Zrobić generowanie PDF, szablon pdf            

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
                LogSystem("Zapisano plik pdf", listaKolorówLoguSystemu["sukces"]);
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        int width_field = 480;
        float spacing_line = 0.55f;

        public Image generateText(String desc, String title, String subtitle, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(desc, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            int width = (int)Math.Floor((decimal)textSize.Width/(width_field - 60) + 1);
            int height = (int)((textSize.Height + spacing_line) * (width))+1;
            width = width_field;
            //create a new image of the right size
            img = new Bitmap(width, height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            Rectangle rect = new Rectangle(0, 0, width_field, height);
            
            drawing.DrawRectangle(Pens.White, rect);
            //
            
            Rectangle rect1 = new Rectangle(10, 10, 130, 140);

            // Create a StringFormat object with the each line of text, and the block
            // of text centered on the page.
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.Word
            };
            //
            //  drawing.DrawString(desc, font, textBrush, rect, stringFormat);

            //
            List<List<string>> pack_words = new List<List<string>>();
            string[] words = desc.Split(' ');
            // Add a space to each word and get their lengths.
            List<float> word_width = new List<float>();
            float total_width = 0;
            int i_ = 0;
            int i;
            for (i = 0; i < words.Length; i++)
            {
                // See how wide this word is.
                SizeF size = drawing.MeasureString(words[i], font);
                word_width.Add(size.Width);
                total_width += word_width[word_width.Count-1];
                if (total_width >= (width_field - 60) )
                {
                    total_width = 0;
                    List<string> pack = new List<string>();
                    for (int j= i_; j<=i; j++)
                    {
                        pack.Add(words[j]);
                    }
                    i_ = i+1;
                    pack_words.Add(pack);
                   // count_words.Add(word_width.Count-1);
                    word_width = new List<float>();
                }
            }
            if (total_width < (width_field - 60))
            {
                total_width = 0;
                List<string> pack = new List<string>();
                for (int j = i_; j <= i; j++)
                {
                    if (words.Length == j) {
                        pack.Add("");
                    }
                    else {
                        pack.Add(words[j]);
                    }
                }
                pack_words.Add(pack);
                // count_words.Add(word_width.Count-1);
                word_width = new List<float>();
            }
            //podzielić desc na części
            DrawJustifiedLine(drawing, rect, font, textBrush, desc, pack_words);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        private void DrawJustifiedLine(Graphics gr, RectangleF rect, Font font, Brush brush, string text, List<List<string>> pack_words)
        {
            // Break the text into words.
            string[] words = text.Split(' ');
            int i_ = 0;
            string pack_ex = null;

            foreach (var pack in pack_words) {
                /*
                if (pack_ex != null)
                {                    
                    List<string> tmp_pack_ = new List<string>();
                    tmp_pack_.Add(pack_ex);
                    foreach (string p in pack)
                    {
                        tmp_pack_.Add(p);
                    }
                    string tmp_pack = tmp_pack_[tmp_pack_.Count-1];
                    tmp_pack_.RemoveAt(tmp_pack_.Count - 1);
                    pack = tmp_pack_;
                    for (var o=i_; o< pack_words.Count; o++)
                    {
                        pack_words[o] = 

                    }
                   

                   // pack = 

                    pack_ex = pack[pack.Count - 1];
                }*/

                // Add a space to each word and get their lengths.
                float[] word_width = new float[pack.Count];
                float total_width = 0;
                for (int i = 0; i < pack.Count; i++)
                {
                    // See how wide this word is.
                    SizeF size = gr.MeasureString(pack[i], font);
                    word_width[i] = size.Width;
                    total_width += word_width[i];
                }

                // Get the additional spacing between words.
                float extra_space = rect.Width - total_width;
                int num_spaces = pack.Count - 1;
                if (pack.Count > 1) extra_space /= num_spaces;
                
                if (extra_space > 12.20f)
                {
                    extra_space = 12.20f;
                }
                /*
                if (extra_space < 0.005f)//?
                {
                    //jedno słowo za dużo
                    pack_ex = pack[pack.Count - 1];
                    pack.RemoveAt(pack.Count - 1);
                    extra_space = rect.Width - total_width;
                    num_spaces = pack.Count - 1;
                    if (pack.Count > 1) extra_space /= num_spaces;
                }*/

                // Draw the words.
                float x = rect.Left;
                float y = rect.Top;
                for (int i = 0; i < pack.Count; i++)
                {
                    // Draw the word.
                    gr.DrawString(pack[i], font, brush, x, y + (i_*font.Height + spacing_line));

                    // Move right to draw the next word.
                    x += word_width[i] + extra_space;
                }
                i_++;
            }
        }

        private void btn_AddExperience_Click(object sender, EventArgs e)
        {
            cv.Doświadczenie.Add(new Experience());
            lBx_Experiences.Items.Add("Doświadczenie nr " + lBx_Experiences.Items.Count);
        }

        private void btn_RemoveExperience_Click(object sender, EventArgs e)
        {
            select_exp = null;
            int i = lBx_Experiences.SelectedIndex;

            if (i != -1)
            {
                cv.Doświadczenie.RemoveAt(lBx_Experiences.SelectedIndex);
                lBx_Experiences.Items.RemoveAt(i);

                txB_NameCompany.Text = "";
                txB_Position.Text = "";
                txB_DescriptionExperience.Text = "";

                dTP_periodFromExperience.Value = DateTime.Now;
                dTP_periodToExperience.Value = DateTime.Now;

                LogSystem("Element został usunięty", listaKolorówLoguSystemu["sukces"]);
            }
            else
            {
                LogSystem("Wybierz najpierw element", listaKolorówLoguSystemu["fail"]);
            }
        }

        private void btn_UpListExperiences_Click(object sender, EventArgs e)
        {
            int i = lBx_Experiences.SelectedIndex;
            if (i > 0 && i != -1)
            {
                lBx_Experiences.SelectedIndex--;
                LoadDataCompany(lBx_Experiences.SelectedIndex);
            }
        }

        private void btn_DownListExperiences_Click(object sender, EventArgs e)
        {
            int i = lBx_Experiences.SelectedIndex;
            if (i < (lBx_Experiences.Items.Count - 1) && i != -1)
            {
                lBx_Experiences.SelectedIndex++;
                LoadDataCompany(lBx_Experiences.SelectedIndex);
            }
        }

        Experience select_exp = null;
        Education select_edu = null;

        private void lBx_Experiences_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lBx_Experiences.SelectedIndex != -1)
            {
                LoadDataCompany(lBx_Experiences.SelectedIndex);
            }
        }

        private void LoadDataCompany(int i)
        {

            select_exp = cv.Doświadczenie[i];

            txB_NameCompany.Text = select_exp.Company_name;
            txB_Position.Text = select_exp.Position;
            txB_DescriptionExperience.Text = select_exp.Description;

            dTP_periodFromExperience.Value = select_exp.Period_from;
            dTP_periodToExperience.Value = select_exp.Period_to;

            LogSystem("Załadowano dane firmy " + cv.Doświadczenie[i].Company_name, listaKolorówLoguSystemu["info"]);
        }

        private void LogSystem(String str, Color col)
        {
            lbl_logSys.Text = str;
            lbl_logSys.ForeColor = col;
        }

        private void txB_NameCompany_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Experiences.SelectedIndex != -1)
            {
                cv.Doświadczenie[lBx_Experiences.SelectedIndex].Company_name = txB_NameCompany.Text;
                LogSystem("Zapisano nazwę firmy", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txB_Position_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Experiences.SelectedIndex != -1)
            {
                cv.Doświadczenie[lBx_Experiences.SelectedIndex].Position = txB_Position.Text;
                LogSystem("Zapisano stanowisko w firmie", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txB_DescriptionExperience_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Experiences.SelectedIndex != -1)
            {
                cv.Doświadczenie[lBx_Experiences.SelectedIndex].Description = txB_DescriptionExperience.Text;
                cv.Doświadczenie[lBx_Experiences.SelectedIndex].Description_img = ImageToByteArray(
                    generateText(txB_DescriptionExperience.Text, "", "",
                    new Font("Verdana",10.0f),
                    System.Drawing.ColorTranslator.FromHtml(UserColors[2]),
                    System.Drawing.ColorTranslator.FromHtml(UserColors[0])                   
                    ));
                LogSystem("Zapisano opis doświadczenia", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void dTP_periodFrom_ValueChanged(object sender, EventArgs e)
        {
            if (lBx_Experiences.SelectedIndex != -1)
            {
                cv.Doświadczenie[lBx_Experiences.SelectedIndex].Period_from = dTP_periodFromExperience.Value;
                LogSystem("Zapisano okres od", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void dTP_periodTo_ValueChanged(object sender, EventArgs e)
        {
            if (lBx_Experiences.SelectedIndex != -1)
            {
                cv.Doświadczenie[lBx_Experiences.SelectedIndex].Period_to = dTP_periodToExperience.Value;
                LogSystem("Zapisano okres do", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void btn_UpListEducations_Click(object sender, EventArgs e)
        {
            int i = lBx_Educations.SelectedIndex;
            if (i > 0 && i != -1)
            {
                lBx_Educations.SelectedIndex--;
                LoadDataEducation(lBx_Educations.SelectedIndex);
            }
        }

        private void btn_DownListEducations_Click(object sender, EventArgs e)
        {
            int i = lBx_Educations.SelectedIndex;
            if (i < (lBx_Educations.Items.Count - 1) && i != -1)
            {
                lBx_Educations.SelectedIndex++;
                LoadDataEducation(lBx_Educations.SelectedIndex);
            }
        }

        private void LoadDataEducation(int i)
        {
            select_edu = cv.Edukacja[i];

            txB_NameUniversity.Text = select_edu.University_name;
            txB_Direction.Text = select_edu.Direction;
            txB_Degree.Text = select_edu.Degree;
            txB_Specjality.Text = select_edu.Specjality;
            txB_DescriptionEducation.Text = select_edu.Description;

            dTP_periodFromEducation.Value = select_edu.Period_from;
            dTP_periodToEducation.Value = select_edu.Period_to;

            LogSystem("Załadowano dane uczelnii " + cv.Edukacja[i].University_name, listaKolorówLoguSystemu["info"]);
        }

        private void btn_AddEducation_Click(object sender, EventArgs e)
        {
            cv.Edukacja.Add(new Education());
            lBx_Educations.Items.Add("Uczelnia nr " + lBx_Educations.Items.Count);
        }

        private void btn_RemoveEducation_Click(object sender, EventArgs e)
        {
            select_edu = null;
            int i = lBx_Educations.SelectedIndex;

            if (i != -1)
            {
                cv.Edukacja.RemoveAt(lBx_Educations.SelectedIndex);
                lBx_Educations.Items.RemoveAt(i);

                txB_NameUniversity.Text = "";
                txB_Direction.Text = "";
                txB_Degree.Text = "";
                txB_Specjality.Text = "";
                txB_DescriptionEducation.Text = "";

                dTP_periodFromEducation.Value = DateTime.Now;
                dTP_periodToEducation.Value = DateTime.Now;

                LogSystem("Element został usunięty", listaKolorówLoguSystemu["sukces"]);
            }
            else
            {
                LogSystem("Wybierz najpierw element", listaKolorówLoguSystemu["fail"]);
            }
        }

        private void lBx_Educations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lBx_Educations.SelectedIndex != -1)
            {
                LoadDataEducation(lBx_Educations.SelectedIndex);
            }
        }

        private void txB_DescriptionEducation_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Educations.SelectedIndex != -1)
            {
                cv.Edukacja[lBx_Educations.SelectedIndex].Description = txB_DescriptionEducation.Text;
                cv.Edukacja[lBx_Educations.SelectedIndex].Description_img = ImageToByteArray(
                   generateText(txB_DescriptionEducation.Text, "", "",
                   new Font("Verdana", 10.0f),
                   System.Drawing.ColorTranslator.FromHtml(UserColors[2]),
                   System.Drawing.ColorTranslator.FromHtml(UserColors[0])
                   ));
                LogSystem("Zapisano opis studiów", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void dTP_periodToEducation_ValueChanged(object sender, EventArgs e)
        {
            if (lBx_Educations.SelectedIndex != -1)
            {
                cv.Edukacja[lBx_Educations.SelectedIndex].Period_from = dTP_periodToEducation.Value;
                LogSystem("Zapisano okres od", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void dTP_periodFromEducation_ValueChanged(object sender, EventArgs e)
        {
            if (lBx_Educations.SelectedIndex != -1)
            {
                cv.Edukacja[lBx_Educations.SelectedIndex].Period_from = dTP_periodFromEducation.Value;
                LogSystem("Zapisano okres od", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txB_Specjality_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Educations.SelectedIndex != -1)
            {
                cv.Edukacja[lBx_Educations.SelectedIndex].Specjality = txB_Specjality.Text;
                LogSystem("Zapisano specjalność", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txB_Direction_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Educations.SelectedIndex != -1)
            {
                cv.Edukacja[lBx_Educations.SelectedIndex].Direction = txB_Direction.Text;
                LogSystem("Zapisano kierunek", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txB_Degree_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Educations.SelectedIndex != -1)
            {
                cv.Edukacja[lBx_Educations.SelectedIndex].Degree = txB_Degree.Text;
                LogSystem("Zapisano stopień", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txB_NameUniversity_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Educations.SelectedIndex != -1)
            {
                cv.Edukacja[lBx_Educations.SelectedIndex].University_name = txB_NameUniversity.Text;
                LogSystem("Zapisano nazwę uczelnii", listaKolorówLoguSystemu["sukces"]);
            }
        }

        //Menu programu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_Settings(this).Show();
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_Author(this).Show();
        }

        private void openDataFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //3. odczyt z pliku
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Json File | *.json";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Check if you really have a file name 
                if (openFileDialog1.FileName.Trim() != string.Empty)
                {
                    using (StreamReader r = new StreamReader(openFileDialog1.OpenFile()))
                    {
                        clearData();

                        string json = r.ReadToEnd();
                        cv = JsonConvert.DeserializeObject<CV>(json);

                        txb_Name.Text = cv.Imię;
                        txb_Surname.Text = cv.Nazwisko;
                        txb_Address.Text = cv.Adres;
                        txb_BirthdayPlace.Text = cv.MiejsceUrodzenia;
                        txb_Citizienship.Text = cv.Obywatelstwo;
                        txb_ConfidentialityClause.Text = cv.Klauzula;
                        txb_Email.Text = cv.Email;
                        txb_Github.Text = cv.Github;
                        txb_Linkedin.Text = cv.Linkedin;
                        txb_Interests.Text = cv.Zainteresowania;
                        txb_LastWork.Text = cv.Ostatniapraca;
                        txb_MaritalStatus.Text = cv.StanCywilny;
                        txb_Phone.Text = cv.Telefon;
                        cBx_sex.SelectedItem = cv.Płeć;
                        dTP_BirthdayDate.Text = cv.DataUrodzin;

                        try
                        {
                            LoadPicture(Image.FromFile(cv.Zdjęcie));
                        }
                        catch (FileNotFoundException)
                        {
                            LoadPicture(new Bitmap(Properties.Resources.image_null));
                        }
                        //
                        _imageUrl = cv.Zdjęcie;
                        //Doświadczenie
                        foreach (var exp_item in cv.Doświadczenie)
                        {
                            lBx_Experiences.Items.Add("Doświadczenie nr " + lBx_Experiences.Items.Count);
                            exp_item.Description_img = ImageToByteArray(
                                generateText(exp_item.Description.ToString(), "", "",
                                new Font("Verdana", 10.0f),
                                System.Drawing.ColorTranslator.FromHtml(UserColors[2]),
                                System.Drawing.ColorTranslator.FromHtml(UserColors[0])
                                ));
                        }
                        //Edukacja
                        foreach (var edu_item in cv.Edukacja)
                        {
                            lBx_Educations.Items.Add("Uczelnia nr " + lBx_Educations.Items.Count);
                            edu_item.Description_img = ImageToByteArray(
                                generateText(edu_item.Description.ToString(), "", "",
                                new Font("Verdana", 10.0f),
                                System.Drawing.ColorTranslator.FromHtml(UserColors[2]),
                                System.Drawing.ColorTranslator.FromHtml(UserColors[0])
                                ));
                        }
                        //Umiejętności
                        foreach (var skill_item in cv.Umiejętności)
                        {
                            lBx_Skills.Items.Add("Umiejętność nr " + lBx_Skills.Items.Count);
                        }
                        //Język
                        LoadLanguagesToComboBox();
                        foreach (var lang_item in cv.Języki) {
                            lBx_Languages.Items.Add("Język nr " + lBx_Languages.Items.Count);
                        }                        

                        LogSystem("Załadowano dane z pliku", listaKolorówLoguSystemu["sukces"]);
                    }
                }
            }            
        }

        private void saveDataToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "CV_data.json";
            save.Filter = "Json File | *.json";
            if (save.ShowDialog() == DialogResult.OK)
            {
                CV _data = cv;

                using (StreamWriter file = new StreamWriter(save.OpenFile()))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, _data);
                    file.Dispose();
                    file.Close();
                }
            }
        }

        private void ClearStripMenuItem_Click(object sender, EventArgs e)
        {
            clearData();
            LogSystem("Wyczyszczono dane", listaKolorówLoguSystemu["info"]);
        }

        private void clearData()
        {
            cv = new CV();
            txb_Name.Text = "";
            txb_Surname.Text = "";
            txb_Address.Text = "";
            txb_BirthdayPlace.Text = "";
            dTP_BirthdayDate.Text = DateTime.Now.ToString();
            txb_Citizienship.Text = "";
            txb_ConfidentialityClause.Text = "";
            txb_Email.Text = "";
            txb_Github.Text = "";
            txb_Linkedin.Text = "";
            txb_Interests.Text = "";
            txb_LastWork.Text = "";
            txb_MaritalStatus.Text = "";
            txb_Phone.Text = "";
            cBx_sex.SelectedIndex = -1;

            LoadPicture(new Bitmap(Properties.Resources.image_null));

            lBx_Experiences.Items.Clear();
            lBx_Educations.Items.Clear();
            lBx_Skills.Items.Clear();
            lBx_Languages.Items.Clear();

            txB_Degree.Text = "";
            txB_DescriptionEducation.Text = "";
            txB_DescriptionExperience.Text = "";
            txB_DescriptionLanguage.Text = "";
            txB_DescriptionSkill.Text = "";
            txB_Direction.Text = "";
            txB_NameCompany.Text = "";
            txB_NameSkill.Text = "";
            txB_NameUniversity.Text = "";
            txB_Position.Text = "";
            txB_Specjality.Text = "";
            tBr_Language.Value = 0;
            tBr_Skill.Value = 0;
            cBx_Languages.SelectedIndex = -1;
            dTP_periodFromEducation.Text = DateTime.Now.ToString();
            dTP_periodFromExperience.Text = DateTime.Now.ToString();
            dTP_periodToEducation.Text = DateTime.Now.ToString();
            dTP_periodToExperience.Text = DateTime.Now.ToString();
        }

        private void generatePdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Wygeneruj_pdf();
        }

        //Skills
        private void btn_AddSkill_Click(object sender, EventArgs e)
        {
            cv.Umiejętności.Add(new Skill());
            lBx_Skills.Items.Add("Umiejętność nr " + lBx_Skills.Items.Count);
        }

        private void btn_RemoveSkill_Click(object sender, EventArgs e)
        {
            select_exp = null;
            int i = lBx_Skills.SelectedIndex;

            if (i != -1)
            {
                cv.Umiejętności.RemoveAt(lBx_Skills.SelectedIndex);
                lBx_Skills.Items.RemoveAt(i);

                txB_DescriptionSkill.Text = "";
                txB_NameSkill.Text = "";
                tBr_Skill.Value = 0;                

                LogSystem("Umiejętność została usunięta", listaKolorówLoguSystemu["sukces"]);
            }
            else
            {
                LogSystem("Wybierz najpierw umiejętność", listaKolorówLoguSystemu["fail"]);
            }
        }

        private void btn_UpListSkills_Click(object sender, EventArgs e)
        {
            int i = lBx_Skills.SelectedIndex;
            if (i > 0 && i != -1)
            {
                lBx_Skills.SelectedIndex--;
                LoadDataSkill(lBx_Skills.SelectedIndex);
            }
        }

        private void btn_DownListSkills_Click(object sender, EventArgs e)
        {
            int i = lBx_Skills.SelectedIndex;
            if (i < (lBx_Skills.Items.Count - 1) && i != -1)
            {
                lBx_Skills.SelectedIndex++;
                LoadDataSkill(lBx_Skills.SelectedIndex);
            }
        }

        Skill select_umiej = null;

        private void LoadDataSkill(int i)
        {
            select_umiej = cv.Umiejętności[i];

            txB_DescriptionSkill.Text = select_umiej.Description;
            txB_NameSkill.Text = select_umiej.Name;
            tBr_Skill.Value = select_umiej.Degree;

            LogSystem("Załadowano umiejętność " + cv.Umiejętności[i].Name, listaKolorówLoguSystemu["info"]);
        }

        private void txB_NameSkill_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Skills.SelectedIndex != -1)
            {
                cv.Umiejętności[lBx_Skills.SelectedIndex].Name = txB_NameSkill.Text;
                LogSystem("Zapisano nazwę umiejętności", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txB_DescriptionSkill_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Skills.SelectedIndex != -1)
            {
                cv.Umiejętności[lBx_Skills.SelectedIndex].Description = txB_DescriptionSkill.Text;
                LogSystem("Zapisano opis umiejętności", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void tBr_Skill_ValueChanged(object sender, EventArgs e)
        {
            if (lBx_Skills.SelectedIndex != -1)
            {
                cv.Umiejętności[lBx_Skills.SelectedIndex].Degree = tBr_Skill.Value;
                LogSystem("Zapisano znajomość umiejętności", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void lBx_Skills_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lBx_Skills.SelectedIndex != -1)
            {
                LoadDataSkill(lBx_Skills.SelectedIndex);
            }
        }

        //Languages
        private void btn_AddLanguage_Click(object sender, EventArgs e)
        {
            cv.Języki.Add(new Language());            
            lBx_Languages.Items.Add("Język nr " + lBx_Languages.Items.Count);

            cBx_Languages.SelectedIndex = -1;
            lBx_Languages.SelectedIndex = -1;
            txB_DescriptionLanguage.Text = "";                     
            tBr_Language.Value = 0;

            LoadLanguagesToComboBox();
        }

        private void btn_RemoveLanguage_Click(object sender, EventArgs e)
        {
            select_exp = null;
            int i = lBx_Languages.SelectedIndex;

            if (i != -1)
            {
                cv.Języki.RemoveAt(lBx_Languages.SelectedIndex);
                lBx_Languages.Items.RemoveAt(i);

                tBr_Language.Value = 0;
                txB_DescriptionLanguage.Text = "";
                LoadLanguagesToComboBox();                

                LogSystem("Język został usunięty", listaKolorówLoguSystemu["sukces"]);
            }
            else
            {
                LogSystem("Wybierz najpierw język", listaKolorówLoguSystemu["fail"]);
            }
        }

        private void btn_UpListLanguage_Click(object sender, EventArgs e)
        {
            int i = lBx_Languages.SelectedIndex;
            if (i > 0 && i != -1)
            {
                lBx_Languages.SelectedIndex--;
                LoadDataLanguage(lBx_Languages.SelectedIndex);
            }
        }

        private void btn_DownListLanguage_Click(object sender, EventArgs e)
        {
            int i = lBx_Languages.SelectedIndex;
            if (i < (lBx_Languages.Items.Count - 1) && i != -1)
            {
                lBx_Languages.SelectedIndex++;
                LoadDataLanguage(lBx_Languages.SelectedIndex);
            }
        }

        private void lBx_Languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lBx_Languages.SelectedIndex != -1)
            {
                LoadDataLanguage(lBx_Languages.SelectedIndex);
            }
        }

        private void cBx_Languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lBx_Languages.SelectedIndex != -1 && cBx_Languages.SelectedIndex != -1)
            {
                cv.Języki[lBx_Languages.SelectedIndex].Name = cBx_Languages.SelectedValue.ToString();
                LogSystem("Zapisano nazwę języka", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void tBr_Language_ValueChanged(object sender, EventArgs e)
        {
            if (lBx_Languages.SelectedIndex != -1)
            {
                cv.Języki[lBx_Languages.SelectedIndex].Degree = tBr_Language.Value;
                LogSystem("Zapisano znajomość język", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txB_DescriptionLanguage_TextChanged(object sender, EventArgs e)
        {
            if (lBx_Languages.SelectedIndex != -1)
            {
                cv.Języki[lBx_Languages.SelectedIndex].Description = txB_DescriptionLanguage.Text;
                LogSystem("Zapisano opis język", listaKolorówLoguSystemu["sukces"]);
            }
        }

        Language select_lang = null;

        private void LoadDataLanguage(int i)
        {
            select_lang = cv.Języki[i];

            txB_DescriptionLanguage.Text = select_lang.Description;
            cBx_Languages.SelectedIndex = cBx_Languages.FindStringExact(select_lang.Name);
            tBr_Language.Value = select_lang.Degree;

            LogSystem("Załadowano język " + cv.Języki[i].Name, listaKolorówLoguSystemu["info"]);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.FileInfo fInfo = new System.IO.FileInfo(dlg.FileName);
                    _imageUrl = fInfo.DirectoryName + "\\" + fInfo.Name;
                    LoadPicture(new Bitmap(dlg.FileName));
                    cv.Zdjęcie = _imageUrl;
                    LogSystem("Załadowano zdjęcie " + _imageUrl, listaKolorówLoguSystemu["info"]);
                }
            }
        }

        private void LoadPicture(Image img)
        {
            pBx_photo.Image = ResizeImage(img, pBx_photo.Width, pBx_photo.Height);                               
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            float scaleHeight = (float)height / (float)image.Height;
            float scaleWidth = (float)width / (float)image.Width;

            float scale = Math.Min(scaleHeight, scaleWidth);

            var destImage = new Bitmap(image, (int)(image.Width * scale), (int)(image.Height * scale));
            
            return destImage;
        }

        private void txb_Name_TextChanged(object sender, EventArgs e)
        {
            if (txb_Name.Text != null)
            {
                cv.Imię = txb_Name.Text;
                LogSystem("Zapisano imię", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_Surname_TextChanged(object sender, EventArgs e)
        {
            if (txb_Surname.Text != null)
            {
                cv.Nazwisko = txb_Surname.Text;
                LogSystem("Zapisano nazwisko", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_LastWork_TextChanged(object sender, EventArgs e)
        {
            if (txb_LastWork.Text != null)
            {
                cv.Ostatniapraca = txb_LastWork.Text;
                LogSystem("Zapisano ostatnią pracę", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void cBx_sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBx_sex.SelectedItem != null)
            {
                cv.Płeć = cBx_sex.SelectedItem.ToString();
                LogSystem("Zapisano płeć", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void dTP_BirthdayDate_ValueChanged(object sender, EventArgs e)
        {
            if (dTP_BirthdayDate.Text != null)
            {
                cv.DataUrodzin = dTP_BirthdayDate.Text.ToString();
                LogSystem("Zapisano datę urodzenia", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_BirthdayPlace_TextChanged(object sender, EventArgs e)
        {
            if (txb_BirthdayPlace.Text != null)
            {
                cv.MiejsceUrodzenia = txb_BirthdayPlace.Text.ToString();
                LogSystem("Zapisano miejsce urodzenia", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_Citizienship_TextChanged(object sender, EventArgs e)
        {
            if (txb_Citizienship.Text != null)
            {
                cv.Obywatelstwo = txb_Citizienship.Text.ToString();
                LogSystem("Zapisano obywatelstwo", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_MaritalStatus_TextChanged(object sender, EventArgs e)
        {
            if (txb_MaritalStatus.Text != null)
            {
                cv.StanCywilny = txb_MaritalStatus.Text.ToString();
                LogSystem("Zapisano stan cywilny", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_Github_TextChanged(object sender, EventArgs e)
        {
            if (txb_Github.Text != null)
            {
                cv.Github = txb_Github.Text.ToString();
                LogSystem("Zapisano adres na github", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_Linkedin_TextChanged(object sender, EventArgs e)
        {
            if (txb_Linkedin.Text != null)
            {
                cv.Linkedin = txb_Linkedin.Text.ToString();
                LogSystem("Zapisano adres na linkedin", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_Interests_TextChanged(object sender, EventArgs e)
        {
            if (txb_Interests.Text != null)
            {
                cv.Zainteresowania = txb_Interests.Text.ToString();
                LogSystem("Zapisano zainteresowania", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_ConfidentialityClause_TextChanged(object sender, EventArgs e)
        {
            if (txb_ConfidentialityClause.Text != null)
            {
                cv.Klauzula = txb_ConfidentialityClause.Text.ToString();
                LogSystem("Zapisano klauzulę", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_Phone_TextChanged(object sender, EventArgs e)
        {
            if (txb_Phone.Text != null)
            {
                cv.Telefon = txb_Phone.Text.ToString();
                LogSystem("Zapisano telefon", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_Email_TextChanged(object sender, EventArgs e)
        {
            if (txb_Email.Text != null)
            {
                cv.Email = txb_Email.Text.ToString();
                LogSystem("Zapisano adres e-mail", listaKolorówLoguSystemu["sukces"]);
            }
        }

        private void txb_Address_TextChanged(object sender, EventArgs e)
        {
            if (txb_Address.Text != null)
            {
                cv.Adres = txb_Address.Text.ToString();
                LogSystem("Zapisano adres zamieszkania", listaKolorówLoguSystemu["sukces"]);
            }
        }        
    }
}