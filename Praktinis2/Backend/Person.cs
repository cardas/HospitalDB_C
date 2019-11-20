using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Praktinis2
{



    public class Person : BackEnd.PersonDBSet
    {
        
        public List<string> PersonLogin = new List<string>();
        public List<string> PersonName = new List<string>();
        public List<string> Gydytojas = new List<string>();



        public void GydytojasInfo()
        {
            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);

            string sqlGydytojas = "select * from tbl_Gydytojas;";
        

            SQLiteCommand commandGydytojas = new SQLiteCommand(sqlGydytojas, dbConection);

            dbConection.Open();


            SQLiteDataReader readerGydytojas = commandGydytojas.ExecuteReader();

            while (readerGydytojas.Read())
            {
                Gydytojas.Add($"{readerGydytojas[2]},{readerGydytojas[7]}");
            }



        }
            public void PersonLoginInfo()
        {
            try {  
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
                PersonLogin.Add($"{readerGydytojas[2]},{readerGydytojas[4]}");
            }
         /*   dbConection.Close();
            dbConection.Open();*/

            while (readerPacientas.Read())
            {
                PersonLogin.Add($"{readerPacientas[2]},{readerPacientas[4]}");
            }
      

            }
            catch (Exception ex)
            {
                System.ArgumentException argEx = new System.ArgumentException("Index is out of range", "index", ex);
                throw argEx;

            }
        }

        public void PersonNameInfo()
        {

            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);

            dbConection.Open();
            string sqlGydytojas = "select * from tbl_Gydytojas;";
            string sqlPacientas = "select * from tbl_Pacientas;";

            SQLiteCommand commandGydytojas = new SQLiteCommand(sqlGydytojas, dbConection);
            SQLiteCommand commandPacientas = new SQLiteCommand(sqlPacientas, dbConection);




            SQLiteDataReader readerGydytojas = commandGydytojas.ExecuteReader();
            SQLiteDataReader readerPacientas = commandPacientas.ExecuteReader();






      
  

            while (readerGydytojas.Read())
            {
                PersonName.Add($"{readerGydytojas[1]}");
            }

            while (readerPacientas.Read())
            {
                PersonName.Add($"{readerPacientas[1]}");
            }
            dbConection.Close();
        }



    }
}
