using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Server
{
    /// <summary>
    /// Hub para el tiempo real
    /// </summary>
    public class ChatHub : Hub<IChatClient>
    {
        /// <summary>
        /// Ruta donde este hub escucha
        /// </summary>
        public const String Path = "/chat";

        private IUserRepository repository;

        public ChatHub(IUserRepository repository)
        {
            this.repository = repository;
        }

        public Task Send(string userName, String message)
        {
            return Clients.Client(repository.FindIdByName(userName))
                .Received(repository.FindNameById(Context.ConnectionId), DateTime.Now, message);
        }

        public async Task Register(string userName)
        {
            repository.Register(userName, Context.ConnectionId);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            repository.RemoveById(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }

    /// <summary>
    /// Interfaz para tipar los clientes de un hub
    /// </summary>
    public interface IChatClient
    {
        /// <summary>
        /// Mensaje "Event"
        /// </summary>
        /// <param name="time"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        Task Received(string userName, DateTime time, String message);
    }
}
