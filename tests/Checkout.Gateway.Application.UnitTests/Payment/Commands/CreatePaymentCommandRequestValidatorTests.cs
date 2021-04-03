using Checkout.Domain.Common;
using Checkout.Domain.Enums;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Checkout.Application.Common.Interfaces;
using Checkout.Application.Common.Payments.Commands;
using Xunit;

namespace Checkout.Gateway.Application.UnitTests.Payment.Commands
{
	
	public class CreatePaymentCommandRequestValidatorTests
	{
		public class MockHelper
		{
			public DateTime CurrentDate { get; set; }
			public IDateTime DateProvider { get; set; }

			public ICreditCardValidator CreditCardValidator { get; set; }
			public MockHelper(int currentYear, int currentMonth)
			{
				CurrentDate = new DateTime(currentYear, currentMonth, 1);
				DateProvider = Substitute.For<IDateTime>();
				DateProvider.Value.Returns(CurrentDate);
				CreditCardValidator = Substitute.For<ICreditCardValidator>();
				CreditCardValidator.Validate(Arg.Any<string>()).Returns(true);
			}

			public CreatePaymentCommandValidator CreateTarget()
			{
				return new CreatePaymentCommandValidator(DateProvider, CreditCardValidator);
			}

		}

		[Theory]
		[InlineData(2021, 1, 2021, 1)]
		[InlineData(2021, 1, 2020, 12)]
		[InlineData(2021, 1, 2000, 1)]
		public async Task ValidateCardExpiry_ShouldBeInvalid_WhenEqualOrBeforeCurrent(int currentYear, int currentMonth, int expiryYear, int expiryMonth)
		{
			var helper = new MockHelper(currentYear, currentMonth);
			var target = helper.CreateTarget();

			var command = CreateValidCommand(expiryYear, expiryMonth);

			var result = await target.ValidateAsync(command);

			result.IsValid.Should().BeFalse();
		}


		[Theory]
		[InlineData(2021, 1, 2021, 2)]
		[InlineData(2021, 1, 2022, 1)]
		[InlineData(2021, 1, 2031, 1)]
		[InlineData(2021, 1, 2041, 1)]
		public async Task ValidateDate_ShouldBeValid_WhenExpireOneMonthAfterAndBefore20YearsFromCurrentDate(int currentYear, int currentMonth, int expiryYear, int expiryMonth)
		{
			var helper = new MockHelper(currentYear, currentMonth);
			CreatePaymentCommandValidator target = helper.CreateTarget();

			var command = CreateValidCommand(expiryYear, expiryMonth);
			var result = await target.ValidateAsync(command);

			result.IsValid.Should().BeTrue();
		}

		[Theory]
		[InlineData(2021, 1, 2041, 2)]
		[InlineData(2021, 1, 2041, 3)]
		[InlineData(2021, 1, 2061, 12)]
		public async Task ValidateDate_ShouldBeInvalid_WhenExpireOneMonthAfterAndBefore20YearsFromCurrentDate(int currentYear, int currentMonth, int expiryYear, int expiryMonth)
		{
			var helper = new MockHelper(currentYear, currentMonth);
			var target = helper.CreateTarget();

			var command = CreateValidCommand(expiryYear, expiryMonth);

			var result = await target.ValidateAsync(command);

			result.IsValid.Should().BeFalse();
		}

		private CreatePaymentCommand CreateValidCommand(int expiryYear, int expiryMonth)
		{
			return new CreatePaymentCommand
			{
				Amount = 100,
				CardCVV = 100,
				CardExpiryYear = expiryYear,
				CardExpiryMonth = expiryMonth,
				CardNumber = "1234567890123456",
				CurrencyType = CurrencyType.EUR.ToString(),
				MerchantId = 1
			};
		}
	}
}
