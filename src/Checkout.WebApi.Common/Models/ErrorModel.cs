using System.Collections.Generic;

namespace Checkout.WebApi.Common.Models
{
	public class ErrorModel
	{
		public List<string> Errors { get; }

		public ErrorModel(string error)
		{
			Errors.Add(error);
		}
		public ErrorModel(IEnumerable<string> errors)
		{
			Errors.AddRange(errors);
		}
	}
}
