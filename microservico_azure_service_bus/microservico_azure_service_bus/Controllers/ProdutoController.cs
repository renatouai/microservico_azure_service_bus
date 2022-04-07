﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using microservico_azure_service_bus.Model;
using System.Text.Json;
using System.Text;

namespace microservico_azure_service_bus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly string connectionString;

        public ProdutoController(IConfiguration config)
        {

            this.config = config;
            this.connectionString = this.config.GetValue<string>("AzureServiceBus");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Produto produto)
        {
            await SendMessageQueue(produto);
            return Ok(produto);
        }

        private async Task SendMessageQueue(Produto produto)
        {
            string queueName = "produto";
            var client = new QueueClient(connectionString, queueName, ReceiveMode.PeekLock);
            string messageBody = JsonSerializer.Serialize(produto);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await client.SendAsync(message);
            await client.CloseAsync();

        }

       
    }
}
