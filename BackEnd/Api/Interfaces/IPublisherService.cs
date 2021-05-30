using DAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IPublisherService
    {
        int PublishToQueue(QueueItem item, string queue, string hostname);
    }
}
