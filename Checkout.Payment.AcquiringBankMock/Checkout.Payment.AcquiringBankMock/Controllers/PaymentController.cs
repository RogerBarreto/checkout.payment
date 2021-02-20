using Checkout.Payment.AcquiringBankMock.Application.Exceptions;
using Checkout.Payment.AcquiringBankMock.Application.Interfaces;
using Checkout.Payment.AcquiringBankMock.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Checkout.Payment.AcquiringBankMock.Controllers
{
	[ApiController]
	[Authorize]
	[Route("[controller]")]
	public class PaymentController : ControllerBase
	{
		private readonly ILogger<PaymentController> _logger;
		private readonly IPaymentService _paymentService;

		public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
		{
			_logger = logger;
			_paymentService = paymentService;
		}

		[Route("Send")]
		[HttpPost]
		public IActionResult SendPayment([FromBody] SendPaymentRequestModel request)
		{
			try
			{
				return Ok(_paymentService.ExecutePayment(request));
			}
			catch (BadRequestException badEx)
			{
				return BadRequest(new { errorMessage = badEx.Message });
			}
		}
	}
}
