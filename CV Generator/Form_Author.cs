using System;
using System.Windows.Forms;

namespace CV_Generator
{
    internal class Form_Author : Form
    {
        private Label label1;
        private Label label3;
        private Label label2;

        public Form_Author(Form1 f) 
        {
            InitializeComponent();

            ChangeLanguage(f.Language);
        }

        private void ChangeLanguage(string language)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("CV_Generator.Resources.lang_" + language,
               System.Reflection.Assembly.GetExecutingAssembly());

            label1.Text = rm.GetString("Author1");
            label2.Text = rm.GetString("Author2");
            label3.Text = rm.GetString("Author3");
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Autor oprogramowania";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Damian Łukasik";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Częstochowa 2018";
            // 
            // Form_Author
            // 
            this.ClientSize = new System.Drawing.Size(423, 98);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form_Author";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}