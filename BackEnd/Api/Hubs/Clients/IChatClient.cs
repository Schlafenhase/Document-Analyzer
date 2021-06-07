using DAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Hubs.Clients
{
    public interface IChatClient
    {
        /// <summary>
        /// Method that sends a message to the connected clients using websockets
        /// </summary>
        /// <param name="message">
        /// Message to be sent
        /// </param>
        /// <returns>
        /// Confirmation of sent message
        /// </returns>
        Task ReceiveMessage(ChatMessage message);
    }
}
