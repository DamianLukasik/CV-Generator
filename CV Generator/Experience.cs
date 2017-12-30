using System;

namespace CV_Generator
{
    public class Experience
    {
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

        private String company_name;

        public String Company_name
        {
            get { return company_name; }
            set { company_name = value; }
        }

        private String position;

        public String Position
        {
            get { return position; }
            set { position = value; }
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


        public Experience()
        {
            description = "Opis doświadczenia";
            position = "Stanowisko w firmie";
            company_name = "Nazwa firmy";

            period_from = DateTime.Now;
            period_to = DateTime.Now;
        }



    }
}