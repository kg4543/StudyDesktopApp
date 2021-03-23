using MahApps.Metro.Controls;
using System.Windows;

namespace wpfPracticeApp
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var navWin = new MetroNavigationWindow();
            navWin.Title = @"WPF Bikeshop";
            navWin.Show();
            navWin.Navigate(new MainMenu());
        }
    }
}
