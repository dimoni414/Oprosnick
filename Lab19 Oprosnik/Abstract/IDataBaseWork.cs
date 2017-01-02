using Lab19_Oprosnik.Services;

namespace Lab19_Oprosnik.Abstract
{
    public interface IDataBaseWork// очень тупое название
    {
        int AddPeopleToDataBase(User user);

        User FindPeopleInDataBase(string email, string password);
    }
}