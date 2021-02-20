using System.Text.Json.Serialization;

namespace Checkout.Payment.Command.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Processing,
        RejectedInsuficientFunds,
        RejectedBlocked,
        RejectedCustom,
        Succeeded
    }
}
