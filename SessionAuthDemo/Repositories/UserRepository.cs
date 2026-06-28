using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SessionAuthDemo.Models;
using SessionAuthDemo.Security;

namespace SessionAuthDemo.Repositories
{
    public class UserRepository
    {

        private readonly List<User> _users =
        [
            new User
            {
                Id = 1,
                Username = "admin",
                PasswordHasher = PasswordHasher.Hash("123456"),
                Role = "Admin"
            }
        ];

        public User? Find(string username)
        {
            return _users.FirstOrDefault(x => x.Username == username);
        }
    }
}