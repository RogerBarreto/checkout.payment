using System.Threading.Tasks;
using Checkout.Domain.Entities;

namespace Checkout.Identity.Application.Authentication
{
    public interface IUserRepository
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);

        Task<MerchantUser> FindBySubjectIdAsync(string subjectId);

        Task<MerchantUser> FindByUsernameAsync(string username);
    }
}