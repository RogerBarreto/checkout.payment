using Checkout.WebApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.WebApi.Common
{
	public class BaseController : ControllerBase
	{
		public BadRequestObjectResult BadRequest(string error)
		{
			return base.BadRequest(new ErrorModel(error));
		}
	}
}
