using System;
using System.Text.Json;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Checkout.Payment.Processor.Application.Interfaces;
using Checkout.Payment.Processor.Application.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Checkout.Payment.Processor.Lambda
{
    public class Function
    {
        public Function()
        {
            Startup.ConfigureServices();
        }

        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            var serviceProvider = Startup.GetServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Function>>();

            foreach (var message in evnt.Records)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var paymentService = scope.ServiceProvider.GetService<IPaymentService>();
                    var paymentMessage = JsonSerializer.Deserialize<PaymentMessageRequestModel>(message.Body);

                    var processResult = await paymentService.TryProcessPaymentAsync(paymentMessage);
                    if (!processResult.Success)
					{
                        logger.LogError($"Unable conclude payment message process [paymentMessagePayload={message.Body}]");

                        //If a payment process transation is unable to complete with rollback halt the application, and let the message go back to visible state in SNS for future processing.
                        throw new ApplicationException("Payment process error - Safely aborting payment processing batch");
					}
                }
            }
        }
    }
}
