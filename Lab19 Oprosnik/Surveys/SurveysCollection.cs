using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab19_Oprosnik.Abstract;

namespace Lab19_Oprosnik.Surveys
{
    [Serializable]
    class SurveysCollection:ViewModelBase
    {
      public  SurveysCollection()
        {
            ListSurveys=new List<ISurvey>();
        }
        //public SurveysCollection(IEnumerable<ISurvey>  collection)
        //{
        //    ListSurveys = new List<ISurvey>(collection);
        //}
        public void Add(ISurvey survey)
        {
            ListSurveys.Add(survey);
        }
        public List<ISurvey> ListSurveys
        {
            get { return _listSurveys; }
            set { UpdateValue(value,ref _listSurveys);}
        }

        private List<ISurvey> _listSurveys;

    }
}
