using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Domain.Entities;
using Checkout.Identity.Application.Authentication;

namespace Checkout.Identity.Infrastructure.Authentication
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly List<MerchantApi> _users = new List<MerchantApi>();
        public ApiKeyRepository()
        {
            for(var i = 1; i < 100; i++)
            {
                _users.Add(new MerchantApi
                {
                    Id = (i+1000).ToString(),

                    //Must match the cliendId
                    ApiKey = $"merchant.api.{i}.key",

                    UserName = $"merchant-api{i}",
                    Email = $"merchant{i}-api@checkout.com"
                });
            }
        }

        public async Task<bool> ValidateCredentialsAsync(string apiKey)
        {
            var user = await FindByApiKeyAsync(apiKey);
            return user != null;
        }

        public async Task<MerchantApi> FindBySubjectIdAsync(string subjectId)
        {
            return await Task.FromResult(_users.FirstOrDefault(x => x.Id == subjectId));
        }

        public async Task<MerchantApi> FindByApiKeyAsync(string apiKey)
        {
            return await Task.FromResult(_users.FirstOrDefault(x => x.ApiKey.Equals(apiKey, StringComparison.OrdinalIgnoreCase)));
        }
    }
}