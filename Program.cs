using System;
using System.Windows;

namespace ATMApp
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            SplashScreenWindow splashScreen = new SplashScreenWindow();
            app.Run(splashScreen); 
        }
    }
}
