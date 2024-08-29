using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retail.Controllers
{
    public class OrderController : Controller
    {
        private readonly QueueServiceClient _queueServiceClient;
        private readonly IConfiguration _configuration;

        public OrderController(QueueServiceClient queueServiceClient, IConfiguration configuration)
        {
            _queueServiceClient = queueServiceClient;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrder(string orderId)
        {
            var queueName = _configuration["AzureStorage:QueueName"];
            var queueClient = _queueServiceClient.GetQueueClient(queueName);

            await queueClient.SendMessageAsync($"Processing order: {orderId}");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var queueName = _configuration["AzureStorage:QueueName"];
            var queueClient = _queueServiceClient.GetQueueClient(queueName);

            // Receive messages from the queue
            var response = await queueClient.ReceiveMessagesAsync(maxMessages: 10); // Adjust maxMessages as needed

            // Extract messages and convert to a list
            var messages = new List<QueueMessage>(response.Value);

            return View(messages);
        }
    }
}
