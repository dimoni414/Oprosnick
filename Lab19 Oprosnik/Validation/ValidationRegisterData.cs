using Lab19_Oprosnik.Services;
using Lab19_Oprosnik.ViewModels;
using System;

namespace Lab19_Oprosnik.Validation
{
    internal class ValidationRegisterData
    {
        public ValidationRegisterData()
        {
            _dataBaseWorkService = new DataBaseWorkService();
        }

        public string ValidateEmail(string str)
        {
            if (str.Contains("@"))
            {
                var strSplit = str.Split('@');
                if (strSplit.Length > 2)
                {
                    return "Некорректный Еmail(слишком много @)";
                }
                if (strSplit[0].Length < 3)
                {
                    return "Некорректный Еmail(Длина ника должна быть больше 3)";
                }

                if (strSplit[1].Contains("."))
                {
                    var strDots = strSplit[1].Split('.');
                    if (strDots.Length > 2)
                    {
                        return "Некорректный Еmail(Такого домена не существует)";
                    }
                    if (strDots[0].Length < 2 || strDots[1].Length < 2)
                    {
                        return "Некорректный Еmail(Такого домена не существует)";
                    }
                }
                else
                {
                    return "Некорректный Еmail(Такого домена не существует)";
                }
            }
            else
            {
                return "Некорректный Еmail(Отсутствует @)";
            }

            if (str.Length > 50)
            {
                return "Длина E-mail не должна превышать 50 символов";
            }

            return "";
        }

        public string Validate(string columnName, RegisterViewModel registerViewModel)
        {
            var error = string.Empty;
            switch (columnName)
            {
                case "Email":
                    if (!string.IsNullOrEmpty(registerViewModel.Email))
                    {
                        error = ValidateEmail(registerViewModel.Email);
                    }
                    break;

                case "PasswordStr1":

                    error = ValidatePassword(registerViewModel.PasswordStr1, registerViewModel.PasswordStr2);

                    break;

                case "PasswordStr2":
                    error = ValidatePassword(registerViewModel.PasswordStr1, registerViewModel.PasswordStr2);
                    break;

                case "SelectedDateBirthday":
                    if (registerViewModel.SelectedDateBirthday != null)
                        error = ValidateDateBirthday((DateTime)registerViewModel.SelectedDateBirthday);
                    break;
            }
            return error;
        }

        private string ValidateDateBirthday(DateTime selectedDateBirthday) =>
            selectedDateBirthday > DateTime.Now.AddYears(-18)
                ? "Регистрироваться можно только взрослым пользователям"
                : string.Empty;

        private string ValidatePassword(string pass1, string pass2)
        {
            if (pass1.Length < 50)
            {
                if (!string.IsNullOrEmpty(pass2))
                {
                    if (!string.Equals(pass1, pass2))
                    {
                        return "Пароли не совпадают";
                    }
                }
            }
            else
            {
                return "Длина пароля не должна превышать 50 символов";
            }
            return string.Empty;
        }

        public string FinalValidationAll(string email, string passwordStr1, string passwordStr2, DateTime? selectedDateBirthday)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "Заполните, пожалуйста, поле E-mail";
            }

            if (passwordStr1 == null || passwordStr2 == null)
            {
                return "Пожалуйста, заполните поля с паролями";
            }

            if (!string.Equals(passwordStr1, passwordStr2))
            {
                return "Пароли не совпадают";
            }

            if (selectedDateBirthday == null)
            {
                return "Заполните, пожалуйста, дату своего рождения";
            }

            if (!(string.IsNullOrEmpty(ValidateEmail(email)) &&
                  string.IsNullOrEmpty(ValidatePassword(passwordStr1, passwordStr2)) &&
                  string.IsNullOrEmpty(ValidateDateBirthday(selectedDateBirthday.Value))))
            {
                return "Приглянитесь. Вы где-то некорректно ввели данные. Исправьте, пожалуйста";
            }

            if (_dataBaseWorkService.IsExistEmail(email))
            {
                return "Простите, человек с данным E-mail уже существует, попробуйте выбрать другой адрес";
            }

            return string.Empty;
        }

        private readonly DataBaseWorkService _dataBaseWorkService;
    }
}