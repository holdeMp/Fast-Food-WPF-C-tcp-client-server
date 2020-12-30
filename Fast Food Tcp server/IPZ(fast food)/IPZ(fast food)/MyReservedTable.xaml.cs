using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace IPZ_fast_food_
{
    public partial class MyReservedTable : UserControl
    {
        DataTable BurgersTable;
        public MyReservedTable()
        {
            InitializeComponent();         
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            Client client = new Client();
            BurgersTable = client.MyOrders(LogIn.login, "myreservedtables-");
            BurgersGrid.ItemsSource = BurgersTable.DefaultView;           
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void Drop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String ProductId = dataRowView[1].ToString();                
                Client client = new Client();
                string login = LogIn.login;
                client.DeleteReservations(login);
                BurgersGrid.ItemsSource = null;
                BurgersTable = client.MyOrders(login, "myreservedtables-");
                BurgersGrid.ItemsSource = BurgersTable.DefaultView;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}




