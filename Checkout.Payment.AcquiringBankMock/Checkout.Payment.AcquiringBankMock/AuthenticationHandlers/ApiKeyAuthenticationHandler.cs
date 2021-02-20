using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Checkout.Payment.AcquiringBankMock.AuthenticationHandlers
{

	public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public IServiceProvider ServiceProvider { get; set; }
        private readonly string[] _authApiKeys;

        public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceProvider serviceProvider, IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            ServiceProvider = serviceProvider;
            _authApiKeys = configuration.GetSection("AuthApiKeys").Get<string[]>();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var headers = Request.Headers;
            var apiKey = headers["Authorization"];

            if (string.IsNullOrEmpty(apiKey))
            {
                return Task.FromResult(AuthenticateResult.Fail("ApiKey is null"));
            }

            bool isValidKey = _authApiKeys.Any(key => key == apiKey);

            if (!isValidKey)
            {
                return Task.FromResult(AuthenticateResult.Fail($"Invalid key: [apiKey={apiKey}]"));
            }
            
            var claims = new[] { new Claim("api", apiKey) };
            var identity = new ClaimsIdentity(claims, nameof(ApiKeyAuthenticationHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), this.Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
    
}
