using Checkout.Domain.Common;
using System;

namespace Checkout.Gateway.Application.Common
{
	public class DateTimeProvider : IDateTime
	{
		public DateTime Value => DateTime.Now;
	}
}
