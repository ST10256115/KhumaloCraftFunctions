// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace KhumaloCraftFunctions
{
    public class SendOrderConfirmation
    {
        [FunctionName("SendOrderConfirmation")]
        public static async Task<string> Run(
        [ActivityTrigger] OrderData orderData, ILogger log)
        {
            // order is a simulation
            await Task.Delay(500); // Simulate some delay
            return $"Order confirmation sent for OrderID {orderData.OrderId}";
        }
    }
}
