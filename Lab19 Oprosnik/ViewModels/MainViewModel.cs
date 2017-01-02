using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Services;
using System;

namespace Lab19_Oprosnik
{
    internal class MainViewModel : IViewModel
    {
        private string param;

        public MainViewModel()
        {
        }

        public MainViewModel(WindowManagerService windowManagerService, string param)
        {
            this.param = param;
        }

        public Action CloseAction { get; set; }
    }
}