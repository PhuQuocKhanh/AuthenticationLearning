using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthDemo.Services
{
    public class UserService
    {
        private readonly Dictionary<string, string> _user = new()
        {
            {
                "admin",
                "123456"
            },
            {
                "khanh",
                "password"
            }
        };

        public bool Validate(string username, string password)
        {
            _user.TryGetValue(username, out var storedPassword);
            return storedPassword == password;
        }
    }
}