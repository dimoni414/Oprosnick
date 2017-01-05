using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Services;
using System;
using Lab19_Oprosnik.Factory;

namespace Lab19_Oprosnik.ViewModels
{
    public class AdminViewModel : IViewModel
    {
        private string _param;

        public AdminViewModel(WindowManagerService windowManagerService, CommandFactory commandFactory, string param)
        {
            _param = param;
        }

    }
}