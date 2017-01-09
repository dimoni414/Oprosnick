using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Lab19_Oprosnik.Abstract;

namespace Lab19_Oprosnik.Surveys
{
    [Serializable]
    public class ChooseManySurvey:ViewModelBase ,ISurvey
    {
        public ObservableCollection<QuestionObject> QuestionsCollection { get; set; }

        public ObservableCollection<int> CheckedVariants { get; set; }

        public string Title
        {
            get { return _title; }
            set { UpdateValue(value, ref _title); }
        }

        public bool IsRelativeResults
        {
            get { return _isRelativeResults; }
            set { UpdateValue(value, ref _isRelativeResults); }
        }

        public ChooseManySurvey(ObservableCollection<QuestionObject> list, string titleSurvey, bool isRelative)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            QuestionsCollection = list;
            Title = titleSurvey;

            IsRelativeResults = isRelative;

            CheckedVariants=new ObservableCollection<int>();
        }
        
        private bool _isRelativeResults;
        private string _title;
    }
}