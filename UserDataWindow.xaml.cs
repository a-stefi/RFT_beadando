using System.Windows;

namespace ATMApp
{
    public partial class UserDataWindow : Window
    {
        private readonly User _user;

        public UserDataWindow(User user)
        {
            InitializeComponent();  
            _user = user;
            DisplayUserData();
        }

        private void DisplayUserData()
        {
            usernameTextBlock.Text = _user.Username;
            accountNumberTextBlock.Text = _user.AccountNumber;
            phoneNumberTextBlock.Text = _user.PhoneNumber;
            birthPlaceTextBlock.Text = _user.BirthPlace;
            birthDateTextBlock.Text = _user.BirthDate.ToShortDateString();
        }
    }
}
