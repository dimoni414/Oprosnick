using System;
using System.Windows.Input;

namespace Lab19_Oprosnik
{
    public class RelayCommad<TArg> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommad(Action<TArg> execute, Func<TArg, bool> canexecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            _execute = execute;
            _canexecute = canexecute;
        }

        public bool CanExecute(TArg parameter) => _canexecute == null || _canexecute(parameter);

        bool ICommand.CanExecute(object parameter) => this.CanExecute(Cast(parameter));

        public void Execute(TArg parameter) => _execute(parameter);

        void ICommand.Execute(object parameter) => this.Execute(Cast(parameter));

        private TArg Cast(object parameter) => parameter is TArg ? (TArg)parameter : default(TArg);

        private readonly Action<TArg> _execute;
        private readonly Func<TArg, bool> _canexecute;
    }
}