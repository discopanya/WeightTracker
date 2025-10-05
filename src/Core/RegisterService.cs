using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WeightTracker.Infrastructure;

namespace Core
{
    public class RegisterService
    {
        private WeightTrackerContext _weightTrackerContext;
        public RegisterService(WeightTrackerContext weightTrackerContext)
        {
            _weightTrackerContext = weightTrackerContext;
        }

        public string RegisterUser(Users user)
        {

            if (string.IsNullOrEmpty(user.Login)
                || string.IsNullOrEmpty(user.Password)
                || string.IsNullOrEmpty(user.Email))
            {
                return "All fields are required.";
            }

            // Login
            if(_weightTrackerContext.Users.Any(u=>u.Login==user.Login))
            {
                return "This login already exists.";
            }

            // Password 
            if (user.Password.Length < 8)
            {
                return "Password must contain at least 8 signs.";
            }
            if (!user.Password.Any(char.IsUpper))
            {
                return "Password must contain at least one uppercase letter.";
            }
            if (!user.Password.Any(char.IsDigit))
            {
                return "Password must contain at least one digit.";
            }
            if (!user.Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return "Password must contain at least one special character.";
            }

            // Email
            if (_weightTrackerContext.Users.Any(u => u.Email == user.Email))
            {
                return "This email already exists.";
            }
            if(!Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return "Invalid email format.";
            }

            user.Password = HashPassword(user.Password);

            _weightTrackerContext.Users.Add(user);
            _weightTrackerContext.SaveChanges();

            return "User registered successfully.";
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);    
            return Convert.ToBase64String(hash);
        }
    }
}
