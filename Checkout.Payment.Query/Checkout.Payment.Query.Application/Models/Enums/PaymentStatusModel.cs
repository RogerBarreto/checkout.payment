using System.Text.Json.Serialization;

namespace Checkout.Payment.Query.Application.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatusModel
    {
        Processing,
        RejectedIncorrect,
        RejectedInsuficientFunds,
        RejectedCardBlocked,
        RejectedCustom,
        Succeeded,
        Unexpected
    }
}
