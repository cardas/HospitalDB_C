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
    /// <summary>
    /// Interaction logic for TypeDynamic.xaml
    /// </summary>
    public partial class TypeAdminDynamic : UserControl
    {
        public TypeAdminDynamic (string typeName)
        {
            InitializeComponent();
            typeLabel.Content = typeName;
        }

        private void typeButton_Click(object sender, RoutedEventArgs e)
        {
            GetStartedAsAdmin getStarted = Window.GetWindow(this) as GetStartedAsAdmin;
         
            getStarted.scrollViewer.Width = 900;
    /*        getStarted.button1.Visibility = Visibility.Visible;*/
            getStarted.itemPanel.Children.Clear();
           getStarted.PopulateWithUsers(Convert.ToString(typeLabel.Content));
      
        }
    }
}
