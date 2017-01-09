using Lab19_Oprosnik.Services;

namespace Lab19_Oprosnik.Abstract
{
    public interface IDataBaseWork// очень тупое название
    {
        int AddUser(User user);

        int DeleteUser(string email);

        bool IsExistEmail(string email);

        User FindRegistredUser(string email, string password);
    }
}