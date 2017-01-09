using System;

namespace Lab19_Oprosnik.Surveys
{
    [Serializable]

    public class QuestionObject:ViewModelBase
    {
        
   public string Question
        {
            get { return _question; }
            set { UpdateValue(value,ref _question);}
        }

        private string _question;
        /// сюда можно добавить изображение, но я уже подзапарился.

    }
}