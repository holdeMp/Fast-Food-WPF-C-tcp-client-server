using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace IPZ_fast_food_
{

    public partial class ChickenADD : UserControl
    {
       
        DataTable BurgersTable;
        public ChickenADD()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {           
            Client client = new Client();
            BurgersTable = client.Menu("chicken");
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
