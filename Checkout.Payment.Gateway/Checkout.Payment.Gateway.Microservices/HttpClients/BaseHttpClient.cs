using Checkout.Payment.Gateway.Seedwork.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.MicroServices.HttpClients
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

        protected async Task<ITryResult<HttpResponseMessage>> TryPostJsonAsync(string url, object payload, NameValueCollection additionalHeaders)
        {
            return await TrySendJsonAsync(url, HttpMethod.Post, payload, additionalHeaders);

        }
        protected async Task<ITryResult<HttpResponseMessage>> TrySendJsonAsync(string url, HttpMethod method, object payload, NameValueCollection additionalHeaders)
        {
            var requestMessage = new HttpRequestMessage(method, url);

            string jsonPayload = string.Empty;  
            if (payload != null)
            {
                jsonPayload = JsonConvert.SerializeObject(payload);
                requestMessage.Content = new StringContent(jsonPayload, Encoding.UTF8, "applitation/json");
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
                return TryResult<HttpResponseMessage>.CreateSuccessResult(await _httpClient.SendAsync(requestMessage));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostJsonAsync Call Failed [requestUrl={url}, method={method}, payload={jsonPayload}, exMessage={ex.Message}, exStrackTrace={ex.StackTrace}");
                return TryResult<HttpResponseMessage>.CreateFailResult($"PostJsonAsync Call Failed - {ex.Message}");
            }
        }

    }
}
