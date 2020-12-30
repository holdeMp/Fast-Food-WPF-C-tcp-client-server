using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace IPZ_fast_food_
{
    public partial class Reservation : UserControl
    {
       
        DataTable BurgersTable;
        public Reservation()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            Client client = new Client();           
            BurgersTable = client.Menu("seatreserve");                        
            BurgersGrid.ItemsSource = BurgersTable.DefaultView;           
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void ReservedTable(object sender, RoutedEventArgs e) {

            
            GridPri.Children.Clear();
            GridPri.Children.Add(new ReservedTables());
        }
        private void MyReservedTable(object sender, RoutedEventArgs e)
        {
            GridPri.Children.Clear();
            GridPri.Children.Add(new MyReservedTable());
        }
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String ProductName = dataRowView[1].ToString();               
                DateTime aDate = Convert.ToDateTime(pickerafter.Text);
                DateTime bDate = Convert.ToDateTime(pickerbefore.Text);
                DateTime cDate = DateTime.Now;
                cDate = cDate.AddMinutes(59);                
                string SearchREs = LogIn.login;
                Client client = new Client();
                if (aDate < cDate) {
                    MessageBox.Show("After Date must be from 1 hour of current time");
                }
                else if (aDate > bDate)
                {
                    MessageBox.Show("Incorrect Date ");
                }
                else
                {
                    aDate =aDate.AddMinutes(59);
                    if (aDate < bDate) {
                        aDate = aDate.AddHours(2);
                        aDate = aDate.AddMinutes(2);
                        if (aDate > bDate)
                        {
                            if (client.TableCheck(SearchREs) == "False")
                            {
                                client.ReserveTable(SearchREs, ProductName, pickerafter.Text, pickerbefore.Text);
                                MessageBox.Show("Succesful reservation!");
                            }
                        }
                                                else
                            MessageBox.Show("Maximum reservation time is 3 hours");
                    }                        
                    else
                    {
                     
                            MessageBox.Show("Minimum reservation time is 1 hour");
                    }                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
