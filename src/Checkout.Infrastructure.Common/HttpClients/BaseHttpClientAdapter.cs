using Checkout.Infrastructure.Common.HttpClients.Errors;
using Microsoft.Extensions.Logging;
using OneOf;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkout.Infrastructure.Common.HttpClients
{
    public class BaseHttpClientAdapter
    {
        protected readonly HttpClient _httpClient;
        protected readonly ILogger _logger;
        public BaseHttpClientAdapter(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected async Task<OneOf<HttpResponseMessage, HttpCallError>> PostJsonAsync(string url, object payload, NameValueCollection additionalHeaders)
        {
            return await SendJsonAsync(url, HttpMethod.Post, payload, additionalHeaders);

        }
        protected async Task<OneOf<HttpResponseMessage, HttpCallError>> GetAsync(string url, NameValueCollection additionalHeaders)
        {
            return await SendJsonAsync(url, HttpMethod.Get, null, additionalHeaders);

        }

        protected async Task<OneOf<HttpResponseMessage, HttpCallError>> SendJsonAsync(string url, HttpMethod method, object payload, NameValueCollection additionalHeaders)
        {
            var requestMessage = new HttpRequestMessage(method, url);

            string jsonPayload = string.Empty;
            if (payload != null)
            {
                jsonPayload = JsonSerializer.Serialize(payload);
                requestMessage.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            };

            if (additionalHeaders != null)
            {
                foreach (string key in additionalHeaders.AllKeys)
                {
                    requestMessage.Headers.TryAddWithoutValidation(key, additionalHeaders[key]);
                }
            }

            try
            {
                var successResult = await _httpClient.SendAsync(requestMessage);
                _logger.LogDebug($"{method} Async Call Succeeded [requestUrl={url}, method={method}, responseStatus={successResult.StatusCode}, payload={jsonPayload}");
                return successResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{method} Async Call Failed [requestUrl={url}, method={method}, payload={jsonPayload}, exMessage={ex.Message}, exStrackTrace={ex.StackTrace}");
                return new HttpCallError($"PostJsonAsync Call Failed - {ex.Message}");
            }
        }

    }
}
