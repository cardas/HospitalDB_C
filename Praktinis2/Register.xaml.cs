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
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;
using Path = System.IO.Path;

namespace Praktinis2
{
   
    public partial class Register : Window
    {



        Person grabperson = new Person();
        Backend.PersonDB person = new Backend.PersonDB();
        RegistrationDB personReg = new RegistrationDB();
        public Register()
        {

            InitializeComponent();

        }

   

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          

            if (image1.Source == null )
            {
                MessageBox.Show("Idekite Nuotrauka");
                return;
            }
            if (this.TxtAbout.Text.Trim() == String.Empty || this.TxtAge.Text.Trim() == String.Empty || this.TxtContact.Text.Trim() == String.Empty || this.TxtName.Text.Trim() == String.Empty || this.TxtPassword.Text.Trim() == String.Empty || this.TxtSurname.Text.Trim() == String.Empty )
            {
                MessageBox.Show("Uzpildikite Visus Laukus");
                return;
            }
            if (RadioGydytojas.IsChecked == false)
            {
                if ( RadioPacientas.IsChecked == false )
                {
                    MessageBox.Show("Pazymekite RadioButton, bent viena");
                    return;
                }
            }

               if (RadioPacientas.IsChecked == false)
               {
                   if (RadioGydytojas.IsChecked == false)
                   {
                       MessageBox.Show("Pazymekite RadioButton, bent viena");
                       return;
                   }
               }
            /////////////////////////INSERTING DATA///////////////////////////////////////////
            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);

            dbConection.Open();
            SQLiteCommand cmdGydytojas = new SQLiteCommand("insert into tbl_Gydytojas values(@ID, @status, @name, @surname,@password,@age,@contact,@about,@picture,@User_Stat)", dbConection);
            SQLiteCommand cmdPacientas = new SQLiteCommand("insert into tbl_Pacientas values(@ID, @status, @name, @surname,@password,@age,@contact,@about,@picture,@User_Stat)", dbConection);
            SQLiteCommand cmdReg = new SQLiteCommand("insert into tbl_Registration values(@Gydytojas_ID,@Pacientas_ID,@Data)", dbConection);


            string sqlGydytojas = "select * from tbl_Gydytojas;";
            string sqlPacientas = "select * from tbl_Pacientas;";


            SQLiteCommand commandGydytojas = new SQLiteCommand(sqlGydytojas, dbConection);
            SQLiteCommand commandPacientas = new SQLiteCommand(sqlPacientas, dbConection);

            SQLiteDataReader readerGydytojas = commandGydytojas.ExecuteReader();
            SQLiteDataReader readerPacientas = commandPacientas.ExecuteReader();

            ////////////////////////////////RadioButtons To DataBase //////////////////////////
            if (RadioPacientas.IsChecked == true)
            {
              
                int i = 1;
                while (readerPacientas.Read())
                {
                    i++;
                }

              
                cmdPacientas.Parameters.AddWithValue("@ID", Convert.ToString(i));
                cmdPacientas.Parameters.AddWithValue("@status", RadioPacientas.Content);
                ////////////////////////////////////////////////////////////////////////////////////
                cmdPacientas.Parameters.AddWithValue("@name", TxtName.Text.Trim());
                cmdPacientas.Parameters.AddWithValue("@surname", TxtSurname.Text.Trim());
                cmdPacientas.Parameters.AddWithValue("@password", TxtPassword.Text.Trim());
                cmdPacientas.Parameters.AddWithValue("@age", TxtAge.Text.Trim());
                cmdPacientas.Parameters.AddWithValue("@contact", TxtContact.Text.Trim());
                cmdPacientas.Parameters.AddWithValue("@about", TxtAbout.Text.Trim());
                cmdPacientas.Parameters.AddWithValue("@User_Stat", "User");
                ////////////////////////////////////////////////////////////////////////////////////

                //////////////////////////////// Image To DataBase //// /////////////////////////////
                cmdPacientas.Parameters.AddWithValue("@picture", Path.GetFileName(TextBox1.Text));
    
                cmdPacientas.ExecuteNonQuery();

                cmdReg.Parameters.AddWithValue("@Gydytojas_ID", Convert.ToString(personReg.GydytojoID));
                cmdReg.Parameters.AddWithValue("@Pacientas_ID", Convert.ToString(i));
                cmdReg.Parameters.AddWithValue("@Data", Convert.ToString("Nenustatyta"));

                cmdReg.ExecuteNonQuery();

                dbConection.Close();

                MessageBox.Show("Registration Is Successful");

                Clear();
            }
            else if (RadioGydytojas.IsChecked == true)
            {


                int i = 1;
                while (readerGydytojas.Read())
                {
                    i++;
                }

                cmdGydytojas.Parameters.AddWithValue("@ID", Convert.ToString(i));
                cmdGydytojas.Parameters.AddWithValue("@status", RadioGydytojas.Content);
                ////////////////////////////////////////////////////////////////////////////////////
                cmdGydytojas.Parameters.AddWithValue("@name", TxtName.Text.Trim());
                cmdGydytojas.Parameters.AddWithValue("@surname", TxtSurname.Text.Trim());
                cmdGydytojas.Parameters.AddWithValue("@password", TxtPassword.Text.Trim());
                cmdGydytojas.Parameters.AddWithValue("@age", TxtAge.Text.Trim());
                cmdGydytojas.Parameters.AddWithValue("@contact", TxtContact.Text.Trim());
                cmdGydytojas.Parameters.AddWithValue("@about", TxtAbout.Text.Trim());
                cmdGydytojas.Parameters.AddWithValue("@User_Stat", "User");
                ////////////////////////////////////////////////////////////////////////////////////

                //////////////////////////////// Image To DataBase //// /////////////////////////////
                cmdGydytojas.Parameters.AddWithValue("@picture", Path.GetFileName(TextBox1.Text));

                cmdGydytojas.ExecuteNonQuery();
                dbConection.Close();
                MessageBox.Show("Registration Is Successful");

                Clear();
            }
   

  
        }
        void Clear()
        {
            TxtAbout.Text = TxtAge.Text = TxtName.Text = TxtPassword.Text = TxtContact.Text = TxtSurname.Text = "";
            RadioGydytojas = RadioPacientas = null;
            image1.Source = null;
        }

   

  
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    TextBox1.Text = op.FileName;
                    image1.Source = new BitmapImage(new Uri(op.FileName));
                }

            }
            catch
            {

            }
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            MainWindow LogIn = new MainWindow();
            LogIn.Show();


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            File.Copy(TextBox1.Text, Path.Combine(@"C:\Users\s034240\Desktop\Praktinis2\Praktinis2\bin\Debug\Pictures", Path.GetFileName(TextBox1.Text)),true);
            label1.Content = "";
            label1.Content = "Image File Saved Successfully!";       
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ViewerGydytojai.Visibility = Visibility.Visible;


            grabperson.GydytojasInfo();

            foreach (string i in grabperson.Gydytojas)
            {
                ViewerGydytojai.Items.Add(i);
            }

         






        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {


           
            string text = Convert.ToString(ViewerGydytojai.SelectedItem);
         



            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);

            string sqlGydytojas = "select * from tbl_Gydytojas;";


            SQLiteCommand commandGydytojas = new SQLiteCommand(sqlGydytojas, dbConection);

            dbConection.Open();


            SQLiteDataReader readerGydytojas = commandGydytojas.ExecuteReader();
            string uGeneral;
            while (readerGydytojas.Read())
            {

                person.Name = Convert.ToString(readerGydytojas[2]);
                person.about = Convert.ToString(readerGydytojas[7]);
                uGeneral = person.Name + "," + person.about;
                if (uGeneral == text)
                {

                    personReg.GydytojoID = Convert.ToString(readerGydytojas[0]);
                   
                }

            }





        }
    }
}
