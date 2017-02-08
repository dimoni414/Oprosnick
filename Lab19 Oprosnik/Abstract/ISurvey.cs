using Lab19_Oprosnik.Surveys;
using System.Collections.ObjectModel;

namespace Lab19_Oprosnik.Abstract
{
    public interface ISurvey
    {
        string Title { get; set; }
        ObservableCollection<QuestionObject> QuestionsCollection { get; set; }
        bool IsRelativeResults { get; }
    }
}