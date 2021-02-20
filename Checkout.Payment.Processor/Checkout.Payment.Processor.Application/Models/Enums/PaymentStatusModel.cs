using System.Text.Json.Serialization;

namespace Checkout.Payment.Processor.Application.Models.Enums
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum PaymentStatusModel
	{
		Processing,
		Failed,
		RejectedInsufficient,
		RejectedIncorrect,
		RejectedBlocked,
		Succeeded
	}
}
