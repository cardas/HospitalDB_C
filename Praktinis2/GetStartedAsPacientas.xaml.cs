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
using System.IO;
using Microsoft.Win32;
using Path = System.IO.Path;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;

namespace Praktinis2
{

    public partial class GetStartedAsPacientas : Window
    {

        Backend.PersonDB PersonInf = new Backend.PersonDB();
        RegistrationDB RegistrationInf = new RegistrationDB();
        RegistrationDBSet RegistrationSet = new RegistrationDBSet();

        Person person = new Person();

        public GetStartedAsPacientas(string Str_Value)
        {
            InitializeComponent();
            Calendar1.DisplayDateStart = DateTime.Now;
            GrabName.Content = Str_Value;
            ShowPac(Str_Value);
            ShowGyd(Str_Value);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.LineNumber()) + " Line Eror ", "Login Denied", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            File.Copy(TextBox1.Text, Path.Combine(@"C:\Users\s034240\Desktop\Praktinis2\Praktinis2\bin\Debug\Pictures", Path.GetFileName(TextBox1.Text)), true);
            label1.Content = "";
            label1.Content = "Image File Saved Successfully!";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            PersonInf.Name = lblName.Text;
            PersonInf.about = lblDescription.Text;
            PersonInf.Surname = lblSurname.Text;
            PersonInf.Age = lblAge.Text;
            PersonInf.contact = lblContact.Text;
            PersonInf.image = Path.GetFileName(TextBox1.Text); // Convert.ToString(image1.Source)


            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);

            string sql1 = $"update tbl_Pacientas set surname=@surname where name=@name";
            string sql2 = $"update tbl_Pacientas set age=@age where name=@name";
            string sql3 = $"update tbl_Pacientas set contact=@contact where name=@name";
            string sql4 = $"update tbl_Pacientas set about=@about where name=@name";
            string sql5 = $"update tbl_Pacientas set Picture=@Picture where name=@name";

            SQLiteCommand command1 = new SQLiteCommand(sql1, dbConection);
            SQLiteCommand command2 = new SQLiteCommand(sql2, dbConection);
            SQLiteCommand command3 = new SQLiteCommand(sql3, dbConection);
            SQLiteCommand command4 = new SQLiteCommand(sql4, dbConection);
            SQLiteCommand command5 = new SQLiteCommand(sql5, dbConection);

            dbConection.Open();
            command1.Parameters.AddWithValue("@name", PersonInf.Name);
            command2.Parameters.AddWithValue("@name", PersonInf.Name);
            command3.Parameters.AddWithValue("@name", PersonInf.Name);
            command4.Parameters.AddWithValue("@name", PersonInf.Name);
            command5.Parameters.AddWithValue("@name", PersonInf.Name);

            command1.Parameters.AddWithValue("@surname", PersonInf.Surname);
            command2.Parameters.AddWithValue("@age", PersonInf.Age);
            command3.Parameters.AddWithValue("@contact", PersonInf.contact);
            command4.Parameters.AddWithValue("@about", PersonInf.about);
            command5.Parameters.AddWithValue("@Picture", PersonInf.image);

            command1.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            command3.ExecuteNonQuery();
            command4.ExecuteNonQuery();
            command5.ExecuteNonQuery();
            dbConection.Close();
            MessageBox.Show("Modification Completed! ");
        }

        void ShowPac(string Str_Value)
        {

            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);
            string sql = "select * from tbl_Pacientas;";
            SQLiteCommand command = new SQLiteCommand(sql, dbConection);


            dbConection.Open();
            try
            {
                person.PersonLoginInfo();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    PersonInf.Name = Convert.ToString(reader[2]);
                    if (PersonInf.Name == Str_Value)
                    {

                        lblDescription.Text = Convert.ToString(reader[7]);
                        lblName.Text = Convert.ToString(reader[2]);
                        lblSurname.Text = Convert.ToString(reader[3]);
                        lblAge.Text = Convert.ToString(reader[4]);
                        lblContact.Text = Convert.ToString(reader[5]);
                        lblStatus.Text = Convert.ToString(reader[1]);


                        ImageSource itemImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Pictures\\" + Convert.ToString(reader[8])));
                        image1.Source = itemImage;

                    
                    }

                }
                dbConection.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.LineNumber()) + " Line Eror ", "Login Denied", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void ShowGyd(string Str_Value)
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
            //Got Pacientas ID
                while (readerPac.Read())
                {
                    PersonInf.Name = Convert.ToString(readerPac[2]);
                    if (PersonInf.Name == Str_Value)
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
                        RegistrationInf.Data = Convert.ToString(readerReg[2]);
                    }
                }

                //Getting This Gydytotas
                while (readerGyd.Read())
                {

                    if (RegistrationInf.GydytojoID == Convert.ToString(readerGyd[0]))
                    {

                        lblDescriptionGyd.Text = Convert.ToString(readerGyd[7]);
                        lblNameGyd.Text = Convert.ToString(readerGyd[2]);
                        lblSurnameGyd.Text = Convert.ToString(readerGyd[3]);
                        lblAgeGyd.Text = Convert.ToString(readerGyd[4]);
                        lblContactGyd.Text = Convert.ToString(readerGyd[5]);
                        ImageSource itemImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Pictures\\" + Convert.ToString(readerGyd[8])));
                        ImageGyd.Source = itemImage;


                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.LineNumber()) + " Line Eror ", "Login Denied", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void DataPasirinkimas_Click(object sender, RoutedEventArgs e)
        {

            ListControl1.Items.Clear();
            foreach (DateTime dateTime in Calendar1.SelectedDates)
            {
                
   



                /* int selectedIndex = ControlCombo.SelectedIndex;
                 Object selectedItem = ControlCombo.SelectedItem;*/
                String s = ControlCombo.Text;
                RegistrationInf.Data = (dateTime.ToShortDateString() + " " + s);
          
                MessageBox.Show("Pasirinkta: " + RegistrationInf.Data );
                ListControl1.Items.Add(RegistrationInf.Data);

            }

            


        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
        

           

            string conn = "C:\\Users\\s034240\\Desktop\\DB BRowser\\SQLiteDatabaseBrowserPortable\\Data\\Ligonine.db";
            SQLiteConnection dbConection = new SQLiteConnection("Data Source=" + conn);
            string sqlRegSelect = "select * from tbl_Registration;";
            string sqlReg = $"update tbl_Registration set Data=@Data where Pacientas_ID=@Pacientas_ID";
            SQLiteCommand commandRegSelect = new SQLiteCommand(sqlRegSelect, dbConection);
            SQLiteCommand commandRegUpdate = new SQLiteCommand(sqlReg, dbConection);
            dbConection.Open();

        


            
            SQLiteDataReader readerReg = commandRegSelect.ExecuteReader();

            while (readerReg.Read())
            {

                if (RegistrationInf.PacientoID == Convert.ToString(readerReg[1]))
                {
                   if ( Convert.ToString(readerReg[2]) == "Nenustatyta")
                    {
                        if(RegistrationInf.Data == "Nenustatyta")
                        {
                            MessageBox.Show("Is Pradziu Issirinkite Data");
                        }
                        else
                        {

                            commandRegUpdate.Parameters.AddWithValue("@Pacientas_ID", RegistrationInf.PacientoID);
                            commandRegUpdate.Parameters.AddWithValue("@Data", RegistrationInf.Data);
                            commandRegUpdate.ExecuteNonQuery();
                            MessageBox.Show("Registracija Atlikta Sekmingai");
                        
                        }
                    }
                   else
                    {
                        MessageBox.Show("Jus Jau Esate Registruotas: " + Convert.ToString(readerReg[2]));
                    }
               

                }
            }

            dbConection.Close();


        }

        private void lblDescriptionGyd_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
