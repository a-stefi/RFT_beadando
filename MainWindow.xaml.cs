using System.Windows;
using System.Linq;

namespace ATMApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string accountNumber = accountNumberBox.Text;
            string pin = pinBox.Password;

            var users = new UserRepository().LoadUsers();
            var user = users.FirstOrDefault(u => u.AccountNumber == accountNumber && u.Pin == pin);

            if (user != null)
            {
                MessageBox.Show("Sikeres bejelentkezés!");
                this.Close();
            }
            else
            {
                errorLabel.Content = "Hibás fiókszám vagy PIN kód!";
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e) { }
    }
}
