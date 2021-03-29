using Checkout.Gateway.Application.Payments.Commands;
using Checkout.Gateway.Application.Payments.Errors;
using OneOf;
using System;
using System.Threading.Tasks;

namespace Checkout.Gateway.Application.Common.Interfaces
{
	public interface IPaymentCommandClient
	{
		Task<OneOf<Guid, PaymentError>> CreatePaymentAsync(CreatePaymentCommand create);
	}
}
