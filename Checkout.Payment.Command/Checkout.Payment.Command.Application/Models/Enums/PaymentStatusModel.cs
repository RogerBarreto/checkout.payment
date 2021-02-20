using System.Text.Json.Serialization;

namespace Checkout.Payment.Command.Application.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatusModel
    {
        Processing,
        RejectedInsuficientFunds,
        RejectedBlocked,
        RejectedCustom,
        Succeded
    }
}
