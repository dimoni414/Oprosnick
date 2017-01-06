using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace Lab19_Oprosnik.ViewModels
{
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

        public LoginViewModel(WindowManagerService windowManager, ICommandFactory commandFactory)
        {

            Email = "dimoni414@ya.ru";
            _regAndLoginPeopleService = new RegAndLoginPeopleService();
            _windowManager = windowManager;

            OpenRegisterWindowCommand = commandFactory.CreateCommand((param) 
                => windowManager.Show(WindowType.Register,new User(param as string)));


            TryLoginCommand = commandFactory.CreateCommand(TryLogin);
        }

        private void TryLogin(object obj)
        {
            User user;
            try
            {
                user = _regAndLoginPeopleService.FindPeople(Email, Password);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), @"Ошибка при создании человека");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return;
            }

            _windowManager.Show(user.IsAdmin ? WindowType.Admin : WindowType.Main, user);
            _windowManager.Close(WindowType.Login);
        }

        private readonly RegAndLoginPeopleService _regAndLoginPeopleService;
        private string _email;
        private string _password;
        private readonly WindowManagerService _windowManager;
    }
}