using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Factory;
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
            _commandFactory = new CommandFactory();
            _dictionaryWindows = new Dictionary<WindowType, Window>();
        }

        public void Show(WindowType windowType, User user)
        {
            var window = SwitchWindow(windowType);
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window.DataContext = SwitchViewModel(windowType, user);

            if (!CanCreateWindow(windowType))
            {
                return;
            }
            _dictionaryWindows.Add(windowType, window);
            window.Show();
        }

        public void Close(WindowType windowType)
        {
            _dictionaryWindows[windowType].Close();
            _dictionaryWindows.Remove(windowType);
        }

        public IViewModel GetViewModel(WindowType windowType)
        {
            return _dictionaryWindows[windowType].DataContext as IViewModel;
        }

        private Window SwitchWindow(WindowType windowType)

        {
            Window win;

            switch (windowType)
            {
                case WindowType.Admin:
                    win = new AdminWindow();
                    break;

                case WindowType.Login:
                    win = new LoginWindow();
                    break;

                case WindowType.Main:
                    win = new MainWindow();
                    break;

                case WindowType.Register:
                    win = new RegisterWindow();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null);
            }
            win.Closing += (a, b) => _dictionaryWindows.Remove(windowType);
            return win;
        }

        private IViewModel SwitchViewModel(WindowType windowType, User user)
        {
            switch (windowType)
            {
                case WindowType.Admin:
                    return new AdminViewModel(this, _commandFactory, user);

                case WindowType.Login:
                    return new LoginViewModel(this, _commandFactory);

                case WindowType.Main:
                    return new MainViewModel(this, _commandFactory, user);

                case WindowType.Register:
                    return new RegisterViewModel(this, _commandFactory, user);

                default:
                    throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null);
            }
        }

        private bool CanCreateWindow(WindowType windowType) => !_dictionaryWindows.ContainsKey(windowType);

        private readonly Dictionary<WindowType, Window> _dictionaryWindows;
        private readonly CommandFactory _commandFactory;
    }
}