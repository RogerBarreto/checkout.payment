using System.Text.Json.Serialization;

namespace Checkout.Payment.Gateway.Application.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatusModel
    {
        Processing = 1000,
        RejectedIncorrect = 2000,
        RejectedInsuficientFunds = 2001,
        RejectedCardBlocked = 2002,
        RejectedCustom = 2003,
        Unexpected = 3000,
        Succeeded = 4000
    }
}
