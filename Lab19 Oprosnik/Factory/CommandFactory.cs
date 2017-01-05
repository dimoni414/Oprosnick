using Lab19_Oprosnik.Abstract;
using System;
using System.Windows.Input;

namespace Lab19_Oprosnik.Factory
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(Action execute) => new RelayCommand<object>(o => execute());

        public ICommand CreateCommand(Action execute, Func<bool> validate)
        {
            if (validate == null)
            {
                throw new ArgumentNullException(nameof(validate));
            }

            return new RelayCommand<object>(o => execute(), o => validate());
        }

        public ICommand CreateCommand<T>(Action<T> execute) => new RelayCommand<T>(execute);

        public ICommand CreateCommand<T>(Action<T> execute, Func<T, bool> validate)
        {
            if (validate == null)
            {
                throw new ArgumentNullException(nameof(validate));
            }
            return new RelayCommand<T>(execute, validate);
        }

        public ICommand CreateCommand(Action<object> execute) => new RelayCommand<object>(execute);

        public ICommand CreateCommand(Action<object> execute, Func<object, bool> validate)
        {
            if (validate == null)
            {
                throw new ArgumentNullException(nameof(validate));
            }
            return new RelayCommand<object>(execute, validate);
        }
    }
}