using Checkout.Gateway.Application.Payments.Commands;
using OneOf;
using System;
using System.Threading.Tasks;
using Checkout.Application.Common.Payments.Commands;
using Checkout.Domain.Errors;

namespace Checkout.Gateway.Application.Common.Interfaces
{
	public interface IPaymentCommandClient
	{
		Task<OneOf<Guid, PaymentError>> CreatePaymentAsync(CreatePaymentCommand create);
	}
}
