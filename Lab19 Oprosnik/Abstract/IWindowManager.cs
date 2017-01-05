using Lab19_Oprosnik.Services;
using System;

namespace Lab19_Oprosnik.Abstract
{
    public interface IWindowManager
    {
        void Show(WindowType windowType, string param);

        void Close(WindowType windowType);
    }

    public interface IViewModel
    {
      
    }
}