using Checkout.Domain.Common;
using System.Collections.Generic;

namespace Checkout.Domain.ValueObjects
{
	public class CardExpiry : ValueObject
	{
		public int Year { get; set;  }
		public int Month { get; set; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return ConvertToNumberYYYYMM();
		}

		private int ConvertToNumberYYYYMM()
		{
			return (Year * 100) + Month;
		}
	}
}
