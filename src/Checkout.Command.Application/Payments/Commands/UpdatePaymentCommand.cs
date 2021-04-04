using System;
using Checkout.Application.Common.Models.Payments;
using Checkout.Domain.Enums;
using MediatR;
using OneOf;

namespace Checkout.Command.Application.Payments.Commands
{
	public class UpdatePaymentCommand : IRequest<OneOf<UpdatePaymentCommandResponse, PaymentNotFound, PaymentError>>
	{
		public Guid PaymentId { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public string PaymentStatusDescription { get; set; }
		public string BankPaymentId { get; set; }
	}
}
