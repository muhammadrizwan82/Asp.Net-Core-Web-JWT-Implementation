namespace ASPCORE_JWT_Authentication.Models
{
    public class User
    {
        public int Id;
        public string Username;
        public string Name;
        public string Email;
        public string Password;
        public string[] Roles;      

        public User(int id, string username, string name, string email, string password, string[] roles)
        {
            Id = id;
            Username = username;
            Name = name;
            Email = email;
            Password = password;
            Roles = roles;
        }
    }
}
