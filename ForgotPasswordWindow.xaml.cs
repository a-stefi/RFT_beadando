using System.Linq;
using System.Windows;

namespace ATMApp
{
    public partial class ForgotPasswordWindow : Window
    {
        private readonly UserRepository _repository;

        public ForgotPasswordWindow()
        {
            InitializeComponent();
            _repository = new UserRepository();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string accountNumber = AccountNumberBox.Text;
            string newPassword = NewPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(accountNumber) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("A jelszavak nem egyeznek!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var users = _repository.LoadUsers();
            var user = users.FirstOrDefault(u => u.Username == username && u.AccountNumber == accountNumber);

            if (user == null)
            {
                MessageBox.Show("Nem található ilyen felhasználó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            user.Pin = newPassword;
            _repository.SaveUsers(users);

            MessageBox.Show("Jelszó sikeresen módosítva!", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
