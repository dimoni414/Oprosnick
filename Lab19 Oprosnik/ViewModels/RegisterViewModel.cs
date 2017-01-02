using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace Lab19_Oprosnik.ViewModels
{
    public class RegisterViewModel : ViewModelBase, IViewModel
    {
        public ICommand RegistrationCommand { get; private set; }

        public string Email
        {
            get { return _email; }
            set { UpdateValue(value, ref _email); }
        }

        public string PasswordStr
        {
            get { return _passwordStr; }
            set { UpdateValue(value, ref _passwordStr); }
        }

        public string Birthday
        {
            get { return _birthday; }
            set { UpdateValue(value, ref _birthday); }
        }

        public bool Sex
        {
            get { return _sex; }
            set { UpdateValue(value, ref _sex); }
        }

        public RegisterViewModel(WindowManagerService windowManagerService, string email) // почему он добавил это сюда? : this(email)
        {
            _windowManagerService = windowManagerService;
            Email = email;
            _registerPeopleService = new RegAndLoginPeopleService();

            RegistrationCommand = new RelayCommad<object>(RegisterNewPeople);//переделать всё это на фабрики а то пиздец)))
        }

        private void RegisterNewPeople(object param)
        {
            // вызываем методы из датабейз сервиса, чтобы внести в БД нашего человечка
            // да да, я знаю, что это не очень безопасно, но другого более красивого решения я не нашел.
            // по хорошему можно хранить ключь и записывать в БД, загружать из нее парость с помощью этого ключа, чтобы там он лежал зашифрованный
            if (_registerPeopleService.AddNewPeople(Email, PasswordStr, Birthday, Sex, false))
            {
                MessageBox.Show("Вы упешно зарегистрированы!");
                _windowManagerService.Close(WindowType.Register);
            }
            else
            {
                MessageBox.Show("Что-то пошло не так... Регистрация не была произведена!");
            }
        }

        private string _email;
        private string _passwordStr;

        private RegAndLoginPeopleService _registerPeopleService;
        private string _birthday;
        private bool _sex;
        private WindowManagerService _windowManagerService;
        public Action CloseAction { get; set; }
    }
}