using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments.Commands;
using Checkout.Application.Common.Models.Shared;
using OneOf;

namespace Checkout.Command.Application.Common.Interfaces
{
    public interface IPaymentNotifier
    {
        Task<OneOf<NotificationSuccess, NotificationError>> NotifyCreatePaymentAsync(CreatePaymentCommand command);
    }
}