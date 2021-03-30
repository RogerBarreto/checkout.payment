using System.Threading.Tasks;
using Checkout.Domain.Entities;

namespace Checkout.Identity.Application.Authentication
{
    public interface IApiKeyRepository
    {
        Task<bool> ValidateCredentialsAsync(string apiKey);

        Task<MerchantApi> FindBySubjectIdAsync(string subjectId);

        Task<MerchantApi> FindByApiKeyAsync(string username);

    }
}