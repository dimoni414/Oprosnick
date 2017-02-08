using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Services;
using System;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Lab19_Oprosnik.Surveys
{
    [Serializable]
    public class ChooseManySurvey : ViewModelBase, ISurvey
    {
        public ObservableCollection<QuestionObject> QuestionsCollection { get; set; }

        public ObservableCollection<int> CheckedVariants { get; set; }

        //public ICommand SaveAllSurveyCommand { get; set; }

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

            CheckedVariants = new ObservableCollection<int>();

            //SaveAllSurveyCommand = new Factory.CommandFactory().CreateCommand(OnSaveAllSurvey);
            _binarySerialization = new BinarySerializationService();
            _pathAnswer = ConfigurationManager.ConnectionStrings["Answer"].ConnectionString;
        }

        //private void OnSaveAllSurvey(object obj)
        //{
        //    CheckedVariants.Clear();
        //    for (int i = 0; i < QuestionsCollection.Count; i++)
        //    {
        //        if (QuestionsCollection[i].IsChecked)
        //        {
        //            CheckedVariants.Add(i);
        //        }
        //    }

        //}

        //private void SerializeData()
        //{
        //   using (Stream fStream = new FileStream(_pathAnswer,
        //                     FileMode.Create, FileAccess.Write))
        //        {
        //            _binarySerialization.Serialize(fStream, );
        //        }

        //}

        private bool _isRelativeResults;
        private string _title;
        private BinarySerializationService _binarySerialization;
        private string _pathAnswer;
    }
}