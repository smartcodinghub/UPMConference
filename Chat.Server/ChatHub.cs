using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Server
{
    /// <summary>
    /// Hub para el tiempo real
    /// </summary>
    public class ChatHub : Hub<IChatClient>
    {
    }

    /// <summary>
    /// Interfaz para tipar los clientes de un hub
    /// </summary>
    public interface IChatClient
    {
    }
}
