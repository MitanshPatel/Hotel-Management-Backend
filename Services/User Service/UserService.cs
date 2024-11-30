using Hostel_Management.Models;
using Hostel_Management.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Hostel_Management.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool RegisterUser(User user)
        {
            if (_userRepository.GetUserByEmail(user.Email) != null)
            {
                return false;
            }

            user.PasswordHash = HashPassword(user.PasswordHash);
            _userRepository.AddUser(user);
            _userRepository.SaveChanges();
            return true;
        }

        public User AuthenticateUser(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null || !user.IsActive || !VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
