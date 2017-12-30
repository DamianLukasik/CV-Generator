using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Generator
{
    class CV
    {
        private String imię;

        public String Imię
        {
            get { return imię; }
            set { imię = value; }
        }

        private String nazwisko;

        public String Nazwisko
        {
            get { return nazwisko; }
            set { nazwisko = value; }
        }

        private String ostatniapraca;

        public String Ostatniapraca
        {
            get { return ostatniapraca; }
            set { ostatniapraca = value; }
        }

        private String płeć;

        public String Płeć
        {
            get { return płeć; }
            set { płeć = value; }
        }

        private String zdjęcie;

        public String Zdjęcie
        {
            get { return zdjęcie; }
            set { zdjęcie = value; }
        }

        private String dataUrodzin;

        public String DataUrodzin
        {
            get { return dataUrodzin; }
            set { dataUrodzin = value; }
        }

        private String miejsceUrodzenia;

        public String MiejsceUrodzenia
        {
            get { return miejsceUrodzenia; }
            set { miejsceUrodzenia = value; }
        }

        private String obywatelstwo;

        public String Obywatelstwo
        {
            get { return obywatelstwo; }
            set { obywatelstwo = value; }
        }

        private String stancywilny;

        public String StanCywilny
        {
            get { return stancywilny; }
            set { stancywilny = value; }
        }

        private String linkedin;

        public String Linkedin
        {
            get { return linkedin; }
            set { linkedin = value; }
        }

        private String github;

        public String Github
        {
            get { return github; }
            set { github = value; }
        }

        private String zainteresowania;

        public String Zainteresowania
        {
            get { return zainteresowania; }
            set { zainteresowania = value; }
        }

        private String klauzula;

        public String Klauzula
        {
            get { return klauzula; }
            set { klauzula = value; }
        }

        private String telefon;

        public String Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }

        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        private String adres;

        public String Adres
        {
            get { return adres; }
            set { adres = value; }
        }

        private List<Experience> doświadczenie;

        public List<Experience> Doświadczenie
        {
            get { return doświadczenie; }
            set { doświadczenie = value; }
        }

        private List<Education> edukcja;

        public List<Education> Edukacja
        {
            get { return edukcja; }
            set { edukcja = value; }
        }

        private List<Skill> umiejętności;

        public List<Skill> Umiejętności
        {
            get { return umiejętności; }
            set { umiejętności = value; }
        }

        private List<Language> języki;

        public List<Language> Języki
        {
            get { return języki; }
            set { języki = value; }
        }

        public CV()
        {
            doświadczenie = new List<Experience>();
            edukcja = new List<Education>();
            umiejętności = new List<Skill>();
            języki = new List<Language>();
        }

    }
}
