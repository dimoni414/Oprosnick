using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Factory;
using Lab19_Oprosnik.Services;
using Lab19_Oprosnik.Surveys;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;

namespace Lab19_Oprosnik.ViewModels
{
    internal class MainViewModel : IViewModel
    {
        public ObservableCollection<ISurvey> Surveys { get; set; }
        public string CurrentUserName { get; set; }

        public MainViewModel(WindowManagerService windowManagerService, CommandFactory commandFactory, User user)
        {
            _currentUser = user;
            CurrentUserName = user.Email;
            _binarySerialization = new BinarySerializationService();
            _pathSurveys = ConfigurationManager.ConnectionStrings["Survey"].ConnectionString;

            Surveys = new ObservableCollection<ISurvey>(LoadSerializedSurveys()?.ListSurveys);
        }

        private SurveysCollection LoadSerializedSurveys()
        {
            
            if (File.Exists(_pathSurveys))
            {
                using (Stream fStream = new FileStream(_pathSurveys,
                    FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    SurveysCollection surCollection = null;
                    while (fStream.Position != fStream.Length)
                    {
                        surCollection = _binarySerialization.Deserialize<SurveysCollection>(fStream);
                    }
                    return surCollection;
                }
            }
            return new SurveysCollection();
        }

        private User _currentUser;
        private string _pathSurveys;
        private BinarySerializationService _binarySerialization;
    }
}