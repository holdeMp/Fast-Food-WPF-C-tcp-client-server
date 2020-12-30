using System.Windows;
using System.Windows.Controls;

namespace IPZ_fast_food_
{ 
    public partial class ViewGoods : UserControl
    {
        public ViewGoods()
        {
            InitializeComponent();
        }

        private void Burgers_Click(object sender, RoutedEventArgs e)
        {
            GridPrinc.Children.Clear();
            GridPrinc.Children.Add(new BurgersADD());
        }
        private void Beverages_Click(object sender, RoutedEventArgs e)
        {
           
            GridPrinc.Children.Clear();
            GridPrinc.Children.Add(new BeveragesADD());
        }
        private void Chicken_Click(object sender, RoutedEventArgs e)
        {
            GridPrinc.Children.Clear();
            GridPrinc.Children.Add(new ChickenADD());
        }
        private void Happy_Click(object sender, RoutedEventArgs e)
        {
            GridPrinc.Children.Clear();
            GridPrinc.Children.Add(new HappyMealADD());
        }

    }
}
