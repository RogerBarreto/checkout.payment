using FluentValidation;

namespace Checkout.Application.Common.Payments.Queries
{
    public class GetPaymentQueryValidator : AbstractValidator<GetPaymentQuery>
    {
        public GetPaymentQueryValidator()
        {
            RuleFor(o => o.PaymentId)
                .NotEmpty();

            RuleFor(o => o.MerchantId)
                .GreaterThan(0);
        }
    }
}