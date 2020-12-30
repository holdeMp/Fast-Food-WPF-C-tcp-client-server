using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace IPZ_fast_food_
{
    public partial class ReservedTables : UserControl
    {
        DataTable BurgersTable;
        public ReservedTables()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            Client client = new Client();
            BurgersTable = client.Menu("reservedtables");
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
