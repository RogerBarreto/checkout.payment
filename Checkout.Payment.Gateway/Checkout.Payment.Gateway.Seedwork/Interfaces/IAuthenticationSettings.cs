using System;

namespace Checkout.Payment.Gateway.Seedwork.Interfaces
{
    public interface IAuthenticationSettings
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string Authority { get; }
    }
}
