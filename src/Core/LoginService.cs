using WeightTracker.Infrastructure;

namespace Core
{
    public class LoginService
    {
        private DatabaseService _databaseService;
        public LoginService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public bool ValidateUser(string username, string password)
        {
            var user = _databaseService.GetUser(username);
            if (user == null)
            {
                throw new Exception($"User {username} not found.");
            }
            return user.Password == password;
        }
    }
}
