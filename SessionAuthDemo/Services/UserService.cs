using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SessionAuthDemo.Models;
using SessionAuthDemo.Repositories;
using SessionAuthDemo.Security;

namespace SessionAuthDemo.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Authenticate(string username, string password)
        {
            var user = _userRepository.Find(username);

            if (user == null || !PasswordHasher.Verify(password, user.PasswordHasher))
                return null;

            return user;
        }
    }
}