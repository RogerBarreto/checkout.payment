using OneOf;
using System;
using System.Threading.Tasks;
using Checkout.Domain.Entities;
using Checkout.Gateway.Application.Payments.Errors;

namespace Checkout.Gateway.Application.Common.Interfaces
{
	public interface IPaymentQueryClient
	{
		Task<OneOf<Payment, PaymentError>> GetPaymentAsync(Guid paymentId);
	}
}
