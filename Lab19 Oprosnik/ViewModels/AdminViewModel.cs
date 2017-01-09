using Lab19_Oprosnik.Abstract;
using Lab19_Oprosnik.Factory;
using Lab19_Oprosnik.Services;
using Lab19_Oprosnik.Surveys;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Lab19_Oprosnik.ViewModels
{
    public class AdminViewModel : ViewModelBase, IViewModel
    {
        #region Main Properties

        #region Commands

        public ICommand ParceCurrentCommand { get; set; }

        #endregion Commands

        public bool IsAddSurvey
        {
            get { return _isAddSurvay; }
            set { UpdateValue(value, ref _isAddSurvay); }
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

        #endregion Main Properties

        #region AddSurvay Properties

        public ICommand AddSurveyCommand { get; set; }
        public ICommand AddNewTextBoxCommand { get; set; }

        public ObservableCollection<QuestionObject> QuestionCollection
        {
            get { return _questionCollection; }
            set
            {
                UpdateValue(value, ref _questionCollection);
            }
        }

        public bool IsRelativeResults
        {
            get { return _isRelativeResults; }
            set { UpdateValue(value, ref _isRelativeResults); }
        }

        public bool IsChooseOneElement
        {
            get { return _isChooseOneElement; }
            set { UpdateValue(value, ref _isChooseOneElement); }
        }

        public bool IsChooseManyElement
        {
            get { return _isChooseManyElement; }
            set { UpdateValue(value, ref _isChooseManyElement); }
        }

        public bool IsChooseYesNo
        {
            get { return _isChooseYesNo; }
            set { UpdateValue(value, ref _isChooseYesNo); }
        }

        public string TitleSurvey
        {
            get { return _titleSurvey; }
            set { UpdateValue(value, ref _titleSurvey); }
        }

        #endregion AddSurvay Properties

        #region Change Survey

        //почему не Observeble? оно не работает и у меня не понятный баг с Сериаизицией. Welсomе в SaveChangesSurSerialize
        public List<ISurvey> Surveys { get; set; }

        public ICommand SaveChangesSurCommand { get; set; }

        #endregion Change Survey

        #region DeleteUser

        public ICommand DeleteUserCommand { get; set; }

        public string DeleteUserEmail
        {
            get { return _deleteUserEmail; }
            set { UpdateValue(value, ref _deleteUserEmail); }
        }

        #endregion DeleteUser

        public AdminViewModel(WindowManagerService windowManagerService, CommandFactory commandFactory, User user)
        {
            _binarySerialization = new BinarySerializationService();
            AddSurveyCommand = commandFactory.CreateCommand(AddNewSurvey);
            ParceCurrentCommand = commandFactory.CreateCommand(ModeChanged);
            AddNewTextBoxCommand = commandFactory.CreateCommand(() => { QuestionCollection?.Add(new QuestionObject()); });
            SaveChangesSurCommand = commandFactory.CreateCommand(SaveChangesSurSerialize);
            DeleteUserCommand = commandFactory.CreateCommand(DeleteUser);

            _pathSurveys = ConfigurationManager.ConnectionStrings["Survey"].ConnectionString;
        }

        private void AddNewSurvey()
        {
            if (string.IsNullOrWhiteSpace(TitleSurvey))
            {
                MessageBox.Show("Введите название опроса");
                return;
            }
            if (QuestionCollection == null)
            {
                MessageBox.Show("Выбирете, пожалуйста, вариант опроса");
                return;
            }

            ISurvey sur = null;
            var question = ValidateQuestionsCollection(QuestionCollection);
            QuestionCollection = question;
            if (QuestionCollection.Count < 1)
            {
                MessageBox.Show("Попытка создания пустого опроса");
                return;
            }

            if (IsChooseOneElement)
            {
                sur = new ChooseOneSurvey(question, TitleSurvey, IsRelativeResults);
            }
            else if (IsChooseManyElement)
            {
                sur = new ChooseManySurvey(question, TitleSurvey, IsRelativeResults);
            }
            else if (IsChooseYesNo)
            {
                sur = new ChooseYesNoSurvey(question, TitleSurvey, IsRelativeResults);
            }

            AddSurveySerialize(sur);

            TitleSurvey = "";
            QuestionCollection = new ObservableCollection<QuestionObject>(new QuestionObject[] { new QuestionObject() });
        }

        private void AddSurveySerialize(ISurvey survey)
        {
            if (File.Exists(_pathSurveys) && new FileInfo(_pathSurveys).Length > 0)
            {
                var surCollection = LoadSerializedSurveys();

                using (Stream fStream = new FileStream(_pathSurveys,
                             FileMode.Open, FileAccess.Write))
                {
                    surCollection?.Add(survey);
                    _binarySerialization.Serialize(fStream, surCollection);
                }
            }
            else
            {
                using (Stream fStream = new FileStream(_pathSurveys,
                             FileMode.Create, FileAccess.Write))
                {
                    var tmp = new SurveysCollection();
                    tmp.Add(survey);
                    _binarySerialization.Serialize(fStream, tmp);
                }
            }
            MessageBox.Show("Вы успешно добавили опрос!");
        }

        private void SaveChangesSurSerialize()
        {
            List<int> listForDelete = new List<int>();
            for (int i = 0; i < Surveys.Count; i++)
            {
                var question = ValidateQuestionsCollection(Surveys[i].QuestionsCollection);
                Surveys[i].QuestionsCollection = question;
                if (Surveys[i].QuestionsCollection.Count < 1)
                {
                    listForDelete.Add(i);
                }
            }
            foreach (var i in listForDelete)
            {
                Surveys.RemoveAt(i);
            }

            MessageBox.Show("Тут должно было быть исключение. Создай заново опрос. Ищи функцию SaveChangesSurSerialize");
            return;

            if (File.Exists(_pathSurveys))
            {
                using (Stream fStream = new FileStream(_pathSurveys,
                             FileMode.Truncate, FileAccess.Write))
                {
                    SurveysCollection surCollection = new SurveysCollection();
                    //У меня ОЧЕНЬ горит... ВЫПАДАЕТ В РАНТАЙМЕ ИСКЛЮЧЕНИЕ
                    //ВСё хорошо, если создаю только так:

                    surCollection.Add(
                     new ChooseOneSurvey(
                         new ObservableCollection<QuestionObject>(new QuestionObject[]
                             {
                                 new QuestionObject() {Question = "ssd"},
                                 new QuestionObject() {Question = "21"}}),
                         "Заголовок", true));

                    // если вот это раскомментить, то всё пойдет по бороде............
                    //
                    //foreach (var item in Surveys)
                    //{
                    //    surCollection.Add(item);
                    //}

                    _binarySerialization.Serialize(fStream, surCollection);
                }
            }
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
            return null;
        }

        private ObservableCollection<QuestionObject> ValidateQuestionsCollection(ObservableCollection<QuestionObject> questions)
        {
            var result = new ObservableCollection<QuestionObject>();
            foreach (var item in questions)
            {
                if (!(string.IsNullOrEmpty(item.Question) || string.IsNullOrWhiteSpace(item.Question)))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        private void DeleteUser()
        {
            var dbService = new DataBaseWorkService();
            try
            {
                if (dbService.DeleteUser(DeleteUserEmail) > 0)
                {
                    MessageBox.Show("Пользователь успешно удален");
                }
                else
                {
                    MessageBox.Show("Пользователя с данным адресом не зарегистрировано");
                }
            }
            catch (SqlException e)
            {
                MessageBox.Show("Что-то пошло не так. Не удалось удалить пользователя.\n" + e.Message);
            }
        }

        private void ModeChanged(object param)
        {
            switch (param as string)
            {
                case "ChangeSurvey":
                    List<ISurvey> list;
                    if ((list = LoadSerializedSurveys()?.ListSurveys) == null)
                    {
                        MessageBox.Show("Еще не было создано ни одного опроса");
                        IsAddSurvey = true;
                        return;
                    }

                    Surveys = list;
                    OnPropertyChanged(nameof(Surveys));
                    break;

                case "AnalysisSurvey":
                    MessageBox.Show("");
                    break;

                case "JoinSurveyResults":
                    MessageBox.Show("");
                    break;

                case "AddAdmin":
                    MessageBox.Show("");
                    break;
                //addCommads select Mode Syrvey
                case "ChooseOneElement":

                case "ChooseManyElement":

                case "ChooseYesNo":
                    QuestionCollection = new ObservableCollection<QuestionObject>(new QuestionObject[] { new QuestionObject() });

                    break;
                    //end
            }
        }

        private bool _isAddSurvay;
        private bool _isChangeSurvey;
        private bool _isAnalysisSurvey;
        private bool _isJoinSurveyResults;
        private bool _isAddAdmin;
        private bool _isDeleteUser;

        private bool _isRelativeResults;
        private bool _isChooseOneElement;
        private bool _isChooseManyElement;
        private bool _isChooseYesNo;

        private string _deleteUserEmail;

        private ObservableCollection<QuestionObject> _questionCollection;
        private readonly BinarySerializationService _binarySerialization;
        private readonly string _pathSurveys;
        private string _titleSurvey;
    }
}