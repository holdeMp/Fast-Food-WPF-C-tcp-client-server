using System.Windows;

namespace IPZ_fast_food_
{
    public partial class Confirm : Window
    {
        
        public Confirm()
        {
            InitializeComponent();
            Client client = new Client();
            LogIn logIn = new LogIn();
            string login = LogIn.login;
            Summ.Text = "       Total price is:"+client.Confirm(login)+ "€";
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            LogIn logIn = new LogIn();
            string login = LogIn.login;
            client.ConfirmOrder(login);
            Close();
        }
    }
}
