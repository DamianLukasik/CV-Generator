using System;

namespace CV_Generator
{
    internal class Skill
    {
        private String name;

        public String Name
        {
            get { return  name; }
            set {  name = value; }
        }

        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        private int degree;

        public int Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        public Skill() {
            degree = 0;
            description = "";
            name = "";
        }
    }
}