using Lab19_Oprosnik.Services;
using System;

namespace Lab19_Oprosnik.Abstract
{
    public interface IWindowManager
    {
        void Close(WindowType windowType);

        void Show(WindowType windowType, string param);
    }

    public interface IViewModel
    {
        Action CloseAction { get; set; }
    }
}