using System.Text.Json.Serialization;

namespace Checkout.Payment.Processor.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Processing,
        RejectedInsuficientFunds,
        RejectedBlocked,
        RejectedCustom,
        Succeded,
        Unexpected
    }
}
