﻿using Checkout.Payment.Processor.Domain.Commands;
using System;

namespace Checkout.Payment.Processor.Domain.Models.AcquiringBank
{
	public class BankPaymentRequest : IBankPaymentRequest
	{
		public BankPaymentRequest(SendBankPaymentCommand command)
		{
			PaymentId = command.PaymentId;
			CardNumber = command.CardNumber;
			CardCVV = command.CardCVV;
			ExpiryDate = command.ExpiryDate;
			Amount = command.Amount;
			CurrencyType = command.CurrencyType.ToString();
		}

		public Guid PaymentId { get; set; }
		public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; }
	}
}
