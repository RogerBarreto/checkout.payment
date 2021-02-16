using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Payment.Gateway.Seedwork.Interfaces;
using Checkout.Payment.Gateway.MicroServices.Configurations;
using Checkout.Payment.Gateway.Seedwork.Models;
using Checkout.Payment.Gateway.Domain;

namespace Checkout.Payment.Gateway.MicroServices.HttpClients
{
    public class PaymentCommandHttpClientAdapter : IPaymentCommandHttpClientAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly IDomainNotificationBus _bus;
        public PaymentCommandHttpClientAdapter(HttpClient client, MicroServiceSettings microServiceSettings, ApplicationManifest manifest, IDomainNotificationBus notificationBus)
        {
            _httpClient = client;
            _bus = notificationBus;
            _httpClient.BaseAddress = new Uri(microServiceSettings.PaymentCommandBaseAddress);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{manifest.Name} {manifest.Version}");
        }

        public Task<Guid> CreatePayment(CreatePayment request)
        {
            throw new NotImplementedException();
        }
    }
}
