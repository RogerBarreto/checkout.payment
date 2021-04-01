using OneOf;
using System.Threading.Tasks;
using Checkout.Domain.Errors;
using Checkout.Gateway.Application.Payments.Queries;

namespace Checkout.Gateway.Application.Common.Interfaces
{
	public interface IPaymentQueryClient
	{
		Task<OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>> GetPaymentAsync(GetPaymentQuery query);
	}
}
