using System;
using System.Windows.Input;

namespace Lab19_Oprosnik.Abstract
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(Action execute);

        ICommand CreateCommand(Action execute, Func<bool> validate);

        ICommand CreateCommand<T>(Action<T> execute);

        ICommand CreateCommand<T>(Action<T> execute, Func<T, bool> validate);

        ICommand CreateCommand(Action<object> execute);

        ICommand CreateCommand(Action<object> execute, Func<object, bool> validate);
    }
}