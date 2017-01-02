namespace Lab19_Oprosnik.Services
{
    public class User
    {
        public User(string email, string password, string birthday, bool sex, bool isAdmin)
        {
            Email = email;
            Password = password;
            Birthday = birthday;
            Sex = sex;
            IsAdmin = isAdmin;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }
        public string Birthday { get; private set; }

        public bool Sex { get; private set; }

        public bool IsAdmin { get; private set; }
    }
}