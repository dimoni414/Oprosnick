using System;

namespace Lab19_Oprosnik.Surveys
{
    [Serializable]
    public class QuestionObject : ViewModelBase
    {
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { UpdateValue(value, ref _IsChecked); }
        }

        public string Question
        {
            get { return _question; }
            set { UpdateValue(value, ref _question); }
        }

        private string _question;
        private bool _IsChecked;
        // можно добавить изображение
    }
}