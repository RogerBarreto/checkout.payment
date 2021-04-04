using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments;
using Checkout.Application.Common.Models.Payments.Queries;
using Checkout.Domain.Entities;
using OneOf;

namespace Checkout.Query.Application.Common.Interfaces
{
	public interface IPaymentRepository
	{
		Task<OneOf<Payment, PaymentNotFound, PaymentError>> GetPaymentAsync(GetPaymentQuery request);
	}
}
