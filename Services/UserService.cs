using System.Threading.Tasks;

namespace FlightPlanner.Services
{
    public class UserService : UserService.IUserService
    {
        public interface IUserService
        {
            Task<User> Authenticate(string username, string password);
        }
        
        public class User
        {        
            public string Id { get; internal set; }
            public string Username { get; internal set; }
            public string Password { get; internal set; }
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (username == "codelex-admin" && password == "Password123")
            {
                return new User {Id = "1", Username = "Bobby"};
            }

            return null;
        }
    }
}