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
        
        private void BalanceCheck_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Jelenlegi egyenleg: {_currentUser.Balance} Ft", "Egyenleg", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void WithdrawButton_Click(object sender, RoutedEventArgs e) {}
        
        private void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            private void DepositButton_Click(object sender, RoutedEventArgs e)
            {
                int összeg = KérÖsszeg("Kérem adja meg a befizetni kívánt összeget:");
                if (összeg > 0) {
                    _currentUser.Balance += összeg;
                    MessageBox.Show("Sikeres befizetés!");
                }
            }
        }

        private int KérÖsszeg(string üzenet)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(üzenet, "Összeg", "0");
            if (int.TryParse(input, out int összeg) && összeg > 0)
                return összeg;
            else
            {
                MessageBox.Show("Érvénytelen összeg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }
    }
}
