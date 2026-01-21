using System.Windows;

namespace ATMApp
{
    public partial class ATMWindow : Window
    {
        private readonly User _currentUser;
        private readonly UserRepository _repository;
        private int egyenleg;

        public ATMWindow(User currentUser)
        {
            InitializeComponent();
            _repository = new UserRepository();
            _currentUser = _repository.LoadUsers().First(u => u.AccountNumber == currentUser.AccountNumber);
            egyenleg = _currentUser.Balance;
        }
        
        private void BalanceCheck_Click(object sender, RoutedEventArgs e) {}
        private void WithdrawButton_Click(object sender, RoutedEventArgs e) {}
        private void DepositButton_Click(object sender, RoutedEventArgs e) {}
    }
}
