using Checkout.Payment.Identity.Domain.Interfaces;
using Checkout.Payment.Identity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Payment.Identity.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<CustomUser> _users = new List<CustomUser>();
        public UserRepository()
        {
            for(var i = 1; i < 100; i++)
            {
                _users.Add(new CustomUser
                {
                    SubjectId = i.ToString(),
                    UserName = $"merchant{i}",
                    Password = $"merchant{i}",
                    Email = $"merchant{1}@checkout.com"
                });
            }
        }
        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await FindByUsernameAsync(username);
            if (user != null)
            {
                return user.Password.Equals(password);
            }

            return false;
        }

        public async Task<CustomUser> FindBySubjectIdAsync(string subjectId)
        {
            return _users.FirstOrDefault(x => x.SubjectId == subjectId);
        }

        public async Task<CustomUser> FindByUsernameAsync(string username)
        {
            return _users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
