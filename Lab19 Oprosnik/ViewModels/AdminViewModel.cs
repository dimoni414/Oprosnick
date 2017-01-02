using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Services;
using System;

namespace Lab19_Oprosnik.ViewModels
{
    public class AdminViewModel : IViewModel
    {
        private string param;

        public AdminViewModel(WindowManagerService windowManagerService, string param)
        {
            this.param = param;
        }

        public Action CloseAction { get; set; }
    }
}