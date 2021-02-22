using Checkout.Payment.Identity.Domain.Models;
using System.Threading.Tasks;

namespace Checkout.Payment.Identity.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);

        Task<MerchantUser> FindBySubjectIdAsync(string subjectId);

        Task<MerchantUser> FindByUsernameAsync(string username);
    }
}
