using Hostel_Management.Data;
using Hostel_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hostel_Management.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Users ON");
                _context.Users.Add(user);
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Users OFF");
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}


