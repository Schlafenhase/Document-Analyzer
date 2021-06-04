using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Configuration
{
    public class RabbitMQConfig
    {
        public string HostName { get; set; }
        public string NameQueue { get; set; }
        public string SentimentQueue { get; set; }
        public string SwearQueue { get; set; }
    }
}
