using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Services;
using Lab19_Oprosnik.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace Lab19_Oprosnik.ViewModels
{
    /// <LoginViewModel>
    /// Я еще сделаю обязательно парсинг введеных данных. пока Вьюшка - шаболнчик
    /// </LoginViewModel>
    public enum Status
    {
        Error,
        User,
        Admin
    }

    public class LoginViewModel : ViewModelBase, IViewModel
    {
        public ICommand OpenRegisterWindowCommand { get; private set; }

        public ICommand TryLoginCommand { get; private set; }

        public string Password
        {
            get { return _password; }
            set { UpdateValue(value, ref _password); }
        }

        public string Email
        {
            get { return _email; }
            set { UpdateValue(value, ref _email); }
        }

        public LoginViewModel(WindowManagerService windowManager, string s = null)
        {
            Email = "dimoni414@ya.ru";
            _regAndLoginPeopleService = new RegAndLoginPeopleService();
            _windowManager = windowManager;
            OpenRegisterWindowCommand = new RelayCommad<string>((param) => windowManager.Show(WindowType.Register, param));
            TryLoginCommand = new RelayCommad<object>(TryLogin);
        }

        private void TryLogin(object obj)
        {
            User user = null;
            try
            {
                user = _regAndLoginPeopleService.FindPeople(Email, Password);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "Ошибка при создании человека");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return;
            }

            if (user.IsAdmin)
            {
                _windowManager.Show(WindowType.Admin, user.Email);
            }
            else
            {
                _windowManager.Show(WindowType.Main, user.Email);
            }
            _windowManager.Close(WindowType.Login);

            //switch ()
            //{
            //    case Status.Error:
            //        MessageBox.Show("Не правильный логин или пароль. Попробуйте снова");
            //        break;

            //    case Status.User:
            //        _createAndOpenWindow.MainWindow(Email);
            //        App.Current.Windows[0].Close();
            //        break;

            //    case Status.Admin:
            //        _createAndOpenWindow.AdminWindow(Email);
            //        App.Current.Windows[0].Close();
            //        break;

            //    default:
            //        break;
            //}
        }

        private AdminWindow _adminWindow;
        private MainWindow _mainWindow;
        private RegAndLoginPeopleService _regAndLoginPeopleService;
        private string _email;
        private string _password;
        private WindowManagerService _windowManager;
        public Action CloseAction { get; set; }
    }
}