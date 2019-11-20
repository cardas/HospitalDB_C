using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;


namespace Praktinis2
{
 
    public partial class UserGydytojasDynamic : UserControl
    {

        Person person = new Person();
        Backend.PersonDB PersonInf = new Backend.PersonDB();
        RegistrationDB RegistrationInf = new RegistrationDB();
        RegistrationDBSet RegistrationSet = new RegistrationDBSet(); 

        public UserGydytojasDynamic(Backend.PersonDB person)
        {
            InitializeComponent();
            lblDescription.Text = person.about;
            lblName.Text = person.Name;
            lblSurname.Text = person.Surname;
            lblAge.Text = person.Age;
            lblContact.Text = person.contact;
            lblStatus.Text = person.Status;


            ImageSource itemImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Pictures\\" + person.image));
            image.Source = itemImage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PersonInf.Name = lblName.Text;
            PersonInf.about = lblDescription.Text;
            PersonInf.Surname = lblSurname.Text;
            PersonInf.Age = lblAge.Text;
            PersonInf.contact = lblContact.Text;


            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);
            string sql1 = $"update tbl_user set surname=@surname where name=@name";
            string sql2 = $"update tbl_user set age=@age where name=@name";
            string sql3 = $"update tbl_user set contact=@contact where name=@name";
            string sql4 = $"update tbl_user set about=@about where name=@name";

            SQLiteCommand command1 = new SQLiteCommand(sql1, dbConection);
            SQLiteCommand command2 = new SQLiteCommand(sql2, dbConection);
            SQLiteCommand command3 = new SQLiteCommand(sql3, dbConection);
            SQLiteCommand command4 = new SQLiteCommand(sql4, dbConection);


            dbConection.Open();
            command1.Parameters.AddWithValue("@name", PersonInf.Name);
            command2.Parameters.AddWithValue("@name", PersonInf.Name);
            command3.Parameters.AddWithValue("@name", PersonInf.Name);
            command4.Parameters.AddWithValue("@name", PersonInf.Name);

            command1.Parameters.AddWithValue("@surname", PersonInf.Surname);
            command2.Parameters.AddWithValue("@age", PersonInf.Age);
            command3.Parameters.AddWithValue("@contact", PersonInf.contact);
            command4.Parameters.AddWithValue("@about", PersonInf.about);


            command1.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            command3.ExecuteNonQuery();
            command4.ExecuteNonQuery();
            dbConection.Close();
            MessageBox.Show("Modification Completed! ");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {



            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);
            string sqlPac = "select * from tbl_Pacientas;";
            string sqlGyd = "select * from tbl_Gydytojas;";
            string sqlReg = "select * from tbl_Registration;";

            SQLiteCommand commandPac = new SQLiteCommand(sqlPac, dbConection);
            SQLiteCommand commandGyd = new SQLiteCommand(sqlGyd, dbConection);
            SQLiteCommand commandReg = new SQLiteCommand(sqlReg, dbConection);


            dbConection.Open();


            SQLiteDataReader readerPac = commandPac.ExecuteReader();
            SQLiteDataReader readerGyd = commandGyd.ExecuteReader();
            SQLiteDataReader readerReg = commandReg.ExecuteReader();

            try
            {
                PersonInf.Name = lblName.Text;

                while (readerPac.Read())
                {
                  
                    if (PersonInf.Name == Convert.ToString(readerPac[2]))
                    {
                        RegistrationInf.PacientoID = Convert.ToString(readerPac[0]);
                    }

                }


                //Got Gydytojas ID And Data info by Person
                // Error
                while (readerReg.Read())
                {

                    if (RegistrationInf.PacientoID == Convert.ToString(readerReg[1]))
                    {
                        RegistrationInf.GydytojoID = Convert.ToString(readerReg[0]);

                        while (readerGyd.Read())
                        {
                            if (RegistrationInf.GydytojoID == Convert.ToString(readerGyd[0]))
                            {

                          

                                RegistrationSet.RegistratedGydytojas.Add($"Vardas: {(Convert.ToString(readerGyd[2]))}\n");
                                RegistrationSet.RegistratedGydytojas.Add($"Pavarde: {(Convert.ToString(readerGyd[3]))}\n");
                                RegistrationSet.RegistratedGydytojas.Add($"Metu: {(Convert.ToString(readerGyd[5]))}\n");
                                RegistrationSet.RegistratedGydytojas.Add($"Kontaktinis Numers: {(Convert.ToString(readerGyd[6]))}\n");
                                RegistrationSet.RegistratedGydytojas.Add($"Jus Registruotas: {Convert.ToString(readerReg[2])}\n");
                                RegistrationSet.RegistratedGydytojas.Add($"Apie: {(Convert.ToString(readerGyd[7]))}\n");
                                break;

                            }
                        }

                    }

                }

            


                //Getting This Gydytotas
                MessageBox.Show($" Jusu Paciento Vardas Duomenys:\n" +
               $"{RegistrationSet.RegistratedGydytojas[0]}\n" +
                $"{RegistrationSet.RegistratedGydytojas[1]}\n" +
                $"{RegistrationSet.RegistratedGydytojas[2]}\n" +
                $"{RegistrationSet.RegistratedGydytojas[3]}\n" +
                $"{RegistrationSet.RegistratedGydytojas[4]}\n" +
                $"{RegistrationSet.RegistratedGydytojas[5]}\n");


            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.LineNumber()) + " Line Eror ", "Login Denied", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GetStartedAsAdmin getStarted = Window.GetWindow(this) as GetStartedAsAdmin;

            PersonInf.Status = lblStatus.Text;

            person.PersonNameInfo();

            if (PersonInf.Status == "Pacientas")
            {
                string selected = String.Format(lblName.Text + "  " + lblSurname.Text + " ----- Deleted from DataBase!");
                string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
                SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);
                dbConection.Open();

                string sqldeletePac = $"delete from tbl_Pacientas where name=@name";
                string sqldeleteReg = $"delete from tbl_Registration where Pacientas_ID=@Pacientas_ID";
                string sqlPacientas = "select * from tbl_Pacientas;";
             

                SQLiteCommand commandPacientas = new SQLiteCommand(sqlPacientas, dbConection);

                SQLiteDataReader readerPacientas = commandPacientas.ExecuteReader();

                while (readerPacientas.Read())
                {

                    if (lblName.Text == Convert.ToString(readerPacientas[2]))
                    {

                        RegistrationInf.PacientoID = Convert.ToString(readerPacientas[0]);

                    
                    }

                }


                    SQLiteCommand commandPac = new SQLiteCommand(sqldeletePac, dbConection);
                SQLiteCommand commandReg = new SQLiteCommand(sqldeleteReg, dbConection);

                commandPac.Parameters.AddWithValue("@name", lblName.Text);
                commandReg.Parameters.AddWithValue("@Pacientas_ID", RegistrationInf.PacientoID);

                commandPac.ExecuteNonQuery();
                commandReg.ExecuteNonQuery();

                dbConection.Close();
                MessageBox.Show("Deleted! ");


            }
            else if (PersonInf.Status == "Gydytojas")
            {
                string selected = String.Format(lblName.Text + "  " + lblSurname.Text + " ----- Deleted from DataBase!");
                string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
                SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);
                dbConection.Open();

                string sqldelete = $"delete from tbl_Gydytojas where name=@name";
                string sqldeleteReg = $"delete from tbl_Registration where Gydytojas_ID=@Gydytojas_ID";
                string sqlGydytojas = "select * from tbl_Gydytojas;";

                SQLiteCommand commandGydytojas = new SQLiteCommand(sqlGydytojas, dbConection);

                SQLiteDataReader readerGydytojas = commandGydytojas.ExecuteReader();

                while (readerGydytojas.Read())
                {

                    if (lblName.Text == Convert.ToString(readerGydytojas[2]))
                    {

                        RegistrationInf.GydytojoID = Convert.ToString(readerGydytojas[0]);


                    }

                }


                SQLiteCommand commandGyd = new SQLiteCommand(sqldelete, dbConection);
                SQLiteCommand commandReg = new SQLiteCommand(sqldeleteReg, dbConection);


                commandGyd.Parameters.AddWithValue("@name", lblName.Text);
                commandReg.Parameters.AddWithValue("@Pacientas_ID", Convert.ToString(person.ID));

                commandGyd.ExecuteNonQuery();
                commandReg.ExecuteNonQuery();

                dbConection.Close();
                MessageBox.Show("Deleted! ");
            }
            else
            {
                MessageBox.Show("Jus Jau Pasalinuote Sita Useri ");
            }
        }
    }
}
