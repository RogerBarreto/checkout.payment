namespace Checkout.Infrastructure.Common.Extensions
{
    public static class JsonSerializer
    {
        public static string Serialize(object obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj,
                new System.Text.Json.JsonSerializerOptions() {Converters = {new System.Text.Json.Serialization.JsonStringEnumConverter()}});
        }

        public static T Deserialize<T>(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json,
                new System.Text.Json.JsonSerializerOptions() {Converters = {new System.Text.Json.Serialization.JsonStringEnumConverter()}});
        }
    }
}