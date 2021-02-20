using Checkout.Payment.Processor.Domain.Commands;
using Checkout.Payment.Processor.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payment.Processor.Domain.Models.Notification
{
    public class ReprocessPaymentMessage
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public string BankPaymentId { get; set; }

		public ReprocessPaymentMessage(ReprocessPaymentCommand command)
		{
            PaymentId = command.PaymentId;
            CardNumber = command.CardNumber;
            CardCVV = command.CardCVV;
            ExpiryDate = command.ExpiryDate;
            Amount = command.Amount;
            BankPaymentId = command.BankPaymentId;
            CurrencyType = command.CurrencyType;
		}
    }
}
