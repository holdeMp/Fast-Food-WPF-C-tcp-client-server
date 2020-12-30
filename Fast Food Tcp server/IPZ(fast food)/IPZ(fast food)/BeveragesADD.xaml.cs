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
    public partial class BeveragesADD : UserControl
    {
     
        DataTable BurgersTable;
        public BeveragesADD()
        {
            InitializeComponent();           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            Client client = new Client();         
            BurgersTable = client.Menu("beverages");         
            BurgersGrid.ItemsSource = BurgersTable.DefaultView;            
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String ProductName = dataRowView[0].ToString();
                String ProductPrice = dataRowView[2].ToString();
                String ProductDescription = dataRowView[1].ToString();
                String ProductImage = dataRowView[3].ToString();
                Client client = new Client();
                LogIn logIn = new LogIn();
                string login = LogIn.login;
                client.AddToOrders(login, ProductName, ProductDescription, ProductPrice, ProductImage);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}