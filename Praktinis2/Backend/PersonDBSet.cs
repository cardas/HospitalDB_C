using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;


namespace Praktinis2.BackEnd
{
    public class PersonDBSet : Backend.PersonDB
    {
        public static List<Backend.PersonDB> Data = new List<Backend.PersonDB>();
     
        public static List<string> PersonStatus = new List<string>();


        /////////////////////////////////////////////////////////////////////////////////
        //////////List Grab From DataBase////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        public static void LoadToList()
        {
            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);
            string sqlGydytojas = "select * from tbl_Gydytojas;";
            string sqlPacientas = "select * from tbl_Pacientas;";

            SQLiteCommand commandGydytojas = new SQLiteCommand(sqlGydytojas, dbConection);
            SQLiteCommand commandPacientas = new SQLiteCommand(sqlPacientas, dbConection);


            dbConection.Open();


            SQLiteDataReader readerGydytojas = commandGydytojas.ExecuteReader();
            SQLiteDataReader readerPacientas = commandPacientas.ExecuteReader();



            while (readerGydytojas.Read())
            {
                Backend.PersonDB PersonData = new Backend.PersonDB();
                
                PersonData.ID = Convert.ToString(readerGydytojas[0]);
                PersonData.Status= Convert.ToString(readerGydytojas[1]);
                PersonData.Name = Convert.ToString(readerGydytojas[2]);
                PersonData.Surname= Convert.ToString(readerGydytojas[3]);
                PersonData.Password = Convert.ToString(readerGydytojas[4]);
                PersonData.Age= Convert.ToString(readerGydytojas[5]);
                PersonData.contact= Convert.ToString(readerGydytojas[6]);
                PersonData.about = Convert.ToString(readerGydytojas[7]);
                PersonData.image = Convert.ToString(readerGydytojas[8]);
                PersonData.User_Status = Convert.ToString(readerGydytojas[9]);
                Data.Add(PersonData);

            }
         

            while (readerPacientas.Read())
            {
                Backend.PersonDB PersonData = new Backend.PersonDB();

                PersonData.ID = Convert.ToString(readerPacientas[0]);
                PersonData.Status = Convert.ToString(readerPacientas[1]);
                PersonData.Name = Convert.ToString(readerPacientas[2]);
                PersonData.Surname = Convert.ToString(readerPacientas[3]);
                PersonData.Password = Convert.ToString(readerPacientas[4]);
                PersonData.Age = Convert.ToString(readerPacientas[5]);
                PersonData.contact = Convert.ToString(readerPacientas[6]);
                PersonData.about = Convert.ToString(readerPacientas[7]);
                PersonData.image = Convert.ToString(readerPacientas[8]);
                PersonData.User_Status = Convert.ToString(readerPacientas[9]);
                Data.Add(PersonData);

            }
            dbConection.Close();



        }

        /////////////////////////////////////////////////////////////////////////////////
        //////////List Grab PAGAL STATUSA////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        public static void LoadTypes()
        {

            for (int i = 0; i < Data.Count; i++)
            {
                int hit = 0;
                for (int j = 0; j < PersonStatus.Count; j++)
                {
                    if (Data[i].Status == PersonStatus[j])
                        hit++;
                }
                if (hit == 0)
                    PersonStatus.Add(Data[i].Status);
            }
        }
    }


}
