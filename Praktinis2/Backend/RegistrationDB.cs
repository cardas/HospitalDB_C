using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktinis2
{
   public class RegistrationDB : RegistrationDBSet
    {
        private string Gydytojoid;
        private string Pacientoid;
        private string data;
     


     


        public string Data 
        {

            get { return data; }
            set { data = value; }
        }

        public string GydytojoID
        {

            get { return Gydytojoid; }
            set { Gydytojoid = value; }
        }

        public string PacientoID
        {

            get { return Pacientoid; }
            set { Pacientoid = value; }
        }




    }
}
