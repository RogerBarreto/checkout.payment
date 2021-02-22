using Checkout.Payment.Identity.Domain.Models;
using System.Threading.Tasks;

namespace Checkout.Payment.Identity.Domain.Interfaces
{
    public interface IApiKeyRepository
    {
        Task<bool> ValidateCredentialsAsync(string apiKey);

        Task<MerchantApiKey> FindBySubjectIdAsync(string subjectId);

        Task<MerchantApiKey> FindByApiKeyAsync(string username);
    }
}
