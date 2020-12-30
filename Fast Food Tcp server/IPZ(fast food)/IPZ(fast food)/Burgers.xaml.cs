using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Configuration;

namespace IPZ_fast_food_
{
    /// <summary>
    /// Логика взаимодействия для Burgers.xaml
    /// </summary>
    public partial class Burgers : UserControl
    {
        DataTable BurgersTable;
        public Burgers()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            Client client = new Client();            
            BurgersTable = client.Menu("burgers");            
            BurgersGrid.ItemsSource = BurgersTable.DefaultView;            
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
