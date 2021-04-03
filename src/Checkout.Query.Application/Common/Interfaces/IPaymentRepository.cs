using System.Threading.Tasks;
using Checkout.Application.Common.Payments.Queries;
using Checkout.Domain.Entities;
using Checkout.Domain.Errors;
using OneOf;

namespace Checkout.Query.Application.Common.Interfaces
{
	public interface IPaymentRepository
	{
		Task<OneOf<Payment, PaymentNotFound, PaymentError>> GetPaymentAsync(GetPaymentQuery request);
	}
}
