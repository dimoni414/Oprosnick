using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.View;
using Lab19_Oprosnik.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab19_Oprosnik.Services
{
    public enum WindowType
    {
        Admin,
        Login,
        Main,
        Register
    }

    public class WindowManagerService : IWindowManager
    {
        public WindowManagerService()
        {
            _dictionaryWindows = new Dictionary<WindowType, Window>();
        }

        private Window SwitchWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.Admin:
                    return new AdminWindow();

                case WindowType.Login:
                    return new LoginWindow();

                case WindowType.Main:
                    return new MainWindow();

                case WindowType.Register:
                    return new RegisterWindow();

                default:
                    throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null);
            }
        }

        private IViewModel SwitchViewModel(WindowType windowType, string param)
        {
            switch (windowType)
            {
                case WindowType.Admin:
                    return new AdminViewModel(this, param);

                case WindowType.Login:
                    return new LoginViewModel(this);

                case WindowType.Main:
                    return new MainViewModel(this, param);

                case WindowType.Register:
                    return new RegisterViewModel(this, param);

                default:
                    throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null);
            }
        }

        public void Show(WindowType windowType, string param)
        {
            var window = SwitchWindow(windowType);
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window.DataContext = SwitchViewModel(windowType, param);

            _dictionaryWindows.Add(windowType, window);
            window.Show();
        }

        public void Close(WindowType windowType)
        {
            _dictionaryWindows[windowType].Close();
        }

        private readonly Dictionary<WindowType, Window> _dictionaryWindows;
    }
}