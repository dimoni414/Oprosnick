using Lab19_Oprosnik.Services;
using System;

namespace Lab19_Oprosnik.Abstract
{
    public interface IWindowManager
    {
        void Show(WindowType windowType, User param);

        void Close(WindowType windowType);
    }

    // я не уверен, хорошее ли это решение, я сначала сделал, 
    //чтобы у каждой ViewModel была команда Close, но потом пришел менеджер окон и это сошло на нет
    public interface IViewModel{
      
    }
}