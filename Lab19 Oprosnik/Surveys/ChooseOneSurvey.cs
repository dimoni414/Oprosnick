using Lab19_Oprosnik.Abstract;
using System;
using System.Collections.ObjectModel;

namespace Lab19_Oprosnik.Surveys
{
    [Serializable]
    public class ChooseOneSurvey : ViewModelBase, ISurvey
    {
        public ObservableCollection<QuestionObject> QuestionsCollection { get; set; }

        public int CheckedVariant
        {
            get { return _checkedVariant; }
            set { UpdateValue(value, ref _checkedVariant); }
        }

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

        public ChooseOneSurvey(ObservableCollection<QuestionObject> list, string titleSurvey, bool isRelative)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            QuestionsCollection = list;
            Title = titleSurvey;
            IsRelativeResults = isRelative;
        }

        private bool _isRelativeResults;
        private string _title;
        private int _checkedVariant;
    }
}