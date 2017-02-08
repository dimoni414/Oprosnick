using Lab19_Oprosnik.Services;
using System.Windows;

namespace Lab19_Oprosnik
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var windowManager = new WindowManagerService();
            windowManager.Show(WindowType.Login, null);
            base.OnStartup(e);
        }
    }
}