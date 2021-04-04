using OneOf;
using System;
using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments;
using Checkout.Application.Common.Models.Payments.Commands;

namespace Checkout.Gateway.Application.Common.Interfaces
{
	public interface IPaymentCommandClient
	{
		Task<OneOf<Guid, PaymentError>> CreatePaymentAsync(CreatePaymentCommand create);
	}
}
