using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Generator
{
    class Education
    {
        //Edukacja - 1 pozycja = okres(od do) nazwa uczelni, kierunek, stopień, specjalność, opis,

        private DateTime period_from;

        public DateTime Period_from
        {
            get { return period_from; }
            set { period_from = value; }
        }

        private DateTime period_to;

        public DateTime Period_to
        {
            get { return period_to; }
            set { period_to = value; }
        }

        private String university_name;

        public String University_name
        {
            get { return university_name; }
            set { university_name = value; }
        }

        private String direction;

        public String Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private String degree;

        public String Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        private String specjality;

        public String Specjality
        {
            get { return specjality; }
            set { specjality = value; }
        }

        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        private byte[] description_img;

        public byte[] Description_img
        {
            get { return description_img; }
            set { description_img = value; }
        }

        public Education() {
            period_from = DateTime.Now;
            period_to = DateTime.Now;

            university_name = "Nazwa uczelni";
            direction = "nazwa kierunku";
            degree = "stopień studiów";
            specjality = "specjalność";
            description = "opis";
        }
    }
}
