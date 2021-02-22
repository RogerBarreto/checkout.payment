using Checkout.Payment.Query.Domain.Interfaces;
using Checkout.Payment.Query.Domain.Models;
using Checkout.Payment.Query.Seedwork.Extensions;
using Checkout.Payment.Query.Seedwork.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payment.Query.Domain.CommandHandlers
{
    public class PaymentQueryHandler : 
        IRequestHandler<GetPaymentQuery, ITryResult<GetPaymentQueryResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentQueryHandler> _logger;
		private readonly IDomainNotification _bus;

		public PaymentQueryHandler(IPaymentRepository paymentRepository, ILogger<PaymentQueryHandler> logger, IDomainNotification bus)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
			_bus = bus;
		}
        public async Task<ITryResult<GetPaymentQueryResponse>> Handle(GetPaymentQuery query, CancellationToken cancellationToken)
        {
            ITryResult<PaymentRequest> foundPayment = await _paymentRepository.TryGetPayment(query.PaymentId);
            if (!foundPayment.Success)
            {
                _logger.LogWarning($"Payment id not found. [merchantId={query.MerchantId}, paymentId={query.PaymentId}]");
                _bus.PublishNotFound("Failed to find payment");
                return TryResult<GetPaymentQueryResponse>.CreateFailResult();
            }

            if (foundPayment.Result.MerchantId != query.MerchantId)
            {
                _logger.LogWarning($"Found payment id using wrong merchant id. Not found result returned instead [merchantId={query.MerchantId}, paymentId={query.PaymentId}]");
                _bus.PublishNotFound("Failed to find payment");
                return TryResult<GetPaymentQueryResponse>.CreateFailResult();
            }

            return TryResult<GetPaymentQueryResponse>.CreateSuccessResult(new GetPaymentQueryResponse(foundPayment.Result));
        }
	}
}
