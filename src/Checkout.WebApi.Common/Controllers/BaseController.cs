using System.Linq;
using Checkout.WebApi.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.WebApi.Common.Controllers
{
	public class BaseController : ControllerBase
	{
		protected int GetCurrentUserId()
		{
			var userIdClaim = GetCurrentUserClaim("user_id");

			if (int.TryParse(userIdClaim, out int userId))
			{
				return userId;
			}

			throw new AuthUserNotFoundException();
		}

		protected string GetCurrentUserClaim(string claim)
		{
			return User.Claims.FirstOrDefault(c => c.Type == claim)?.Value;
		}
	}
}
