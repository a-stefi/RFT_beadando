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
            if (string.IsNullOrWhiteSpace(usernameBox.Text) || 
                accountNumberBox.Text.Length != 8 || 
                pinBox.Password.Length != 4 || 
                string.IsNullOrWhiteSpace(birthPlaceBox.Text) || 
                birthDatePicker.SelectedDate == null)
            {
                errorLabel.Content = "Hibás adat! Ellenőrizze a bevitelt.";
                return;
            }
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
