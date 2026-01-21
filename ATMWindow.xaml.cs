using System.Windows;

namespace ATMApp
{
    public partial class ATMWindow : Window
    {
        private readonly User _currentUser;
        private readonly UserRepository _repository;
        private int egyenleg;
        private List<string> tranzakciók;
        private ObservableCollection<dynamic> naplóAdatok;

        public ATMWindow(User currentUser)
        {
            InitializeComponent();
            transactionList.ItemsSource = naplóAdatok;
            _repository = new UserRepository();
            _currentUser = _repository.LoadUsers().First(u => u.AccountNumber == currentUser.AccountNumber);
            egyenleg = _currentUser.Balance;

            tranzakciók = new List<string>(_currentUser.Transactions ?? new List<string>());
        }

        private void MentésFelhasználóra()
        {
            var users = _repository.LoadUsers();
            var currentUser = users.FirstOrDefault(u => u.AccountNumber == _currentUser.AccountNumber);
            if (currentUser != null)
            {
                currentUser.Balance = _currentUser.Balance;
                _repository.SaveUsers(users);
            }
        }
        
        private void BalanceCheck_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Jelenlegi egyenleg: {_currentUser.Balance} Ft", "Egyenleg", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void WithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            private void WithdrawButton_Click(object sender, RoutedEventArgs e)
            {
                int összeg = KérÖsszeg("Kérem adja meg a felvenni kívánt összeget:");
                if (összeg > _currentUser.Balance)
                {
                    MessageBox.Show("Nincs elegendő egyenleg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (összeg > 0)
                {
                    _currentUser.Balance -= összeg;
                    MessageBox.Show("Sikeres pénzfelvétel!");
                }
            }
        }
        
        private void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            int összeg = KérÖsszeg("Kérem adja meg a befizetni kívánt összeget:");
            if (összeg > 0) 
            {
                _currentUser.Balance += összeg;
                MentésFelhasználóra();
                    
                MessageBox.Show("Sikeres befizetés!");
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

        private void NaplózTranzakció(string tranzakció)
        {
            tranzakciók.Add(tranzakció);
            MentésFelhasználóra();
            FrissítTranzakciók();
        }

        private void FrissítTranzakciók()
        {
            naplóAdatok.Clear();
            foreach (var tranzakció in tranzakciók)
            {
                var időpont = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
                naplóAdatok.Add(new { Típus = tranzakció, Időpont = időpont });
            }
        }

        private void ToggleHistory_Click(object sender, RoutedEventArgs e)
        {
            naplóLátható = !naplóLátható;
            transactionList.Visibility = naplóLátható ? Visibility.Visible : Visibility.Collapsed;
            toggleHistoryButton.Content = naplóLátható ? "Előzmények elrejtése" : "Előzmények megjelenítése";
        }
    }
}
