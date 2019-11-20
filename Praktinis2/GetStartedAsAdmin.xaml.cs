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

namespace Praktinis2
{
    public partial class GetStartedAsAdmin : Window
    {
        public GetStartedAsAdmin()
        {
            InitializeComponent();
            BackEnd.PersonDBSet.LoadToList();
            BackEnd.PersonDBSet.LoadTypes();
            PopulateWithStatus();
          
        }
        void PopulateWithStatus()
        {
        
                TypeAdminDynamic dynamic = new TypeAdminDynamic(BackEnd.PersonDBSet.PersonStatus[0]);
                typesPanel.Children.Add(dynamic);
            TypeAdminDynamic dynamicc = new TypeAdminDynamic(BackEnd.PersonDBSet.PersonStatus[1]);
            typesPanel.Children.Add(dynamicc);
        }
        public void PopulateWithUsers(string typeName)
        {
            scrollViewer.Visibility = Visibility.Collapsed;
            scrollViewerItem.Visibility = Visibility.Visible;
            for (int i = 0; i < BackEnd.PersonDBSet.Data.Count; i++)
            {
                if (typeName == "Gydytojas")
                {
                    if (BackEnd.PersonDBSet.Data[i].Status == "Gydytojas") {
                    UserAdminDynamic dynamic = new UserAdminDynamic(BackEnd.PersonDBSet.Data[i]);
                    itemPanel.Children.Add(dynamic);
                    }
                }
                else if (typeName == "Pacientas")
                {

                    if (BackEnd.PersonDBSet.Data[i].Status == "Pacientas")
                    {
                        UserGydytojasDynamic dynamicc = new UserGydytojasDynamic(BackEnd.PersonDBSet.Data[i]);
                        itemPanel.Children.Add(dynamicc);
                    }
                    
                }

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            scrollViewer.Visibility = Visibility.Visible;
            scrollViewerItem.Visibility = Visibility.Collapsed;
        }
        private void Log_Out_Button(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
