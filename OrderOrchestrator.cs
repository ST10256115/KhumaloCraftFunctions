using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace KhumaloCraftFunctions
{
    public class OrderOrchestrator
    {
        [FunctionName("OrderOrchestrator")]
        public static async Task<List<string>> RunOrchestrator(
          [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            var orderData = context.GetInput<OrderData>();

            // Call activity functions
            outputs.Add(await context.CallActivityAsync<string>("UpdateInventory", orderData));
            outputs.Add(await context.CallActivityAsync<string>("ProcessPayment", orderData));
            outputs.Add(await context.CallActivityAsync<string>("SendOrderConfirmation", orderData));

            return outputs;
        }
    }

    public class OrderData
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string Status { get; set; }
    }
}

