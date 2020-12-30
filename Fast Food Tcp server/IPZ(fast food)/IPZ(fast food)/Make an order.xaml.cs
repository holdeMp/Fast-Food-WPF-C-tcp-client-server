using System.Windows;
using System.Windows.Controls;

namespace IPZ_fast_food_
{
    public partial class Makeorder : UserControl
    {
        public static string search { get; set; }
        public Makeorder()
        {
            InitializeComponent();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (Search.Text.Contains("-"))
            {
                MessageBox.Show("Incorrect search query . Query contains '-'");
            }
            else
            {
                search = Search.Text;
                GridPrin.Children.Clear();
                GridPrin.Children.Add(new SearchResult());
            }
        }
        private void View_Click(object sender, RoutedEventArgs e)
        {
            GridPrin.Children.Clear();
            GridPrin.Children.Add(new ViewGoods());
        }
        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            GridPrin.Children.Clear();
            GridPrin.Children.Add(new Cart());
        }
      
    }
}
