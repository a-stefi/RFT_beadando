using System.Windows;

namespace ATMApp
{
    public partial class RegistrationWindow : Window
    {
        private readonly UserRepository _repository;

        public RegistrationWindow()
        {
            InitializeComponent();
            _repository = new UserRepository();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameBox.Text;
            string accountNumber = accountNumberBox.Text;
            string pin = pinBox.Password;
            string phoneNumber = phoneNumberBox.Text;
            string birthPlace = birthPlaceBox.Text;
            DateTime? birthDate = birthDatePicker.SelectedDate;
        
            if (string.IsNullOrWhiteSpace(usernameBox.Text) || 
                accountNumberBox.Text.Length != 8 || 
                pinBox.Password.Length != 4 || 
                string.IsNullOrWhiteSpace(birthPlaceBox.Text) || 
                birthDatePicker.SelectedDate == null)
            {
                errorLabel.Content = "Hibás adat! Ellenőrizze a bevitelt.";
                return;
            }

            if (!CsakBetűk(username))
            {
                errorLabel.Content = "A felhasználónév csak betűket tartalmazhat!";
                return;
            }
            
            if (!CsakBetűk(birthPlace))
            {
                errorLabel.Content = "A település neve csak betűket tartalmazhat!";
                return;
            }
            
            if (!KezdődikNagyBetűvel(birthPlace))
            {
                errorLabel.Content = "A település neve nagy kezdőbetűvel kell kezdődjön!";
                return;
            }
            
            if (!FelhasználónévÉrvényes(username))
            {
                errorLabel.Content = "A felhasználónév csak betűket tartalmazhat, és legalább 4 karakter hosszúnak kell lennie!";
                return;
            }
            
            if (!TelepülésÉrvényes(birthPlace))
            {
                errorLabel.Content = "A település neve legalább 3 betűből kell álljon, nagy kezdőbetűvel, és a többi kisbetű lehet!";
                return;
            }

            var users = _repository.LoadUsers();
            if (users.Any(u => u.AccountNumber == accountNumber))
            {
                errorLabel.Content = "Már létezik ilyen fiókszám!";
                return;
            }
            
            if (IsUnderage(születésiDátum))
            {
                MessageBox.Show("A regisztrációhoz legalább 14 évesnek kell lenned!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            var newUser = new User
            {
                Username = username,
                AccountNumber = accountNumber,
                Pin = pin,
                PhoneNumber = phoneNumber,
                BirthPlace = birthPlace,
                BirthDate = birthDate.Value
            };
            users.Add(newUser);
            _repository.SaveUsers(users);
            
            MessageBox.Show("Sikeres regisztráció!", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private bool KezdődikNagyBetűvel(string szöveg)
        {
            return !string.IsNullOrEmpty(szöveg) && char.IsUpper(szöveg[0]) && szöveg.Skip(1).All(char.IsLetter);
        }
        
        private bool CsakBetűk(string szöveg)
        {
            return !string.IsNullOrEmpty(szöveg) && szöveg.All(char.IsLetter);
        }
        
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            return (phoneNumber.StartsWith("062") || phoneNumber.StartsWith("063") || phoneNumber.StartsWith("067")) &&
                    phoneNumber.Length == 11 && phoneNumber[3] == '0' &&
                    phoneNumber.Skip(4).All(char.IsDigit);
        }

        private bool IsUnderage(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
            {
                age--;
            }
            return age < 14;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
