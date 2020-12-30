using System.Windows;

namespace IPZ_fast_food_
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Make_order_Click(object sender, RoutedEventArgs e)
        {

            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Makeorder());
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Menu());
        }
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Reservation());
        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            Close();
        }
        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new MyOrders());
        }
    }
}
