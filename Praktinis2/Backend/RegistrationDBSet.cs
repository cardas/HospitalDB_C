using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;



namespace Praktinis2
{

   

    public class RegistrationDBSet : Person
    {

        public static List<RegistrationDB> DataReg = new List<RegistrationDB>();
        public List<string> RegistrationTime = new List<string>();
        public List<string> RegistratedPacientai = new List<string>();
        public List<string> RegistratedGydytojas = new List<string>();


        public static void LoadToList()
        {
            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);
            string sqlRegistration = "select * from tbl_Registration;";
     

            SQLiteCommand commandRegistration = new SQLiteCommand(sqlRegistration, dbConection);



            dbConection.Open();


            SQLiteDataReader readerRegistration = commandRegistration.ExecuteReader();
    



            while (readerRegistration.Read())
            {
                RegistrationDB RegistrationData = new RegistrationDB();


                RegistrationData.GydytojoID = Convert.ToString(readerRegistration[0]);
                RegistrationData.PacientoID = Convert.ToString(readerRegistration[1]);
                RegistrationData.Data = Convert.ToString(readerRegistration[2]);
                DataReg.Add(RegistrationData);

            }


            dbConection.Close();



        }




    }
}
