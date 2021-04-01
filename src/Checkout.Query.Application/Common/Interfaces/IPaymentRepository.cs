using System.Threading.Tasks;
using Checkout.Domain.Entities;
using Checkout.Domain.Errors;
using Checkout.Query.Application.Payments.Queries;
using OneOf;

namespace Checkout.Query.Application.Common.Interfaces
{
	public interface IPaymentRepository
	{
		Task<OneOf<Payment, PaymentNotFound, PaymentError>> GetPaymentAsync(GetPaymentQuery request);
	}
}
