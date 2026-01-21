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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
