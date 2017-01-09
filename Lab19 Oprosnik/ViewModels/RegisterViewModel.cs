using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Factory;
using Lab19_Oprosnik.Services;
using Lab19_Oprosnik.Validation;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Lab19_Oprosnik.ViewModels
{
    public class RegisterViewModel : ViewModelBase, IViewModel, IDataErrorInfo
    {
        public ICommand RegistrationCommand { get; private set; }

        #region Properties

        public string Email
        {
            get { return _email; }
            set { UpdateValue(value, ref _email); }
        }

        public string PasswordStr1
        {
            get { return _passwordStr1; }
            set
            {
                UpdateValue(value, ref _passwordStr1);
                OnPropertyChanged(nameof(LengthPassw1));
                ColorPassw1Brush = ChooseColorPassw(PasswordStr1);
            }
        }

        public string PasswordStr2
        {
            get { return _passwordStr2; }
            set
            {
                UpdateValue(value, ref _passwordStr2);
                OnPropertyChanged(nameof(LengthPassw2));
                ColorPassw2Brush = ChooseColorPassw(PasswordStr2);
            }
        }

        public DateTime? SelectedDateBirthday
        {
            get { return _selectedDateBirthday; }
            set { UpdateValue(value, ref _selectedDateBirthday); }
        }

        public bool Sex
        {
            get { return _sex; }
            set { UpdateValue(value, ref _sex); }
        }

        public string LengthPassw1
        {
            get { return PasswordStr1.Length.ToString(); }
            set { }
        }

        public string LengthPassw2
        {
            get { return PasswordStr2.Length.ToString(); }
            set { }
        }

        public Brush ColorPassw1Brush
        {
            get { return _colorPassw1Brush; }
            set { UpdateValue(value, ref _colorPassw1Brush); }
        }

        public Brush ColorPassw2Brush
        {
            get { return _colorPassw2Brush; }
            set { UpdateValue(value, ref _colorPassw2Brush); }
        }

        public DateTime DateTimeLastBirthday { get; set; }

        #endregion Properties

        public RegisterViewModel(WindowManagerService windowManagerService, CommandFactory commandFactory, User user)
        {
            _windowManagerService = windowManagerService;
            Email = user.Email;
            Sex = true;
            DateTimeLastBirthday = GetAdultDate();
            SelectedDateBirthday = null;
            ColorPassw1Brush = ColorPassw2Brush = new SolidColorBrush(new Color() { A = 255, B = 203, G = 215, R = 203 });

            _registerPeopleService = new RegAndLoginPeopleService();
            _validationRegisterData = new ValidationRegisterData();

            RegistrationCommand = new RelayCommand<object>(RegisterNewPeople);
        }

        private DateTime GetAdultDate() => DateTime.Now.AddYears(-18);

        private void RegisterNewPeople(object param)
        {
            string resStr;
            if (string.IsNullOrEmpty(resStr = _validationRegisterData.FinalValidationAll
                (Email, PasswordStr1, PasswordStr2, SelectedDateBirthday)))
            {
                try
                {
                    if (_registerPeopleService.AddNewPeople(Email, PasswordStr2, SelectedDateBirthday?.ToShortDateString(), Sex, false))
                    {
                        MessageBox.Show("Вы упешно зарегистрированы!");
                        ((LoginViewModel)_windowManagerService.GetViewModel(WindowType.Login)).Email = Email;
                        _windowManagerService.Close(WindowType.Register);
                    }
                    else
                    {
                        MessageBox.Show("Что-то пошло не так... Регистрация не была произведена!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Что-то пошло не так... Регистрация не была произведена! Cкорее всего вы ввели слишком длинные данные ");
                    _windowManagerService.Close(WindowType.Register);
                }
            }
            else
            {
                MessageBox.Show(resStr, "Ошибка регистрации");
            }
        }

        #region Validation

        public string this[string columnName] => _validationRegisterData.Validate(columnName, this);

        public string Error { get; set; }

        private Brush ChooseColorPassw(string str)
        {
            var count = str.Length;
            if (count > 8)
            {
                return new SolidColorBrush(new Color() { A = 255, B = 15, G = 235, R = 38 });
            }
            else if (count > 5)
            {
                return new SolidColorBrush(new Color() { A = 255, B = 15, G = 229, R = 235 });
            }
            else if (count > 3)
            {
                return new SolidColorBrush(new Color() { A = 255, B = 15, G = 134, R = 235 });
            }

            return new SolidColorBrush(new Color() { A = 255, B = 9, G = 9, R = 179 });
        }

        #endregion Validation

        private string _email;
        private string _passwordStr1;
        private string _passwordStr2;
        private DateTime? _selectedDateBirthday;
        private bool _sex;

        private Brush _colorPassw1Brush;
        private Brush _colorPassw2Brush;

        private readonly RegAndLoginPeopleService _registerPeopleService;
        private readonly WindowManagerService _windowManagerService;
        private readonly ValidationRegisterData _validationRegisterData;
    }
}