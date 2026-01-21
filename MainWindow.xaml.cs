using System.Windows;
using System.Linq;

namespace ATMApp
{
    public partial class MainWindow : Window
    {
        private int próbálkozásokSzáma = 0;
        private bool tiltottBejelentkezés = false;
        private DispatcherTimer tiltásiIdőzítő;
        private int hátralévőMásodpercek;
    
        public MainWindow()
        {
            InitializeComponent();
            tiltásiIdőzítő = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            tiltásiIdőzítő.Tick += TiltásiIdőzítő_Tick;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (tiltottBejelentkezés)
            {
                errorLabel.Content = $"Bejelentkezés tiltva {hátralévőMásodpercek} másodpercig!";
                return;
            }
        
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
        
        private void TiltásIndítása()
        {
            tiltottBejelentkezés = true;
            hátralévőMásodpercek = 30;
            errorLabel.Content = "Túl sok hibás próbálkozás!";
            próbálkozásokSzáma = 0;
            tiltásiIdőzítő.Start();
        }

        private void TiltásiIdőzítő_Tick(object sender, EventArgs e)
        {
            hátralévőMásodpercek--;
            if (hátralévőMásodpercek <= 0)
            {
                tiltásiIdőzítő.Stop();
                tiltottBejelentkezés = false;
                errorLabel.Content = "Újrapróbálkozhat.";
            }
            else
            {
                errorLabel.Content = $"Bejelentkezés tiltva {hátralévőMásodpercek} másodpercig!";
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e) { }

        private void ForgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.ShowDialog();
        }
    }
}
