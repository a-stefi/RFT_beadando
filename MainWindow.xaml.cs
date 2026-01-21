using System.Windows;
using System.Linq;

namespace ATMApp
{
    public partial class MainWindow : Window
    {
        private int próbálkozásokSzáma = 0;
    
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
                próbálkozásokSzáma++;
                if (próbálkozásokSzáma >= 3)
                {
                    errorLabel.Content = "Túl sok hibás próbálkozás!";
                }
                else
                {
                    errorLabel.Content = "Hibás fiókszám vagy PIN kód!";
                }
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e) { }
    }
}
