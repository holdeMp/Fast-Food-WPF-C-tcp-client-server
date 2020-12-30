using System.Windows;
using System.Windows.Controls;

namespace IPZ_fast_food_
{
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Burgers_Click(object sender, RoutedEventArgs e)
        {
           
            GridPrinc.Children.Clear();
            GridPrinc.Children.Add(new Burgers());
        }
        private void Beverages_Click(object sender, RoutedEventArgs e)
        {
           
            GridPrinc.Children.Clear();
            GridPrinc.Children.Add(new Beverages());
        }
        private void Chicken_Click(object sender, RoutedEventArgs e)
        {          
            GridPrinc.Children.Clear();
            GridPrinc.Children.Add(new Chicken());
        }
        private void Happy_Click(object sender, RoutedEventArgs e)
        {
            GridPrinc.Children.Clear();
            GridPrinc.Children.Add(new HappyMeal());
        }
    }
}
