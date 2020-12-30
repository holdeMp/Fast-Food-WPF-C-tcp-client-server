using System.Windows;

namespace IPZ_fast_food_
{
    public partial class LogIn : Window
    {
        public static string login { get; set; }
        Client client = new Client();
        public LogIn()
        {
            InitializeComponent();
            client.Connect();
        }
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

            if (Login.Text.Length == 0 || Password.Password.Length == 0)
            {
                MessageBox.Show("Fill all fields!");
            }
            else
            {
                if(client.Login("login", Login.Text, Password.Password)) {
                    login = Login.Text;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }               
            }
        }
        private void Reg_Click(object sender, RoutedEventArgs e)
        {           
            Registration reg = new Registration();
            reg.Show();
            Close();
        }
    }
}
