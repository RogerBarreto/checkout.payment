using Checkout.Payment.AcquiringBankMock.Application.Models;
using System.Threading.Tasks;

namespace Checkout.Payment.AcquiringBankMock.Application.Interfaces
{
	public interface IPaymentService
	{
		SendPaymentResponseModel ExecutePayment(SendPaymentRequestModel request);
	}
}
