using Checkout.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Gateway.Application.Common
{
	public class DateTimeProvider : IDateTime
	{
		public DateTime Value => DateTime.Now;
	}
}
