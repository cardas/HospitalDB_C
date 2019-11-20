using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktinis2.Backend
{
    public class PersonDB 
    {

        private string id;
        private string status;
        private string name;
        private string surname;
        private string password;
        private string age;
        private string Contact;
        private string About;
        private string Image;
        private string User_Stat;

      

        public string ID
        {

            get { return id; }
            set { id = value; }
        }
        public string User_Status
        {

            get { return User_Stat; }
            set { User_Stat = value; }
        }

        public string Status
        {

            get { return status; }
            set { status = value; }
        }
        public string Name
        {

            get { return name; }
            set { name = value; }
        }
        public string Surname
        {

            get { return surname; }
            set { surname = value; }
        }
        public string Password
        {

            get { return password; }
            set { password = value; }
        }
        public string Age
        {

            get { return age; }
            set { age = value; }
        }
        public string contact
        {

            get { return Contact; }
            set { Contact = value; }
        }
        public string about
        {

            get { return About; }
            set { About = value; }
        }

        public string image
        {

            get { return Image; }
            set { Image = value; }
        }


    }




}
