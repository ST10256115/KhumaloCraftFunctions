// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace KhumaloCraftFunctions
{
    public class UpdateInventory
    {
        [FunctionName("UpdateInventory")]
        public static async Task<string> Run(
            [ActivityTrigger] OrderData orderData, ILogger log)
        {
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                var commandText = "UPDATE Products SET Quantity = Quantity - @Quantity WHERE ProductId = @ProductId";
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.Parameters.AddWithValue("@Quantity", orderData.Quantity);
                    cmd.Parameters.AddWithValue("@ProductId", orderData.ProductId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return $"Inventory updated for OrderID {orderData.OrderId}";
        }
    }
}
