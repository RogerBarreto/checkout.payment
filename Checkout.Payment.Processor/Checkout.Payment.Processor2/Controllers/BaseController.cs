using Checkout.Payment.Processor.Seedwork.Interfaces;
using Checkout.Payment.Processor.Seedwork.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace Checkout.Payment.Processor.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IDomainNotification _notificationBus;
        public BaseController(IDomainNotification notificationBus)
        {
            _notificationBus = notificationBus;
        }

        protected IActionResult Result(HttpStatusCode successStatus, object responseModel)
        {
            if (_notificationBus.HasNotificationType(DomainNotificationType.Error))
            {
                return CreateInternalError();
            }

            if (_notificationBus.HasNotificationType(DomainNotificationType.BusinessViolation))
            {
                return CreateBadRequest();
            }

            switch (successStatus)
            {
                case HttpStatusCode.Created: return Created(responseModel);
                case HttpStatusCode.Accepted: return Accepted(responseModel);
                case HttpStatusCode.NoContent: return NoContent();
                default: return Ok(responseModel);
            }
        }

        private IActionResult CreateInternalError()
        {
            throw new NotImplementedException();
        }

        private IActionResult Created(object responseModel)
        {
            return new ObjectResult(responseModel)
            {
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        private IActionResult CreateBadRequest()
        {
            return BadRequest(new
            {
                Message = string.Join(" | ", _notificationBus.GetNotifications(DomainNotificationType.BusinessViolation).Select(n => n.Message))
            });
        }
        private IActionResult CreateInternalServerError()
        {
            return BadRequest(new
            {
                Message = string.Join(" | ", _notificationBus.GetNotifications(DomainNotificationType.Error).Select(n => n.Message))
            });
        }

        protected int? GetCurrentUserId()
        {
            var userIdClaim = GetCurrentUserClaim("user_id");

            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }
            return null;
        }

        protected string GetCurrentUserClaim(string claim)
        {
            return User.Claims.FirstOrDefault(c => c.Type == claim)?.Value;
        }

    }
}
