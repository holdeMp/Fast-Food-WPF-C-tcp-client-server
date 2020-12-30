using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
namespace IPZ_fast_food_
{
    public partial class MyOrders : UserControl
    {      
        DataTable BurgersTable;
        public MyOrders()
        {
            InitializeComponent();           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {          
            Client client = new Client();
            BurgersTable = client.MyOrders(LogIn.login, "myorders-");
            CollectionViewSource mycollection = new CollectionViewSource();
            mycollection.Source = BurgersTable;
            mycollection.GroupDescriptions.Add(new PropertyGroupDescription("Date_time"));
            BurgersGrid.ItemsSource = mycollection.View;            
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }       
    }
}
