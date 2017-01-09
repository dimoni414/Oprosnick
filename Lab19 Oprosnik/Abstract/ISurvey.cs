using System;
using System.Collections.ObjectModel;
using Lab19_Oprosnik.Surveys;

namespace Lab19_Oprosnik.Abstract
{
    public interface ISurvey
    {
        string Title { get; set; }
        ObservableCollection<QuestionObject> QuestionsCollection { get; set; }
        bool IsRelativeResults { get; }
    }
}