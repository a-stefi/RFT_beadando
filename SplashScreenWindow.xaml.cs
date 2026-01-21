using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace ATMApp
{
    public partial class SplashScreenWindow : Window
    {
        public SplashScreenWindow()
        {
            this.InitializeComponent();
            this.Loaded += OnSplashScreenLoaded;
        }

        private async void OnSplashScreenLoaded(object sender, RoutedEventArgs e)
        {
            await StartLoadingAsync(); 
            OpenMainWindow();
        }

        private async Task StartLoadingAsync()
        {
            for (int i = 1; i <= 100; i++)
            {
                loadingBar.Value = i;             
                progressText.Text = $"{i}%";    
                await Task.Delay(50);   
            }
        }

        private void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow(); 
            mainWindow.Show();                        
            this.Close();                             
        }
    }
}



