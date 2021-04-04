using OneOf;
using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments;
using Checkout.Application.Common.Models.Payments.Queries;

namespace Checkout.Gateway.Application.Common.Interfaces
{
	public interface IPaymentQueryClient
	{
		Task<OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>> GetPaymentAsync(GetPaymentQuery query);
	}
}
