using System.Collections.Generic;
using IdentityServer4.Models;

namespace Checkout.Identity.Application.Authentication
{
    public interface IConfigRepository
    {
        public IEnumerable<ApiScope> GetApiScopes();
        public IEnumerable<Client> GetClients();
        public IEnumerable<IdentityResource> GetIdentityResources();
    }
}