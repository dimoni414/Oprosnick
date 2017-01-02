using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab19_Oprosnik
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void UpdateValue<TValue>(TValue value, ref TValue storage, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TValue>.Default.Equals(value, storage))
            {
                return;
            }
            storage = value;
            OnPropertyChange(propertyName);
        }

        protected void OnPropertyChange(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(propertyName)));
        }
    }
}