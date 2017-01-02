namespace Lab19_Oprosnik.Services
{
    public class RegAndLoginPeopleService
    {
        public bool AddNewPeople(string email, string password, string birthday, bool sex, bool isAdmin)
        {
            var dataBaseService = new DataBaseWorkService();
            var succsses = dataBaseService.AddPeopleToDataBase(new User(email, password, birthday, sex, isAdmin));

            return succsses > 0;
        }

        public User FindPeople(string email, string password)
            => new DataBaseWorkService().FindPeopleInDataBase(email, password);
    }
}