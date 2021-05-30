using DAApi.Hubs.Clients;
using DAApi.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
    }
}
