using System;

using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;

namespace IPZ_fast_food_
{
    public partial class Cart : UserControl
    {
        DataTable BurgersTable;
        public Cart()
        {
            InitializeComponent();            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {           
            Client client = new Client();
            BurgersTable = client.Orders(LogIn.login, "userorders-");
            BurgersGrid.ItemsSource = BurgersTable.DefaultView;            
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BurgersGrid.ItemsSource = null;
                Client client = new Client();               
                BurgersTable = client.Orders(LogIn.login, "userorders-");
                BurgersGrid.ItemsSource = BurgersTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Drop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String ProductId = dataRowView[0].ToString();                
                Client client = new Client();
                client.DeleteOrders(ProductId);
                BurgersGrid.ItemsSource = null;
                BurgersTable = client.Orders(LogIn.login, "userorders-");
                BurgersGrid.ItemsSource = BurgersTable.DefaultView;              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Confirm cw ;
                BurgersGrid.ItemsSource = null;
                Client client = new Client();
                LogIn logIn = new LogIn();
                string login = LogIn.login;
                BurgersTable = client.Orders(LogIn.login, "userorders-");
                BurgersGrid.ItemsSource = BurgersTable.DefaultView;
                if (BurgersTable.Rows.Count!=0)
                {
                    cw = new Confirm();

                    cw.Show();
                }               
                else
                    MessageBox.Show("Cart is empty!");
            }           
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

    }
}