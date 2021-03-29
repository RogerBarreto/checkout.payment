using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Infrastructure.Common.HttpClients.Errors
{
	public struct HttpCallError
	{
		public string Message { get;  }
		public HttpCallError(string message)
		{
			Message = message;
		}
	}
}
