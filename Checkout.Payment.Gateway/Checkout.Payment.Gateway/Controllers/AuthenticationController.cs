using Checkout.Payment.Gateway.Application;
using Checkout.Payment.Gateway.Application.Interfaces;
using Checkout.Payment.Gateway.Seedwork.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(
            ILogger<AuthenticationController> logger, 
            IAuthenticationService authenticationService, 
            IDomainNotificationBus notificationBus) : base(notificationBus)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> GetTokenAsync([FromBody] UserTokenRequestModel requestModel)
        {
            var token = await _authenticationService.LoginGetTokenAsync(requestModel);

            return Result(HttpStatusCode.OK, new UserTokenResponseModel(token));
        }

        [Authorize]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Get()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            _logger.LogInformation("claims: {claims}", claims);

            return new JsonResult(claims);
        }
    }
}
