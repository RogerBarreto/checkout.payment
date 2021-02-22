using Checkout.Payment.Identity.Domain.Interfaces;
using Checkout.Payment.Identity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Payment.Identity.Data.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly List<MerchantApiKey> _users = new List<MerchantApiKey>();
        public ApiKeyRepository()
        {
            for(var i = 1; i < 100; i++)
            {
                _users.Add(new MerchantApiKey
                {
                    SubjectId = (i+1000).ToString(),

                    //Must match the cliendId
                    ApiKey = $"merchant.api.{i}.key",

                    UserName = $"merchant-api{i}",
                    Email = $"merchant{1}-api@checkout.com"
                });
            }
        }

        public async Task<bool> ValidateCredentialsAsync(string apiKey)
        {
            var user = await FindByApiKeyAsync(apiKey);
            return user != null;
        }

        public async Task<MerchantApiKey> FindBySubjectIdAsync(string subjectId)
        {
            return _users.FirstOrDefault(x => x.SubjectId == subjectId);
        }

        public async Task<MerchantApiKey> FindByApiKeyAsync(string apiKey)
        {
            return _users.FirstOrDefault(x => x.ApiKey.Equals(apiKey, StringComparison.OrdinalIgnoreCase));
        }
    }
}
