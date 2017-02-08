using Lab19_Oprosnik.Abstract;
using System;
using System.Collections.Generic;

namespace Lab19_Oprosnik.Surveys
{
    [Serializable]
    internal class SurveysCollection : ViewModelBase
    {
        public SurveysCollection()
        {
            ListSurveys = new List<ISurvey>();
        }

        public void Add(ISurvey survey)
        {
            ListSurveys.Add(survey);
        }

        public List<ISurvey> ListSurveys
        {
            get { return _listSurveys; }
            set { UpdateValue(value, ref _listSurveys); }
        }

        private List<ISurvey> _listSurveys;
    }
}