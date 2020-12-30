using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IPZ_fast_food_
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            Close();
        }
        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            bool containsInt = FirstName.Text.Any(char.IsDigit);
            bool SurnamecontainsInt = LastName.Text.Any(char.IsDigit);
            Client client = new Client();

            if (Login.Text.Length == 0 || Password.Password.Length == 0 || RepeatPassword.Password.Length == 0 || FirstName.Text.Length == 0 || LastName.Text.Length == 0 || Email.Text.Length == 0)
            {
                MessageBox.Show("Fill all fields!");
            }
            else if (Login.Text.Contains("-") )
            {
                MessageBox.Show("Incorrect Login , '-' is forbiden");
            }
            else if (Password.Password.Contains("-"))
            {
                MessageBox.Show("Incorrect Password , '-' is forbiden");
            }
            else if (RepeatPassword.Password.Contains("-"))
            {
                MessageBox.Show("Incorrect RepeatPassword , '-' is forbiden");
            }
            else if (FirstName.Text.Contains("-"))
            {
                MessageBox.Show("Incorrect FirstName , '-' is forbiden");
            }
            else if (LastName.Text.Contains("-"))
            {
                MessageBox.Show("Incorrect LastName , '-' is forbiden");
            }
            else if (Email.Text.Contains("-"))
            {
                MessageBox.Show("Incorrect Email , '-' is forbiden");
            }

            else if (containsInt == true)
            {
                MessageBox.Show("Enter correct Name");
            }
            else if (SurnamecontainsInt == true)
            {
                MessageBox.Show("Enter correct Surname");
            }
            else if (!Email.Text.Contains("@") || !Email.Text.Contains("."))
            {
                MessageBox.Show("Enter correct Email");

            }
            else if (Password.Password != RepeatPassword.Password)
            {
                MessageBox.Show("Passwords does not match");

            }
            else if (Password.Password.Length < 8)
            {
                MessageBox.Show("Minimum length of Password must be 8");

            }
            
            else
            {
                if (client.LoginCheck(Login.Text) == "False")
                {
                    client.Registration("registration", FirstName.Text, LastName.Text, Login.Text, Password.Password, Email.Text);
                    MessageBox.Show("Succesful registration");

                    LogIn mainWindow = new LogIn();
                    mainWindow.Show();
                    Close();
                }

            }

        }

    }
}
