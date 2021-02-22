using System.Text.Json.Serialization;

namespace Checkout.Payment.Gateway.Application.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatusModel
    {
        Processing = 100,
        RejectedIncorrect = 400,
        RejectedInsuficientFunds = 401,
        RejectedCardBlocked = 402,
        RejectedCustom = 403,
        Succeeded = 200,
        Unexpected = 500
    }
}
