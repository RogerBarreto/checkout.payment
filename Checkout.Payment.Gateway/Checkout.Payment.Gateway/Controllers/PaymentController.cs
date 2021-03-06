﻿using Checkout.Payment.Gateway.Application.Interfaces;
using Checkout.Payment.Gateway.Application.Models;
using Checkout.Payment.Gateway.Seedwork.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Authorize]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IDomainNotificationBus notificationBus, IPaymentService paymentService) : base(notificationBus)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatePaymentResponseModel), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreatePaymentAsync(CreatePaymentRequestModel requestModel)
        {
            var responseModel = await _paymentService.TryCreatePaymentAsync(GetCurrentUserId().Value, requestModel);

            return Result(HttpStatusCode.Accepted, responseModel.Result);
        }

        [HttpGet]
        [Route("{paymentId}")]
        [ProducesResponseType(typeof(GetPaymentResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPaymentAsync([FromRoute] Guid paymentId)
        {
            var responseModel = await _paymentService.TryGetPaymentAsync(GetCurrentUserId().Value, paymentId);

            return Result(HttpStatusCode.OK, responseModel.Result);
        }
    }
}
