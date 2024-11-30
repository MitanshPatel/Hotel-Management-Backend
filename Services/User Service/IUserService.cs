using Hostel_Management.Models;

namespace Hostel_Management.Services
{
    public interface IUserService
    {
        bool RegisterUser(User user);
        User AuthenticateUser(string email, string password);
    }
}
