using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Gateway.Application.Common.Interfaces
{
	public interface ICreditCardValidator
	{
		bool Validate(string cardNumber);
	}
}
