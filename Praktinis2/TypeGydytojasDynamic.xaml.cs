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

    public partial class TypeGydytojasDynamic : UserControl
    {
        public TypeGydytojasDynamic(string typeName)
        {
            InitializeComponent();
            typeLabel.Content = typeName;
        }
        private void typeButton_Click(object sender, RoutedEventArgs e)
        {

            GetStartedAsGydytojas getStartedDoc = Window.GetWindow(this) as GetStartedAsGydytojas;

            getStartedDoc.scrollViewer.Width = 900;
            getStartedDoc.itemPanel.Children.Clear();
            getStartedDoc.PopulateWithUsers(Convert.ToString(typeLabel.Content));
        }



    }
}
