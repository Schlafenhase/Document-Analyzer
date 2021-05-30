using DAApi.Configuration;
using DAApi.Hubs;
using DAApi.Hubs.Clients;
using DAApi.Interfaces;
using DAApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {

        private readonly IPublisherService _publisherService;
        private readonly PublisherConfig _publisherConfig;
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;

        public TestController(IPublisherService publisherService, IOptionsMonitor<PublisherConfig> azureOptionsMonitor, IHubContext<ChatHub, IChatClient> chatHub)
        {
            _publisherService = publisherService;
            _publisherConfig = azureOptionsMonitor.CurrentValue;
            _chatHub = chatHub;
        }

        [Route("test")]
        [HttpGet]
        public string Test()
        {
            return "hola";
        }

        [Route("queue1")]
        [HttpGet]
        public string Queue1Test()
        {
           // _publisherService.PublishToQueue("Hola1", "q1", _publisherConfig.HostName);
            return "Ok";
        }

        [Route("queue2")]
        [HttpGet]
        public string Queue2Test()
        {
          //  _publisherService.PublishToQueue("Hola2", "q2", _publisherConfig.HostName);
            return "Ok";
        }

        [Route("messages")]
        [HttpPost]
        public async Task Post(ChatMessage message)
        {
            // run some logic...

            await _chatHub.Clients.All.ReceiveMessage(message);
        }
    }
}
