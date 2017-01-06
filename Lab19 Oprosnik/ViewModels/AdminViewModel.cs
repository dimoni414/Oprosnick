using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Services;
using System;
using Lab19_Oprosnik.Factory;

namespace Lab19_Oprosnik.ViewModels
{
    public class AdminViewModel :ViewModelBase, IViewModel
    {
        

        public bool IsAddSurvay
        {
            get { return _isAddSurvay; }
            set { UpdateValue(value,ref _isAddSurvay);}
        }

        public bool IsChangeSurvey
        {
            get { return _isChangeSurvey; }
            set { UpdateValue(value, ref _isChangeSurvey); }
        }
        public bool IsAnalysisSurvey
        {
            get { return _isAnalysisSurvey; }
            set { UpdateValue(value, ref _isAnalysisSurvey); }
        }
        public bool IsJoinSurveyResults
        {
            get { return _isJoinSurveyResults; }
            set { UpdateValue(value, ref _isJoinSurveyResults); }
        }
        public bool IsAddAdmin
        {
            get { return _isAddAdmin; }
            set { UpdateValue(value, ref _isAddAdmin); }
        }
        public bool IsDeleteUser
        {
            get { return _isDeleteUser; }
            set { UpdateValue(value, ref _isDeleteUser); }
        }



        public AdminViewModel(WindowManagerService windowManagerService, CommandFactory commandFactory, User user)
        {
            
              
        }

        private bool _isAddSurvay;
        private bool _isChangeSurvey;
        private bool _isAnalysisSurvey;
        private bool _isJoinSurveyResults;
        private bool _isAddAdmin;
        private bool _isDeleteUser;
    }
}