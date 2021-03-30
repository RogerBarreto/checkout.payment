using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Domain.Entities;
using Checkout.Identity.Application.Authentication;

namespace Checkout.Identity.Infrastructure.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly List<MerchantUser> _users = new List<MerchantUser>();
        public UserRepository()
        {
            for(var i = 1; i < 100; i++)
            {
                _users.Add(new MerchantUser
                {
                    Id = i.ToString(),
                    UserName = $"merchant{i}",
                    Password = $"merchant{i}password",
                    Email = $"merchant{i}@checkout.com"
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

        public async Task<MerchantUser> FindBySubjectIdAsync(string subjectId)
        {
            return await Task.FromResult(_users.FirstOrDefault(x => x.Id == subjectId));
        }

        public async Task<MerchantUser> FindByUsernameAsync(string username)
        {
            return await Task.FromResult(_users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)));
        }
    }
}