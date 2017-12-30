using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CV_Generator
{
    internal class Form_Settings : Form
    {
        private Label label1;
        private Button btn_SaveSettings;
        private Button btn_Exit;
        private ComboBox cBx_LanguageVersion;

        List<String> listSettingLanguages = new List<String> { "Angielski", "Polski" };

        public Form_Settings(Form1 f)
        {
            form = f;

            InitializeComponent();

            ChangeLanguage(f.Language);
        }

        private void LoadSettingLanguagesToComboBox(string lang)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("CV_Generator.Resources.lang_" + lang,
               System.Reflection.Assembly.GetExecutingAssembly());

            listSettingLanguages[0] = rm.GetString("English");
            listSettingLanguages[1] = rm.GetString("Polish");

            //dodanie języków
            List<String> tmp_lang = new List<string>();

            foreach (var lang__ in listSettingLanguages)
            {
                tmp_lang.Add(lang__);
            }

            cBx_LanguageVersion.DataSource = tmp_lang;
        }

        private void ChangeLanguage(string language)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("CV_Generator.Resources.lang_" + language,
               System.Reflection.Assembly.GetExecutingAssembly());

            label1.Text = rm.GetString("LanguageVersion");
            btn_SaveSettings.Text = rm.GetString("SaveSettings");
            btn_Exit.Text = rm.GetString("Exit");

            LoadSettingLanguagesToComboBox(language);
        }

        Form1 form;

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cBx_LanguageVersion = new System.Windows.Forms.ComboBox();
            this.btn_SaveSettings = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wersja językowa";
            // 
            // cBx_LanguageVersion
            // 
            this.cBx_LanguageVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBx_LanguageVersion.FormattingEnabled = true;
            this.cBx_LanguageVersion.Location = new System.Drawing.Point(105, 6);
            this.cBx_LanguageVersion.Name = "cBx_LanguageVersion";
            this.cBx_LanguageVersion.Size = new System.Drawing.Size(121, 21);
            this.cBx_LanguageVersion.TabIndex = 1;
            // 
            // btn_SaveSettings
            // 
            this.btn_SaveSettings.Location = new System.Drawing.Point(350, 90);
            this.btn_SaveSettings.Name = "btn_SaveSettings";
            this.btn_SaveSettings.Size = new System.Drawing.Size(116, 23);
            this.btn_SaveSettings.TabIndex = 2;
            this.btn_SaveSettings.Text = "Zapisz zmiany";
            this.btn_SaveSettings.UseVisualStyleBackColor = true;
            this.btn_SaveSettings.Click += new System.EventHandler(this.btn_SaveSettings_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(12, 90);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(116, 23);
            this.btn_Exit.TabIndex = 4;
            this.btn_Exit.Text = "Wyjdź";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // Form_Settings
            // 
            this.ClientSize = new System.Drawing.Size(478, 125);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_SaveSettings);
            this.Controls.Add(this.cBx_LanguageVersion);
            this.Controls.Add(this.label1);
            this.Name = "Form_Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btn_SaveSettings_Click(object sender, System.EventArgs e)
        {
            SaveSettings();
        }
        
        private void btn_SaveSettingsAndExit_Click(object sender, System.EventArgs e)
        {
            SaveSettings();
            this.Close();
        }

        private void btn_Exit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void SaveSettings()
        {
            ChangeLanguageVersion(cBx_LanguageVersion.SelectedItem.ToString());
            Console.WriteLine("Zapisano ustawienia");
        }

        private void ChangeLanguageVersion(string lang)
        {
            switch (lang)
            {
                case "Angielski":
                case "English":
                    lang = "en";
                    break;
                case "Polski":
                case "Polish":
                    lang = "pl";
                    break;
            }

            form.ChangeLanguage(lang);
            this.ChangeLanguage(lang);
        }
    }
}