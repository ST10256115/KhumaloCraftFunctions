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
    public class ProcessPayments
    {
        [FunctionName("ProcessPayment")]
        public static async Task<string> Run(
            [ActivityTrigger] OrderData orderData, ILogger log)
        {
            // payment is a simulation
            await Task.Delay(1000); // Simulate some delay
            return $"Payment processed for OrderID {orderData.OrderId}";
        }
    }
}
