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


    public partial class MainWindow : Window
    {

      
        Person person = new Person();

        Backend.PersonDB PersonData = new Backend.PersonDB();
   /*     BackEnd.PersonDBSet PersonSet = new BackEnd.PersonDBSet();*/



        public MainWindow()
        {

            InitializeComponent();

        }



        ////////////////////////////////////////////////////////////////////////
        ////////////////////LOGIN////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string uName = TxtName.Text;
            string uPassword = TxtPass.Text;
            string uGeneral = uName + "," + uPassword;
            ////////////////////LOGIN AND PASS TIKRINIMAS////////////////////////////////////////////

            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);

            string sqlGydytojas = "select * from tbl_Gydytojas;";
            string sqlPacientas = "select * from tbl_Pacientas;";

            SQLiteCommand commandGydytojas = new SQLiteCommand(sqlGydytojas, dbConection);
            SQLiteCommand commandPacientas = new SQLiteCommand(sqlPacientas, dbConection);



        


            dbConection.Open();




            //   SQLiteDataReader reader = command.ExecuteReader();
            //  while (reader.Read()) { }

            if (TxtName.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtName.Focus();
                return;            }
            if (TxtPass.Text == "")
            {
                MessageBox.Show("Please enter Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtName.Focus();
                return;            }

            try
            {


                person.PersonLoginInfo();

                if (person.PersonLogin.Contains(uGeneral))
                {


                    SQLiteDataReader readerGydytojas = commandGydytojas.ExecuteReader();
                    SQLiteDataReader readerPacientas = commandPacientas.ExecuteReader();

                    while (readerGydytojas.Read())
                    {
                        PersonData.Name = Convert.ToString(readerGydytojas[2]);
        
                        if (uName == PersonData.Name)
                        {

                            PersonData.User_Status = Convert.ToString(readerGydytojas[9]);
                            PersonData.Status = Convert.ToString(readerGydytojas[1]);

                            if (PersonData.User_Status == "Admin")
                            {

                                MessageBox.Show("WELCOME Admin: " + TxtName.Text + "!" );  
                                this.Hide();
                                GetStartedAsAdmin LogedInAdmin = new GetStartedAsAdmin();
                                LogedInAdmin.Show();

                            }

                         
                          else if (PersonData.Status == "Gydytojas") 
                                {  
                                    MessageBox.Show("WELCOME Gydytaju: " + TxtName.Text + "!");
                                    this.Hide();
                                    GetStartedAsGydytojas LogedInUserDoc = new GetStartedAsGydytojas();
                                    LogedInUserDoc.Show();

                           }
                          /*      if (PersonData.Status == "Pacientas")
                                {
                                    MessageBox.Show("WELCOME Pacientas: " + TxtName.Text + "!");
                                    this.Hide();
                                    GetStartedAsPacientas LogedInUserPac = new GetStartedAsPacientas(TxtName.Text);
                                    LogedInUserPac.Show();

                                }
*/

                            
                        }
                    }

                    while (readerPacientas.Read())
                    {
                        PersonData.Name = Convert.ToString(readerPacientas[2]);

                        if (uName == PersonData.Name)
                        {

                            PersonData.User_Status = Convert.ToString(readerPacientas[9]);
                            PersonData.Status = Convert.ToString(readerPacientas[1]);

                            if (PersonData.User_Status == "Admin")
                            {

                                MessageBox.Show("WELCOME Admin: " + TxtName.Text + "!");
                                this.Hide();
                                GetStartedAsAdmin LogedInAdmin = new GetStartedAsAdmin();
                                LogedInAdmin.Show();

                            }



                            if (PersonData.Status == "Pacientas")
                            {
                                MessageBox.Show("WELCOME Pacientas: " + TxtName.Text + "!");
                                this.Hide();

                                GetStartedAsPacientas LogedInUserPac = new GetStartedAsPacientas(TxtName.Text);
                                LogedInUserPac.Show();

                            }



                        }
                    }
                    
                    dbConection.Close();

                }
                else
                {
                    MessageBox.Show("Login Failed.. Try again !", "Login Denied", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
            }
            catch(Exception ex)
            {

          
                MessageBox.Show(Convert.ToString(ex.LineNumber()) + " Line Eror ", "Login Denied", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Register reg = new Register();
            reg.Show();
        }
    }
}
