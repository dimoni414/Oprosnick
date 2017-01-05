using Lab19_Oprosnik.Abstract;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Lab19_Oprosnik.Services
{
    public class DataBaseWorkService : IDataBaseWork
    {
        public int AddUser(User user)
        {
            var connection = TryConnectionToDataBase();

            var sqlExpression =
                $"INSERT INTO OprosUsersCol (EmailAdress, Password, Birthday, Sex, IsAdmin) VALUES ('{user.Email}', '{user.Password}', '{user.Birthday}', '{user.Sex}', '{user.IsAdmin}')";
            using (var command = new SqlCommand(sqlExpression, connection))
            {
                return command.ExecuteNonQuery();
            }
        }

        public bool IsExistEmail(string email)
        {
            var connection = TryConnectionToDataBase();

            var sqlExpression = $"SELECT * FROM OprosUsersCol WHERE EmailAdress = '{email}'";
            using (var sqlCommand = new SqlCommand(sqlExpression, connection))
            {
                var reader = sqlCommand.ExecuteReader();

                return reader.HasRows;
            }
        }

        public User FindRegistredUser(string email, string password)
        {
            var connection = TryConnectionToDataBase();

            var sqlExpression = $"SELECT * FROM OprosUsersCol WHERE EmailAdress = '{email}' And Password = '{password}'";
            using (var sqlCommand = new SqlCommand(sqlExpression, connection))
            {
                var reader = sqlCommand.ExecuteReader();
                User findUser;

                if (reader.HasRows)
                {
                    reader.Read();
                    findUser = new User((string)reader[1], (string)reader[2], (string)reader[3], (bool)reader[4],
                        (bool)reader[5]);
                }
                else
                {
                    throw new ArgumentException("Ошибка логина или пароля");
                }

                return findUser;
            }
        }

        private SqlConnection TryConnectionToDataBase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionOpros"].ConnectionString;

            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Не удалось подключиться к БД", ex);
            }
            return connection;
        }
    }
}